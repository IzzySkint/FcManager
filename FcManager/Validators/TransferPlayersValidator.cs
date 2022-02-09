using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class TransferPlayersValidator : IActionValidator<IEnumerable<PlayerModel>>
    {
        private readonly ILogger _logger;
        
        public TransferPlayersValidator(ILogger logger)
        {
            _logger = logger;
        }
        
        public async Task<ValidatorResult> ValidateAsync(IEnumerable<PlayerModel> models)
        {
            var validationResult = await Task.Run(() =>
            {
                List<string> errors = new List<string>();

                if ((models == null) && (!models.Any()))
                {
                    errors.Add("Missing players");
                }
                else
                {
                    foreach (var model in models)
                    {
                        if ((model.Id <= 0) || (string.IsNullOrEmpty(model.Team)))
                        {
                            errors.Add("Either Id or Team is missing");
                        }
                    }
                }

                return new ValidatorResult(errors.Count == 0, Actions.TransferPlayers,
                    errors.ToArray());
            });

            return validationResult;
        }
    }
}