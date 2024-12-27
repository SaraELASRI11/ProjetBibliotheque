namespace GestionBibliotheque.Models
{
    public class Livre
    {
    
        public int Id { get; set; }
        public string Titre { get; set; }
       
        public int Annee{ get; set; }
        public string ISBN { get; set; } 
        public string Auteur { get; set; }
        public double prix { get; set; }

        public int NbrExemplaire { get; set; }

      
    }
}
