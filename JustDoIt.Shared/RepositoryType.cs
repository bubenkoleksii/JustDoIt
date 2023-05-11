using System.ComponentModel;

namespace JustDoIt.Shared;

public enum RepositoryType
{
    [Description("MS SQL Server")] MsSqlServer,

    [Description("XML")] Xml
}