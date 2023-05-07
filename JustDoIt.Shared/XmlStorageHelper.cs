using System.Xml;

namespace JustDoIt.Shared
{
    public static class XmlStorageHelper
    {
        public static void CreateXmlStorageIfNotExists(string storagePath, string jobStoragePath, string categoryStoragePath)
        {
            var rootFolderPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            var storageFullPath = Path.Combine(rootFolderPath, storagePath);
            if (!Directory.Exists(storageFullPath))
            {
                Directory.CreateDirectory(storageFullPath);
            }

            var jobStorageFullPath = Path.Combine(storageFullPath, jobStoragePath);

            if (!File.Exists(jobStorageFullPath))
            {
                using var writer = XmlWriter.Create(jobStorageFullPath);
                writer.WriteStartDocument();
                writer.WriteStartElement("Jobs");
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            var categoryStorageFullPath = Path.Combine(storageFullPath, categoryStoragePath);
            if (!File.Exists(categoryStorageFullPath))
            {
                using var writer = XmlWriter.Create(categoryStorageFullPath);
                writer.WriteStartDocument();
                writer.WriteStartElement("Categories");
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
