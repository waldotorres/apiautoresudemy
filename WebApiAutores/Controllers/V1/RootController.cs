﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiAutores.DTO;

namespace WebApiAutores.Controllers.V1
{
    [ApiController]
    [Route("api")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RootController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        public RootController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpGet(Name = "obtenerRoot")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DatoHATEOAS>>> Get()
        {
            var datosHateoas = new List<DatoHATEOAS>();

            var esAdmin = await authorizationService.AuthorizeAsync(User, "esAdmin");

            datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("obtenerRoot", new { }),
                            descripcion: "self", metodo: "GET"));

            datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("obtenerAutores", new { }),
                                descripcion: "autores", metodo: "GET"));


            if (esAdmin.Succeeded)
            {
                datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearAutor", new { }),
                                descripcion: "autor-crear", metodo: "POST"));

                datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearLibro", new { }),
                                    descripcion: "libro-crear", metodo: "POST"));
            }


            return datosHateoas;
        }

    }
}
