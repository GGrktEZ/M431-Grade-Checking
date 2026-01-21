using DataAccess.Model;

namespace DataAccess.Repository;

public interface IclassesRepository
{
   /// <summary>
   /// Gets the classes with a specifed Id.
   /// </summary>
   /// <param name="id"> The Id of the classes to get. </param>
   /// <returns> The classes model with the specified Id. </returns>
   public classes? GetclassesById(int id);
   /// <summary>
   /// Gets all classess.
   /// </summary>
   /// <returns> A list of classes models. </returns>
   public IEnumerable<classes> GetAllclassess();
   /// <summary>
   /// Gets classes by a list of class IDs.
   /// </summary>
   /// <param name="classIds">List of class IDs to retrieve.</param>
   /// <returns>A list of classes models matching the IDs.</returns>
   public IEnumerable<classes> GetClassesByIds(IEnumerable<int> classIds);
   /// <summary>
   /// Adds the given model to the Database.
   /// </summary>
   /// <param name="newclasses"> The classes to get added. </param>
   public void Addclasses(classes newclasses);
   /// <summary>
   /// Updates the given classes with its new values.
   /// </summary>
   /// <param name="updateclasses"> The classes containing the updated values. </param>
   public void Updateclasses(classes updateclasses);
   /// <summary>
   /// Deletes the given classes.
   /// </summary>
   /// <param name="removeclasses"> The classes to remove. </param>
   public void Deleteclasses(classes removeclasses);

}