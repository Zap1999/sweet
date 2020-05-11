﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.ApiModels;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/sweet_storage")]
    public class SweetStorageController : ControllerBase
    {
        private readonly SweetStorageService _service;


        public SweetStorageController()
        {
            _service = new SweetStorageService(new SweetLifeDbContext());
        }

        [HttpGet("{factoryId}")]
        public IEnumerable<SweetStorage> GetSweetStorageForFactory([FromRoute] long factoryId)
        {
            return _service.GetSweetStorageForFactory(factoryId);
        }
        
        [HttpPut("{sweetId}/{factoryId}/{count}")]
        public void UpdateSweetStorage([FromRoute] int sweetId, [FromRoute] int factoryId, [FromRoute] int count)
        {
            _service.UpdateSweetStorage(sweetId, factoryId, count);
        }

        [HttpDelete("{factoryId}/{sweetId}")]
        public void DeleteSweetStorageRecord([FromRoute] long factoryId, [FromRoute] long sweetId)
        {
            _service.Delete(factoryId, sweetId);
        }
    }
}