using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Model
{
    public class VitualFileSystem : IFileSystem
    {
        public List<IDirectory> Directories { get; } = new List<IDirectory>();

        public List<FileInformation> Files { get; } = new List<FileInformation>();
    }
}