using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearTarjetaModel
    {
        //Lógica Microservicio...
        public bool CrearTarjetas(List<Card> cards, IRepositoryEntityFrameworkCQRS<Card> cardRepository){

            //Generamos nuevo GUID para cada tarjeta.
            foreach (Card card in cards) {
                card.Id = new Guid();
            }

            cardRepository.InsertMany(cards);
            if (cardRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<CardResponse> CrearTarjeta(CardBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Card> cardRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (CrearTarjetas(objBodyObjectRequest.Cards, cardRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar tarjetas";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            CardResponse bodyResponse = new CardResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Cards = objBodyObjectRequest.Cards
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
