using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class CreateTeamsValidator : IActionValidator<IEnumerable<TeamModel>>
    {
        private readonly ILogger _logger;
        
        public CreateTeamsValidator(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<ValidatorResult> ValidateAsync(IEnumerable<TeamModel> models)
        {
            var validatorResult = await Task.Run(() =>
            {
                List<string> errors = new List<string>();

                if ((models == null) || (!models.Any()))
                {
                    errors.Add("No teams to add.");
                }
                else
                {
                    foreach (var model in models)
                    {
                        if (string.IsNullOrEmpty(model.Name))
                        {
                            errors.Add("Missing or empty Name field");
                        }
                        else if (string.IsNullOrEmpty(model.CoachName))
                        {
                            errors.Add("Missing or empty CoachName field");
                        }
                        else if (string.IsNullOrEmpty(model.ManagerName))
                        {
                            errors.Add("Missing or empty ManagerName field");
                        }
                        else if (string.IsNullOrEmpty(model.Stadium))
                        {
                            errors.Add("Missing or empty Stadium field");
                        }
                    }
                }

                return new ValidatorResult(errors.Count == 0, Actions.CreateTeams, errors.ToArray());
            });

            return validatorResult;
        }
    }
}