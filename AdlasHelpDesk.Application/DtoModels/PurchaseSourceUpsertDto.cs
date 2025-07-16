using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{

    public class PurchaseSourceUpsertDto
    {
        public Guid? Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}