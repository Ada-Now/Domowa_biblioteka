using Microsoft.AspNetCore.Identity;
using System.Security.Permissions;
using System.ComponentModel;

namespace Domowa_biblioteka.Models
{
    public class Ksiazka1
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public string Gatunek { get; set; }
        public string Opis { get; set; }
        public DateTime Data_wydania { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
