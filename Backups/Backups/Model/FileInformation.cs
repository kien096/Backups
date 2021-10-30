namespace Backups.Model
{
    /// <summary>
    /// Информация о файле
    /// </summary>
    public class FileInformation
    {
        /// <summary>
        /// Размер
        /// </summary>
        public decimal Size { get; }

        /// <summary>
        /// Путь
        /// </summary>
        public string FullName { get; }

        public FileInformation(decimal size, string fullName)
        {
            Size = size;
            FullName = fullName;
        }

        public virtual FileInformation Copy()
        {
            return new FileInformation(Size, FullName);
        }
    }
}