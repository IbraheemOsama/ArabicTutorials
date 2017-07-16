using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArabicTutorials.Data.Models;

namespace ArabicTutorials.Data.Infrastructure
{
    public class MongoRepository<TDocument> : IRepository<TDocument> where TDocument : ModelBase
    {
        private readonly IMongoHelper<TDocument> _mongoHelper;
        private readonly string _collectionName;

        public MongoRepository(IMongoHelper<TDocument> mongoHelper, string collectionName)
        {
            _mongoHelper = mongoHelper;
            _collectionName = collectionName;
        }

        public MongoRepository(IMongoHelper<TDocument> mongoHelper) :this(mongoHelper, null) {}

        public Task<TDocument> GetAsync(string id)
        {
            return _mongoHelper.GetAsync(id, _collectionName);
        }

        public Task<IList<TDocument>> GetAllAsync()
        {
            return _mongoHelper.GetAllAsync(_collectionName);
        }

        public Task<IList<TDocument>> FindByAsync(Expression<Func<TDocument, bool>> query, int? page = null, int? count = null)
        {
            return _mongoHelper.FindByAsync(query, _collectionName, page, count);
        }

        public Task<long> CountAsync(Expression<Func<TDocument, bool>> query = null)
        {
            return _mongoHelper.CountAsync(query, _collectionName);
        }

        public Task<TDocument> SaveAsync(TDocument obj)
        {
            return _mongoHelper.SaveAsync(obj, _collectionName);
        }

        public Task Remove(string id)
        {
            return _mongoHelper.Remove(id, _collectionName);
        }

        public Task RemoveAll()
        {
            return _mongoHelper.RemoveAll(_collectionName);
        }
    }
}
