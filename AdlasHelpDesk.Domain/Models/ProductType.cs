using System.Net.Sockets;

namespace AdlasHelpDesk.Domain.Models
{
    public class ProductType : BaseEntity<Guid>
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
