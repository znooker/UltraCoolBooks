using Microsoft.AspNetCore.Identity;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Data
{
    public class UltraCoolUser : IdentityUser
    {
        public virtual ICollection<Book> Books { get; } = new List<Book>();

        public virtual ICollection<Review> Reviews { get; } = new List<Review>();

        public virtual ICollection<ReviewFeedBack> Feedbacks { get; set; }
    }
}
