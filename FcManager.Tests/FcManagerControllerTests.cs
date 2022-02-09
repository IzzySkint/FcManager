using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FcManager.Controllers;
using FcManager.Data;
using FcManager.Models;
using FcManager.Services;
using FcManager.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILogger = Castle.Core.Logging.ILogger;

namespace FcManager.Tests
{
    [TestFixture]
    public class FcManagerControllerTests
    {
        [Test]
        public async Task CreatePlayersTest()
        {
            //Setup
            var player = new PlayerModel
            {
                Id = 0,
                FirstName = "Tom",
                LastName = "MiddleTon", 
                Height = 1.70,
                Position = "Defender",
                Weight = 65,
                Team = "Joburg Swifts",
                DateOfBirth = DateTime.Parse("1970-03-21"),
            };
            var mockRepository = new Mock<IRepository<PlayerModel, Player>>();
            mockRepository.Setup(m => m.CreateAsync(player)).Returns(
                    () => Task.Run(() =>
                    {
                        return new RepositoryResult<PlayerModel>(true, new PlayerModel
                        {
                            Id = 1,
                            FirstName = player.FirstName,
                            MiddleName = player.MiddleName,
                            LastName = player.LastName,
                            NickName = player.NickName,
                            DateOfBirth = player.DateOfBirth,
                            Height = player.Height,
                            Weight = player.Weight,
                            Position = player.Position,
                            Team = player.Team
                        }, Array.Empty<string>());
                    }));
            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(m => m.CreateAsync<PlayerModel, Player>())
                .Returns(() => Task.Run(() => mockRepository.Object));

            var mockLogger = new Mock<ILogger<FcManagerController>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(m => m.GetService(typeof(ILogger<CreatePlayersValidator>)))
                .Returns(() => new Mock<ILogger<CreatePlayersValidator>>().Object);

            var validatorFactory = new ActionValidatorFactory(mockServiceProvider.Object);

            var controller = new FcManagerController(mockRepositoryFactory.Object,
                validatorFactory, mockLogger.Object);

            var players = new List<PlayerModel>();
            players.Add(player);
            
            //Execute
            var result = await controller.CreatePlayers(players);
            var objectResult = result as OkObjectResult;
            var content = objectResult.Value as IEnumerable<RepositoryResult<PlayerModel>>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.NotNull(content);
        }

        [Test]
        public async Task CreateTeamsTest()
        {
            //Setup
            var team = new TeamModel
            {
                Id = 0,
                Name = "XYZ Team",
                ManagerName = "Ted Newman",
                CoachName = "John Smith", 
                AssistantCoachName = null,
                Stadium = "The Big Stadium"
            };
            var mockRepository = new Mock<IRepository<TeamModel, Team>>();
            mockRepository.Setup(m => m.CreateAsync(team)).Returns(
                    () => Task.Run(() =>
                    {
                        return new RepositoryResult<TeamModel>(true, new TeamModel
                        {
                            Id = 1,
                            Name = team.Name,
                            ManagerName = team.ManagerName,
                            CoachName = team.CoachName,
                            AssistantCoachName = team.AssistantCoachName,
                            Stadium = team.Stadium
                        }, Array.Empty<string>());
                    }));
            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(m => m.CreateAsync<TeamModel, Team>())
                .Returns(() => Task.Run(() => mockRepository.Object));

            var mockLogger = new Mock<ILogger<FcManagerController>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(m => m.GetService(typeof(ILogger<CreateTeamsValidator>)))
                .Returns(() => new Mock<ILogger<CreateTeamsValidator>>().Object);

            var validatorFactory = new ActionValidatorFactory(mockServiceProvider.Object);

            var controller = new FcManagerController(mockRepositoryFactory.Object,
                validatorFactory, mockLogger.Object);

            var teams = new List<TeamModel>();
            teams.Add(team);
            
            //Execute
            var result = await controller.CreateTeams(teams);
            var objectResult = result as OkObjectResult;
            var content = objectResult.Value as IEnumerable<RepositoryResult<TeamModel>>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.NotNull(content);
        }

