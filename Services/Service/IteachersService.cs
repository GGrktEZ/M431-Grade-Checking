using Shared.DTOs;

namespace Services.Service;

public interface IteachersService
{
    /// <summary>
    /// Gets the teachers with the given id.
    /// </summary>
    /// <param name="id"> The id of the looked up teachers.</param>
    /// <returns>The teachersDto of the Model with the specified id.</returns>
    teachersDto? GetteachersById(int id);
    /// <summary>
    /// Gets all teacherss.
    /// </summary>
    /// <returns> A list of all teachersDtos.</returns>
    IEnumerable <teachersDto> GetAllteacherss();
    /// <summary>
    /// Adds a teachers to the database via the CreateteachersDto.
    /// </summary>
    /// <param name="CreateteachersDto">A Dto to be added to the database. </param>
    /// <returns> The id of the newly created teachers. </returns>
    int Addteachers(CreateteachersDto teachers);
    /// <summary>
    /// Updates a teachers of the database via the UpdateteachersDto.
    /// </summary>
    /// <param name="CreateteachersDto">The Dto to be changed in the database. </param>
    /// <returns> Wether or not the update was successful. </returns>
    bool Updateteachers(int id, UpdateteachersDto teachers);
    /// <summary>
    /// Deletes a teachers with the specified id.
     /// </summary>
    /// <param name="id"> The id of the teachers to be deleted. </param>
    /// <returns> Wether or not the deletion was successful. </returns>
    bool Deleteteachers(int id);

}