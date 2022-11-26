using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {

            Liked = new List<Item>();
            Cart = new List<Item>();
        }

        public virtual ICollection<Item> Liked { get; set; }

        public virtual ICollection<Item> Cart { get; set; }
    }
}
