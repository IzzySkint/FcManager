using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class CreateTeamsValidator : IActionValidator<IEnumerable<TeamModel>>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILogger _logger;
        
        public CreateTeamsValidator(IRepositoryFactory repositoryFactory, ILogger logger)
        {
            _repositoryFactory = repositoryFactory;
            _logger = logger;
        }

        public async Task<ValidatorResult> ValidateAsync(IEnumerable<TeamModel> model)
        {
            throw new System.NotImplementedException();
        }
    }
}