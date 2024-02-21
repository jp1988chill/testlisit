using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarRolUserModel
    {
        //Lógica Microservicio...
        public bool ActualizarRolUsers(List<RolUser> Rolusers, IRepositoryEntityFrameworkCQRS<RolUser> RoluserRepository)
        {
            foreach (RolUser Roluser in Rolusers) {
                RolUser RolUserToBeUpdated = RoluserRepository.GetAll().Where(id => id.Idroluser == Roluser.Idroluser).FirstOrDefault();
                if (RolUserToBeUpdated != null) {
                    RolUserToBeUpdated.Nombreroluser = Roluser.Nombreroluser;
                    RoluserRepository.Update(RolUserToBeUpdated);
                }
            }
            if (RoluserRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<RolUserResponse> ActualizarRolUser(RolUserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<RolUser> RoluserRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (ActualizarRolUsers(objBodyObjectRequest.RolUsers, RoluserRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar Roles de usuarios";
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
