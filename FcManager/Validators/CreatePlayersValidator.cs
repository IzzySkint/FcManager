using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class CreatePlayersValidator : IActionValidator<IEnumerable<PlayerModel>>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILogger _logger;
        
        public CreatePlayersValidator(IRepositoryFactory repositoryFactory, ILogger logger)
        {
            _repositoryFactory = repositoryFactory;
            _logger = logger;
        }

        public async Task<ValidatorResult> ValidateAsync(IEnumerable<PlayerModel> model)
        {
            throw new System.NotImplementedException();
        }
    }
}