using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET.Aula02Manha.Models;

namespace ASPNET.Aula02Manha.API
{
    [Route("Endereco")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly Context _context;

        public CepController(Context context)
        {
            _context = context;
        }

        [Route("ListarEnderecos")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_Cep>>> GetCeps()
        {
            return await _context.Ceps.ToListAsync();
        }

        [Route("ListarEndereco/{id}")]
        [HttpGet]
        public async Task<ActionResult<_Cep>> Get_Cep(string id)
        {
            var _Cep = await _context.Ceps.FindAsync(id);

            if (_Cep == null)
            {
                return NotFound();
            }

            return _Cep;
        }

        [Route("CadastrarEndereco")]
        [HttpPost]
        public async Task<ActionResult<_Cep>> Post_Cep(_Cep _Cep)
        {
            _context.Ceps.Add(_Cep);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_CepExists(_Cep.Cep))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Get_Cep", new { id = _Cep.Cep }, _Cep);
        }

        [Route("AlterarEndereco/{id}")]
        [HttpPut]
        public async Task<IActionResult> Put_Cep(string id, _Cep _Cep)
        {
            if (id != _Cep.Cep)
            {
                return BadRequest();
            }

            _context.Entry(_Cep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_CepExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Route("DeletarEndereco/{id}")]
        [HttpDelete]
        public async Task<ActionResult<_Cep>> Delete_Cep(string id)
        {
            var _Cep = await _context.Ceps.FindAsync(id);
            if (_Cep == null)
            {
                return NotFound();
            }

            _context.Ceps.Remove(_Cep);
            await _context.SaveChangesAsync();

            return _Cep;
        }

        private bool _CepExists(string id)
        {
            return _context.Ceps.Any(e => e.Cep == id);
        }
    }
}
