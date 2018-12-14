using System.Web.Mvc;
using CQRS.Sales.Interfaces.Presentation;

namespace CQRS.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IProductFinder ProductFinder { get; set; }

        public ActionResult Index(string containsText = "", decimal? maxPrice = null, int pageIndex = 0, string sort = "", bool ascending = false)
        {
            var p = ProductFinder.FindProducts(new ProductSearchCriteria
                                                    {
                                                        ContainsText=containsText,
                                                        MaxPrice = maxPrice,
                                                        PageNumber = pageIndex,
                                                        OrderBy = GetOrderBy(sort),
                                                        Ascending = ascending
                                                    });

            ViewBag.sort = sort;
            ViewBag.ascending = ascending;
            ViewBag.maxPrice = maxPrice;
            ViewBag.containsText = containsText;
            ViewBag.pageIndex = pageIndex;

            return View(p);
        }

        private static ProductSearchOrder GetOrderBy(string sort)
        {
            if (sort == "name")
                return ProductSearchOrder.Name;
            if (sort == "price")
                return ProductSearchOrder.Price;
            return ProductSearchOrder.None;
        }

        /*public String productsList(Model model, @RequestParam(value = "sortBy", required = false) String sortBy,
            @RequestParam(value = "ascending", required = false) Boolean ascending,
            @RequestParam(value = "page", required = false) Integer page,
            @RequestParam(value = "maxPrice", required = false) Double maxPrice,
            @RequestParam(value = "containsText", required = false) String containsText) {

        ProductSearchCriteria criteria = new ProductSearchCriteria();
        addFiltering(criteria, containsText, maxPrice);
        addPagination(criteria, page, RESULTS_PER_PAGE);
        addOrdering(criteria, sortBy, ascending);

        model.addAttribute("products", productFinder.findProducts(criteria));
        model.addAttribute("sortBy", criteria.getOrderBy().toString());
        model.addAttribute("ascending", criteria.isAscending());
        model.addAttribute("containsTextFilter", criteria.getContainsText());
        model.addAttribute("maxPriceFilter", criteria.getMaxPrice());
        return "sales/productsList";
    }*/

    }
}
