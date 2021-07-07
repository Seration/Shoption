using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using Shoption.Catalog.Dto;
using Shoption.Catalog.Models;
using Shoption.Catalog.Settings;
using Shoption.Shared.Dto;
using Shoption.Shared.Messages;

namespace Shoption.Catalog.Services
{
    internal class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings, IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dto.Response<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(pro => true).ToListAsync();

            if (!products.Any())
                products = new List<Product>();
            else
            {
                foreach (var item in products)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.Id == item.CategoryId).FirstAsync();
                }
            }

            return Shared.Dto.Response<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
        }

        public async Task<Shared.Dto.Response<ProductDto>> GetByIdAsync(string id)
        {
            var product = await _productCollection.Find<Product>(x => x.Id == id).FirstOrDefaultAsync();

            if (product == null)
                return Shared.Dto.Response<ProductDto>.Fail("Product not found", 404);

            product.Category = await _categoryCollection.Find<Category>(x => x.Id == product.CategoryId).FirstAsync();

            return Shared.Dto.Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }

        public async Task<Shared.Dto.Response<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);

            product.CratedTime = DateTime.Now;
            await _productCollection.InsertOneAsync(product);

            return Shared.Dto.Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), 201);
        }

        public async Task<Shared.Dto.Response<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var product = _mapper.Map<Product>(productUpdateDto);

            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);

            if (result == null)
                return Shared.Dto.Response<NoContent>.Fail("Product Not Found", 404);

            await _publishEndpoint.Publish<ProductNameChangedEvent>(new ProductNameChangedEvent
            {
                ProductId = productUpdateDto.Id,
                UpdatedName = productUpdateDto.Name
            });

            return Shared.Dto.Response<NoContent>.Success(204);
        }

        public async Task<Shared.Dto.Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
                return Shared.Dto.Response<NoContent>.Success(204);
            else
                return Shared.Dto.Response<NoContent>.Fail("Course not found", 404);
        }

    }
}
