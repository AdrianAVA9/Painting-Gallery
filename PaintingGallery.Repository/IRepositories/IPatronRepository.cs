using PaintingGallery.Repository.Entities;
using System.Linq;

namespace PaintingGallery.Repository.IRepositories
{
    public interface IPatronRepository
    {
        RepositoryActionResult<Patron> Register(Patron patron);
        RepositoryActionResult<Patron> EditPatron(Patron patron);
        RepositoryActionResult<Patron> EditPatronImage(Patron patron);
        IQueryable<Patron> GetPatrons();
        Patron GetPatron(int id);
        RepositoryActionResult<Patron> DeletePatron(int id);
    }
}
