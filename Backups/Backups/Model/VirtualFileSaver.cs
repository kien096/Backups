using Backups.Enums;
using Backups.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace Backups.Model
{
    /// <summary>
    /// Объект сохранения для виртуальной файловой системы
    /// </summary>
    public class VirtualFileSaver : ISaver
    {
        /// <summary>
        /// Количество точек восстановления
        /// </summary>
        public ulong PointCount { get; private set; } = 0;


        /// <summary>
        /// Джоба для которой создаётся бекап
        /// </summary>
        public BackupJob Job { get; }

        /// <summary>
        /// Папка с точкой восстановления
        /// </summary>
        private VirtualDirectoty _lastPointDirectory;

        /// <summary>
        /// Папка джоба
        /// </summary>
        private IDirectory _jobDirectory;

        /// <summary>
        /// Виртуальная файловая система
        /// </summary>
        public VitualFileSystem FileSystem { get; }

        public VirtualFileSaver(BackupJob job, VitualFileSystem fileSystem)
        {
            Job = job;
            FileSystem = fileSystem;
        }

        public RestorePoint Run()
        {
            CreatePoint();

            switch (Job.Type)
            {
                case StorageType.Single:
                    Single();
                    break;
                case StorageType.Split:
                    Split();
                    break;
            }

            var point = new RestorePoint(PointCount, Job.Files);

            return point;
        }

        public void Single()
        {
            var archive = new ArchiveFileInformation(Job.Files.Sum(x => x.Size),
                                                     Path.Combine(_lastPointDirectory.Name, $@"Restore Point {PointCount}.zip"));

            _lastPointDirectory.Files.Add(archive);

            foreach (FileInformation file in Job.Files)
            {
                archive.Files.Add(file.Copy());
            }
        }

        public void Split()
        {
            foreach (var file in Job.Files)
            {
                var archive = new ArchiveFileInformation(file.Size,
                                                         Path.Combine(_lastPointDirectory.Name, $@"{file.FullName}.zip"));

                archive.Files.Add(file.Copy());

                _lastPointDirectory.Files.Add(archive);
            }
        }

        public void CreateDirectory()
        {
            FileSystem.Directories.RemoveAll(d => d.Name == Job.Name);

            _jobDirectory = new VirtualDirectoty(Job.Name);
            FileSystem.Directories.Add(_jobDirectory);
        }

        public void CreatePoint()
        {
            if (_jobDirectory == null)
                throw new Exception("Job doesnt exist");

            PointCount++;

            _jobDirectory.Directories.RemoveAll(d => d.Name == $@"Restore Point {PointCount}");

            _lastPointDirectory = new VirtualDirectoty(Path.Combine(Job.Name, $@"Restore Point {PointCount}"), _jobDirectory as VirtualDirectoty);

            _jobDirectory.Directories.Add(_lastPointDirectory);
        }
    }
}