using System.Threading.Tasks;

namespace FcManager.Services
{
    public interface IRepositoryFactory
    {
        Task<IRepository<T, U>> CreateAsync<T, U>();
    }
}