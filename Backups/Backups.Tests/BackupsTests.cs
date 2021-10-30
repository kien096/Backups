using Backups.Enums;
using Backups.Model;
using NUnit.Framework;
using System;

namespace Backups.Tests
{
    public class Tests
    {
        private BackupJob _job;

        private VitualFileSystem _fileSystem;

        [SetUp]
        public void Setup()
        {
            _fileSystem = new VitualFileSystem();

            var file1 = new FileInformation(124, "1.ssg");
            var file2 = new FileInformation(115135, "2.png");
            _fileSystem.Files.Add(file1);
            _fileSystem.Files.Add(file2);

            _job = new BackupJob("Test1", StorageType.Split, SaverType.VirtualSystem, _fileSystem); // создаём джоб, передаём: имя, тип архивации, тип сохранения - было условие,
            // что функционал сохранения был отделён от джоба. По типу он создаст нужный объект.
            _job.AddFile(file1);
            _job.AddFile(file2);
        }

        [Test]
        public void Test1()
        {
            if (_job.Files.Count != 2)
                throw new Exception("Wrong file count");

            _job.Run(); 

            if (_job.RestorePoints.Count != 1)
                throw new Exception("Restore point doesnt exist");

            if (_job.RestorePoints[0].Files.Count != 2)
                throw new Exception("Wrong file count");

            _job.DeleteFile(1); 

            if (_job.Files.Count != 1)
                throw new Exception("Wrong file count");

            _job.Run(); 

            if (_job.RestorePoints.Count != 2)
                throw new Exception("Restore point doesnt exist");

            if (_job.RestorePoints[1].Files.Count != 1)
                throw new Exception("Wrong file count");

            Assert.That(_fileSystem.Directories[0].Directories.Count, Is.EqualTo(2));
        }
    }
}