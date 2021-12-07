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

namespace Prueba.UnitTests
{
    [TestClass]
    public class PruebaUnitTest
    {
        private IConfiguration Configuration;
        private string _sqlCon;

        [TestInitialize]
        public void Setup()
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

        private DatabaseContext GetDatabaseContext(){
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(_sqlCon).Options;
            var databaseContext = new DatabaseContext(options);
            if (!databaseContext.Database.IsInMemory())
            {
                databaseContext.Database.Migrate();
            }
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [TestMethod]
        public void TestEFCoreConnection()
        {
            //Arrange
            DatabaseContext dbCtx = GetDatabaseContext();

            //Act
            dbCtx.Database.OpenConnection();
            var dbConnection = dbCtx.Database.GetDbConnection();
            var connStateShouldBeOpen = dbConnection.State;
            dbCtx.Database.CloseConnection();
            var connStateShouldBeClosed = dbConnection.State;

            //Assert
            Assert.AreEqual(connStateShouldBeOpen, ConnectionState.Open);
            Assert.AreEqual(connStateShouldBeClosed, ConnectionState.Closed);

            //Finish
            dbCtx.Dispose();
        }

        [TestMethod]
        public void TestEFCoreOperation()
        {
            //Arrange
            DatabaseContext dbCtx = GetDatabaseContext();

            //Act
            dbCtx.Database.OpenConnection();
            dbCtx.Database.ExecuteSqlRaw("TRUNCATE TABLE [Users]");
            int i = 0;
            for (i = 0; i < 10; i++)
            {
                dbCtx.Users.Add(new User(name: "TestName" + i, password: "TestPassword" + i, token: new Guid(), tokenleasetime: DateTime.Now.AddSeconds(60 * 10).ToString("dd-MM-yyyy HH:mm:ss")));
                dbCtx.SaveChanges();
            }
            var shouldNotBeZero = dbCtx.Users.Count();
            dbCtx.Database.ExecuteSqlRaw("TRUNCATE TABLE [Users]");
            var shouldBeZero = dbCtx.Users.Count();
            dbCtx.Database.CloseConnection();

            //Assert
            Assert.AreEqual(shouldNotBeZero, i);
            Assert.AreEqual(shouldBeZero, 0);

            //Finish
            dbCtx.Dispose();
        }
    }
}
