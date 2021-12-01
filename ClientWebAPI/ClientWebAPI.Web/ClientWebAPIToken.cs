using System;
using System.Collections.Generic;
using ClientWebAPI.Web.Models;
using Newtonsoft.Json;

namespace Client
{
    internal class MantenedorMVCEntityApiToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        public string ExpiresAt { get; set; }

        public string Scope { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }

    public class UserTokenServiceRequest
    {
        public UserTokenServiceRequest(List<User> users)
        {
            Users = users;
        }
        public List<User> Users { get; set; }
    }

    public class UserTokenServiceResponse
    {
        public int httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<UserResponse> usersNuevoTokenAsignado { get; set; }
    }

    ///////////////////////////////////////////////////////////////////////////
    public class CardTokenServiceResponse
    {
        public int httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<CardResponse> cardInfoResponse { get; set; }
    }
}