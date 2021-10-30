using System.Collections.Generic;
using Backups.Model;

namespace Backups.Interfaces
{
    public interface IFileSystem
    {
        public List<IDirectory> Directories { get; }

        public List<FileInformation> Files { get; }
    }
}