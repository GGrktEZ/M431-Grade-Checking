using Services.DTO;

namespace Services.Service;

public interface IclassesService
{
    /// <summary>
    /// Gets the classes with the given id.
    /// </summary>
    /// <param name="id"> The id of the looked up classes.</param>
    /// <returns>The classesDto of the Model with the specified id.</returns>
    classesDto? GetclassesById(int id);
    /// <summary>
    /// Gets all classess.
    /// </summary>
    /// <returns> A list of all classesDtos.</returns>
    IEnumerable <classesDto> GetAllclassess();
    /// <summary>
    /// Adds a classes to the database via the CreateclassesDto.
    /// </summary>
    /// <param name="CreateclassesDto">A Dto to be added to the database. </param>
    /// <returns> The id of the newly created classes. </returns>
    int Addclasses(CreateclassesDto classes);
    /// <summary>
    /// Updates a classes of the database via the UpdateclassesDto.
    /// </summary>
    /// <param name="CreateclassesDto">The Dto to be changed in the database. </param>
    /// <returns> Wether or not the update was successful. </returns>
    bool Updateclasses(int id, UpdateclassesDto classes);
    /// <summary>
    /// Deletes a classes with the specified id.
     /// </summary>
    /// <param name="id"> The id of the classes to be deleted. </param>
    /// <returns> Wether or not the deletion was successful. </returns>
    bool Deleteclasses(int id);

}