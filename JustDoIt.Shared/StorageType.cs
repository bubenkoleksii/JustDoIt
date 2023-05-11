using System.ComponentModel;

namespace JustDoIt.Shared;

public enum StorageType
{
    [Description("MS SQL Server")] MsSqlServer,

    [Description("XML")] Xml
}