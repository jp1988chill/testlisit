using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearServicioSocialModel
    {
        //Lógica Microservicio...
        public bool CrearServiciosSociales(List<ServicioSocial> serviciosocial, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository){
            //Generamos nuevo PK para cada user automáticamente
            servicioSocialRepository.InsertMany(serviciosocial);
            if (servicioSocialRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ServicioSocialResponse> CrearServicioSocial(ServicioSocialBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (CrearServiciosSociales(objBodyObjectRequest.ServiciosSociales, servicioSocialRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar Servicios Sociales";
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
