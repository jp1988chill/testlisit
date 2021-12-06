using Prueba.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Prueba.Repository
{
    public class AppSettingsRepository : IAppSettingsRepository
    {
        private IConfiguration _config = null;
        public AppSettingsRepository()
        {
            _config = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).AddJsonFile("appsettings.json").Build();
        }
        public string GetRestAPIPath()
        {
            return _config.GetSection("RestAPIServer:TokenUrl").Value;
        }
        public string GetRestAPIStubUser()
        {
            return _config.GetSection("RestAPIServer:Name").Value;
        }
        public string GetRestAPIStubPassword()
        {
            return _config.GetSection("RestAPIServer:Password").Value;
        }
    }
}