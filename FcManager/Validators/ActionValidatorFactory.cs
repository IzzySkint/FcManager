using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcManager.Data;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FcManager.Controllers;

namespace FcManager.Validators
{
    public class ActionValidatorFactory : IActionValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        public ActionValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task<IActionValidator<T>> CreateAsync<T>(string action)
        {
            IActionValidator<T> validator = null;

            if (typeof(T) == typeof(IEnumerable<PlayerModel>))
            {
                switch (action)
                {
                    case Actions.CreatePlayers:
                        validator = (IActionValidator<T>)await Task.Run(() => new CreatePlayersValidator(
                            _serviceProvider.GetService(typeof(ILogger<CreatePlayersValidator>)) as ILogger<CreatePlayersValidator>));
                        break;
                    case Actions.AddToTeams:
                        validator = (IActionValidator<T>)await Task.Run(() => new AddToTeamsValidator(
                            _serviceProvider.GetService(typeof(ILogger<AddToTeamsValidator>)) as ILogger<AddToTeamsValidator>));
                        break;
                    case Actions.TransferPlayers:
                        validator = (IActionValidator<T>)await Task.Run(() => new TransferPlayersValidator(
                            _serviceProvider.GetService(typeof(ILogger<TransferPlayersValidator>)) as ILogger<TransferPlayersValidator>));
                        break;
                }
            }
            else if (typeof(T) == typeof(PlayerModel))
            {
                switch (action)
                {
                    case Actions.GetPlayer:
                        validator = (IActionValidator<T>)await Task.Run(() => new GetPlayerValidator(
                            _serviceProvider.GetService(typeof(ILogger<GetPlayerValidator>)) as ILogger<GetPlayerValidator>));
                        break;
                }
            }
            else if ((typeof(T) == typeof(IEnumerable<TeamModel>)))
            {
                switch (action)
                {
                    case Actions.CreateTeams:
                        validator = (IActionValidator<T>)await Task.Run(() => new CreateTeamsValidator(
                            _serviceProvider.GetService(typeof(ILogger<CreateTeamsValidator>)) as ILogger<CreateTeamsValidator>));
                        break;
                }
                
            }
            else if (typeof(T) == typeof(TeamModel))
            {
                switch (action)
                {
                    case Actions.GetTeam:
                        validator = (IActionValidator<T>)await Task.Run(() => new GetTeamValidator(
                            _serviceProvider.GetService(typeof(ILogger<GetTeamValidator>)) as ILogger<GetTeamValidator>));
                        break;
                    case Actions.LinkStadium:
                        validator = (IActionValidator<T>)await Task.Run(() => new LinkStadiumValidator(
                            _serviceProvider.GetService(typeof(ILogger<LinkStadiumValidator>)) as ILogger<LinkStadiumValidator>));
                        break;
                }
            }

            return validator;
        }
    }
}