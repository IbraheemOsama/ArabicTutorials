using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArabicTutorials.Data.Models;
using MongoDB.Driver;

namespace ArabicTutorials.Data.Infrastructure
{
    public class MongoHelper<TDocument> :  IMongoHelper<TDocument> where TDocument : MongoDocument
    {
        private readonly IAppConfig _appConfig;
        private readonly MongoClient _mongoClient;

        public MongoHelper(IMongoConnectionFactory mongoConnectionFactory, IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _mongoClient = mongoConnectionFactory.GetClient();
        }

        public Task<long> CountAsync(Expression<Func<TDocument, bool>> query = null, string collectionName = null)
        {
            var filters = new List<FilterDefinition<TDocument>>
            {
                query != null ? Builders<TDocument>.Filter.Where(query) : Builders<TDocument>.Filter.Where(f => true)
            };

            var combinedFilter = Builders<TDocument>.Filter.And(filters);

            return GetCollection(collectionName).CountAsync(combinedFilter);
        }

        public Task<IList<TDocument>> GetAllAsync(string collectionName = null)
        {
            return GetCollection(collectionName).FindAsync(i => true)
                    .ContinueWith(r => (IList<TDocument>) r.Result.ToList());
        }

        public Task<TDocument> GetAsync(string id, string collectionName = null)
        {
            var filter = Builders<TDocument>.Filter.Eq(s => s.Id, id);
            var result = GetCollection(collectionName)
                            .FindAsync(filter)
                            .ContinueWith(r => r.Result.SingleOrDefault());

            return result;
        }

        public Task<TDocument> GetByAsync(Expression<Func<TDocument, bool>> query, string collectionName = null)
        {
            return GetCollection(collectionName)
                            .FindAsync(query)
                            .ContinueWith(r => r.Result.SingleOrDefault());

        }

        public Task<IList<TDocument>> FindByAsync(Expression<Func<TDocument, bool>> query, string collectionName = null, int? page = null, int? count = null)
        {
            var filters = new List<FilterDefinition<TDocument>> {Builders<TDocument>.Filter.Where(query)};

            var combinedFilter = Builders<TDocument>.Filter.And(filters);

            FindOptions<TDocument, TDocument> option = null;

            if (page.HasValue && count.HasValue)
            {
                option = new FindOptions<TDocument, TDocument> {Skip = count.Value * page.Value, Limit = count.Value};
            }

            return
                GetCollection(collectionName)
                    .FindAsync(combinedFilter, option)
                    .ContinueWith(r => (IList<TDocument>) r.Result.ToList());
        }

        public Task<TDocument> Update(string collectionName,
            Expression<Func<TDocument, bool>> query, UpdateDefinition<TDocument> updateDefinition)
        {
            return GetCollection(collectionName).FindOneAndUpdateAsync(query, updateDefinition,
                new FindOneAndUpdateOptions<TDocument> {IsUpsert = true,});
        }

        public async Task<TDocument> SaveAsync(TDocument obj, string collectionName = null)
        {
            var filter = Builders<TDocument>.Filter.Eq(s => s.Id, obj.Id);
            await GetCollection(collectionName)
                .ReplaceOneAsync(filter, obj, new UpdateOptions { IsUpsert = true });
            return obj;
        }

        private IMongoCollection<TDocument> GetCollection(string collectionName)
        {
            collectionName = string.IsNullOrEmpty(collectionName) ? typeof(TDocument).Name : collectionName;
            return _mongoClient.GetDatabase(_appConfig.DbName()).GetCollection<TDocument>(collectionName);
        }

        public Task Remove(string id, string collectionName = null)
        {
            var filter = Builders<TDocument>.Filter.Eq(s => s.Id, id);
            return GetCollection(collectionName).DeleteOneAsync(filter);
        }

        public Task RemoveAll(string collectionName = null)
        {
            return GetCollection(collectionName).DeleteManyAsync(s => true);
        }
    }
}