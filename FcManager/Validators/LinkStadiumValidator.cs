using System.Threading.Tasks;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;

namespace FcManager.Validators
{
    public class LinkStadiumValidator : IActionValidator<TeamModel>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILogger _logger;
        
        public LinkStadiumValidator(IRepositoryFactory repositoryFactory, ILogger logger)
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