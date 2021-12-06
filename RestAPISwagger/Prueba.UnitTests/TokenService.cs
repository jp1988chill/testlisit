using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Prueba.Domain;
using Prueba.Repository;

namespace Prueba.UnitTests
{
    public interface ITokenService
    {
        Task<string> GetToken();
        string GetRestAPIPath();
    }

    public class TokenService : ITokenService
    {
        private string _token = null;
        private readonly IAppSettingsRepository _appSettingsRepository;
        public TokenService() {
            var services = new ServiceCollection();
            services.AddTransient<IAppSettingsRepository, AppSettingsRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _appSettingsRepository = serviceProvider.GetService<IAppSettingsRepository>();
        }

        public async Task<string> GetToken()
        {
	        if(_token == null) { 
                _token = await GetNewAccessToken();
            }
            return _token;
        }

        public string GetRestAPIPath()
        {
            return _appSettingsRepository.GetRestAPIPath();
        }

        private async Task<String> GetNewAccessToken()
        {
	        var Name = _appSettingsRepository.GetRestAPIStubUser();
	        var Password = _appSettingsRepository.GetRestAPIStubPassword();
            string apiPath = GetRestAPIPath();
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            //Forge the RestAPI request
            List <User> users = new List<User>();
            users.Add(new User(name: Name, password: Password, token: new Guid(), tokenleasetime: ""));
            UserTokenServiceRequest Req = new UserTokenServiceRequest(users);
            
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(Req);
            var json = client.UploadString(apiPath + "/CrearToken", "Put", envelopeSignedUserTokenOutStr);
            UserTokenServiceResponse response = JsonConvert.DeserializeObject<UserTokenServiceResponse>(json);
            
            if (response.usersNuevoTokenAsignado.Count > 0)
	        {
		        return response.usersNuevoTokenAsignado[0].Token.ToString();
	        }

	        throw new ApplicationException("Unable to retrieve access token from ClientWebAPI");
        }
    }
}