using System.Collections.Generic;

namespace CQRS.Sales.Interfaces.Presentation
{
    public class ProductSearchCriteria
    {
        // constraints
        public string ContainsText { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<int> SpecificProductIds { get; set; }
        
        public ProductSearchOrder OrderBy { get; set; }
        public bool Ascending { get; set; }

        // pagination support
        private int _pageNumber;
        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value < 0 ? 0 : value; }
        }

        private int _itemsPerPage = 10;
        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set { _itemsPerPage = value < 1 ? 1 : value; }
        }

        public ProductSearchCriteria()
        {
            SpecificProductIds = new List<int>();
            OrderBy = ProductSearchOrder.None;
            Ascending = true;
        }

        public bool HasSpecificProductIdsFilter()
        {
            return SpecificProductIds != null && SpecificProductIds.Count != 0;
        }
    }
}