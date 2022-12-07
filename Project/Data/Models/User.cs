using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Models
{ 
    public class User : IdentityUser
    {

        public List<UserInformation> Addresses { get; set; }
    }
}
