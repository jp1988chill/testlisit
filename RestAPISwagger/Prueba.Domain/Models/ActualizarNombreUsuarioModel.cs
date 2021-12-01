using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarNombreUsuarioModel
    {
        //Lógica Microservicio...
        public bool ActualizarNombreUsuarios(string NombreUsuarioOriginal, string NombreUsuarioNuevo, IRepositoryEntityFrameworkCQRS<Card> cardRepository, IRepositoryEntityFrameworkCQRS<User> userRepository){
            //Por cada registro, lo eliminamos de la BD, y creamos uno nuevo,
            List<Card> lstCards = cardRepository.GetAll().Where(id => id.Name == NombreUsuarioOriginal).ToList();
            List<User> lstUsers = userRepository.GetAll().Where(id => id.Name == NombreUsuarioOriginal).ToList();
            if (lstCards.Count > 0) {
                foreach (Card card in lstCards) {
                    cardRepository.Delete(card.Id);
                    cardRepository.Save();

                    card.Name = NombreUsuarioNuevo;
                    cardRepository.Insert(card);
                    cardRepository.Save();
                }
            }
            if (lstUsers.Count > 0)
            {
                foreach (User user in lstUsers)
                {
                    userRepository.Delete(user.Token);
                    userRepository.Save();

                    user.Name = NombreUsuarioNuevo;
                    userRepository.Insert(user);
                    userRepository.Save();
                }
            }
            return true;
        }
        public async Task<CardResponse> ActualizarNombreUsuario(string NombreUsuarioOriginal, string NombreUsuarioNuevo, IRepositoryEntityFrameworkCQRS<Card> cardRepository, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Nombre de TarjetaHabiente actualizado desde ("+ NombreUsuarioOriginal + ") a (" + NombreUsuarioNuevo + ") Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            bool OperacionOK = ActualizarNombreUsuarios(NombreUsuarioOriginal, NombreUsuarioNuevo, cardRepository, userRepository);
            if (OperacionOK == false) {
                httpCod = 400;
                httpMsg = "No existe el Nombre de TarjetaHabiente:("+ NombreUsuarioOriginal + ")";
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
