using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FcManager.Services
{
    public interface IRepository<T, U>
    {
        Task<RepositoryResult<T>> CreateAsync(T model);
        Task<RepositoryResult<IEnumerable<T>>> RetrieveAsync(Func<U, bool> predicate);
        Task<RepositoryResult<T>> UpdateAsync(T model);
        Task<RepositoryResult<T>> DeleteAsync(T model);
    }
}