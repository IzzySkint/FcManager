using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class LinkStadiumValidator : IActionValidator<TeamModel>
    {
        private readonly ILogger _logger;
        
        public LinkStadiumValidator(ILogger logger)
        {
            _logger = logger;
        }
        
        public async Task<ValidatorResult> ValidateAsync(TeamModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}