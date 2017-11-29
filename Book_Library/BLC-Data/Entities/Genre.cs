using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC_Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
