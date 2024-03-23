using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdventureWorks_DropDown_Detail.Models
{
    public class GetViewModel
    {
        public int SeletedId {  get; set; }
        public List<SelectListItem> Products { get; set; }

        public List<ProductDetail> Detail { get; set; }

    }
    public class ProductDetail {

        public string Name { get; set; }

        public object? Value { get; set; }
    }

}
