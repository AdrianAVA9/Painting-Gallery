using PaintingGallery.Repository;
using PaintingGallery.Repository.Entities;
using System.Linq;

namespace PaintingGallery.Manager.IManagers
{
    public interface IPatronManager
    {
        Patron GetPatron(int id);
        RepositoryActionResult<Patron> RegisterPatron(Patron patron);
        RepositoryActionResult<Patron> EditPatron(Patron patron);
        RepositoryActionResult<Patron> EditPatronImage(Patron patron);
        RepositoryActionResult<Patron> DeletePatron(int id);
        IQueryable<Patron> GetPatrons();
    }
}