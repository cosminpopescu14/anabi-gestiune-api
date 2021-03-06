﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Anabi.Common.ViewModels;
using Anabi.Domain.Asset.Commands;
using Anabi.Features.Assets.Models;
using Anabi.Middleware;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anabi.Features.Assets
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api")]
    public class SolutionsController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SolutionsController(IMediator _mediator, IMapper _mapper)
        {
            mediator = _mediator;
            mapper = _mapper;
        }

        /// <summary>
        /// Adds a new solution for an Asset
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns the id of the newly added solution
        /// Validation Errors:
        /// ASSET_INVALID_ID -- if lower than or equal to zero, or the id does not exist in the database
        /// DECISION_INVALID_ID -- if lower than or equal to zero, or the id does not exist in the database
        /// INSTITUTION_INVALID_ID -- if lower than or equal to zero, or the id does not exist in the database
        /// RECOVERYBENEFICIARY_INVALID_ID -- it is checked only if the value is not null (null value passes validation, but 0 will not)
        /// PRECAUTIONARYMEASURE_INVALID_ID -- it is checked only if the value is not null (null value passes validation, but 0 will not) (scop masura asiguratorie)
        /// STAGE_INVALID_ID -- if lower than or equal to zero, or the id does not exist in the database
        /// DECISION_DECISIONNUMBER_INVALID -- empty or exceeds 50 chars
        /// DECISION_DECISIONDATE_INVALID
        /// SOLUTION_FILENUMBER_INVALID -- if solution object is not null and field is empty or exceeds 200 characters
        /// SOLUTION_FILENUMBERPARQUET_INVALID -- if solution object is not null and field is empty or exceeds 200 characters
        /// SOLUTION_RECEIVINGDATE_INVALID -- if solution object is not null and field is null or default value (01.01.0001)
        /// RECOVERY_PERSONRESPONSIBLE_INVALID -- if recovery object is not null and field is empty or exceeds 200 characters
        /// RECOVERY_ESTIMATEDAMOUNT_GREATER_THAN_ZERO -- if recovery object is not null and field is 0 or less
        /// RECOVERY_ESTIMATEDAMOUNTCURRENCY_3_CHARS -- if recovery object is not null and field has more or less than 3 chars
        /// RECOVERY_ACTUALAMOUNT_GREATER_THAN_ZERO -- if recovery object is not null and field is 0 or less
        /// RECOVERY_ACTUALAMOUNTCURRENCY_3_CHARS -- if recovery object is not null and field has more or less than 3 chars
        /// RECOVERY_RECOVERYCOMMITTEEPRESIDENT_MAX_LENGTH_200 -- if recovery object is not null and recovery committee object is not null and field exceeds 200 characters
        /// RECOVERY_EVALUATIONCOMMITTEEPRESIDENT_MAX_LENGTH_200  -- if recovery object is not null and evaluation committee object is not null and field exceeds 200 characters
        /// </para>
        /// </remarks>
        /// <response code="200">The id of the new solution</response>
        /// <response code="400">Validation errors</response>
        /// <param name="assetId">Asset id to add solution for</param>
        /// <param name="request"></param>
        /// <returns>Id of the new solution</returns>
        [ProducesResponseType(typeof(SolutionViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("assets/{assetId}/solutions")]
        public async Task<IActionResult> AddSolution(int assetId, [FromBody] AddSolutionRequest request)
        {
            var message = mapper.Map<AddSolutionRequest, AddSolution>(request);
            message.AssetId = assetId;

            var model = await mediator.Send(message);

            return Created("", model);
        }

        /// <summary>
        /// Returns a list of solutions for a given asset id
        /// </summary>
        /// <param name="assetId">Asset id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<SolutionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("assets/{assetId}/solutions")]
        public async Task<IActionResult> GetSolutions(int assetId)
        {
            var models = await mediator.Send(new GetSolutions(assetId));

            return Ok(models);
        }
    }
}