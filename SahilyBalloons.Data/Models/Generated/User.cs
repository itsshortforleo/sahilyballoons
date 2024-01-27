using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SahilyBalloons.Data.Models
{
    [Table("users")]
    public partial class User
    {
        [Key]
        [Column("users_id")]
        public int UsersId { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column("created_by")]
        public int? CreatedBy { get; set; }
        [Column("first_name")]
        [StringLength(200)]
        public string? FirstName { get; set; }
        [Column("last_name")]
        [StringLength(200)]
        public string? LastName { get; set; }
        [Column("full_name")]
        [StringLength(200)]
        public string? FullName { get; set; }
    }
}
