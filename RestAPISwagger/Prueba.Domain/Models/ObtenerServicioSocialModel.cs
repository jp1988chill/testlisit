using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;

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

        //Helper Lógica de negocio

        //Lógica: Servicios de ayudas sociales: Están asignados por comuna y solo a los residentes de dichas comunas
        //A una persona no se le puede asignar más de una vez con el mismo servicio social el mismo año.

        //crear método AsignacionServicioSocialExiste(IdComuna, IdUsuario, Año) == true/false. Se implementa validación en Create / Update de Servicio(s)Social(es).
        //Si es true, no se puede registrar, si es false, se registra.
        public bool AsignacionServicioSocialExiste(string IdComuna, string IdUsuario, string Año, IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository)
        {
            List<ServicioSocial> servSocConsultado = ObtenerServiciosSociales(servicioSocialRepository).Where(id => (id.Idcomuna == Convert.ToInt32(IdComuna)) && (id.Iduser == Convert.ToInt32(IdUsuario))).ToList();
            DateTime inputDT = DateTime.ParseExact(Año, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            foreach (ServicioSocial servSoc in servSocConsultado)
            {
                DateTime queryDT = DateTime.ParseExact(servSoc.Fecharegistro, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                if (inputDT.Year == queryDT.Year)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
