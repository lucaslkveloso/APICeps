using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET.Aula02Manha.Models
{
    public class Context : DbContext
    {
        //Microsoft.EnitityFramworkCore.SqlServer
        //Microsoft.EnitityFramworkCore.Tools
        // Add-Migration NomeMigração
        //Update-Database -verbose
        public DbSet<_Cep> Ceps { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

    }
}
