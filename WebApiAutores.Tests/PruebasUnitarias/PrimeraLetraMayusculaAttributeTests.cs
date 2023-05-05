using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Tests.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaAttributeTests
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            //preparacion
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var valor = "waldo";
            var valContext = new ValidationContext(new {Nombre = valor});
            //ejecucion
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);
            //verificacion
            Assert.AreEqual("La primera letra debe ser mayuscula", resultado.ErrorMessage) ;
        }

        [TestMethod]
        public void ValorNulo_NoDevuelveError()
        {
            //preparacion
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = null;
            var valContext = new ValidationContext(new { Nombre = valor });
            //ejecucion
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);
            //verificacion
            Assert.IsNull(resultado);
		}

		[TestMethod]
		public void ValorConPrimeraLetraMayuscula_NoDevuelveError()
		{
			//preparacion
			var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
			string valor = "Waldo";
			var valContext = new ValidationContext(new { Nombre = valor });
			//ejecucion
			var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);
			//verificacion
			Assert.IsNull(resultado);
		}
	}
}