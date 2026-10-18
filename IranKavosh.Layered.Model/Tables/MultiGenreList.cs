using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "GenreList", Schema = "Movie")]
    public class MultiGenreList : ModelId
    {
        public Guid MultimediaHeaderId { get; set; }
        public virtual MultimediaHeader MultimediaHeader { get; set; }

        public Guid MultiGenreId { get; set; }
        public virtual MultiGenre MultiGenre { get; set; }

        public short Order { get; set; }
    }
}