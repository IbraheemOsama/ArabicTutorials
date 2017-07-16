using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArabicTutorials.Data.Models;

namespace ArabicTutorials.Data.Infrastructure
{
    public interface IMongoHelper<TDocument> where TDocument : ModelBase
    {
        Task<IList<TDocument>> GetAllAsync(string collectionName = null);

        Task<long> CountAsync(Expression<Func<TDocument, bool>> query = null, string collectionName = null);

        Task<TDocument> GetAsync(string id, string collectionName = null);

        Task<TDocument> GetByAsync(Expression<Func<TDocument, bool>> query, string collectionName = null);

        Task<IList<TDocument>> FindByAsync(Expression<Func<TDocument, bool>> query, string collectionName = null, int? page = null, int? count = null);

        Task<TDocument> SaveAsync(TDocument obj, string collectionName = null);

        Task Remove(string id, string collectionName = null);

        Task RemoveAll(string collectionName = null);

    }
}