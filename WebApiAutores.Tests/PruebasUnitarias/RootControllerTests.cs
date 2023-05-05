using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiAutores.Controllers.V1;
using WebApiAutores.Tests.Mocks;

namespace WebApiAutores.Tests.PruebasUnitarias
{
	[TestClass]
	public class RootControllerTests
	{
		[TestMethod]
		public async Task SiUsuarioAdmin_Obtenermos4Links()
		{
			//preparacion
			var authorizationService = new AuthorizationServiceMock();
			authorizationService.Resultado = AuthorizationResult.Success();  
			var rootController = new RootController(authorizationService);
			rootController.Url = new URLHelperMock();
			//ejecucion
			var resultado = await  rootController.Get();
			//verificacion
			Assert.AreEqual(4, resultado.Value.Count());
		}

		[TestMethod]
		public async Task SiUsuarioNoEsAdmin_Obtenermos2Links()
		{
			//preparacion
			var authorizationService = new AuthorizationServiceMock();
			authorizationService.Resultado = AuthorizationResult.Failed();
			var rootController = new RootController(authorizationService);
			rootController.Url = new URLHelperMock();
			//ejecucion
			var resultado = await rootController.Get();
			//verificacion
			Assert.AreEqual(2, resultado.Value.Count());
		}

		[TestMethod]
		public async Task SiUsuarioNoEsAdmin_Obtenermos2Links_UsandoMoq()
		{
			//preparacion
			var moqAuthorizationService = new Mock<IAuthorizationService>();
			moqAuthorizationService.Setup(x => x.AuthorizeAsync(
				It.IsAny<ClaimsPrincipal>(),
				It.IsAny<object>(),
				It.IsAny<IEnumerable<IAuthorizationRequirement>>()
				)).Returns( Task.FromResult( AuthorizationResult.Failed()) );

			moqAuthorizationService.Setup(x => x.AuthorizeAsync(
				It.IsAny<ClaimsPrincipal>(),
				It.IsAny<object>(),
				It.IsAny<string>()
				)).Returns(Task.FromResult(AuthorizationResult.Failed()));

			var moqURLHelper = new Mock<IUrlHelper>();
			moqURLHelper.Setup(x => x.Link( 
				It.IsAny<string>(), 
				It.IsAny<object>()
				)).Returns( string.Empty );

			var rootController = new RootController(moqAuthorizationService.Object);
			rootController.Url = moqURLHelper.Object;
			//ejecucion
			var resultado = await rootController.Get();
			//verificacion
			Assert.AreEqual(2, resultado.Value.Count());
		}
	}
}
