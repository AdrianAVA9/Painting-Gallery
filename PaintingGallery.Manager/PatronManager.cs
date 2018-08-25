using PaintingGallery.Manager.IManagers;
using PaintingGallery.Repository;
using PaintingGallery.Repository.Entities;
using PaintingGallery.Repository.IRepositories;
using PaintingGallery.Repository.Repositories;
using System.Linq;

namespace PaintingGallery.Manager
{
    public class PatronManager : IPatronManager
    {
        public IPatronRepository _repository { get; set; }

        public PatronManager(string conn)
        {
            _repository = new PatronRepository(conn);
        }

        public Patron GetPatron(int id)
        {
            return _repository.GetPatron(id);
        }

        public RepositoryActionResult<Patron> RegisterPatron(Patron patron)
        {
            patron.Picture = "default-user-image.png";

            return _repository.Register(patron);
        }

        public RepositoryActionResult<Patron> EditPatron(Patron patron)
        {
            return _repository.EditPatron(patron);
        }

        public IQueryable<Patron> GetPatrons()
        {
            return _repository.GetPatrons();
        }

        public RepositoryActionResult<Patron> EditPatronImage(Patron patron)
        {
            return _repository.EditPatronImage(patron);
        }

        public RepositoryActionResult<Patron> DeletePatron(int id)
        {
            return _repository.DeletePatron(id);
        }
    }
}
