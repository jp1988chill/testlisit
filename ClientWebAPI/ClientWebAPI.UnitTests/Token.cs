using System;
using System.Collections.Generic;
using ClientWebAPI.Web.Models;
using Newtonsoft.Json;

namespace Prueba.UnitTests
{
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
        public List<User> usersNuevoTokenAsignado { get; set; }
    }

    ///////////////////////////////////////////////////////////////////////////
    public class CardTokenServiceRequest
    {
        public CardTokenServiceRequest(List<Card> cards)
        {
            Cards = cards;
        }
        public List<Card> Cards { get; set; }
    }

    public class CardTokenServiceResponse
    {
        public int httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<CardResponse> Cards { get; set; }
    }
}