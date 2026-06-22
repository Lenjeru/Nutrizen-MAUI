using TiendaCursos.Shared;

namespace TiendaCursos.API.Helpers
{
    public static class HelperQuerys
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> consulta,
    PaginacionDTO paginacion)
        {
            return consulta
            .Skip((paginacion.Pagina - 1) * paginacion.Registros)
            .Take(paginacion.Registros);
        }
    }
}
