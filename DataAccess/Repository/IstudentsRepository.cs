using DataAccess.Model;

namespace DataAccess.Repository;

public interface IstudentsRepository
{
   /// <summary>
   /// Gets the students with a specifed Id.
   /// </summary>
   /// <param name="id"> The Id of the students to get. </param>
   /// <returns> The students model with the specified Id. </returns>
   public students? GetstudentsById(int id);
   /// <summary>
   /// Gets all studentss.
   /// </summary>
   /// <returns> A list of students models. </returns>
   public IEnumerable<students> GetAllstudentss();
   /// <summary>
   /// Adds the given model to the Database.
   /// </summary>
   /// <param name="newstudents"> The students to get added. </param>
   public void Addstudents(students newstudents);
   /// <summary>
   /// Updates the given students with its new values.
   /// </summary>
   /// <param name="updatestudents"> The students containing the updated values. </param>
   public void Updatestudents(students updatestudents);
   /// <summary>
   /// Deletes the given students.
   /// </summary>
   /// <param name="removestudents"> The students to remove. </param>
   public void Deletestudents(students removestudents);

}