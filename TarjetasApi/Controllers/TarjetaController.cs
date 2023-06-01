using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TarjetasApi.Application.Context;
using TarjetasApi.Domain.Entities;
using TarjetasApi.Infrastructure.Repositories;

namespace TarjetasApi.Controllers
{
    [ApiController]
    [Route("api/tarjetas")]
    public class TarjetaController : ControllerBase
    {
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<List<tarjeta>>> TodasTarjetas()
        {
            try
            {
                var repo = new TarjetaRepository();
                var lista = await repo.GetTarjeta();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public async Task InsertTarjeta([FromBody] tarjeta tarjeta)
        {
            try
            {
                var repo = new TarjetaRepository();
                await repo.CreateTarjetaAsync(tarjeta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public async Task UpdateTarjeta([FromBody] tarjeta tarjeta)
        {
            try
            {
                var repo = new TarjetaRepository();
                await repo.UpdateTarjetaAsync(tarjeta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
