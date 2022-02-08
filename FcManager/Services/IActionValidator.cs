using System.Threading.Tasks;

namespace FcManager.Services
{
    public interface IActionValidator<in T>
    {
        Task<ValidatorResult> ValidateAsync(T model);
    }
}