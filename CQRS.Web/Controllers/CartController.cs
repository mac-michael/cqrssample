using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CQRS.Base.CQRS.Commands;
using CQRS.Sales.Interfaces.Application.Commands;
using CQRS.Sales.Interfaces.Presentation;

namespace CQRS.Web.Controllers
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }


    public class CartController : Controller
    {
        public IProductFinder ProductFinder { get; set; }
        public IOrderFinder OrderFinder { get; set; }
        public IGate Gate { get; set; }

        public ActionResult Index()
        {
            var cartItems = Enumerable.Empty<CartItemDto>();

            if ( Cart.HasItems )
            {
                // LINQ
                var productsIdsWithCount = Cart.ProductsIdsWithCount;
                var products = ProductFinder.FindProducts(new ProductSearchCriteria
                                            {
                                                SpecificProductIds = productsIdsWithCount.Keys.ToList()
                                            });

                cartItems = from p in products.Items
                    select new CartItemDto {Name = p.DisplayedName, ProductId = p.ProductId, Count=productsIdsWithCount[p.ProductId]};
            }


            return View(cartItems);
        }

        public ActionResult Add(int productId, string returnUrl)
        {
            Cart.Add(productId);

            return Redirect(returnUrl);
        }
        
        public ActionResult Checkout()
        {
            var createOrderCommand = new CreateOrderCommand(Cart.ProductsIdsWithCount);
            Gate.Dispatch(createOrderCommand);

            return RedirectToAction("ConfirmOrder", new {orderId = createOrderCommand.OrderId});
        }

        public ActionResult Clear(string returnUrl)
        {
            Cart.Clear();

            return Redirect(returnUrl);
        }

        public ActionResult ConfirmOrder(int orderId)
        {
            return View(OrderFinder.GetClientOrderDetails(orderId));
        }

        public ActionResult Confirm(int orderId)
        {
            Gate.Dispatch(new SubmitOrderCommand(orderId));
            return RedirectToAction("ConfirmOrder", new { orderId});
        }

        public Cart Cart { get; set; }
    }
}
