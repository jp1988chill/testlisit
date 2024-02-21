using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarServicioSocialModel
    {
        //Lógica Microservicio...
        public bool ActualizarServiciosSociales(List<ServicioSocial> serviciossociales, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            foreach (ServicioSocial serviciosocial in serviciossociales) {
                ServicioSocial ServicioSocialToBeUpdated = servicioSocialRepository.GetAll().Where(id => id.Idserviciosocial == serviciosocial.Idserviciosocial).FirstOrDefault();
                if (ServicioSocialToBeUpdated != null) {
                    ServicioSocialToBeUpdated.Idcomuna = serviciosocial.Idcomuna;
                    ServicioSocialToBeUpdated.Iduser = serviciosocial.Iduser;
                    ServicioSocialToBeUpdated.Nombreserviciosocial = serviciosocial.Nombreserviciosocial;
                    servicioSocialRepository.Update(ServicioSocialToBeUpdated);
                }
            }
            if (servicioSocialRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ServicioSocialResponse> ActualizarServicioSocial(ServicioSocialBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (ActualizarServiciosSociales(objBodyObjectRequest.ServiciosSociales, servicioSocialRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al actualizar servicios sociales. ";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ServicioSocialResponse bodyResponse = new ServicioSocialResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                ServiciosSociales = objBodyObjectRequest.ServiciosSociales
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
