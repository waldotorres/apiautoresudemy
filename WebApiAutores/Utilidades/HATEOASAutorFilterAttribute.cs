using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiAutores.DTO;
using WebApiAutores.Servicios;

namespace WebApiAutores.Utilidades
{
	public class HATEOASAutorFilterAttribute : HATEOASFiltroAttribute
	{
		private readonly GeneradorEnlaces generadorEnlaces;

		public HATEOASAutorFilterAttribute(GeneradorEnlaces generadorEnlaces)
		{
			this.generadorEnlaces = generadorEnlaces;
		}
		public override async Task OnResultExecutionAsync(ResultExecutingContext context,
									ResultExecutionDelegate next)
		{
			var debeIncluir = base.DebeIncluirHATEOAS(context);
			if (!debeIncluir)
			{
				await next();
				return;
			}

			var resultado = context.Result as ObjectResult;
			//var modelo = resultado.Value as AutorDTO ?? throw new ArgumentNullException("Se esperaba una instancia de AutorDTO");
			var autorDto = resultado.Value as AutorDTO;
			if (autorDto is null)
			{
				var autoresDto = resultado.Value as List<AutorDTO> ??
						throw new ArgumentNullException("Se esperaba una instancia de AutorDTO o Lista de AutorDTO"); ;

				autoresDto.ForEach(async autor => await generadorEnlaces.GenerarEnlaces(autor));
				resultado.Value = autoresDto;
			}
			else
			{
				await generadorEnlaces.GenerarEnlaces(autorDto);
			}


			await next();
		}
	}
}
