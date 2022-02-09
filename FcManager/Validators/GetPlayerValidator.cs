using System;
using System.Collections.Generic;
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
            var validationResult = await Task.Run(() =>
            {
                List<string> errors = new List<string>();

                if (model == null)
                {
                    errors.Add("Missing player");
                }
                else if (model.Id > 0)
                {
                    return new ValidatorResult(true, Actions.GetPlayer, Array.Empty<string>());
                }
                else if (!string.IsNullOrEmpty(model.FirstName) && !string.IsNullOrEmpty(model.LastName))
                {
                    return new ValidatorResult(true, Actions.GetPlayer, Array.Empty<string>());
                }
                else if (!string.IsNullOrEmpty(model.Team))
                {
                    return new ValidatorResult(true, Actions.GetPlayer, Array.Empty<string>());
                }
                
                return new ValidatorResult(false, Actions.GetPlayer,
                    new string[] { "Missing query field, either Id, FirstName and LastName or Team" });
            });

            return validationResult;
        }
    }
}