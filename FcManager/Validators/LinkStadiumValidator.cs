using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            var validationResult = await Task.Run(() =>
            {
                List<string> errors = new List<string>();

                if (model == null)
                {
                    errors.Add("Missing team");
                }
                else if ((model.Id > 0) && (!string.IsNullOrEmpty(model.Stadium)))
                {
                    return new ValidatorResult(true, Actions.GetTeam, Array.Empty<string>());
                }

                return new ValidatorResult(false, Actions.LinkStadium,
                    new string[] { "Missing fields, Id and Stadium" });
            });

            return validationResult;
        }
    }
}