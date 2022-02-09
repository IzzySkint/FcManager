using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class AddToTeamsValidator : IActionValidator<IEnumerable<PlayerModel>>
    {
        private readonly ILogger _logger;
        
        public AddToTeamsValidator(ILogger logger)
        {
            _logger = logger;
        }
        
        public async Task<ValidatorResult> ValidateAsync(IEnumerable<PlayerModel> model)
        {
            throw new System.NotImplementedException();
        }
    }
}