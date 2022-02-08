using System;
using System.Threading.Tasks;
using FcManager.Data;
using FcManager.Models;
using FcManager.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FcManager.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly FcManagerDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        
        public RepositoryFactory(FcManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }
        
        public async Task<IRepository<T,U>> CreateAsync<T, U>()
        {
            IRepository<T, U> repository = null;
            
            if ((typeof(T) == typeof(PlayerModel)) && (typeof(U) == typeof(Player)))
            {
                repository = (IRepository<T, U>) await Task.Run(() => new PlayerRepository(_dbContext, _serviceProvider.GetService<ILogger<PlayerRepository>>()));
            }
            else if ((typeof(T) == typeof(TeamModel)) && (typeof(U) == typeof(Team)))
            {
                repository = (IRepository<T, U>) await Task.Run(() => new TeamRepository(_dbContext, _serviceProvider.GetService<ILogger<TeamRepository>>()));
            }

            return repository;
        }
    }
}