        [Test]
        public async Task GetPlayerTest()
        {
            //Setup
            var player = new PlayerModel
            {
                Id = 1,
            };

            var mockRepository = new Mock<IRepository<PlayerModel, Player>>();
            mockRepository.Setup(m => m.RetrieveAsync(It.IsAny<Func<Player, bool>>())).Returns(
                    () => Task.Run(() =>
                    {
                        List<PlayerModel> players = new List<PlayerModel>();
                        
                        players.Add(new PlayerModel
                        {
                            Id = 0,
                            FirstName = "Tom",
                            LastName = "MiddleTon",
                            MiddleName = null,
                            NickName = null,
                            Height = 1.70,
                            Position = "Defender",
                            Weight = 65,
                            Team = "Joburg Swifts",
                            DateOfBirth = DateTime.Parse("1970-03-21"),
                        });
                        
                        return new RepositoryResult<IEnumerable<PlayerModel>>(true, players, Array.Empty<string>());
                    }));
            
            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(m => m.CreateAsync<PlayerModel, Player>())
                .Returns(() => Task.Run(() => mockRepository.Object));

            var mockLogger = new Mock<ILogger<FcManagerController>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(m => m.GetService(typeof(ILogger<GetPlayerValidator>)))
                .Returns(() => new Mock<ILogger<GetPlayerValidator>>().Object);

            var validatorFactory = new ActionValidatorFactory(mockServiceProvider.Object);

            var controller = new FcManagerController(mockRepositoryFactory.Object,
                validatorFactory, mockLogger.Object);

            //Execute
            var result = await controller.GetPlayer(player);
            var objectResult = result as OkObjectResult;
            var content = objectResult.Value as RepositoryResult<IEnumerable<PlayerModel>>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.NotNull(content);
        }

        [Test]
        public async Task GetTeamTest()
        {
             //Setup
            var team = new TeamModel
            {
                Id = 1,
            };

            var mockRepository = new Mock<IRepository<TeamModel, Team>>();
            mockRepository.Setup(m => m.RetrieveAsync(It.IsAny<Func<Team, bool>>())).Returns(
                    () => Task.Run(() =>
                    {
                        List<TeamModel> teams = new List<TeamModel>();
                        
                        teams.Add(new TeamModel
                        {
                            Id = 0,
                            Name = "XYZ Team",
                            ManagerName = "Ted Newman",
                            CoachName = "John Smith", 
                            AssistantCoachName = null,
                            Stadium = "The Big Stadium"
                        });
                        
                        return new RepositoryResult<IEnumerable<TeamModel>>(true, teams, Array.Empty<string>());
                    }));
            
            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(m => m.CreateAsync<TeamModel, Team>())
                .Returns(() => Task.Run(() => mockRepository.Object));

            var mockLogger = new Mock<ILogger<FcManagerController>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(m => m.GetService(typeof(ILogger<GetTeamValidator>)))
                .Returns(() => new Mock<ILogger<GetTeamValidator>>().Object);

            var validatorFactory = new ActionValidatorFactory(mockServiceProvider.Object);

            var controller = new FcManagerController(mockRepositoryFactory.Object,
                validatorFactory, mockLogger.Object);

            //Execute
            var result = await controller.GetTeam(team);
            var objectResult = result as OkObjectResult;
            var content = objectResult.Value as RepositoryResult<IEnumerable<TeamModel>>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.NotNull(content);
        }

