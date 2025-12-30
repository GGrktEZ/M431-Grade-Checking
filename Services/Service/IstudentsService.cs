using Shared.DTOs;

namespace Services.Service;

public interface IstudentsService
{
    /// <summary>
    /// Gets the students with the given id.
    /// </summary>
    /// <param name="id"> The id of the looked up students.</param>
    /// <returns>The studentsDto of the Model with the specified id.</returns>
    studentsDto? GetstudentsById(int id);
    /// <summary>
    /// Gets all studentss.
    /// </summary>
    /// <returns> A list of all studentsDtos.</returns>
    IEnumerable <studentsDto> GetAllstudentss();
    /// <summary>
    /// Adds a students to the database via the CreatestudentsDto.
    /// </summary>
    /// <param name="CreatestudentsDto">A Dto to be added to the database. </param>
    /// <returns> The id of the newly created students. </returns>
    int Addstudents(CreatestudentsDto students);
    /// <summary>
    /// Updates a students of the database via the UpdatestudentsDto.
    /// </summary>
    /// <param name="CreatestudentsDto">The Dto to be changed in the database. </param>
    /// <returns> Wether or not the update was successful. </returns>
    bool Updatestudents(int id, UpdatestudentsDto students);
    /// <summary>
    /// Deletes a students with the specified id.
     /// </summary>
    /// <param name="id"> The id of the students to be deleted. </param>
    /// <returns> Wether or not the deletion was successful. </returns>
    bool Deletestudents(int id);

}