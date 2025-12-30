using Shared.DTOs;

namespace Services.Service;

public interface Iclass_enrollmentsService
{
    /// <summary>
    /// Gets the class_enrollments with the given id.
    /// </summary>
    /// <param name="id"> The id of the looked up class_enrollments.</param>
    /// <returns>The class_enrollmentsDto of the Model with the specified id.</returns>
    class_enrollmentsDto? Getclass_enrollmentsById(int id);
    /// <summary>
    /// Gets all class_enrollmentss.
    /// </summary>
    /// <returns> A list of all class_enrollmentsDtos.</returns>
    IEnumerable <class_enrollmentsDto> GetAllclass_enrollmentss();
    /// <summary>
    /// Adds a class_enrollments to the database via the Createclass_enrollmentsDto.
    /// </summary>
    /// <param name="Createclass_enrollmentsDto">A Dto to be added to the database. </param>
    /// <returns> The id of the newly created class_enrollments. </returns>
    int Addclass_enrollments(Createclass_enrollmentsDto class_enrollments);
    /// <summary>
    /// Updates a class_enrollments of the database via the Updateclass_enrollmentsDto.
    /// </summary>
    /// <param name="Createclass_enrollmentsDto">The Dto to be changed in the database. </param>
    /// <returns> Wether or not the update was successful. </returns>
    bool Updateclass_enrollments(int id, Updateclass_enrollmentsDto class_enrollments);
    /// <summary>
    /// Deletes a class_enrollments with the specified id.
     /// </summary>
    /// <param name="id"> The id of the class_enrollments to be deleted. </param>
    /// <returns> Wether or not the deletion was successful. </returns>
    bool Deleteclass_enrollments(int id);

}