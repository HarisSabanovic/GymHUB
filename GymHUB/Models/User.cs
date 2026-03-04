using Microsoft.AspNetCore.Identity;

namespace GymHUB.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }

        //Lista på bokningar som användaren har
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
