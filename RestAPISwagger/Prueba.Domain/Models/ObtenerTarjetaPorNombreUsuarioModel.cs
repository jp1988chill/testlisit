using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerTarjetaPorNombreUsuarioModel
    {
        //Lógica Microservicio...
        public List<Card> ObtenerTarjetaPorNombreUsuarios(string NombreUsuario, IRepositoryEntityFrameworkCQRS<Card> cardRepository){
            return cardRepository.GetAll().Where(id => id.Name == NombreUsuario).ToList();
        }
        public async Task<CardInfoResponse> ObtenerTarjetaPorNombreUsuario(string NombreUsuario, IRepositoryEntityFrameworkCQRS<Card> cardRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Card> Tarjeta = ObtenerTarjetaPorNombreUsuarios(NombreUsuario, cardRepository);
            if (Tarjeta == null) {
                httpCod = 400;
                httpMsg = "No hay tarjeta(s) asociadas al TarjetaHabiente:("+NombreUsuario+")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            CardInfoResponse bodyResponse = new CardInfoResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Cards = Tarjeta
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
