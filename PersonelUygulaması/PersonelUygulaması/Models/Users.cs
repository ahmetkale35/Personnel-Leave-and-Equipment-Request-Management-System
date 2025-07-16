using System.Runtime.ConstrainedExecution;

namespace PersonelUygulaması.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }

        public string Email { get; set; }

        public string ParolaHash { get; set; }

        // foreign key for roles
        public int RolId { get; set; }


    }
}
