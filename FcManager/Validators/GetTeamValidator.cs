using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class GetTeamValidator : IActionValidator<TeamModel>
    {
        private readonly ILogger _logger;

        public GetTeamValidator(ILogger logger)
        {
            _logger = logger;
        }
        
        public async Task<ValidatorResult> ValidateAsync(TeamModel model)
        {
            var validationResult = await Task.Run(() =>
            {
                List<string> errors = new List<string>();

                if (model == null)
                {
                    errors.Add("Missing team");
                }
                else if (model.Id > 0)
                {
                    return new ValidatorResult(true, Actions.GetTeam, Array.Empty<string>());
                }
                else if (!string.IsNullOrEmpty(model.Name))
                {
                    return new ValidatorResult(true, Actions.GetTeam, Array.Empty<string>());
                }
                else if (!string.IsNullOrEmpty(model.Stadium))
                {
                    return new ValidatorResult(true, Actions.GetTeam, Array.Empty<string>());
                }
                
                return new ValidatorResult(false, Actions.GetTeam,
                    new string[] { "Missing query field, either Id, Name or Stadium" });
            });

            return validationResult;
        }
    }
}