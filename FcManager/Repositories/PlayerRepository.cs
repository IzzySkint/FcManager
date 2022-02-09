using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcManager.Data;
using FcManager.Models;
using FcManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FcManager.Repositories
{
    public class PlayerRepository : IRepository<PlayerModel, Player>
    {
        private readonly ILogger _logger;
        private readonly FcManagerDbContext _dbContext;
        public PlayerRepository(FcManagerDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task<RepositoryResult<PlayerModel>> CreateAsync(PlayerModel model)
        {
            List<string> errors = new List<string>();
            
            Player player = new Player
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                NickName = model.NickName,
                DateOfBirth = model.DateOfBirth.Value,
                Height = model.Height.Value,
                Weight = model.Weight.Value
            };

            var position = _dbContext.Positions.FirstOrDefault(p => p.Name == model.Position);
            var team = _dbContext.Teams.FirstOrDefault(t => t.Name == model.Team);

            if (position == null)
            {
                errors.Add(RepositoryErrors.InvalidPositionName);
            }
            else
            {
                player.PositionId = position.PositionId;
            }
            
            if (team == null)
            {
                errors.Add(RepositoryErrors.InvalidTeamName);
            }
            else
            {
                player.TeamId = team.TeamId;
            }

            if (errors.Count == 0)
            {
                try
                {
                    _dbContext.Players.Add(player);
                    await _dbContext.SaveChangesAsync();
                    model.Id = player.PlayerId;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, RepositoryErrors.ErrorCreatingPlayer);
                    errors.Add(RepositoryErrors.ErrorCreatingPlayer);
                }
            }

            return new RepositoryResult<PlayerModel>(errors.Count == 0, model, errors.ToArray());
        }

        public async Task<RepositoryResult<IEnumerable<PlayerModel>>> RetrieveAsync(Func<Player, bool> predicate)
        {
            IEnumerable<PlayerModel> players = null;
            List<string> errors = new List<string>();

            try
            {
                players = await Task.Run(() =>
                {
                    return _dbContext.Players.Include(p => p.Team)
                        .Include(p => p.Position)
                        .Where(predicate).Select(p => new PlayerModel
                    {
                        Id = p.PlayerId,
                        FirstName = p.FirstName,
                        MiddleName = p.MiddleName,
                        LastName = p.LastName,
                        NickName = p.NickName,
                        DateOfBirth = p.DateOfBirth,
                        Height = p.Height,
                        Weight = p.Weight,
                        Position = p.Position.Name,
                        Team = p.Team.Name
                    }).ToList();
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, RepositoryErrors.ErrorRetrievingPlayers);
                errors.Add(RepositoryErrors.ErrorRetrievingPlayers);
            }

            return new RepositoryResult<IEnumerable<PlayerModel>>(errors.Count == 0, players, errors.ToArray());
        }

        public async Task<RepositoryResult<PlayerModel>> UpdateAsync(PlayerModel model)
        {
            List<string> errors = new List<string>();
            var player = _dbContext.Players.Include(p => p.Position).FirstOrDefault(p => p.PlayerId == model.Id);

            if (player == null)
            {
                errors.Add(RepositoryErrors.InvalidPlayerId);
            }

            Position position = null;
            
            if (model.Position != null)
            {
                position = _dbContext.Positions.FirstOrDefault(p => p.Name == model.Position);

                if (position == null)
                {
                    errors.Add(RepositoryErrors.InvalidPositionName);
                }
            }
            
            var team = _dbContext.Teams.FirstOrDefault(t => t.Name == model.Team);

            if (team == null)
            {
                errors.Add(RepositoryErrors.InvalidTeamName);
            }

            if (errors.Count == 0)
            {
                try
                {
                    player.FirstName = model.FirstName ?? player.FirstName;
                    player.MiddleName = model.MiddleName ?? player.MiddleName;
                    player.LastName = model.LastName ?? player.LastName;
                    player.NickName = model.NickName ?? player.NickName;
                    player.DateOfBirth = model.DateOfBirth ?? player.DateOfBirth;
                    player.Height = model.Height ?? player.Height;
                    player.Weight = model.Weight ?? player.Weight;
                    player.PositionId = (position != null) ? position.PositionId : player.Position.PositionId;
                    player.TeamId = team.TeamId;
                    
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, RepositoryErrors.ErrorUpdatingPlayer);
                    errors.Add(RepositoryErrors.ErrorUpdatingPlayer);
                }
            }

            return new RepositoryResult<PlayerModel>(errors.Count == 0, model, errors.ToArray());
        }

        public async Task<RepositoryResult<PlayerModel>> DeleteAsync(PlayerModel model)
        {
            List<string> errors = new List<string>();
            var player = _dbContext.Players.FirstOrDefault(p => p.PlayerId == model.Id);

            if (player == null)
            {
                errors.Add(RepositoryErrors.InvalidPlayerId);
            }

            if (errors.Count == 0)
            {
                _dbContext.Remove(player);

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, RepositoryErrors.ErrorDeletingPlayer);
                    errors.Add(RepositoryErrors.ErrorDeletingPlayer);
                }
            }

            return new RepositoryResult<PlayerModel>(errors.Count == 0, model, errors.ToArray());
        }
    }
}