using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prueba.Domain;
using Prueba.Repository;
using System;
using System.Collections.Generic;
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
        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////
       
        public PruebaUnitTest()
        {
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Implementar llamadas Rest aquí (mismas que Cliente WebAPI) además de lógica de negocio
            
        }
    }
}
