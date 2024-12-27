using GestionBibliotheque.Models;

namespace GestionBibliotheque.Services
{
    public class LivreService : ILivreService
    {
        public ICollection<Livre> Livres;
        public LivreService()
        {
            this.Livres = new List<Livre>();
        }

        public ICollection<Livre> GetLivres() => this.Livres;

        public Livre GetLivresById(int id) => this.Livres.FirstOrDefault(c => c.Id == id);

        public ICollection<Livre> GetLivresByTitle(string titre) => this.Livres.Where(c => c.Titre == titre).ToList();

        public void Add(Livre livres)
        {
            this.Livres.Add(livres);
        }
        public void Remove(int id)
        {
            Livre livre = GetLivresById(id);
            if (livre != null)
            {
                this.Livres.Remove(livre);
            }
        }

        public void Update(Livre livre)
        {
            Livre existeLivre = GetLivresById(livre.Id);
            if (existeLivre != null)
            {
                existeLivre.Titre = livre.Titre;
                existeLivre.Auteur = livre.Auteur;
                existeLivre.Annee = livre.Annee;
                existeLivre.ISBN = livre.ISBN;
                existeLivre.NbrExemplaire = livre.NbrExemplaire;
                existeLivre.prix = livre.prix;
            }
        }
    }
}
