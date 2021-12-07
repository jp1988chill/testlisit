using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prueba.Domain;
using Prueba.Repository;
using Prueba.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


/// <summary>
/// ///////////////////////////////// Prueba Unitaria que realiza prueba en tiempo real de un Cliente consumiendo los servicios
/// </summary>
namespace Prueba.UnitTests
{
    

    [TestClass]
    public class PruebaUnitTest
    {
        private readonly IConfiguration Configuration;
        private readonly string _sqlCon;
        public PruebaUnitTest()
        {
            var services = new ServiceCollection().AddEntityFrameworkSqlServer();

            //Add IOption dependency
            services.AddOptions();

            //Build a new Configuration required by Client Rest API Controller
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _sqlCon = Configuration.GetConnectionString("database");
        }

        private async Task<DatabaseContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(_sqlCon)
                .Options;
            var databaseContext = new DatabaseContext(options);
            if (!databaseContext.Database.IsInMemory())
            {
                databaseContext.Database.Migrate();
            }
            databaseContext.Database.EnsureCreated();
            for (int i = 1; i <= 10; i++)
            {
                databaseContext.Users.Add(new User(name: "TestName", password: "TestPassword", token: new Guid(), tokenleasetime: ""));
                databaseContext.SaveChanges();
            }
            return databaseContext;
        }

        [TestMethod]
        public void TestEFCoreConnection()
        {
            //Testea conexión a EF Core
            //Arrange
            var con = GetDatabaseContext().Result;
            con.Database.OpenConnection();
            var dbcon = con.Database.GetDbConnection();
            Assert.AreEqual(dbcon.State, ConnectionState.Open);
            con.Database.ExecuteSqlRaw("TRUNCATE TABLE [Users]");
            con.Database.CloseConnection();
            Assert.AreEqual(dbcon.State, ConnectionState.Closed);
        }
    }
}
