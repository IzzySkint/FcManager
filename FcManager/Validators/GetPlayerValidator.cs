using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class GetPlayerValidator : IActionValidator<PlayerModel>
    {
        private readonly ILogger _logger;
        
        public GetPlayerValidator(ILogger logger)
        {
            _logger = logger;
        }
        
        public async Task<ValidatorResult> ValidateAsync(PlayerModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}