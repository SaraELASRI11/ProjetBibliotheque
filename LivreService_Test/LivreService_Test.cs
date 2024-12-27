using GestionBibliotheque.Models;
using GestionBibliotheque.Services;
using Xunit;

namespace Bibliotheque_Test
{
    public class LivreService_Test
    {
        private readonly LivreService _service;

        public LivreService_Test()
        {
            _service = new LivreService();
        }

        private readonly Livre livre1 = new Livre
        {
            Id = 1,
            Titre = "The Great Gatsby",
            ISBN = "9780743273565",
            prix = 25.99,
            Auteur = "F. Scott Fitzgerald",
            Annee = 2010,
            NbrExemplaire = 40
        };

        private readonly Livre livre2 = new Livre
        {
            Id = 2,
            Titre = "To Kill a Mockingbird",
            ISBN = "0061120081",
            prix = 19.99,
            Auteur = "Harper Lee",
            Annee = 1960,
            NbrExemplaire = 30
        };

        [Fact]
        public void Add_Test()
        {
            // Act
            _service.Add(livre1);
            _service.Add(livre2);

            // Assert
            var livres = _service.GetLivres();
            Assert.Equal(2, livres.Count);
            Assert.Contains(livre1, livres);
            Assert.Contains(livre2, livres);
        }

        [Fact]
        public void GetLivreById_Test()
        {
            // Arrange
            _service.Add(livre1);

            // Act
            var result = _service.GetLivresById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(livre1.Titre, result.Titre);
            Assert.Equal(livre1.ISBN, result.ISBN);
        }

        [Fact]
        public void Update_Test()
        {
            // Arrange
            _service.Add(livre1);
            var updatedLivre = new Livre
            {
                Id = 1,
                Titre = "The Great Gatsby - Updated",
                ISBN = "9780743273565",
                prix = 28.99,
                Auteur = "F. Scott Fitzgerald",
                Annee = 2015,
                NbrExemplaire = 45
            };

            // Act
            _service.Update(updatedLivre);
            var result = _service.GetLivresById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedLivre.Titre, result.Titre);
            Assert.Equal(updatedLivre.prix, result.prix);
            Assert.Equal(updatedLivre.Annee, result.Annee);
            Assert.Equal(updatedLivre.NbrExemplaire, result.NbrExemplaire);
        }

        [Fact]
        public void Remove_Test()
        {
            // Arrange
            _service.Add(livre1);
            _service.Add(livre2);

            // Act
            _service.Remove(1);

            // Assert
            var livres = _service.GetLivres();
            Assert.Single(livres);
            Assert.DoesNotContain(livre1, livres);
            Assert.Contains(livre2, livres);
        }
    }
}
