using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FcManager.Models;
using FcManager.Services;
using FcManager.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

namespace FcManager.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FcManagerController : ControllerBase
	{
		private readonly IRepositoryFactory _repositoryFactory;
		private readonly IActionValidatorFactory _validatorFactory;
		private readonly ILogger _logger;
		
		public FcManagerController(IRepositoryFactory repositoryFactory, 
			IActionValidatorFactory validatorFactory,
			ILogger<FcManagerController> logger)
		{
			_repositoryFactory = repositoryFactory;
			_validatorFactory = validatorFactory;
			_logger = logger;
		}
		
		[HttpPost]
		[Route("createPlayers")]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> CreatePlayers(IEnumerable<PlayerModel> models)
		{
			var validator = await _validatorFactory.CreateAsync<IEnumerable<PlayerModel>>(Actions.CreatePlayers);
			var validationResult = await validator.ValidateAsync(models);

			if (validationResult.IsValid)
			{
				List<RepositoryResult<PlayerModel>> results = new List<RepositoryResult<PlayerModel>>();
				var repository = await _repositoryFactory.CreateAsync<PlayerModel, Player>();

				foreach (var player in models)
				{
					var result = await repository.CreateAsync(player);
					results.Add(result);
				}

				return Ok(results);
			}
			else
			{
				return BadRequest(validationResult);
			}
		}

		[HttpPost]
		[Route("createTeams")]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> CreateTeams(IEnumerable<TeamModel> models)
		{
			var validator = await _validatorFactory.CreateAsync<IEnumerable<TeamModel>>(Actions.CreateTeams);
			var validationResult = await validator.ValidateAsync(models);

			if (validationResult.IsValid)
			{
				List<RepositoryResult<TeamModel>> results = new List<RepositoryResult<TeamModel>>();
				var repository = await _repositoryFactory.CreateAsync<TeamModel, Team>();

				foreach (var team in models)
				{
					var result = await repository.CreateAsync(team);
					results.Add(result);
				}

				return Ok(results);
			}
			else
			{
				return BadRequest(validationResult);
			}
		}

		[HttpGet]
		[Route("getPlayer")]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
		public async Task<IActionResult> GetPlayer(PlayerModel model)
		{
			var validator = await _validatorFactory.CreateAsync<PlayerModel>(Actions.GetPlayer);
			var validationResult = await validator.ValidateAsync(model);

			if (validationResult.IsValid)
			{
				var repository = await _repositoryFactory.CreateAsync<PlayerModel, Player>();
				RepositoryResult<IEnumerable<PlayerModel>> result = null;
				
				if (model.Id > 0)
				{
					result = await repository.RetrieveAsync(p => p.PlayerId == model.Id);
				}
				else if (model.FirstName != null && model.LastName != null)
				{
					result = await repository.RetrieveAsync(p =>
						p.FirstName == model.FirstName && p.LastName == model.LastName);
				}
				else if (model.Team != null)
				{
					result = await repository.RetrieveAsync(p => p.Team.Name == model.Team);
				}

				return Ok(result);
			}
			else
			{
				return BadRequest(validationResult);
			}
		}

		[HttpGet]
		[Route("getTeam")]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
		public async Task<IActionResult> GetTeam(TeamModel model)
		{
			var validator = await _validatorFactory.CreateAsync<TeamModel>(Actions.GetTeam);
			var validationResult = await validator.ValidateAsync(model);

			if (validationResult.IsValid)
			{
				var repository = await _repositoryFactory.CreateAsync<TeamModel, Team>();
				RepositoryResult<IEnumerable<TeamModel>> result = null;
				
				if (model.Id > 0)
				{
					result = await repository.RetrieveAsync(t => t.TeamId == model.Id);
				}
				else if (model.Name != null)
				{
					result = await repository.RetrieveAsync(t => t.Name == model.Name);
				}

				return Ok(result);
			}
			else
			{
				return BadRequest(validationResult);
			}
		}
		
		[HttpPost]
		[Route("addToTeams")]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
		public async Task<IActionResult> AddToTeams(IEnumerable<PlayerModel> models)
		{
			var validator = await _validatorFactory.CreateAsync<IEnumerable<PlayerModel>>(Actions.AddToTeams);
			var validationResult = await validator.ValidateAsync(models);

			if (validationResult.IsValid)
			{
				List<RepositoryResult<PlayerModel>> results = new List<RepositoryResult<PlayerModel>>();
				var repository = await _repositoryFactory.CreateAsync<PlayerModel, Player>();

				foreach (var player in models)
				{
					var result = await repository.UpdateAsync(player);
					results.Add(result);
				}

				return Ok(results);
			}
			else
			{
				return BadRequest(validationResult);
			}
		}

		[HttpPost]
		[Route("transferPlayers")]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
		public async Task<IActionResult> TransferPlayers(IEnumerable<PlayerModel> models)
		{
			var validator = await _validatorFactory.CreateAsync<IEnumerable<PlayerModel>>(Actions.TransferPlayers);
			var validationResult = await validator.ValidateAsync(models);

			if (validationResult.IsValid)
			{
				List<RepositoryResult<PlayerModel>> results = new List<RepositoryResult<PlayerModel>>();
				var repository = await _repositoryFactory.CreateAsync<PlayerModel, Player>();

				foreach (var player in models)
				{
					var result = await repository.UpdateAsync(player);
					results.Add(result);
				}

				return Ok(results);
			}
			else
			{
				return BadRequest(validationResult);
			}
		}

		[HttpPost]
		[Route("linkStadium")]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
		public async Task<IActionResult> LinkStadium(TeamModel model)
		{
			var validator = await _validatorFactory.CreateAsync<TeamModel>(Actions.LinkStadium);
			var validationResult = await validator.ValidateAsync(model);

			if (validationResult.IsValid)
			{
				var repository = await _repositoryFactory.CreateAsync<TeamModel, Team>();
				var result = await repository.UpdateAsync(model);

				return Ok(result);
			}
			else
			{
				return BadRequest(validationResult);
			}
		}
	}
}

