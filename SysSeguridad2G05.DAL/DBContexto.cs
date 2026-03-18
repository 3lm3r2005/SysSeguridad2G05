using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using SysSeguridad2G05.EN;

namespace SysSeguridad2G05.DAL
{
    public class DBContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ELMERPORTILLO\\\\SQLEXPRESS;Initial Catalog=DbSysSeguridad;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");
        }
    }
}