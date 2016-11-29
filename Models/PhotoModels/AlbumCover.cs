using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.PhotoModels
{
    public class AlbumCover
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public virtual PhotoAlbum Album { get; set; }

    }
}
