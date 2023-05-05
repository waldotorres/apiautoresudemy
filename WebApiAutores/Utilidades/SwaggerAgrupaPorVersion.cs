using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebApiAutores.Utilidades
{
	public class SwaggerAgrupaPorVersion : IControllerModelConvention
	{
		public void Apply(ControllerModel controller)
		{
			var nameSpaceControlador = controller.ControllerType.Namespace;
			var versionAPI = nameSpaceControlador.Split(".").Last().ToLower();
			controller.ApiExplorer.GroupName = versionAPI;
		}
	}
}
