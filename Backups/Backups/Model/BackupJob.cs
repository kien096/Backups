using Backups.Enums;
using Backups.Interfaces;
using System.Collections.Generic;

namespace Backups.Model
{
    public class BackupJob
    {
        /// <summary>
        /// Тип хранения файла
        /// </summary>
        public StorageType Type { get; }

        /// <summary>
        /// Имя джоба
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Файлы для бекапа
        /// </summary>
        public List<FileInformation> Files { get; } = new List<FileInformation>();

        /// <summary>
        /// Точки восстановления
        /// </summary>
        public List<RestorePoint> RestorePoints { get; } = new List<RestorePoint>();

        /// <summary>
        /// Объект для реализации сохранения разными способами
        /// </summary>
        public ISaver Saver { get; }

        public BackupJob(string name, StorageType type, SaverType saverType, IFileSystem fileSystem = null)
        {
            Name = name;
            Type = type;
            Saver = SaverCreater.Create(this, saverType, fileSystem);

            Init();
        }

        /// <summary>
        /// Инициализация джобы, создание папки
        /// </summary>
        private void Init()
        {
            Saver.CreateDirectory();
        }

        /// <summary>
        /// Добавить файл
        /// </summary>
        /// <param name="fullFilePath"></param>
        public void AddFile(FileInformation file)
        {
            Files.Add(file);
        }

        /// <summary>
        /// Удаление файла по его номеру
        /// </summary>
        /// <param name="index">Номер</param>
        public void DeleteFile(int index)
        {
            Files.RemoveAt(index);
        }

        /// <summary>
        /// Удаление файла по имени
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        public void DeleteFile(string fileName)
        {
            var file = Files.Find(f => f.FullName == fileName);

            if (file != null)
                Files.Remove(file);
        }

        /// <summary>
        /// Удаление файла по объекту
        /// </summary>
        /// <param name="file">файл</param>
        public void DeleteFile(FileInformation file)
        {
            Files.Remove(file);
        }

        /// <summary>
        /// Запуск джобы
        /// </summary>
        public void Run()
        {
            RestorePoints.Add(Saver.Run());
        }
    }
}