﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiAutores.Utilidades
{
	public class AgregarparametroHATEOAS : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if ( context.ApiDescription.HttpMethod != "GET" )
			{
				return;
			}
			if( operation.Parameters is null)
			{
				operation.Parameters = new List<OpenApiParameter>();	
			}
			operation.Parameters.Add(new OpenApiParameter()
			{
				Name="incluirHATEOAS",
				In= ParameterLocation.Header,
				Required= false,
			});
		}
	}
}
