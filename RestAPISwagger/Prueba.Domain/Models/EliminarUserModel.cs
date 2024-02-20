using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarUserModel
    {
        //Lógica Microservicio...
        public bool EliminarUsers(List<User> users, IRepositoryEntityFrameworkCQRS<User> userRepository){
            foreach (User user in users) {
                /*
                User thisUser = userRepository.GetByID(user.Iduser);
                if ((thisUser != null) && (user.Iduser == thisUser.Iduser)) {
                    userRepository.Delete(thisUser);
                }
                */
            }
            if (userRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<UserResponse> EliminarUser(UserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarUsers(objBodyObjectRequest.Users, userRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar usuario(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            UserResponse bodyResponse = new UserResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                usersNuevoTokenAsignado = objBodyObjectRequest.Users
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
