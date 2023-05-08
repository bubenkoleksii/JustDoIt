using System.ComponentModel;

namespace JustDoIt.Shared;

public enum RepositoryType
{
    [Description("MS SQL Sever")] MsSqlServer,

    [Description("XML")] Xml
}