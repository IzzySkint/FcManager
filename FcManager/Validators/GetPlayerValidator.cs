using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class GetPlayerValidator : IActionValidator<PlayerModel>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILogger _logger;
        
        public GetPlayerValidator(IRepositoryFactory repositoryFactory, ILogger logger)
        {
            _repositoryFactory = repositoryFactory;
            _logger = logger;
        }
        
        public async Task<ValidatorResult> ValidateAsync(PlayerModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}