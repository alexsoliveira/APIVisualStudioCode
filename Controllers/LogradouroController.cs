using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesafioThomasGreg.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
//using DesafioThomasGreg.Services;
using DesafioThomasGreg.Repositories;

namespace DesafioThomasGreg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Default30", NoStore = false, Location = ResponseCacheLocation.Any)]
    public class LogradouroController : ControllerBase
    {
        private readonly LogradouroRepository _repository;

        public LogradouroController(LogradouroRepository repository)
        {
            this._repository = repository;
        }

        // POST api/logradoura
        [HttpPost]
        public async Task Criar([FromBody] Logradouro logradouro)
        {
            await _repository.Criar(logradouro);
        }

        // PUT api/logradouro/1
        [HttpPut]
        public async Task Atualizar([FromBody] Logradouro logradouro)
        {
            Logradouro response = await _repository.Visualizar(logradouro.Id);
            response.Tipo = logradouro.Tipo;
            await _repository.Atualizar(response);
        }

        // GET api/logradouro/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Logradouro>> Visualizar(int id)
        {
            var response = await _repository.Visualizar(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // DELETE api/logradouro/1
        [HttpDelete("{id}")]
        public async Task Remover(int id)
        {
           await _repository.Remover(id);
        }
    }
}