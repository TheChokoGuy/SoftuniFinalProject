using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Item> Liked = new List<Item>();

        public IEnumerable<Item> Cart = new List<Item>();
    }
}
