using System.Collections.Generic;
using System.Threading.Tasks;
using Laroa.Domain;
using Laroa.Infrastructure;

namespace Laroa.Domain.Interfaces.Services
{
    public class ContentBasedRecommendationService : IContentBasedRecommendationService
    {
        private readonly IProductService _productService;
        private readonly TFIDFExtractor _tfidfExtractor;

        public ContentBasedRecommendationService(IProductService productService, TFIDFExtractor tfidfExtractor)
        {
            _productService = productService;
            _tfidfExtractor = tfidfExtractor;
        }

        public async Task<List<Product>> GetContentBasedRecommendationsAsync(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);

            if (product == null)
                return null;

            // Get all products
            var allProducts = await _productService.GetAllAsync();

            // Extract TF-IDF features for the target product name
            var nameFeatures = _tfidfExtractor.ExtractFeatures(product.Name, allProducts.Select(p => p.Name));

            // Calculate similarity and recommend top N products based on name
            var nameBasedRecommendations = allProducts
                .Where(p => p.Id != productId)
                .Select(p => new
                {
                    Product = p,
                    Similarity = CalculateCosineSimilarity(nameFeatures, _tfidfExtractor.ExtractFeatures(p.Name, allProducts.Select(d => d.Name)))
                })
                .OrderByDescending(x => x.Similarity)
                .Take(5)
                .Select(x => x.Product);

            // Extract TF-IDF features for the target product category
            var categoryFeatures = _tfidfExtractor.ExtractFeatures(product.Category?.Name ?? "", allProducts.Select(p => p.Category?.Name ?? ""));

            // Calculate similarity and recommend top N products based on category
            var categoryBasedRecommendations = allProducts
                .Where(p => p.Id != productId)
                .Select(p => new
                {
                    Product = p,
                    Similarity = CalculateCosineSimilarity(categoryFeatures, _tfidfExtractor.ExtractFeatures(p.Category?.Name ?? "", allProducts.Select(d => d.Category?.Name ?? "")))
                })
                .OrderByDescending(x => x.Similarity)
                .Take(5)
                .Select(x => x.Product);

            // Merge or prioritize recommendations based on your business logic
            var recommendedProducts = nameBasedRecommendations.Concat(categoryBasedRecommendations).Distinct().ToList();

            return recommendedProducts;
        }


        private double CalculateCosineSimilarity(Dictionary<string, double> vector1, Dictionary<string, double> vector2)
        {
            // Calculate cosine similarity
            var dotProduct = 0.0;
            var magnitude1 = 0.0;
            var magnitude2 = 0.0;

            foreach (var key in vector1.Keys)
            {
                if (vector2.ContainsKey(key))
                {
                    dotProduct += vector1[key] * vector2[key];
                }

                magnitude1 += Math.Pow(vector1[key], 2);
            }

            foreach (var value in vector2.Values)
            {
                magnitude2 += Math.Pow(value, 2);
            }

            if (magnitude1 == 0 || magnitude2 == 0)
            {
                return 0.0;
            }

            return dotProduct / (Math.Sqrt(magnitude1) * Math.Sqrt(magnitude2));
        }
    }
}
