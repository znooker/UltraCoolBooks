﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UltraCoolBooks.Data;

#nullable disable

namespace UltraCoolBooks.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230504144234_AddedBookQuotes")]
    partial class AddedBookQuotes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "026a026b-3fd0-4d5a-a16e-4d513002da19",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "ba390d43-4aa7-4898-9eaa-5678aef23551",
                            Name = "Moderator",
                            NormalizedName = "MODERATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "Permission",
                            ClaimValue = "CanAccessAdminPanel",
                            RoleId = "026a026b-3fd0-4d5a-a16e-4d513002da19"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "9d233475-81ea-4c3b-b050-df577917e90f",
                            RoleId = "026a026b-3fd0-4d5a-a16e-4d513002da19"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UltraCoolBooks.Data.UltraCoolUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "9d233475-81ea-4c3b-b050-df577917e90f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5cb652de-ee44-4be0-8a0f-e4dca907adb1",
                            Email = "admin@example.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEPQjUTS75+/RDq/TyMQmyhol9fXmQrjy7eejIT+c8WVuSdBBQpEl1NgIeKriQykV5g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin@example.com"
                        });
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            BirthDate = new DateTime(1111, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Unknown",
                            ImagePath = "no-picture.jpg",
                            LastName = "Author"
                        },
                        new
                        {
                            AuthorId = 2,
                            BirthDate = new DateTime(1942, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "J.R.R",
                            ImagePath = "JRR-Tolkien.jpg",
                            LastName = "Tolkien"
                        },
                        new
                        {
                            AuthorId = 3,
                            BirthDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1942),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "William",
                            ImagePath = "William-Gibson.jpg",
                            LastName = "Gibson"
                        },
                        new
                        {
                            AuthorId = 4,
                            BirthDate = new DateTime(1971, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Rafal",
                            ImagePath = "Rafal_Kosik.jpg",
                            LastName = "Kosik"
                        });
                });

            modelBuilder.Entity("UltraCoolBooks.Models.AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BooksBookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BooksBookId");

                    b.HasIndex(new[] { "BooksBookId" }, "IX_AuthorBook_BooksBookId");

                    b.ToTable("AuthorBook", (string)null);

                    b.HasData(
                        new
                        {
                            AuthorId = 2,
                            BooksBookId = 2
                        },
                        new
                        {
                            AuthorId = 2,
                            BooksBookId = 3
                        },
                        new
                        {
                            AuthorId = 3,
                            BooksBookId = 1
                        },
                        new
                        {
                            AuthorId = 3,
                            BooksBookId = 4
                        },
                        new
                        {
                            AuthorId = 3,
                            BooksBookId = 5
                        });
                });

            modelBuilder.Entity("UltraCoolBooks.Models.AuthorQuote", b =>
                {
                    b.Property<int>("AuthorQuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorQuoteId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepeted")
                        .HasColumnType("bit");

                    b.Property<string>("QuoteText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AuthorQuoteId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UserId");

                    b.ToTable("AuthorQuote");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("isDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(0)))");

                    b.HasKey("BookId");

                    b.HasIndex(new[] { "UserId" }, "IX_Books_UserId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Set in the dystopian future.",
                            ISBN = "9780441012039",
                            ImagePath = "Neuromancer.jpg",
                            ReleaseDate = new DateTime(1984, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Neuromancer",
                            UserId = "9d233475-81ea-4c3b-b050-df577917e90f",
                            isDeleted = false
                        },
                        new
                        {
                            BookId = 2,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold.",
                            ISBN = "9780261102217",
                            ImagePath = "The_Hobbit.jpg",
                            ReleaseDate = new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Hobbit",
                            UserId = "9d233475-81ea-4c3b-b050-df577917e90f",
                            isDeleted = false
                        },
                        new
                        {
                            BookId = 3,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Continuing the story begun in The Hobbit, this is the first part of Tolkien's epic masterpiece, The Lord of the Rings, featuring a striking black cover based on Tolkien's own design, the definitive text, and a detailed map of Middle-earth.",
                            ISBN = "9780261103573",
                            ImagePath = "Fellowship_Of_The_Ring.jpg",
                            ReleaseDate = new DateTime(1953, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Fellowship of the Ring",
                            UserId = "9d233475-81ea-4c3b-b050-df577917e90f",
                            isDeleted = false
                        },
                        new
                        {
                            BookId = 4,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Ten stories deal with a human memory bank, UFOs, sleep machines, interstellar travel, a Soviet space station, and computer crime",
                            ISBN = "9780441089345",
                            ImagePath = "Burning_Chrome.jpg",
                            ReleaseDate = new DateTime(1984, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Burning Chrome",
                            UserId = "9d233475-81ea-4c3b-b050-df577917e90f",
                            isDeleted = false
                        },
                        new
                        {
                            BookId = 5,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Set in the world of Cyberpunk 2077, one of the bestselling video games of recent years, from acclaimed Polish science fiction writer Rafal Kosik, this electrifying novel follows a group of strangers as they discover that the dangers of Night City are all too real.",
                            ISBN = "0759557179",
                            ImagePath = "No_Coincidence.jpg",
                            ReleaseDate = new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "No Coincidence",
                            UserId = "9d233475-81ea-4c3b-b050-df577917e90f",
                            isDeleted = false
                        });
                });

            modelBuilder.Entity("UltraCoolBooks.Models.BookGenre", b =>
                {
                    b.Property<int>("BooksBookId")
                        .HasColumnType("int");

                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.HasKey("BooksBookId", "GenresGenreId");

                    b.HasIndex(new[] { "GenresGenreId" }, "IX_BookGenre_GenresGenreId");

                    b.ToTable("BookGenre", (string)null);

                    b.HasData(
                        new
                        {
                            BooksBookId = 1,
                            GenresGenreId = 3
                        },
                        new
                        {
                            BooksBookId = 2,
                            GenresGenreId = 2
                        },
                        new
                        {
                            BooksBookId = 3,
                            GenresGenreId = 2
                        },
                        new
                        {
                            BooksBookId = 4,
                            GenresGenreId = 3
                        },
                        new
                        {
                            BooksBookId = 5,
                            GenresGenreId = 3
                        },
                        new
                        {
                            BooksBookId = 4,
                            GenresGenreId = 2
                        });
                });

            modelBuilder.Entity("UltraCoolBooks.Models.BookQuote", b =>
                {
                    b.Property<int>("BookQuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookQuoteId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepeted")
                        .HasColumnType("bit");

                    b.Property<string>("QuoteText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BookQuoteId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("BookQuote");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasDefaultValueSql("(N'')");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("GenreId");

                    b.HasIndex(new[] { "Title" }, "IX_Genre_Titel");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Other genre that haven't been added to our systems yet!",
                            Title = "Other"
                        },
                        new
                        {
                            GenreId = 2,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Fantasy.",
                            Title = "Fantasy"
                        },
                        new
                        {
                            GenreId = 3,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Cyberpunk.",
                            Title = "Cyberpunk"
                        });
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("Downvotes")
                        .HasColumnType("int");

                    b.Property<int>("FLaggedCount")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(0)))");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasDefaultValueSql("(N'')");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Upvotes")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReviewId");

                    b.HasIndex(new[] { "BookId" }, "IX_Reviews_BookId");

                    b.HasIndex(new[] { "UserId" }, "IX_Reviews_UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.ReviewFeedBack", b =>
                {
                    b.Property<int>("ReviewFeedBackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewFeedBackId"));

                    b.Property<bool?>("HasFlagged")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsHelpful")
                        .HasColumnType("bit");

                    b.Property<int>("ReviewId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReviewFeedBackId");

                    b.HasIndex(new[] { "ReviewId" }, "IX_ReviewFeedBacks_ReviewId");

                    b.HasIndex(new[] { "UserId" }, "IX_ReviewFeedBacks_UserId");

                    b.ToTable("ReviewFeedBacks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UltraCoolBooks.Models.AuthorBook", b =>
                {
                    b.HasOne("UltraCoolBooks.Models.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AuthorId")
                        .IsRequired();

                    b.HasOne("UltraCoolBooks.Models.Book", "BooksBook")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BooksBookId")
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("BooksBook");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.AuthorQuote", b =>
                {
                    b.HasOne("UltraCoolBooks.Models.Author", "Author")
                        .WithMany("AuthorQuotes")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", "User")
                        .WithMany("AuthorQuotes")
                        .HasForeignKey("UserId");

                    b.Navigation("Author");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Book", b =>
                {
                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.BookGenre", b =>
                {
                    b.HasOne("UltraCoolBooks.Models.Book", "BooksBook")
                        .WithMany("BookGenres")
                        .HasForeignKey("BooksBookId")
                        .IsRequired();

                    b.HasOne("UltraCoolBooks.Models.Genre", "GenresGenre")
                        .WithMany("BookGenres")
                        .HasForeignKey("GenresGenreId")
                        .IsRequired();

                    b.Navigation("BooksBook");

                    b.Navigation("GenresGenre");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.BookQuote", b =>
                {
                    b.HasOne("UltraCoolBooks.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", "User")
                        .WithMany("BookQuotes")
                        .HasForeignKey("UserId");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Review", b =>
                {
                    b.HasOne("UltraCoolBooks.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .IsRequired();

                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.ReviewFeedBack", b =>
                {
                    b.HasOne("UltraCoolBooks.Models.Review", "Review")
                        .WithMany("ReviewFeedBacks")
                        .HasForeignKey("ReviewId")
                        .IsRequired();

                    b.HasOne("UltraCoolBooks.Data.UltraCoolUser", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraCoolBooks.Data.UltraCoolUser", b =>
                {
                    b.Navigation("AuthorQuotes");

                    b.Navigation("BookQuotes");

                    b.Navigation("Books");

                    b.Navigation("Feedbacks");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Author", b =>
                {
                    b.Navigation("AuthorBooks");

                    b.Navigation("AuthorQuotes");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Book", b =>
                {
                    b.Navigation("AuthorBooks");

                    b.Navigation("BookGenres");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Genre", b =>
                {
                    b.Navigation("BookGenres");
                });

            modelBuilder.Entity("UltraCoolBooks.Models.Review", b =>
                {
                    b.Navigation("ReviewFeedBacks");
                });
#pragma warning restore 612, 618
        }
    }
}
