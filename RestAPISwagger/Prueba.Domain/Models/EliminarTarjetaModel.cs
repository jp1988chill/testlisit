using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarTarjetaModel
    {
        //Lógica Microservicio...
        public bool EliminarTarjetas(List<Card> cards, IRepositoryEntityFrameworkCQRS<Card> cardRepository){
            foreach (Card card in cards) {
                Card thisCard = cardRepository.GetByID(card.Id);
                if ((thisCard != null) && (card.Id == thisCard.Id)) {
                    cardRepository.Delete(thisCard);
                }
            }
            if (cardRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<CardResponse> EliminarTarjeta(CardBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Card> cardRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarTarjetas(objBodyObjectRequest.Cards, cardRepository) != true) {
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
                userFriendlyError = usrFriendlyErr
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
