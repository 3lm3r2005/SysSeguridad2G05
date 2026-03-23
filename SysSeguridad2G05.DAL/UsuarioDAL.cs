using Microsoft.EntityFrameworkCore;
using SysSeguridad2G05.EN;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SysSeguridad2G05.DAL
{
    public class UsuarioDAL
    {
        private static void EncriptarMD5(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(pUsuario.password));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUsuario.password = strEncriptar;
            }
        }
        private static async Task<bool> ExisteElLogin(Usuario pUsuario, DBContexto pDContexto)
        {
            bool result = false;
            var loginUsuarioExiste = await pDContexto.Usuario.FirstOrDefaultAsync(a => a.login == pUsuario.login && a.Id != pUsuario.Id);
            if (loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 && loginUsuarioExiste.login == pUsuario.login)
                result = true;
            return result;
        }
        public static async Task<int> EliminarAsync(Usuario pUsuario)
        {
            int result = 0;
            try
            {
                using (var dbContext = new DBContexto())
                {
                    var usuario = await dbContext.Usuario.FirstOrDefaultAsync(f => f.Id == pUsuario.Id);
                    dbContext.Usuario.Remove(usuario);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception("Error al eliminar el usuario", ex);

            }
            return result;
        }
        public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            try
            {
                using (var dbContext = new DBContexto())
                {
                    usuario = await dbContext.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por Id", ex);
            }
            return usuario;
        }
        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (var dbContext = new DBContexto())
                {
                    usuarios = await dbContext.Usuario.ToListAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los usuarios", ex);
            }
            return usuarios;
        }
        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
        {
            if (pUsuario.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pUsuario.Id);
            if (pUsuario.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pUsuario.Id);
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pUsuario.Nombre));
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pUsuario.Apellido));
            if (pUsuario.Estatus > 0)
                pQuery = pQuery.Where(s => s.Estatus == pUsuario.Estatus);
            if (pUsuario.FechaRegistro.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUsuario.FechaRegistro.Year, pUsuario.FechaRegistro.Month, pUsuario.FechaRegistro.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaRegistro >= fechaInicial && s.FechaRegistro <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pUsuario.Top_Aux > 0)
                pQuery = pQuery.Take(pUsuario.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var dbContext = new DBContexto())
            {
                var select = dbContext.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario);
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }
        public static async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var dbContext = new DBContexto())
            {
                var select = dbContext.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(s => s.Rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }
        public static async Task<Usuario> loginAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var dbContext = new DBContexto())
            {
                EncriptarMD5(pUsuario);
                    usuario = await dbContext.Usuario.FirstOrDefaultAsync(
                        s => s.login == pUsuario.login && s.password == pUsuario.password
                    && s.Estatus == (byte)Estatus_Usuario.Activo);

            }
            return usuario;
        }
    }
}
