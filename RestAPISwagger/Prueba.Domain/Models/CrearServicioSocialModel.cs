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
        public bool CrearServiciosSociales(List<ServicioSocial> serviciossociales, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository){
            ObtenerServicioSocialModel obtenerServicioSocialModel = new ObtenerServicioSocialModel();
            foreach (ServicioSocial serviciosocial in serviciossociales)
            {
                //La asignación de servicio social no debe existir para la persona, en el mismo año.
                if (obtenerServicioSocialModel.AsignacionServicioSocialExiste(serviciosocial.Idcomuna.ToString(), serviciosocial.Iduser.ToString(), serviciosocial.Fecharegistro, servicioSocialRepository) == false)
                {
                    //Generamos nuevo PK para cada user automáticamente
                    servicioSocialRepository.Insert(serviciosocial);
                }
            }

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
