using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearUserModel
    {
        //Lógica Microservicio...
        public bool CrearUsers(List<User> users, IRepositoryEntityFrameworkCQRS<User> userRepository){

            //Generamos nuevo PK para cada user automáticamente
            userRepository.InsertMany(users);
            if (userRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<UserResponse> CrearUser(UserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (CrearUsers(objBodyObjectRequest.Users, userRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar usuarios";
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
