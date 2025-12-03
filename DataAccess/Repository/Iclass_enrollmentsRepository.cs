using DataAccess.Model;

namespace DataAccess.Repository;

public interface Iclass_enrollmentsRepository
{
   /// <summary>
   /// Gets the class_enrollments with a specifed Id.
   /// </summary>
   /// <param name="id"> The Id of the class_enrollments to get. </param>
   /// <returns> The class_enrollments model with the specified Id. </returns>
   public class_enrollments? Getclass_enrollmentsById(int id);
   /// <summary>
   /// Gets all class_enrollmentss.
   /// </summary>
   /// <returns> A list of class_enrollments models. </returns>
   public IEnumerable<class_enrollments> GetAllclass_enrollmentss();
   /// <summary>
   /// Adds the given model to the Database.
   /// </summary>
   /// <param name="newclass_enrollments"> The class_enrollments to get added. </param>
   public void Addclass_enrollments(class_enrollments newclass_enrollments);
   /// <summary>
   /// Updates the given class_enrollments with its new values.
   /// </summary>
   /// <param name="updateclass_enrollments"> The class_enrollments containing the updated values. </param>
   public void Updateclass_enrollments(class_enrollments updateclass_enrollments);
   /// <summary>
   /// Deletes the given class_enrollments.
   /// </summary>
   /// <param name="removeclass_enrollments"> The class_enrollments to remove. </param>
   public void Deleteclass_enrollments(class_enrollments removeclass_enrollments);

}