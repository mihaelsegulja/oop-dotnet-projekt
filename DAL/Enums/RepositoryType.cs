namespace DAL.Enums;

/// <summary>
/// The type of repository to use for data access.
/// </summary>
public enum RepositoryType
{
    /// <summary>
    /// Use online API
    /// </summary>
    WebApi,

    /// <summary>
    /// Use local JSON files
    /// </summary>
    FileSystem,

    /// <summary>
    /// Try WebApi first, fallback to FileSystem if unavailable
    /// </summary>
    AutoDetect
}
