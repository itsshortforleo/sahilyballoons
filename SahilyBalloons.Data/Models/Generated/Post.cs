using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SahilyBalloons.Data.Models
{
    [Table("post")]
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column("created_by")]
        public int? CreatedBy { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("is_post_published")]
        public bool IsPostPublished { get; set; }

        [InverseProperty("PostParent")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
