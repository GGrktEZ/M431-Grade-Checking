using DataAccess.Model;

namespace DataAccess.Repository;

public interface IgradesRepository
{
   /// <summary>
   /// Gets the grades with a specifed Id.
   /// </summary>
   /// <param name="id"> The Id of the grades to get. </param>
   /// <returns> The grades model with the specified Id. </returns>
   public grades? GetgradesById(int id);
   /// <summary>
   /// Gets all gradess.
   /// </summary>
   /// <returns> A list of grades models. </returns>
   public IEnumerable<grades> GetAllgradess();
   /// <summary>
   /// Adds the given model to the Database.
   /// </summary>
   /// <param name="newgrades"> The grades to get added. </param>
   public void Addgrades(grades newgrades);
   /// <summary>
   /// Updates the given grades with its new values.
   /// </summary>
   /// <param name="updategrades"> The grades containing the updated values. </param>
   public void Updategrades(grades updategrades);
   /// <summary>
   /// Deletes the given grades.
   /// </summary>
   /// <param name="removegrades"> The grades to remove. </param>
   public void Deletegrades(grades removegrades);

}