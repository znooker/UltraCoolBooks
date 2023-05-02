#nullable disable
using System;
using System.Collections.Generic;
using UltraCoolBooks.Data;

namespace UltraCoolBooks.Models
{
    public partial class AuthorQuote
    {
        public int AuthorQuoteId { get; set; }
        public bool IsAccepeted { get; set; } = false;
        public string QuoteText { get; set; }
        public int AuthorId { get; set; }
        public string UserId { get; set; }
        public virtual Author Author { get; set; }
        public virtual UltraCoolUser User { get; set; }
    }
}
