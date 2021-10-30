using Backups.Enums;
using Backups.Model;

namespace Backups.Interfaces
{
    /// <summary>
    /// Интерфейс для объекта реализующего функционал сохранения бекапа
    /// </summary>
    public interface ISaver
    {
        public RestorePoint Run();

        public void Single();

        public void Split();

        public void CreateDirectory();

        public void CreatePoint();
    }
}