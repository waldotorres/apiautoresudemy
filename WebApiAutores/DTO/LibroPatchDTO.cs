using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTO
{
	public class LibroPatchDTO
	{
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[PrimeraLetraMayuscula]
		[StringLength(maximumLength: 250, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres.")]
		public string Titulo { get; set; }
		public DateTime FechaPublicacion { get; set; }
	}
}
