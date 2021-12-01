using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerTarjetasModel
    {
        //Lógica Microservicio...
        public List<Card> ObtenerTarjetas(IRepositoryEntityFrameworkCQRS<Card> cardRepository){
            return cardRepository.GetAll().ToList();
        }
        public async Task<CardInfoResponse> ObtenerTarjeta(IRepositoryEntityFrameworkCQRS<Card> cardRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Card> Tarjeta = ObtenerTarjetas(cardRepository);
            if (Tarjeta == null) {
                httpCod = 400;
                httpMsg = "No existe(n) Tarjeta(s) ingresada(s)";
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
