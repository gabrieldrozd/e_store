using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Business.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IGenericRepository<ProductType> _productTypesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandsRepo, IGenericRepository<ProductType> productTypesRepo,
        IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productBrandsRepo = productBrandsRepo;
            _productTypesRepo = productTypesRepo;
            _mapper = mapper;
        }

        #region Get Actions
        /// <summary>
        /// Action used to return single PRODUCT entity with given Id
        /// </summary>
        /// <param name="productId">Id of entity to be returned</param>
        /// <returns>Entity of type Product with id = productId</returns>
        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int productId)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(productId);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var productToReturn = _mapper.Map<Product, ProductToReturnDTO>(product);

            return Ok(productToReturn);
        }

        /// <summary>
        /// Action used to return ReadOnlyList of PRODUCTS with specified filters
        /// </summary>
        /// <param name="productParams">Body of few parameters used to filter, sort or to limit list of PRODUCTS</param>
        /// <returns>IReadOnlyList of PRODUCTS with executed actions of filtering, sorting, limiting the List</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(productParams);

            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.GetListWithSpecAsync(spec);

            if (products == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

            var productsToReturn = new Pagination<ProductToReturnDTO>(
                productParams.PageIndex, productParams.PageSize, totalItems, data);

            return Ok(productsToReturn);
        }

        /// <summary>
        /// Action used to return ReadOnlyList of PRODUCT BRANDS
        /// </summary>
        /// <returns>IReadOnlyList of PRODUCT BRANDS</returns>
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandsRepo.GetAllAsync();

            return Ok(brands);
        }

        /// <summary>
        /// Action used to return ReadOnlyList of PRODUCT TYPES
        /// </summary>
        /// <returns>IReadOnlyList of PRODUCT TYPES</returns>
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            var types = await _productTypesRepo.GetAllAsync();

            return Ok(types);
        }
        #endregion Get Actions End
    }
}