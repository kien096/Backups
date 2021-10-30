using Backups.Interfaces;
using System.Collections.Generic;

namespace Backups.Model
{
    /// <summary>
    /// Виртуальная папка
    /// </summary>
    public class VirtualDirectoty : IDirectory
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Родитеская папка
        /// </summary>
        public IDirectory ParentDictionary { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public List<FileInformation> Files { get; } = new List<FileInformation>();

        /// <summary>
        /// Папки
        /// </summary>
        public List<IDirectory> Directories { get; } = new List<IDirectory>();

        public VirtualDirectoty(string name, VirtualDirectoty parentDirectoty = null)
        {
            Name = name;
            ParentDictionary = parentDirectoty;
        }
    }
}