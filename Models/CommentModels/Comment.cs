using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NivesBrelihPhotography.Models.PhotoModels;

namespace NivesBrelihPhotography.Models.CommentModels
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }  //primary key for comment

        [Required]
        public string CommentText { get; set; }  //comment text

        [Required]
        public string UserId { get; set; }  //user posting comment

        #region photo_aplied_to
        [ForeignKey("PhotoId")]
        public virtual Photo Photo { get; set; }  //virtual property to access photo props
        [Required]
        public int PhotoId { get; set; }  //foreign photo key
        #endregion

        [Required]
        public DateTime Created { get; set; }  //datetime when was created

        public DateTime? Edited { get; set; }
    }
}