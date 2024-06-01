using API_Linea_Espera.Models.Entities;

namespace API_Linea_Espera.Repositories
{
    public interface IRepository<T> where T : class
    {
        SistemaDeEspera1Context Context { get; set; }

        void Delete(T item);
        T? Get(object id);
        IEnumerable<T> GetAll();
        IEnumerable<Usuarios> GetAllWithInclude();
        object GetOperador(int id);
        void Insert(T item);
        void Update(T item);

    }
}