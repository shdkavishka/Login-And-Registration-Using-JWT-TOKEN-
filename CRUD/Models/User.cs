using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? Email { get; set; }

       /* [NotMapped] // Exclude this property from the database schema
        public string Password { get; set; }*/

        public string? PasswordHash { get; set; }



    }
}
