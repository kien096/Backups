using System.Collections.Generic;
using Backups.Model;

namespace Backups.Interfaces
{
    public interface IDirectory
    {
        public string Name { get; set; }

        public IDirectory ParentDictionary { get; set; }

        public List<FileInformation> Files { get; }

        public List<IDirectory> Directories { get; }
    }
}