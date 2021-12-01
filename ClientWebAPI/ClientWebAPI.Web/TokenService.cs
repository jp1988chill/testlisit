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
    }
    
    public class TokenService : ITokenService
    {
        private MantenedorMVCEntityApiToken _token = new MantenedorMVCEntityApiToken();
        private readonly IOptions<ClientWebAPIConfig> _MantenedorMVCEntitySettings;
        public TokenService(IOptions<ClientWebAPIConfig> MantenedorMVCEntitySettings) => _MantenedorMVCEntitySettings = MantenedorMVCEntitySettings;

        public async Task<string> GetToken()
        {
	        _token = await GetNewAccessToken();
            return _token.AccessToken;
        }

        private async Task<MantenedorMVCEntityApiToken> GetNewAccessToken()
        {
	        var Name = _MantenedorMVCEntitySettings.Value.Name;
	        var Password = _MantenedorMVCEntitySettings.Value.Password;

            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            //Forge the RestAPI request
            List <User> users = new List<User>();
            users.Add(new User(Name, Password, new Guid(), ""));
            UserTokenServiceRequest Req = new UserTokenServiceRequest(users);
            
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(Req);
            var json = client.UploadString(_MantenedorMVCEntitySettings.Value.TokenUrl + "/CrearToken", "Put", envelopeSignedUserTokenOutStr);
            UserTokenServiceResponse response = JsonConvert.DeserializeObject<UserTokenServiceResponse>(json);
            
            if (response.usersNuevoTokenAsignado.Count > 0)
	        {
		        MantenedorMVCEntityApiToken newToken = new MantenedorMVCEntityApiToken() { AccessToken = response.usersNuevoTokenAsignado[0].Token.ToString(), ExpiresAt = response.usersNuevoTokenAsignado[0].Tokenleasetime.ToString(), TokenType = "Api Key", Scope = "User Acess"};
                return newToken;
	        }

	        throw new ApplicationException("Unable to retrieve access token from ClientWebAPI");
        }
    }
}