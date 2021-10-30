using System;
using System.IO;
using System.IO.Compression;
using Backups.Enums;
using Backups.Interfaces;

namespace Backups.Model
{
    /// <summary>
    /// Сохранение в файловой системе
    /// </summary>
    public class FileSystemSaver : ISaver
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
        private DirectoryInfo _lastPointDirectory;

        public FileSystemSaver(BackupJob job)
        {
            Job = job;
        }

        /// <summary>
        /// Запуск точки восстановления
        /// </summary>
        /// <returns>Точка восстановления</returns>
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

        /// <summary>
        /// Создание архива с файлами
        /// </summary>
        public void Single()
        {
            using (var zipToOpen = new FileStream(Path.Combine(_lastPointDirectory.FullName, $@"Restore Point_{PointCount}.zip"), FileMode.CreateNew))
            {
                using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    foreach (var f in Job.Files)
                    {
                        var file = new FileInfo(f.FullName);

                        var copy = file.CopyTo(Path.Combine(_lastPointDirectory.FullName, file.Name));

                        archive.CreateEntryFromFile(copy.FullName, copy.Name);

                        copy.Delete();
                    }
                }
            }
        }

        /// <summary>
        /// Каждый файл в своём архиве
        /// </summary>
        public void Split()
        {
            foreach (var f in Job.Files)
            {
                var file = new FileInfo(f.FullName);

                var copy = file.CopyTo(Path.Combine(_lastPointDirectory.FullName, file.Name));

                using (var zipToOpen = new FileStream(Path.Combine(_lastPointDirectory.FullName, $@"{file.Name}_{PointCount}.zip"), FileMode.CreateNew))
                {
                    using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(copy.FullName, copy.Name);
                    }
                }

                copy.Delete();
            }
        }

        /// <summary>
        /// Создание директории для джоба
        /// </summary>
        public void CreateDirectory()
        {
            if (Directory.Exists(Job.Name))
            {
                Directory.Delete(Job.Name, true);
            }

            Directory.CreateDirectory(Job.Name);
        }

        /// <summary>
        /// Создание директории для точки востановления
        /// </summary>
        public void CreatePoint()
        {
            if (!Directory.Exists(Job.Name))
                throw new Exception("Job doesnt exist");

            PointCount++;

            if (Directory.Exists($@"Restore Point {PointCount}"))
            {
                Directory.Delete($@"Restore Point {PointCount}", true); //Если такая папка есть, то удалим её (её быть не должно ещё)
            }


            _lastPointDirectory = Directory.CreateDirectory(Path.Combine(Job.Name, $@"Restore Point {PointCount}")); //Создали папку для поинта
        }
    }
}