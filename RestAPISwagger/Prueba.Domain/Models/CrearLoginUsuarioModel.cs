using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearLoginUsuarioModel
    {
        //Lógica Microservicio...
        public List<User> CrearLoginUsuarioDesdeUsuarios(List<User> users, IRepositoryEntityFrameworkCQRS<User> userRepository){
            List<User> usersTokenGenerado = new List<User>();
            foreach (User user in users) {
                User thisUser = userRepository.GetAll().Where(id => id.Name == user.Name).FirstOrDefault();
                if (thisUser != null)
                {
                    userRepository.Delete(thisUser);
                    userRepository.Save();
                }
                thisUser = new User(user.Name, user.Password, new Guid(), DateTime.Now.AddMinutes(10).ToString()); //10 minutes lease time
                userRepository.Insert(thisUser);
                userRepository.Save();
                usersTokenGenerado.Add(thisUser);
            }
            return usersTokenGenerado;
        }
        public async Task<LoginUsuarioResponse> CrearLoginUsuario(UserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";
            List<User> lstNuevosTokensPorUsuario = CrearLoginUsuarioDesdeUsuarios(objBodyObjectRequest.Users, userRepository);
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
