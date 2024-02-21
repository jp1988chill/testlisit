using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarPaisModel
    {
        //Lógica Microservicio...
        public bool ActualizarPaises(List<Pais> paises, IRepositoryEntityFrameworkCQRS<Pais> paisRepository){
            foreach (Pais pais in paises) {
                Pais PaisToBeUpdated = paisRepository.GetAll().Where(id => id.Idpais == pais.Idpais).FirstOrDefault();
                if (PaisToBeUpdated != null) {
                    PaisToBeUpdated.Idpais = pais.Idpais;
                    PaisToBeUpdated.Nombre = pais.Nombre;
                    PaisToBeUpdated.Idregion = pais.Idregion;
                    paisRepository.Update(PaisToBeUpdated);
                }
            }
            if (paisRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<PaisResponse> ActualizarPais(PaisBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Pais> paisRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (ActualizarPaises(objBodyObjectRequest.Paises, paisRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al actualizar pais(es)";
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
