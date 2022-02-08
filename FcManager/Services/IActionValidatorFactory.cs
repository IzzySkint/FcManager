using System.Threading.Tasks;

namespace FcManager.Services
{
    public interface IActionValidatorFactory
    {
        Task<IActionValidator<T>> CreateAsync<T>(string action);
    }
}