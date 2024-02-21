using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerServicioSocialModel
    {
        //Lógica Microservicio...
        public List<ServicioSocial> ObtenerServicioSocialPorId(string Idserviciosocial, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            return servicioSocialRepository.GetAll().Where(id => id.Idserviciosocial == Convert.ToInt32(Idserviciosocial)).ToList();
        }

        public List<ServicioSocial> ObtenerServiciosSociales(IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            return servicioSocialRepository.GetAll().ToList();
        }

        public async Task<ServicioSocialResponse> ObtenerServicioSocial(string Idserviciosocial, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<ServicioSocial> ServicioSocial = ObtenerServicioSocialPorId(Idserviciosocial, servicioSocialRepository);
            if ((ServicioSocial == null) || (ServicioSocial.Count == 0)) {
                httpCod = 400;
                httpMsg = "No existe(n) ServicioSocial(s) con Idserviciosocial (" + Idserviciosocial + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ServicioSocialResponse bodyResponse = new ServicioSocialResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                ServiciosSociales = ServicioSocial
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }

        public async Task<ServicioSocialResponse> ObtenerServiciosSocialesCollection(IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<ServicioSocial> ServicioSocial = ObtenerServiciosSociales(servicioSocialRepository);
            if (ServicioSocial == null)
            {
                httpCod = 400;
                httpMsg = "No existe(n) ServicioSocial(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ServicioSocialResponse bodyResponse = new ServicioSocialResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                ServiciosSociales = ServicioSocial
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
