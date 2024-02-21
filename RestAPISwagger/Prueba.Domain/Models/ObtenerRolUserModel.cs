using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerRolUserModel
    {
        //Lógica Microservicio...
        public List<RolUser> ObtenerRolUsersPorId(string IdRolUser, IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository)
        {
            return rolUserRepository.GetAll().Where(id => id.Idroluser == Convert.ToInt32(IdRolUser)).ToList();
        }

        public List<RolUser> ObtenerRolUsers(IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository)
        {
            return rolUserRepository.GetAll().ToList();
        }

        public async Task<RolUserResponse> ObtenerRolUser(string IdRolUser, IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<RolUser> rolUser = ObtenerRolUsersPorId(IdRolUser, rolUserRepository);
            if ((rolUser == null) || (rolUser.Count == 0)) {
                httpCod = 400;
                httpMsg = "No existe(n) Roles de Usuario con IdRolUser ("+ IdRolUser + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RolUserResponse bodyResponse = new RolUserResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                RolUsers = rolUser
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }

        public async Task<RolUserResponse> ObtenerRolUsersCollection(IRepositoryEntityFrameworkCQRS<RolUser> RolUserRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<RolUser> rolUser = ObtenerRolUsers(RolUserRepository);
            if (rolUser == null)
            {
                httpCod = 400;
                httpMsg = "No existe(n) Roles de Usuario(s) registrados.";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RolUserResponse bodyResponse = new RolUserResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                RolUsers = rolUser
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
