namespace Backups.Enums
{
    /// <summary>
    /// Алгоритм хранения файла
    /// </summary>
    public enum StorageType
    {
        Single, // каждый файл в своём архиве
        Split // все файлы в одном архиве
    }
}