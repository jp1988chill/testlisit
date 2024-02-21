using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarServicioSocialModel
    {
        //Lógica Microservicio...
        public bool EliminarServiciosSociales(List<ServicioSocial> serviciossociales, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            List<ServicioSocial> allServicioSocial = servicioSocialRepository.GetAll().ToList();
            foreach (ServicioSocial serviciosocial in serviciossociales) {
                List<ServicioSocial> serviciosSocialesToBeDeleted = allServicioSocial.Where(id => id.Idserviciosocial == serviciosocial.Idserviciosocial).ToList();
                foreach (ServicioSocial servicioSocialToBeDeleted in serviciosSocialesToBeDeleted)
                {
                    servicioSocialRepository.Delete(servicioSocialToBeDeleted);
                }
            }
            if (servicioSocialRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ServicioSocialResponse> EliminarServicioSocial(ServicioSocialBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarServiciosSociales(objBodyObjectRequest.ServiciosSociales, servicioSocialRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar Servicios Sociales ";
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
