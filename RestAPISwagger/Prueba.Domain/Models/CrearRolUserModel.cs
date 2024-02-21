using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearRolUserModel
    {
        //Lógica Microservicio...
        public bool CrearRolUsers(List<RolUser> rolUsers, IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository)
        {
            //Generamos nuevo PK para cada user automáticamente
            rolUserRepository.InsertMany(rolUsers);
            if (rolUserRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<RolUserResponse> CrearRolUser(RolUserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (CrearRolUsers(objBodyObjectRequest.RolUsers, rolUserRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar roles de usuarios.";
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
