using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerUserModel
    {
        //Lógica Microservicio...
        public List<User> ObtenerUsersPorIdRol(string IdRolUser, IRepositoryEntityFrameworkCQRS<User> userRepository){
            return userRepository.GetAll().Where(id => id.Idroluser == Convert.ToInt32(IdRolUser)).ToList();
        }

        public List<User> ObtenerUsersPorId(string IdUser, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            return userRepository.GetAll().Where(id => id.Iduser == Convert.ToInt32(IdUser)).ToList();
        }

        public List<User> ObtenerUsers(IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            return userRepository.GetAll().ToList();
        }

        public async Task<UserResponse> ObtenerUser(string IdUser, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<User> User = ObtenerUsersPorId(IdUser, userRepository);
            if ((User == null) || (User.Count == 0)) {
                httpCod = 400;
                httpMsg = "No existe(n) Usuario(s) con IdUser ("+ IdUser + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            UserResponse bodyResponse = new UserResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                usersNuevoTokenAsignado = User
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }

        public async Task<UserResponse> ObtenerUsersCollection(IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<User> User = ObtenerUsers(userRepository);
            if (User == null)
            {
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
                usersNuevoTokenAsignado = User
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
