using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SahilyBalloons.Data.Models;

namespace SahilyBalloons.Data
{
    public partial class SahilyBalloonsWebsiteDbContext : DbContext
    {
        public SahilyBalloonsWebsiteDbContext()
        {
        }

        public SahilyBalloonsWebsiteDbContext(DbContextOptions<SahilyBalloonsWebsiteDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:SahilyBalloonsWebsiteDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.CommentParent)
                    .WithMany(p => p.InverseCommentParent)
                    .HasForeignKey(d => d.CommentParentId)
                    .HasConstraintName("FK__comment__comment__3B75D760");

                entity.HasOne(d => d.PostParent)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostParentId)
                    .HasConstraintName("FK__comment__post_pa__3A81B327");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsersId)
                    .HasName("PK__users__EAA7D14B3BF6166E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
