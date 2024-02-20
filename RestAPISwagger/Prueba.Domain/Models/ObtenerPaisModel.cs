using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerPaisModel
    {
        //Lógica Microservicio...
        public List<Pais> ObtenerPaisesPorId(string Idpais, IRepositoryEntityFrameworkCQRS<Pais> paisRepository)
        {
            return paisRepository.GetAll().Where(id => id.Idpais == Convert.ToInt32(Idpais)).ToList();
        }

        public List<Pais> ObtenerPaises(IRepositoryEntityFrameworkCQRS<Pais> paisRepository)
        {
            return paisRepository.GetAll().ToList();
        }

        public async Task<PaisResponse> ObtenerPais(string Idpais, IRepositoryEntityFrameworkCQRS<Pais> paisRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Pais> Pais = ObtenerPaisesPorId(Idpais, paisRepository);
            if ((Pais == null) || (Pais.Count == 0)) {
                httpCod = 400;
                httpMsg = "No existe(n) Pais(es) con Idpais (" + Idpais + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            PaisResponse bodyResponse = new PaisResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Paises = Pais
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }

        public async Task<PaisResponse> ObtenerPaisesCollection(IRepositoryEntityFrameworkCQRS<Pais> paisRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Pais> Pais = ObtenerPaises(paisRepository);
            if (Pais == null)
            {
                httpCod = 400;
                httpMsg = "No existe(n) Pais(es)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            PaisResponse bodyResponse = new PaisResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Paises = Pais
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
