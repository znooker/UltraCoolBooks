using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UltraCoolBooks.Data;
namespace UltraCoolBooks.Models;

public partial class ReviewComment
{
    public int ReviewCommentId { get; set; }

    public int ReviewId { get; set; }
    public string UserId { get; set; }

    public string Comment { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Review Review { get; set; }
    public virtual UltraCoolUser User { get; set; }
}
