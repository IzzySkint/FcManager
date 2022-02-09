using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcManager.Data;
using System.Linq;
using FcManager.Models;
using FcManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FcManager.Repositories
{
    public class TeamRepository : IRepository<TeamModel, Team>
    {
        private readonly ILogger _logger;
        private readonly FcManagerDbContext _dbContext;
        public TeamRepository(FcManagerDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task<RepositoryResult<TeamModel>> CreateAsync(TeamModel model)
        {
            List<string> errors = new List<string>();
            
            Team team = new Team
            {
                Name = model.Name,
                ManagerName = model.ManagerName,
                CoachName = model.CoachName,
                AssistantCoachName = model.AssistantCoachName
            };

            var stadium = _dbContext.Stadiums.FirstOrDefault(s => s.Name == model.Stadium);
            
            if (stadium == null)
            {
                errors.Add(RepositoryErrors.InvalidStadiumName);
            }
            else
            {
                team.StadiumId = stadium.StadiumId;
            }

            if (errors.Count == 0)
            {
                try
                {
                    _dbContext.Teams.Add(team);
                    await _dbContext.SaveChangesAsync();
                    model.Id = team.TeamId;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, RepositoryErrors.ErrorCreatingTeam);
                    errors.Add(RepositoryErrors.ErrorCreatingTeam);
                }
            }

            return new RepositoryResult<TeamModel>(errors.Count == 0, model, errors.ToArray());
        }

        public async Task<RepositoryResult<IEnumerable<TeamModel>>> RetrieveAsync(Func<Team, bool> predicate)
        {
            IEnumerable<TeamModel> teams = null;
            List<string> errors = new List<string>();

            try
            {
                teams = await Task.Run(() =>
                {
                    return _dbContext.Teams.Include(t => t.Stadium).Where(predicate).Select(t => new TeamModel
                    {
                        Id = t.TeamId,
                        Name = t.Name,
                        ManagerName = t.ManagerName,
                        CoachName = t.CoachName,
                        AssistantCoachName = t.AssistantCoachName,
                        Stadium = t.Stadium.Name
                    }).ToList();
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, RepositoryErrors.ErrorRetrievingTeams);
                errors.Add(RepositoryErrors.ErrorRetrievingTeams);
            }

            return new RepositoryResult<IEnumerable<TeamModel>>(errors.Count == 0, teams, errors.ToArray());
        }

        public async Task<RepositoryResult<TeamModel>> UpdateAsync(TeamModel model)
        {
            List<string> errors = new List<string>();
            var team = _dbContext.Teams.FirstOrDefault(t => t.TeamId == model.Id);

            if (team == null)
            {
                errors.Add(RepositoryErrors.InvalidTeamId);
            }

            var stadium = _dbContext.Stadiums.FirstOrDefault(s => s.Name == model.Stadium);

            if (stadium == null)
            {
                errors.Add(RepositoryErrors.InvalidStadiumName);
            }

            if (errors.Count == 0)
            {
                try
                {
                    team.Name = model.Name ?? team.Name;
                    team.ManagerName = model.ManagerName ?? team.ManagerName;
                    team.CoachName = model.CoachName ?? team.CoachName;
                    team.AssistantCoachName = model.AssistantCoachName ?? team.AssistantCoachName;
                    team.StadiumId = stadium.StadiumId;

                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, RepositoryErrors.ErrorUpdatingTeam);
                    errors.Add(RepositoryErrors.ErrorUpdatingTeam);
                }
            }

            return new RepositoryResult<TeamModel>(errors.Count == 0, model, errors.ToArray());
        }

        public async Task<RepositoryResult<TeamModel>> DeleteAsync(TeamModel model)
        {
            List<string> errors = new List<string>();
            var team = _dbContext.Teams.FirstOrDefault(t => t.TeamId == model.Id);

            if (team == null)
            {
                errors.Add(RepositoryErrors.InvalidTeamId);
            }

            if (errors.Count == 0)
            {
                _dbContext.Remove(team);

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, RepositoryErrors.ErrorDeletingTeam);
                    errors.Add(RepositoryErrors.ErrorDeletingTeam);
                }
            }

            return new RepositoryResult<TeamModel>(errors.Count == 0, model, errors.ToArray());
        }
    }
}