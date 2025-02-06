using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    public required DateTimeOffset? UpdatedDate { get; set; }
}