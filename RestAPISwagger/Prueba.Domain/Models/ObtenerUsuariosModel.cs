using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerUsuariosModel
    {
        //Lógica Microservicio...
        public List<User> ObtenerUsuarios(IRepositoryEntityFrameworkCQRS<User> userRepository){
            return userRepository.GetAll().ToList();
        }
        public async Task<UserResponse> ObtenerUsuario(IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<User> Usuario = ObtenerUsuarios(userRepository);
            if (Usuario == null) {
                httpCod = 400;
                httpMsg = "No existe(n) Usuario(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            UserResponse bodyResponse = new UserResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                usersNuevoTokenAsignado = Usuario
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
