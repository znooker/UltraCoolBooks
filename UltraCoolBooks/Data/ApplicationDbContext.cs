using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<UltraCoolUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<AuthorBook> AuthorBooks { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BookGenre> BookGenres { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<ReviewFeedBack> ReviewFeedBacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Created)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BooksBookId });

                entity.ToTable("AuthorBook");

                entity.HasIndex(e => e.BooksBookId, "IX_AuthorBook_BooksBookId");

                entity.HasOne(d => d.Author).WithMany(p => p.AuthorBooks)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BooksBook).WithMany(p => p.AuthorBooks)
                    .HasForeignKey(d => d.BooksBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Books_UserId");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(e => e.ISBN)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.ImagePath).IsRequired();
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.isDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasKey(e => new { e.BooksBookId, e.GenresGenreId });

                entity.ToTable("BookGenre");

                entity.HasIndex(e => e.GenresGenreId, "IX_BookGenre_GenresGenreId");

                entity.HasOne(d => d.BooksBook).WithMany(p => p.BookGenres)
                    .HasForeignKey(d => d.BooksBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.GenresGenre).WithMany(p => p.BookGenres)
                    .HasForeignKey(d => d.GenresGenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.Title, "IX_Genre_Titel");

                entity.Property(e => e.Created)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("(N'')");
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasIndex(e => e.BookId, "IX_Reviews_BookId");

                entity.HasIndex(e => e.UserId, "IX_Reviews_UserId");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
                entity.Property(e => e.ReviewText)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("(N'')");
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ReviewFeedBack>(entity =>
            {
                entity.HasIndex(e => e.ReviewId, "IX_ReviewFeedBacks_ReviewId");

                entity.HasIndex(e => e.UserId, "IX_ReviewFeedBacks_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.Review).WithMany(p => p.ReviewFeedBacks)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}