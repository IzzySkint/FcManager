using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class GetTeamValidator : IActionValidator<TeamModel>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILogger _logger;

        public GetTeamValidator(IRepositoryFactory repositoryFactory, ILogger logger)
        {
            _repositoryFactory = repositoryFactory;
            _logger = logger;
        }
        
        public async Task<ValidatorResult> ValidateAsync(TeamModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}