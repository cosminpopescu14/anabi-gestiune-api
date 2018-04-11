﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anabi.Domain.Person.Commands;
using Anabi.Features.Person.Models;
using Anabi.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Anabi.Features.Person
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PersonController : Controller
    {
        private readonly IMediator mediator;

        public PersonController(IMediator _mediator)
        {
            mediator = _mediator;
        }
        //// GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET: api/identifiers
        /// <summary>
        /// Returns identifiers in the database
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns identifiers in the database filtered for person type (person or company)
        /// </para>
        /// </remarks>
        /// <response code="200">Array of Identifiers</response>
        /// <response code="400">No identifiers found!</response>
        /// <param name="isForPerson"></param>
        [ProducesResponseType(typeof(List<Models.Identifier>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("identifiers")]
        public async Task<IActionResult> GetIdentifiers(bool isForPerson)
        {

            var models = await mediator.Send(new GetIdentifier() { IsForPerson = isForPerson });
            return Ok(models);

        }


        //POST api/<controller>
        /// <summary>
        /// Creates a new defendant
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a new defendant by adding a new Person record and a new AssetDefendants record. 
        /// Checks that the AssetId provided exists in the database
        /// 
        /// Validation errors:
        /// 
        ///IDNUMBER_MAX_LENGTH_6;
        ///IDSERIE_MAX_LENGTH_2;
        ///IDENTIFICATION_MAX_LENGTH_20;
        ///PERSONNAME_MAX_LENGTH_200;
        ///FIRSTNAME_MAX_LENGTH_50;
        ///NATIONALITY_MAX_LENGTH_20;
        ///IDENTIFICATION_CANNOT_BE_EMPTY;
        ///PERSONNAME_CANNOT_BE_EMPTY;
        ///PERSONIDENTIFICATION_ALREADY_EXISTS;
        ///ASSET_INVALID_ID
        /// </para>
        /// </remarks>
        /// <response code="201">Id of the new defendant</response>
        /// <response code="400">Validation errors</response>
        /// <param name="addPerson">Details for new defendant</param>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("defendant")]
        public async Task<IActionResult> AddDefendant([FromBody]AddDefendant addPerson)
        {

            var personId = await mediator.Send(addPerson);
            return Created("api/person", personId);

        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}