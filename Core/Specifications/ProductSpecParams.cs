using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        /// <summary>
        /// Maximum size of page
        /// </summary>
        private const int MaxPageSize = 50;
        /// <summary>
        /// Index of page
        /// </summary>
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        /// <summary>
        /// Size of page
        /// </summary>
        public int PageSize
        {
            get => _pageSize; // return _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        /// <summary>
        /// Id of PRODUCTS BRAND
        /// </summary>
        public int? BrandId { get; set; }
        /// <summary>
        /// Id of PRODUCTS TYPE
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// Sort parameter (priceAsc | priceDesc)
        /// </summary>
        public string Sort { get; set; }

        private string _search;
        /// <summary>
        /// Search phrase to be found
        /// </summary>
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
