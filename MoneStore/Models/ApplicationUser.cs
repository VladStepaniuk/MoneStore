using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models
{
    public class ApplicationUser:IdentityUser
    {
        public byte[] ProfileImage { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
