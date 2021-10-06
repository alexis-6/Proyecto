using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly BdParcialContext context;

        public AreasController(BdParcialContext context)
        {
            this.context = context;
            //this.mapper = mapper;
        }
        /// <summary>
        /// Obtiene una lista de estudiantes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            //var areas = context.Areas.ToList();
            //var areasDTO = areas.Select(x => mapper.Map<AreaOutputDTO>(x)).OrderByDescending(x => x.IdArea);

            //return Ok(new RespuestaAreaDTO { IdArea = (int)HttpStatusCode.OK, Data = areasDTO });
            var data = context.Areas.Select(x => x.DArea).ToList();
            return Ok(data);
        }
    }
}
