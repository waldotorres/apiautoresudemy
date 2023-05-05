using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using WebApiAutores.DTO;

namespace WebApiAutores.Servicios
{
	public class GeneradorEnlaces
	{
		private readonly IAuthorizationService authorizationService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IActionContextAccessor actionContextAccessor;

		public GeneradorEnlaces( IAuthorizationService authorizationService,
								IHttpContextAccessor httpContextAccessor,
								IActionContextAccessor actionContextAccessor)
        {
			this.authorizationService = authorizationService;
			this.httpContextAccessor = httpContextAccessor;
			this.actionContextAccessor = actionContextAccessor;
		}

		private async Task<bool> EsAdmin()
		{
			var esAdmin = await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, "esAdmin");
			return esAdmin.Succeeded;
		}
		private IUrlHelper ConstruirUrlHelper()
		{
			var factoria = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
			return factoria.GetUrlHelper(actionContextAccessor.ActionContext);
		}

        public async Task GenerarEnlaces(AutorDTO autorDTO )
		{
			var esAdmin =  await EsAdmin();
			var Url = ConstruirUrlHelper();

			autorDTO.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("obtenerAutor", new { autorDTO.Id }),
									descripcion: "self", metodo: "GET"));
			
			if (esAdmin)
			{
				autorDTO.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("actualizarAutor", new { autorDTO.Id }),
										descripcion: "autor-actualizar", metodo: "PUT"));

				autorDTO.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("borrarAutor", new { autorDTO.Id }),
										descripcion: "autor-borrar", metodo: "DELETE"));
			}
		}
	}
}
