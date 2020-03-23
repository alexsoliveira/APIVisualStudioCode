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
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _repository;

        public ClienteController(ClienteRepository repository)
        {
            this._repository = repository;
        }

        // POST api/cliente
        [HttpPost]
        public async Task Criar([FromBody] Cliente cliente)
        {
            var email = await _repository.Email(cliente.Email);
            if(email != null){ NotFound(); }
            await _repository.Criar(cliente);
        }

        // PUT api/cliente/1
        [HttpPut]
        public async Task Atualizar([FromBody] Cliente cliente)
        {
            var response = await _repository.Visualizar(cliente.Id);
            var obj = response.FirstOrDefault(x => x.LogradouroId == cliente.LogradouroId);
            obj.Nome = cliente.Nome;
            obj.Email = cliente.Email;
            obj.LogoTipo = cliente.LogoTipo;
            obj.LogradouroId = cliente.LogradouroId;
            await _repository.Atualizar(obj);
        }

        // GET api/cliente/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Visualizar(int id)
        {
            var response = await _repository.Visualizar(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // DELETE api/cliente/1
        [HttpDelete("{id}/{LogradouroId}")]
        public async Task Remover(int id, int LogradouroId)
        {
           await _repository.Remover(id, LogradouroId);
        }
    }
}