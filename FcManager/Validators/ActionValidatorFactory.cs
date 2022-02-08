using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcManager.Data;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FcManager.Validators
{
    public class ActionValidatorFactory : IActionValidatorFactory
    {
        private IRepositoryFactory _repositoryFactory;
        private IServiceProvider _serviceProvider;
        
        public ActionValidatorFactory(IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider)
        {
            _repositoryFactory = repositoryFactory;
            _serviceProvider = serviceProvider;
        }
        
        public async Task<IActionValidator<T>> CreateAsync<T>(string action)
        {
            IActionValidator<T> validator = null;

            if (typeof(T) == typeof(IEnumerable<PlayerModel>))
            {
                switch (action)
                {
                    case "CreatePlayers":
                        validator = (IActionValidator<T>)await Task.Run(() => new CreatePlayersValidator(
                            _repositoryFactory,
                            _serviceProvider.GetService<ILogger<CreatePlayersValidator>>()));
                        break;
                    case "AddToTeams":
                        validator = (IActionValidator<T>)await Task.Run(() => new AddToTeamsValidator(
                            _repositoryFactory,
                            _serviceProvider.GetService<ILogger<CreatePlayersValidator>>()));
                        break;
                    case "TransferPlayers":
                        validator = (IActionValidator<T>)await Task.Run(() => new TransferPlayersValidator(
                            _repositoryFactory,
                            _serviceProvider.GetService<ILogger<CreatePlayersValidator>>()));
                        break;
                }
            }
            else if (typeof(T) == typeof(PlayerModel))
            {
                switch (action)
                {
                    case "GetPlayer":
                        validator = (IActionValidator<T>)await Task.Run(() => new GetPlayerValidator(
                            _repositoryFactory,
                            _serviceProvider.GetService<ILogger<CreatePlayersValidator>>()));
                        break;
                }
            }
            else if ((typeof(T) == typeof(IEnumerable<TeamModel>)))
            {
                switch (action)
                {
                    case "CreateTeams":
                        validator = (IActionValidator<T>)await Task.Run(() => new CreateTeamsValidator(
                            _repositoryFactory,
                            _serviceProvider.GetService<ILogger<CreatePlayersValidator>>()));
                        break;
                }
                
            }
            else if (typeof(T) == typeof(TeamModel))
            {
                switch (action)
                {
                    case "GetTeam":
                        validator = (IActionValidator<T>)await Task.Run(() => new GetTeamValidator(
                            _repositoryFactory,
                            _serviceProvider.GetService<ILogger<CreatePlayersValidator>>()));
                        break;
                    case "LinkStadium":
                        validator = (IActionValidator<T>)await Task.Run(() => new LinkStadiumValidator(
                            _repositoryFactory,
                            _serviceProvider.GetService<ILogger<CreatePlayersValidator>>()));
                        break;
                }
            }

            return validator;
        }
    }
}