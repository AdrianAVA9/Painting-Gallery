using PaintingGallery.Repository.Entities;
using PaintingGallery.Repository.IRepositories;
using System.Linq;

namespace PaintingGallery.Repository.Repositories
{
    public class PatronRepository : IPatronRepository
    {
        public PaintingGalleryContext _context { get; set; }

        public PatronRepository(string conn)
        {
            _context = new PaintingGalleryContext(conn);
        }


        public RepositoryActionResult<Patron> Register(Patron patron)
        {
            try
            {
                _context.Patrons.Add(patron);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (System.Exception ex)
            {
                return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Patron> EditPatronImage(Patron patron)
        {
            try
            {
                var existingPatron = _context.Patrons.FirstOrDefault(p => p.Id == patron.Id);

                if (existingPatron == null)
                    return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.NotFound, null);

                existingPatron.Picture = patron.Picture;

                _context.SaveChanges();

                return new RepositoryActionResult<Patron>(existingPatron, RepositoryActionStatus.Updated);
            }
            catch (System.Exception ex)
            {
                return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Patron> EditPatron(Patron patron)
        {
            try
            {
                var existingPatron = _context.Patrons.FirstOrDefault(p => p.Id == patron.Id);

                if (existingPatron == null)
                    return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.NotFound, null);

                patron.Picture = existingPatron.Picture;

                _context.Entry(existingPatron).State = System.Data.Entity.EntityState.Detached;

                _context.Patrons.Attach(patron);

                _context.Entry(patron).State = System.Data.Entity.EntityState.Modified;

                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (System.Exception ex)
            {
                return new RepositoryActionResult<Patron>(patron, RepositoryActionStatus.Error, ex);
            }
        }

        public IQueryable<Patron> GetPatrons()
        {
            return _context.Patrons;
        }

        public Patron GetPatron(int id)
        {
            return _context.Patrons.FirstOrDefault(p => p.Id == id);
        }

        public RepositoryActionResult<Patron> DeletePatron(int id)
        {
            try
            {
                var existingPatron = _context.Patrons.FirstOrDefault(p => p.Id == id);

                if (existingPatron == null)
                    return new RepositoryActionResult<Patron>(null, RepositoryActionStatus.NotFound, null);
                _context.Patrons.Remove(existingPatron);

                var result = _context.SaveChanges();

                if (result > 0)
                    return new RepositoryActionResult<Patron>(null, RepositoryActionStatus.Deleted);

                return new RepositoryActionResult<Patron>(null, RepositoryActionStatus.NothingModified, null);
            }
            catch (System.Exception ex)
            {
                return new RepositoryActionResult<Patron>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
