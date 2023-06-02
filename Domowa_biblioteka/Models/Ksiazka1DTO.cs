using Microsoft.AspNetCore.Identity;

namespace Domowa_biblioteka.Models
{
    public class Ksiazka1DTO
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public string Gatunek { get; set; }
        public string Opis { get; set; }
        public DateTime Data_wydania { get; set; }

    }
}
