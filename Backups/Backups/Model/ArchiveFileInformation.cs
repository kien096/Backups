using System.Collections.Generic;
using System.Linq;

namespace Backups.Model
{
    public class ArchiveFileInformation : FileInformation
    {
        /// <summary>
        /// Файлы в архиве
        /// </summary>
        public List<FileInformation> Files { get; } = new List<FileInformation>();

        public ArchiveFileInformation(decimal size, string fullName) : base(size, fullName) { }

        public override FileInformation Copy()
        {
            var n = new ArchiveFileInformation(Files.Sum(f => f.Size), FullName);

            foreach (var file in Files)
            {
                n.Files.Add(file.Copy());
            }

            return n;
        }
    }
}