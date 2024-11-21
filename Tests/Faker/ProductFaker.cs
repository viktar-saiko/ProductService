using Bogus;
using Common.Models;
using MongoDB.Bson;

namespace Tests.Faker
{
    public static class ProductFaker
    {
        /// <summary>
        /// Create the instance of faked Product data
        /// </summary>
        /// <param name="useMongoDb">True - using with MongoDb database. False - using with relative sql database</param>
        /// <returns></returns>
        public static Faker<Product> Create(bool useMongoDb)
        {
            return
                new Faker<Product>()
                .RuleFor(p => p.Id, f => useMongoDb ? ObjectId.GenerateNewId().ToString() : f.Random.Guid().ToString())
                .RuleFor(p => p.Name, f => f.Hacker.Phrase())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(0.1m, 2000), 2))
                .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
                .RuleFor(p => p.Categories, f => f.Random.WordsArray(1, 4).ToList())
                .RuleFor(p => p.Tags, f => f.Random.WordsArray(0, 5).ToList());
        }
    }
}
