using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarRolUserModel
    {
        //Lógica Microservicio...
        public bool EliminarRolUsers(List<RolUser> Rolusers, IRepositoryEntityFrameworkCQRS<RolUser> RoluserRepository)
        {
            List<RolUser> allRolUsers = RoluserRepository.GetAll().ToList();
            foreach (RolUser Roluser in Rolusers) {
                List<RolUser> RolUsersToBeDeleted = allRolUsers.Where(id => id.Idroluser == Roluser.Idroluser).ToList();
                foreach (RolUser RoluserToBeDeleted in RolUsersToBeDeleted)
                {
                    RoluserRepository.Delete(RoluserToBeDeleted);
                }
            }
            if (RoluserRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<RolUserResponse> EliminarRolUser(RolUserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<RolUser> RoluserRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarRolUsers(objBodyObjectRequest.RolUsers, RoluserRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar Rol(es) de usuario(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RolUserResponse bodyResponse = new RolUserResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                RolUsers = objBodyObjectRequest.RolUsers
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
