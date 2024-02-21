using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerComunaModel
    {
        //Lógica Microservicio...
        public List<Comuna> ObtenerComunasPorId(string Idcomuna, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            return comunaRepository.GetAll().Where(id => id.IdComuna == Convert.ToInt32(Idcomuna)).ToList();
        }

        public List<Comuna> ObtenerComunas(IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            return comunaRepository.GetAll().ToList();
        }

        public async Task<ComunaResponse> ObtenerComuna(string Idcomuna, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Comuna> Comuna = ObtenerComunasPorId(Idcomuna, comunaRepository);
            if ((Comuna == null) || (Comuna.Count == 0)) {
                httpCod = 400;
                httpMsg = "No existe(n) Comuna(s) con Idcomuna (" + Idcomuna + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Comunas = Comuna
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }

        public async Task<ComunaResponse> ObtenerComunasCollection(IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Comuna> Comuna = ObtenerComunas(comunaRepository);
            if (Comuna == null)
            {
                httpCod = 400;
                httpMsg = "No existe(n) Comuna(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Comunas = Comuna
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
