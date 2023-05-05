using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:120, ErrorMessage ="El campo {0} debe tener como máximo de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public List<AutorLibro> AutoresLibros { get; set; }


    }
}
