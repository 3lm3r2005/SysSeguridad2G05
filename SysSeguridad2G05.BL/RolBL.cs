using System;
using System.Collections.Generic;
using System.Text;
using SysSeguridad2G05.EN;
using SysSeguridad2G05.DAL;
namespace SysSeguridad2G05.BL
{
    public class RolBL
    {
        public async static Task<int> GuardarAsync(Rol pRol)
        {
            return await RolDAL.CrearAsync(pRol);
        }
        public async Task<int> ModificarAsync(Rol pRol)
        {
            return await RolDAL.ModificarAsync(pRol);
        }
        public async Task<int> EliminarAsync(Rol pRol)
        {
            return await RolDAL.EliminarAsync(pRol);
        }
        public async Task<Rol> ObtenerPorIdAsync(Rol pRol)
        {
            return await RolDAL.ObtenerPorIdAsync(pRol);

        }
        public async Task<List<Rol>> ObtenerTodosAsync()
        {
            return await RolDAL.ObtenerTodosAsync();
        }
        public async Task<List<Rol>> BuscarTodosAsync(Rol pRol)
        {
            return await RolDAL.BuscarAsync(pRol);
        }
    }
}