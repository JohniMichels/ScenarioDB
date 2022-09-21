
using Newtonsoft.Json.Linq;

namespace ScenarioDB.Services;

/// <summary>
/// The IStorageService interface defines the methods that are used to store and retrieve records.
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <param name="path">A path to the entity storage.</param>
    /// <param name="id">An identifier of the object to retrieve.</param>
    /// <returns>The object when the <paramref name="path">/<paramref name="id"> exists, <c>null</c> otherwise.</returns>
    Task<JToken?> GetAsync(string path, string id);

    /// <summary>
    /// Creates an object or updates when it already exists.
    /// </summary>
    /// <param name="path">A path to the entity storage.</param>
    /// <param name="id">An identifier for the object.</param>
    /// <param name="record">The object value to send.</param>
    /// <param name="timestamp">The instant in time the operation was requested.</param>
    /// <returns><c>true</c> when the object was created, <c>false</c> otherwise.</returns>
    Task<bool> UpsertAsync(string path, string id, JToken record, DateTime timestamp);

    /// <summary>
    /// Deletes the object.
    /// </summary>
    /// <param name="path">A path to the entity storage.</param>
    /// <param name="id">An identifier for the object.</param>
    /// <param name="timestamp">The instant in time the operation was requested.</param>
    /// <returns><c>true</c> when the object was deleted, <c>false</c> otherwise.</returns>
    Task<bool> DeleteAsync(string path, string id, DateTime timestamp);
}