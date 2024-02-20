using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Prueba.Domain.Models
{
    public class CrearLoginUserModel
    {
        //Lógica Microservicio...
        public List<User> CrearLoginUserDesdeUsers(List<User> users, IRepositoryEntityFrameworkCQRS<User> userRepository){
            List<User> usersLoginGenerado = new List<User>();
            foreach (User user in users) {
                User thisUser = userRepository.GetAll().Where(id => id.Name == user.Name).FirstOrDefault();
                if (thisUser != null)
                {
                    thisUser.Tokenleasetime = DateTime.Now.AddSeconds(60 * 10).ToString("dd-MM-yyyy HH:mm:ss");
                    thisUser.Token = user.Token;
                    userRepository.Update(thisUser);
                    usersLoginGenerado.Add(thisUser);
                }
            }
            userRepository.Save();
            return usersLoginGenerado;
        }
        public async Task<LoginUsuarioResponse> CrearLoginUser(UserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";
            List<User> lstNuevosTokensPorUsuario = CrearLoginUserDesdeUsers(objBodyObjectRequest.Users, userRepository);
            if (lstNuevosTokensPorUsuario.Count == 0) {
                httpCod = 400;
                httpMsg = "Error al ingresar tokens. No se generó token alguno para usuario(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            LoginUsuarioResponse bodyResponse = new LoginUsuarioResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                UsersNuevoTokenAsignado = lstNuevosTokensPorUsuario
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
