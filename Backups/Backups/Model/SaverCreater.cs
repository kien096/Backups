using Backups.Enums;
using Backups.Interfaces;

namespace Backups.Model
{
    /// <summary>
    /// Создатель для объектов сохранения
    /// </summary>
    public static class SaverCreater
    {
        /// <summary>
        /// Создать объект по типу
        /// </summary>
        /// <param name="job">Джоб</param>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        public static ISaver Create(BackupJob job, SaverType type, IFileSystem fileSystem)
        {
            switch (type)
            {
                case SaverType.FileSystem:
                    return new FileSystemSaver(job);
                case SaverType.VirtualSystem:
                    return new VirtualFileSaver(job, fileSystem as VitualFileSystem);
                case SaverType.GoogleDrive:
                    return null;
            }

            return null;
        }
    }
}