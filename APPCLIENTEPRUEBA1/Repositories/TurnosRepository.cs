using APPCLIENTEPRUEBA1.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCLIENTEPRUEBA1.Repositories
{
    public class TurnosRepository
    {
        SQLiteConnection context;
        public TurnosRepository()
        {
            string ruta = FileSystem.AppDataDirectory + "/turnos.db3";
            context = new SQLiteConnection(ruta);
            context.CreateTable<Turno>();
        }

        public void Insert(Turno T)
        {
            context.Insert(T);
        }

        public IEnumerable<Turno> GetAll()
        {
            return context.Table<Turno>()
               .OrderBy(x => x.Posicion);
        }

        public Turno? Get(int id)
        {
            return context.Find<Turno>(id);
        }

        public void Update(Turno T)
        {
            context.Update(T);
        }

        public void Delete(Turno T)
        {
            context.Delete(T);
        }
    }
}
