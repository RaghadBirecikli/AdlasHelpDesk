
namespace AdlasHelpDesk.Application.DtoModels
{

    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public string FooterDescription { get; set; }
		public string Logo { get; set; }
		public string Icon { get; set; }
		public string? FirstPhone { get; set; }
		public string? SecondPhone { get; set; }
		public string? FirstAddress { get; set; }
		public string? SecondAddress { get; set; }
		public string? Email { get; set; }
		public string? Instagram { get; set; }
		public string? FaceBook { get; set; }
		public string? Twiter { get; set; }
		public string? YouTube { get; set; }
		public string? Linkedin { get; set; }
		public string? GooglePlus { get; set; }
    }
}