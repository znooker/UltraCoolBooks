﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace UltraCoolBooks.Models;

public partial class BookGenre
{
    public int BooksBookId { get; set; }

    public int GenresGenreId { get; set; }

    public virtual Book BooksBook { get; set; }

    public virtual Genre GenresGenre { get; set; }
}