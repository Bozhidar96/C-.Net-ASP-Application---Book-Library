using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC_Data.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(200, MinimumLength = 1)]
        public string LastName { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
