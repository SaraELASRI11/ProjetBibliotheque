using GestionBibliotheque.Models;

namespace GestionBibliotheque.Services
{
    public interface ILivreService
    {

        public ICollection<Livre> GetLivres();
        public ICollection<Livre> GetLivresByTitle(string titre);
        public Livre GetLivresById(int id);
        public void Add(Livre livres);
        public void Remove(int id);
        public void Update(Livre livres);

    }
}
