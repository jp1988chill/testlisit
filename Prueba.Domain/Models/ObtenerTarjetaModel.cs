using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerTarjetaModel
    {
        //Lógica Microservicio...
        public Card ObtenerTarjetas(string guidTarjeta, IRepositoryEntityFrameworkCQRS<Card> cardRepository){
            return cardRepository.GetByID(new Guid(guidTarjeta));
        }
        public async Task<CardInfoResponse> ObtenerTarjeta(string guidTarjeta, IRepositoryEntityFrameworkCQRS<Card> cardRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            Card Tarjeta = ObtenerTarjetas(guidTarjeta, cardRepository);
            if (Tarjeta == null) {
                httpCod = 400;
                httpMsg = "La tarjeta ("+ guidTarjeta + ") no existe.";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            CardInfoResponse bodyResponse = new CardInfoResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                cardInfoResponse = Tarjeta
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
