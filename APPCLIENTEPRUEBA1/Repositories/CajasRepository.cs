using APPCLIENTEPRUEBA1.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCLIENTEPRUEBA1.Repositories
{
    public class CajasRepository
    {
        SQLiteConnection context;

        public CajasRepository()
        {
            string ruta = FileSystem.AppDataDirectory + "/cajas.db3";
            context = new SQLiteConnection(ruta);
            context.CreateTable<Caja>();
        }

        public IEnumerable<Caja> GetAll()
        {
            return context.Table<Caja>()
               .OrderBy(x => x.Id)
               .Where(x => x.Estado == 1);
        }

        public Caja? Get(int id)
        {
            return context.Find<Caja>(id);
        }

        public void Insert(Caja L)
        {
            context.Insert(L);
        }

        public void Update(Caja L)
        {
            context.Update(L);
        }

    }
}
