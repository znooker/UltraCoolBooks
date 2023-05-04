#nullable disable
using System;
using System.Collections.Generic;
using UltraCoolBooks.Data;
using System.ComponentModel.DataAnnotations;

namespace UltraCoolBooks.Models
{
    public partial class BookQuote
    {
        public int BookQuoteId { get; set; }
        public bool IsAccepeted { get; set; } = false;

        [Required]
        public string QuoteText { get; set; }

        public int BookId { get; set; }

        public string UserId { get; set; }

        public virtual Book Book { get; set; }
        public virtual UltraCoolUser User { get; set; }
    }
}