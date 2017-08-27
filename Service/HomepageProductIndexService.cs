using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Service
{
    public class HomepageProductIndexService : IHomepageProductIndexService
    {
        private readonly IProductService _productService;
        private readonly IRepository<Product> _productRepository;
        private readonly Random _random;
        private readonly HomepageProductIndexSettings _productIndexSettings;

        public HomepageProductIndexService(IProductService productService, IRepository<Product> productRepository, HomepageProductIndexSettings productIndexSettings)
        {
            this._productService = productService;
            this._productRepository = productRepository;
            this._productIndexSettings = productIndexSettings;
            this._random = new Random();
        }

        public void Index()
        {
            ExcludeFromHome(_productService.GetAllProductsDisplayedOnHomePage());
            IncludeInHome(GetRandomProducts(GetMaxId));
        }

        private void ExcludeFromHome(IList<Product> products)
        {
            foreach (var product in products)
                product.ShowOnHomePage = false;
            _productService.UpdateProducts(products);
        }

        private void IncludeInHome(IList<Product> products)
        {
            int i = 0;
            foreach (var product in products)
            {
                product.ShowOnHomePage = true;
                product.DisplayOrder = i++;
            }
            _productService.UpdateProducts(products);
        }

        private int GetMaxId => _productRepository.Table.Max(p => p.Id);

        private int TotalEligibleProducts => _productRepository.Table.Count(p => !p.Deleted && p.Published);

        private int QtdProductsInHome => Math.Min(_productIndexSettings.QtdProductsInHome, TotalEligibleProducts);

        private bool IsEligible(Product product) => (!product.Deleted) && (product.Published);

        private IList<Product> GetRandomProducts(int maxId)
        {
            IList<Product> products = new List<Product>();
            while(products.Count() < QtdProductsInHome)
            {
                var product = _productService.GetProductById(_random.Next(maxId + 1));
                if ((IsEligible(product)) && (!products.Contains(product)))
                    products.Add(product);
            }
            return products;
        }
    }
}
