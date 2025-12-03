using Services.DTO;

namespace Services.Service;

public interface IgradesService
{
    /// <summary>
    /// Gets the grades with the given id.
    /// </summary>
    /// <param name="id"> The id of the looked up grades.</param>
    /// <returns>The gradesDto of the Model with the specified id.</returns>
    gradesDto? GetgradesById(int id);
    /// <summary>
    /// Gets all gradess.
    /// </summary>
    /// <returns> A list of all gradesDtos.</returns>
    IEnumerable <gradesDto> GetAllgradess();
    /// <summary>
    /// Adds a grades to the database via the CreategradesDto.
    /// </summary>
    /// <param name="CreategradesDto">A Dto to be added to the database. </param>
    /// <returns> The id of the newly created grades. </returns>
    int Addgrades(CreategradesDto grades);
    /// <summary>
    /// Updates a grades of the database via the UpdategradesDto.
    /// </summary>
    /// <param name="CreategradesDto">The Dto to be changed in the database. </param>
    /// <returns> Wether or not the update was successful. </returns>
    bool Updategrades(int id, UpdategradesDto grades);
    /// <summary>
    /// Deletes a grades with the specified id.
     /// </summary>
    /// <param name="id"> The id of the grades to be deleted. </param>
    /// <returns> Wether or not the deletion was successful. </returns>
    bool Deletegrades(int id);

}