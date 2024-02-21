using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarComunaModel
    {
        //Lógica Microservicio...
        public bool EliminarComunas(List<Comuna> comunas, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository){
            List<Comuna> allComunas = comunaRepository.GetAll().ToList();
            foreach (Comuna comuna in comunas) {
                List<Comuna> ComunasToBeDeleted = allComunas.Where(id => id.IdComuna == comuna.IdComuna).ToList();
                foreach (Comuna comunaToBeDeleted in ComunasToBeDeleted)
                {
                    comunaRepository.Delete(comunaToBeDeleted);
                }
            }
            if (comunaRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ComunaResponse> EliminarComuna(ComunaBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarComunas(objBodyObjectRequest.Comunas, comunaRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar comuna(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Comunas = objBodyObjectRequest.Comunas
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
