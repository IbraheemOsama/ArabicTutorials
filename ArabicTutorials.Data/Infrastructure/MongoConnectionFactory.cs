using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ArabicTutorials.Data.Infrastructure
{
    public class MongoConnectionFactory : IMongoConnectionFactory
    {
        private readonly MongoUrl _url;
        private  MongoClient _client;

        public MongoConnectionFactory(IAppConfig configurations)
        {
            var connectionString = configurations.MongoConnectionString();
            _url = MongoUrl.Create(connectionString);
        }

        public MongoClient GetClient()
        {
            return _client ?? (_client = new MongoClient(_url));
        }

        static MongoConnectionFactory()
        {
            var conventions = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventions, _ => true);
        }
    }
}
