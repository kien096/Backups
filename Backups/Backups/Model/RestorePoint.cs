using System;
using System.Collections.Generic;

namespace Backups.Model
{
    /// <summary>
    /// Точка восстановления
    /// </summary>
    public class RestorePoint
    {
        /// <summary>
        /// Номер точки восстановления
        /// </summary>
        public ulong Num { get; }

        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime Time { get; } = DateTime.Now;

        /// <summary>
        /// Объекты которые бекапились
        /// </summary>
        public List<FileInformation> Files { get; }

        public RestorePoint(ulong num, List<FileInformation> files)
        {
            Num = num;
            Files = files;
        }
    }
}