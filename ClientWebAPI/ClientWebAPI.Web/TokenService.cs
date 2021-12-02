using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientWebAPI.Web.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static ClientWebAPI.Api.Core.Enveloped.EnvelopedObject;

namespace Client
{
    public interface ITokenService
    {
        Task<string> GetToken();
        Task<string> GetRestAPIPath();
    }

    public class TokenService : ITokenService
    {
        private string _token = null;
        private readonly IOptions<ClientWebAPIConfig> _MantenedorMVCEntitySettings;
        public TokenService(IOptions<ClientWebAPIConfig> MantenedorMVCEntitySettings) => _MantenedorMVCEntitySettings = MantenedorMVCEntitySettings;

        public async Task<string> GetToken()
        {
	        if(_token == null) { 
                _token = await GetNewAccessToken();
            }
            return _token;
        }

        public async Task<string> GetRestAPIPath()
        {
            return _MantenedorMVCEntitySettings.Value.TokenUrl;
        }

        private async Task<String> GetNewAccessToken()
        {
	        var Name = _MantenedorMVCEntitySettings.Value.Name;
	        var Password = _MantenedorMVCEntitySettings.Value.Password;

            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            //Forge the RestAPI request
            List <User> users = new List<User>();
            users.Add(new User() { Name = Name, Password = Password, Token = new Guid(), Tokenleasetime = "" });
            UserTokenServiceRequest Req = new UserTokenServiceRequest(users);
            
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(Req);
            var json = client.UploadString(_MantenedorMVCEntitySettings.Value.TokenUrl + "/CrearToken", "Put", envelopeSignedUserTokenOutStr);
            UserTokenServiceResponse response = JsonConvert.DeserializeObject<UserTokenServiceResponse>(json);
            
            if (response.usersNuevoTokenAsignado.Count > 0)
	        {
		        return response.usersNuevoTokenAsignado[0].Token.ToString();
	        }

	        throw new ApplicationException("Unable to retrieve access token from ClientWebAPI");
        }
    }
}