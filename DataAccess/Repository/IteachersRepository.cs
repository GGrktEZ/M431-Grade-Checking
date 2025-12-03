using DataAccess.Model;

namespace DataAccess.Repository;

public interface IteachersRepository
{
   /// <summary>
   /// Gets the teachers with a specifed Id.
   /// </summary>
   /// <param name="id"> The Id of the teachers to get. </param>
   /// <returns> The teachers model with the specified Id. </returns>
   public teachers? GetteachersById(int id);
   /// <summary>
   /// Gets all teacherss.
   /// </summary>
   /// <returns> A list of teachers models. </returns>
   public IEnumerable<teachers> GetAllteacherss();
   /// <summary>
   /// Adds the given model to the Database.
   /// </summary>
   /// <param name="newteachers"> The teachers to get added. </param>
   public void Addteachers(teachers newteachers);
   /// <summary>
   /// Updates the given teachers with its new values.
   /// </summary>
   /// <param name="updateteachers"> The teachers containing the updated values. </param>
   public void Updateteachers(teachers updateteachers);
   /// <summary>
   /// Deletes the given teachers.
   /// </summary>
   /// <param name="removeteachers"> The teachers to remove. </param>
   public void Deleteteachers(teachers removeteachers);

}