        [Test]
        public async Task AddToTeamsTest()
        {
            //Setup
            var player = new PlayerModel
            {
                Id = 1,
                Team = "Joburg Swifts"
            };
            var mockRepository = new Mock<IRepository<PlayerModel, Player>>();
            mockRepository.Setup(m => m.UpdateAsync(player)).Returns(
                    () => Task.Run(() =>
                    {
                        return new RepositoryResult<PlayerModel>(true, new PlayerModel
                        {
                            Id = 1,
                            FirstName = "Tom",
                            LastName = "MiddleTon", 
                            MiddleName = null,
                            NickName = null,
                            Height = 1.70,
                            Position = "Defender",
                            Weight = 65,
                            Team = "Joburg Swifts",
                            DateOfBirth = DateTime.Parse("1970-03-21"),
                        }, Array.Empty<string>());
                    }));
            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(m => m.CreateAsync<PlayerModel, Player>())
                .Returns(() => Task.Run(() => mockRepository.Object));

            var mockLogger = new Mock<ILogger<FcManagerController>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(m => m.GetService(typeof(ILogger<AddToTeamsValidator>)))
                .Returns(() => new Mock<ILogger<AddToTeamsValidator>>().Object);

            var validatorFactory = new ActionValidatorFactory(mockServiceProvider.Object);

            var controller = new FcManagerController(mockRepositoryFactory.Object,
                validatorFactory, mockLogger.Object);

            var players = new List<PlayerModel>();
            players.Add(player);
            
            //Execute
            var result = await controller.AddToTeams(players);
            var objectResult = result as OkObjectResult;
            var content = objectResult.Value as IEnumerable<RepositoryResult<PlayerModel>>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.NotNull(content);
        }

        [Test]
        public async Task TransferPlayersTest()
        {
            //Setup
            var player = new PlayerModel
            {
                Id = 1,
                Team = "Joburg Swifts"
            };
            var mockRepository = new Mock<IRepository<PlayerModel, Player>>();
            mockRepository.Setup(m => m.UpdateAsync(player)).Returns(
                    () => Task.Run(() =>
                    {
                        return new RepositoryResult<PlayerModel>(true, new PlayerModel
                        {
                            Id = 1,
                            FirstName = "Tom",
                            LastName = "MiddleTon", 
                            MiddleName = null,
                            NickName = null,
                            Height = 1.70,
                            Position = "Defender",
                            Weight = 65,
                            Team = "Joburg Swifts",
                            DateOfBirth = DateTime.Parse("1970-03-21"),
                        }, Array.Empty<string>());
                    }));
            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(m => m.CreateAsync<PlayerModel, Player>())
                .Returns(() => Task.Run(() => mockRepository.Object));

            var mockLogger = new Mock<ILogger<FcManagerController>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(m => m.GetService(typeof(ILogger<TransferPlayersValidator>)))
                .Returns(() => new Mock<ILogger<TransferPlayersValidator>>().Object);

            var validatorFactory = new ActionValidatorFactory(mockServiceProvider.Object);

            var controller = new FcManagerController(mockRepositoryFactory.Object,
                validatorFactory, mockLogger.Object);

            var players = new List<PlayerModel>();
            players.Add(player);
            
            //Execute
            var result = await controller.AddToTeams(players);
            var objectResult = result as OkObjectResult;
            var content = objectResult.Value as IEnumerable<RepositoryResult<PlayerModel>>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.NotNull(content);
        }

        [Test]
        public async Task LinkStadiumTest()
        {
            //Setup
            var team = new TeamModel
            {
                Id = 1,
                Stadium = "The Big One"
            };
            var mockRepository = new Mock<IRepository<TeamModel, Team>>();
            mockRepository.Setup(m => m.UpdateAsync(team)).Returns(
                    () => Task.Run(() =>
                    {
                        return new RepositoryResult<TeamModel>(true, new TeamModel
                        {
                            Id = 1,
                            Name = "XYZ Team",
                            ManagerName = "Ted Newman",
                            CoachName = "John Smith", 
                            AssistantCoachName = null,
                            Stadium = "The Big One"
                        }, Array.Empty<string>());
                    }));
            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(m => m.CreateAsync<TeamModel, Team>())
                .Returns(() => Task.Run(() => mockRepository.Object));

            var mockLogger = new Mock<ILogger<FcManagerController>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(m => m.GetService(typeof(ILogger<LinkStadiumValidator>)))
                .Returns(() => new Mock<ILogger<LinkStadiumValidator>>().Object);

            var validatorFactory = new ActionValidatorFactory(mockServiceProvider.Object);

            var controller = new FcManagerController(mockRepositoryFactory.Object,
                validatorFactory, mockLogger.Object);

            //Execute
            var result = await controller.LinkStadium(team);
            var objectResult = result as OkObjectResult;
            var content = objectResult.Value as RepositoryResult<TeamModel>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.NotNull(content);
        }
    }
}