using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC_Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Authors")]
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
