namespace AdlasHelpDesk.UI.Helper
{
    public class ConstantPagingOrderBy
    {
        public static string Name { get; set; } = "Ad";
        public static string FullName { get; set; } = "Ad/Soyad";
        public static string Code { get; set; } = "Kod";
        public static string SystemCode { get; set; } = "Sistem Kod";
        public static string CreateDate { get; set; } = "Oluşturma Tarihi";
        public static string GetFieldValue(string propName)
        {
            var prop = typeof(ConstantPagingOrderBy).GetProperty(propName).GetValue(typeof(ConstantPagingOrderBy), null).ToString();
            return prop;
        }
    }
}
