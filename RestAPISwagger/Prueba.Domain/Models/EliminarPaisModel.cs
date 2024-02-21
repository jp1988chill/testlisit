using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarPaisModel
    {
        //Lógica Microservicio...
        public bool EliminarPaises(List<Pais> paises, IRepositoryEntityFrameworkCQRS<Pais> paisRepository){
            List<Pais> allPaises = paisRepository.GetAll().ToList();
            foreach (Pais pais in paises) {
                List<Pais> PaisesToBeDeleted = allPaises.Where(id => id.Nombre == pais.Nombre).ToList();
                foreach (Pais paisToBeDeleted in PaisesToBeDeleted)
                {
                    paisRepository.Delete(paisToBeDeleted);
                }
            }
            if (paisRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<PaisResponse> EliminarPais(PaisBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Pais> paisRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarPaises(objBodyObjectRequest.Paises, paisRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar pais(es)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            PaisResponse bodyResponse = new PaisResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Paises = objBodyObjectRequest.Paises
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
