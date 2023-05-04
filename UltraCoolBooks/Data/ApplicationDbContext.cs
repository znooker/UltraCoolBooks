using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Models;
using UltraCoolBooks.Data;
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
        public virtual DbSet<AuthorQuote> AuthorQuotes { get; set; }
        public virtual DbSet<BookQuote> BookQuotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var adminRoleId = Guid.NewGuid().ToString();
            var moderatorRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(new IdentityRoleClaim<string>
            {
                Id = 1,
                RoleId = adminRoleId,
                ClaimType = "Permission",
                ClaimValue = "CanAccessAdminPanel"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = moderatorRoleId,
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            });


            var adminUserId = Guid.NewGuid().ToString();
            modelBuilder.Entity<UltraCoolUser>().HasData(new UltraCoolUser
            {
                Id = adminUserId,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<UltraCoolUser>().HashPassword(null, "password"),
                SecurityStamp = string.Empty
            });
            
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });

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

            modelBuilder.Entity<Author>().HasData(new Models.Author()
            {
                AuthorId = 1,
                FirstName = "Unknown",
                LastName = "Author",
                BirthDate = new DateTime(1111, 1, 1),
                ImagePath = "no-picture.jpg",
            }, new Models.Author()
            {
                AuthorId = 2,
                FirstName = "J.R.R",
                LastName = "Tolkien",
                BirthDate = new DateTime(1942, 4, 1),
                ImagePath = "JRR-Tolkien.jpg",
            },
             new Models.Author()
             {
                 AuthorId = 3,
                 FirstName = "William",
                 LastName = "Gibson",
                 BirthDate = new DateTime(1942),
                 ImagePath = "William-Gibson.jpg",
             }, new Models.Author()
             {
                 AuthorId = 4,
                 FirstName = "Rafal",
                 LastName = "Kosik",
                 BirthDate = new DateTime(1971, 3, 1),
                 ImagePath = "Rafal_Kosik.jpg",
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

            modelBuilder.Entity<Book>().HasData(new Models.Book()
            {
                UserId = adminUserId,
                BookId = 1,
                Title = "Neuromancer",
                Description = "Set in the dystopian future.",
                ISBN = "9780441012039",
                ImagePath = "Neuromancer.jpg",
                isDeleted = false,
                ReleaseDate = new DateTime(1984, 1, 7)
            }, new Models.Book()
            {
                UserId = adminUserId,
                BookId = 2,
                Title = "The Hobbit",
                Description = "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold.",
                ISBN = "9780261102217",
                ImagePath = "The_Hobbit.jpg",
                isDeleted = false,
                ReleaseDate = new DateTime(1937, 9, 21)
            }, new Models.Book()
            {
                UserId = adminUserId,
                BookId = 3,
                Title = "Fellowship of the Ring",
                Description = "Continuing the story begun in The Hobbit, this is the first part of Tolkien's epic masterpiece, The Lord of the Rings, featuring a striking black cover based on Tolkien's own design, the definitive text, and a detailed map of Middle-earth.",
                ISBN = "9780261103573",
                ImagePath = "Fellowship_Of_The_Ring.jpg",
                isDeleted = false,
                ReleaseDate = new DateTime(1953, 7, 29)
            }, new Models.Book()
            {
                UserId = adminUserId,
                BookId = 4,
                Title = "Burning Chrome",
                Description = "Ten stories deal with a human memory bank, UFOs, sleep machines, interstellar travel, a Soviet space station, and computer crime",
                ISBN = "9780441089345",
                ImagePath = "Burning_Chrome.jpg",
                isDeleted = false,
                ReleaseDate = new DateTime(1984, 4, 2)
            }, new Models.Book()
            {
                UserId = adminUserId,
                BookId = 5,
                Title = "No Coincidence",
                Description = "Set in the world of Cyberpunk 2077, one of the bestselling video games of recent years, from acclaimed Polish science fiction writer Rafal Kosik, this electrifying novel follows a group of strangers as they discover that the dangers of Night City are all too real.",
                ISBN = "0759557179",
                ImagePath = "No_Coincidence.jpg",
                isDeleted = false,
                ReleaseDate = new DateTime(2023, 9, 3)
            });

            modelBuilder.Entity<AuthorBook>().HasData(new Models.AuthorBook()
            {
                AuthorId = 2,
                BooksBookId = 2,
            },
        new Models.AuthorBook()
        {
            AuthorId = 2,
            BooksBookId = 3,
        },
        new Models.AuthorBook()
        {
            AuthorId = 3,
            BooksBookId = 1,
        },
        new Models.AuthorBook()
        {
            AuthorId = 3,
            BooksBookId = 4,
        },
        new Models.AuthorBook()
        {
            AuthorId = 3,
            BooksBookId = 5,
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

            modelBuilder.Entity<BookGenre>().HasData(
       new Models.BookGenre()
       {
           BooksBookId = 1,
           GenresGenreId = 3,

       },
       new Models.BookGenre()
       {
           BooksBookId = 2,
           GenresGenreId = 2,

       }, new Models.BookGenre()
       {
           BooksBookId = 3,
           GenresGenreId = 2,
       }, new Models.BookGenre()
       {
           BooksBookId = 4,
           GenresGenreId = 3,

       }, new Models.BookGenre()
       {
           BooksBookId = 5,
           GenresGenreId = 3,

       }, new Models.BookGenre()
       {
           BooksBookId = 4,
           GenresGenreId = 2,

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

            modelBuilder.Entity<Genre>().HasData(
        new Models.Genre()
        {
            GenreId = 1,
            Title = "Other",
            Description = "Other genre that haven't been added to our systems yet!",

        },
        new Models.Genre()
        {
            GenreId = 2,
            Title = "Fantasy",
            Description = "Fantasy.",

        }, new Models.Genre()
        {
            GenreId = 3,
            Title = "Cyberpunk",
            Description = "Cyberpunk.",
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

        public DbSet<UltraCoolBooks.Models.AuthorQuote> AuthorQuote { get; set; } = default!;

        public DbSet<UltraCoolBooks.Models.BookQuote> BookQuote { get; set; } = default!;
    }
}