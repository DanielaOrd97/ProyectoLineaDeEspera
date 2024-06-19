using API_Linea_Espera.Models.Entities;

namespace API_Linea_Espera.Repositories
{
    public interface IRepository<T> where T : class
    {
		WebsitosEquipo2bancoContext Context { get; set; }

        void Delete(T item);
        T? Get(object id);
        IEnumerable<T> GetAll();
        IEnumerable<Usuarios> GetAllWithInclude();
        IEnumerable<Turnos> GetAllTurnosWithInclude();
        //IEnumerable<Cajas> GetAllCajasWithInclude();

		void Insert(T item);
        void Update(T item);

    }
}