using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SysSeguridad2G05.EN;

namespace SysSeguridad2G05.DAL
{
    public class RolDAL
    {
        //metodo para crear un nuevo rol
        public static async Task<int> CrearAsync(Rol rol)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                dbContexto.Rol.Add(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        //metodo para modificar un rol
        public static async Task<int> ModificarAsync(Rol pRol)
        {
            int result = 0;

            using (var dbContexto = new DBContexto())
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                dbContexto.Update(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        //Metodo para eliminar un rol
        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                dbContexto.Rol.Remove(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Rol> ObtenerPorIdAsync(int id)
        {
            Rol rol = null;
            using (var dbContexto = new DBContexto())
            {
                rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.Id == id);
            }
            return rol;
        }
        public static async Task<List<Rol>> ObtenerTodosAsync()
        {
            List<Rol> roles = new List<Rol>();
            using (var dbContexto = new DBContexto())
            {
                ///obtener todos los roles
                roles = await dbContexto.Rol.ToListAsync();
            }
            return roles;
        }


        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pRol.Id);
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pRol.Nombre));//Like
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pRol.Top_Aux > 0)
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();
            return pQuery;
        }
        /// <summary>
        /// Elmer Portillo
        /// 18/03/2026
        /// Este metodo se encarga de buscar los roles en la base de datos, utilizando el metodo QuerySelect para filtrar los resultados
        /// </summary>
        /// <param name="pRol"></param>Parametro de tipo Rol que contiene los filtros para la busqueda
        /// <returns></returns>
        public static async Task<List<Rol>> BuscarAsync(Rol pRol)
        {
            
            List<Rol> roles = new List<Rol>();
            using (var dbContexto = new DBContexto())
            {
             ///obtener los roles utilizando el metodo QuerySelect para filtrar los resultados
                var select = dbContexto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }

    }
}
        