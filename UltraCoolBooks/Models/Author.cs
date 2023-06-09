﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraCoolBooks.Models;

public partial class Author
{

    public int AuthorId { get; set; }
    [System.ComponentModel.DataAnnotations.Required]
    public string FirstName { get; set; }
    [System.ComponentModel.DataAnnotations.Required]
    public string LastName { get; set; }
    // This property should not be mapped to the database
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    [System.ComponentModel.DataAnnotations.Required]
    public DateTime BirthDate { get; set; }

    public DateTime Created { get; set; }

    public string ImagePath { get; set; }

    public virtual ICollection<AuthorBook> AuthorBooks { get; } = new List<AuthorBook>();
    public virtual ICollection<AuthorQuote> AuthorQuotes { get; } = new List<AuthorQuote>();
}