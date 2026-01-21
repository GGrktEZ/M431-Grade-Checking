using DataAccess.Model;

namespace DataAccess.Repository;

public interface ImodulesRepository
{
    /// <summary>
    /// Gets the module with a specified Id.
    /// </summary>
    /// <param name="id">The Id of the module to get.</param>
    /// <returns>The module model with the specified Id.</returns>
    modules? GetmoduleById(int id);

    /// <summary>
    /// Gets all modules.
    /// </summary>
    /// <returns>A list of modules models.</returns>
    IEnumerable<modules> GetAllmodules();

    /// <summary>
    /// Gets modules by a list of module IDs.
    /// </summary>
    /// <param name="moduleIds">List of module IDs to retrieve.</param>
    /// <returns>A list of modules models matching the IDs.</returns>
    IEnumerable<modules> GetModulesByIds(IEnumerable<int> moduleIds);
}
