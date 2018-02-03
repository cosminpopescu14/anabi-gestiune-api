﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Anabi.Features.Assets.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Anabi.Controllers;

namespace Anabi.Features.Assets
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Assets")]
    public class AssetsController : BaseController
    {
        private readonly IMediator mediator;
        public AssetsController(IMediator _mediator)
        {
            mediator = _mediator;
        }


        [HttpGet]
        public async Task<IEnumerable<AssetSummary>> Get(SearchAsset filter)
        {
            var results = await mediator.Send(filter);

            return results;
        }



        // GET: api/Assets/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<AssetDetails> Get(int id)
        {
            var model = await mediator.Send(new GetAssetDetails { Id = id });

            return model;
        }
        
        
    }
}
