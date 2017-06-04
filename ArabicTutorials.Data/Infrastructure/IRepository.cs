using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArabicTutorials.Data.Infrastructure
{
    public interface IRepository<TDocument>
    {
        Task<TDocument> GetAsync(string id);
        Task<IList<TDocument>> GetAllAsync();
        Task<IList<TDocument>> FindByAsync(Expression<Func<TDocument, bool>> query, int? page = null, int? count = null);
        Task<long> CountAsync(Expression<Func<TDocument, bool>> query = null);
        Task<TDocument> SaveAsync(TDocument document);
        Task Remove(string id);

        Task RemoveAll();
    }
}