using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using FcManager.Data;
using FcManager.Models;
using FcManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class CreatePlayersValidator : IActionValidator<IEnumerable<PlayerModel>>
    {
        private readonly ILogger _logger;

        public CreatePlayersValidator(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<ValidatorResult> ValidateAsync(IEnumerable<PlayerModel> models)
        {
            List<string> errors = new List<string>();
            
            if ((models == null) || (!models.Any()))
            {
                errors.Add("No players to add.");
            }
            else
            {
                foreach (var model in models)
                {
                    if (string.IsNullOrEmpty(model.FirstName))
                    {
                        errors.Add("Missing or empty FirstName field");
                    }
                    else if (string.IsNullOrEmpty(model.LastName))
                    {
                        errors.Add("Missing or empty LastName field");
                    }
                    else if (model.Height <= 0)
                    {
                        errors.Add("Height must be a value > 0");
                    }
                    else if (model.Weight <= 0)
                    {
                        errors.Add("Weight must be a value > 0");
                    }
                    else if (model.DateOfBirth >= DateTime.Now)
                    {
                        errors.Add("Invalid DateOfBirth field");
                    }
                    else if (string.IsNullOrEmpty(model.Position))
                    {
                        errors.Add("Missing or empty Position field.");
                    }
                    else if (string.IsNullOrEmpty(model.Team))
                    {
                        errors.Add("Missing or empty Team field.");
                    }
                }
            }

            return new ValidatorResult(errors.Count == 0, "CreatePlayers", errors.ToArray());
        }
    }
}