using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{
    public class CompanyUpsertDto
    {
        public Guid? Id { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionTr { get; set; }
        public string? DescriptionDe { get; set; }
        public string? DescriptionTranslated { get; set; }
        [MaxLength(200)]
        public string FooterDescription { get; set; }
        public string? FooterDescriptionEn { get; set; }
        public string? FooterDescriptionTr { get; set; }
        public string? FooterDescriptionDe { get; set; }
        public string? FooterDescriptionTranslated { get; set; }
        [MaxLength(200)]
        public string? Logo { get; set; }
		public IFormFile? LogoFile { get; set; }

		[MaxLength(200)]
        public string? Icon { get; set; }
		public IFormFile? IconFile { get; set; }

		[MaxLength(20)]
        public string? FirstPhone { get; set; }
        [MaxLength(20)]
        public string? SecondPhone { get; set; }
        [MaxLength(200)]
        public string? FirstAddress { get; set; }
        [MaxLength(200)]
        public string? SecondAddress { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(200)]
        public string? Instagram { get; set; }
        [MaxLength(200)]
        public string? FaceBook { get; set; }
        [MaxLength(200)]
        public string? Twiter { get; set; }
        [MaxLength(200)]
        public string? YouTube { get; set; }
        [MaxLength(200)]
        public string? Linkedin { get; set; }
        [MaxLength(200)]
        public string? GooglePlus { get; set; }

        [MaxLength(200)]
        public string? AboutUsPageDescription { get; set; }
        public string? AboutUsPageDescriptionEn { get; set; }
        public string? AboutUsPageDescriptionTr { get; set; }
        public string? AboutUsPageDescriptionDe { get; set; }
        public string? AboutUsPageDescriptionTranslated { get; set; }
        [MaxLength(200)]
        public string? SoftwareProductPageDescription { get; set; }
        public string? SoftwareProductPageDescriptionEn { get; set; }
        public string? SoftwareProductPageDescriptionTr { get; set; }
        public string? SoftwareProductPageDescriptionDe { get; set; }
        public string? SoftwareProductPageDescriptionTranslated { get; set; }
        [MaxLength(200)]
        public string? ContactPageDescription { get; set; }
        public string? ContactPageDescriptionEn { get; set; }
        public string? ContactPageDescriptionTr { get; set; }
        public string? ContactPageDescriptionDe { get; set; }
        public string? ContactPageDescriptionTranslated { get; set; }
        public string? AboutUsPageContext { get; set; }
        public string? AboutUsPageContextEn { get; set; }
        public string? AboutUsPageContextTr { get; set; }
        public string? AboutUsPageContextDe { get; set; }
        public string? AboutUsPageContextTranslated { get; set; }
    }
}