using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarUsuarioModel
    {
        //Lógica Microservicio...
        public bool EliminarUsuarios(List<User> users, IRepositoryEntityFrameworkCQRS<User> userRepository){
            foreach (User user in users)
            {
                User thisUser = userRepository.Get().Where( it => it.Name == user.Name).FirstOrDefault();
                if ((thisUser != null) && (user.Name == thisUser.Name))
                {
                    userRepository.Delete(thisUser);
                }
            }
            if (userRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<UserResponse> EliminarUsuario(UserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarUsuarios(objBodyObjectRequest.Users, userRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar usuarios";
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
