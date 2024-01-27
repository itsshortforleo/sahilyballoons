using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SahilyBalloons.Data.Models
{
    [Table("comment")]
    public partial class Comment
    {
        public Comment()
        {
            InverseCommentParent = new HashSet<Comment>();
        }

        [Key]
        [Column("comment_id")]
        public int CommentId { get; set; }
        [Column("post_parent_id")]
        public int? PostParentId { get; set; }
        [Column("comment_parent_id")]
        public int? CommentParentId { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column("created_by")]
        public int? CreatedBy { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("is_comment_approved")]
        public bool IsCommentApproved { get; set; }

        [ForeignKey("CommentParentId")]
        [InverseProperty("InverseCommentParent")]
        public virtual Comment? CommentParent { get; set; }
        [ForeignKey("PostParentId")]
        [InverseProperty("Comments")]
        public virtual Post? PostParent { get; set; }
        [InverseProperty("CommentParent")]
        public virtual ICollection<Comment> InverseCommentParent { get; set; }
    }
}
