using System.IO;
using Backups.Enums;
using Backups.Model;

namespace Backups
{
    class Program
    {
        static void Main(string[] args)
        {
            var file1 = new FileInfo("1.txt");
            var file2 = new FileInfo("2.txt");
            FileStream fs1 = null;
            FileStream fs2 = null;

            if (!file1.Exists)
                fs1 = file1.Create();

            if (!file2.Exists)
                fs2 = file2.Create();

            var job = new BackupJob("Test2", StorageType.Single, SaverType.FileSystem);

            job.Files.Add(new FileInformation(12, file1.FullName));
            job.Files.Add(new FileInformation(32, file2.FullName));

            fs1?.Close();
            fs2?.Close();

            job.Run();
        }
    }
}
