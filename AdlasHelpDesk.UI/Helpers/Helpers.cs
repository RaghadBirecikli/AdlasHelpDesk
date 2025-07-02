using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Helper
{
    public static class Helpers
    {
        public static void fillOrderByListItems(this List<SelectListItem> OrderByItems, params string[] orderBy)
        {

            foreach (var item in orderBy)
            {
                OrderByItems.Add(new SelectListItem { Value = item, Text = ConstantPagingOrderBy.GetFieldValue(item) });
            }
        }
    }
}
