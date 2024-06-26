﻿using API_Linea_Espera.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Linea_Espera.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public WebsitosEquipo2bancoContext Context { get; set; }

        public Repository(WebsitosEquipo2bancoContext context)
        {
            this.Context = context;
        }

        public virtual T? Get(object id)
        {
            return Context.Find<T>(id);

        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public IEnumerable<Usuarios> GetAllWithInclude()
        {
            return Context.Usuarios.Include(x => x.IdRolNavigation)
                .Include(x => x.IdCajaNavigation);
        }

        public IEnumerable<Turnos> GetAllTurnosWithInclude()
        {
            return Context.Turnos.Include(x => x.Caja)
                .Include(x => x.Usuario)
                .Include(x => x.Estado)
                .OrderBy(x => x.Posicion);
        }

        //public IEnumerable<Cajas> GetAllCajasWithInclude()
        //{
        //    return Context.Cajas.Include(x => x.Turnos).Include(x => x.Usuarios);
        //}

        public virtual void Insert(T item)
        {
            Context.Add(item);
            Context.SaveChanges();
        }

        public virtual void Update(T item)
        {
            Context.Update(item);
            Context.SaveChanges();
        }

        public virtual void Delete(T item)
        {
            Context.Remove(item);
            Context.SaveChanges();
        }
    }
}
