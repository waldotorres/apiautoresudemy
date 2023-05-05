using AutoMapper;
using WebApiAutores.DTO;
using WebApiAutores.Entidades;
using WebApiAutores.Migrations;

namespace WebApiAutores.Utilidades
{
	public class AutoMapperProfiles: Profile
	{
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
			CreateMap<Autor, AutorDTO>();
			
			CreateMap<Autor, AutorDTOConLibros>()
						.ForMember(aDto => aDto.Libros, opciones => opciones.MapFrom(MapAutorDtoLibros));

			CreateMap<LibroCreacionDTO, Libro>()
						.ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros) );


			CreateMap<LibroPatchDTO, Libro>().ReverseMap();
			CreateMap<Libro, LibroDTO>();
			CreateMap<Libro, LibroDTOConAutores>()
					.ForMember(libroDTO => libroDTO.Autores, opciones => opciones.MapFrom(MapLibroDtoAutores));

			CreateMap <ComentarioCreacionDTO, Comentario>();
			CreateMap<Comentario, ComentarioDTO>();
		}

		private List<LibroDTO> MapAutorDtoLibros( Autor autor, AutorDTO autorDTO )
		{
			var resultado = new List<LibroDTO>();
			if(  autor.AutoresLibros == null)
			{
				return resultado;
			}
			foreach (var autorLibro in autor.AutoresLibros)
			{
				resultado.Add(new LibroDTO()
				{
					Id = autorLibro.LibroId,
					Titulo=autorLibro.Libro.Titulo
				});
			}


			return resultado;
		}

		private  List<AutorDTO> MapLibroDtoAutores( Libro libro, LibroDTO libroDTO)
		{
			var resultado = new List<AutorDTO>();
			if(libro.AutoresLibros == null)
			{
				return resultado;
			}
			foreach (var autorLibro in libro.AutoresLibros)
			{
				resultado.Add(new AutorDTO()
				{
					Id = autorLibro.AutorId,
					Nombre = autorLibro.Autor.Nombre
				});
			}

			return resultado;
		}

		private List<AutorLibro> MapAutoresLibros( LibroCreacionDTO libroCreacionDTO, Libro libro)
		{
			var resultado = new List<AutorLibro>();

			if(libroCreacionDTO.AutoresIds == null)
			{
				return resultado;
			}

			foreach (var autorId in libroCreacionDTO.AutoresIds )
			{
				resultado.Add(new AutorLibro() { AutorId = autorId });
			}

			return resultado;
		}
	}
}
