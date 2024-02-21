using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarComunaModel
    {
        //Lógica Microservicio...
        public bool ActualizarComunas(List<Comuna> comunas, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository){
            foreach (Comuna comuna in comunas) {
                Comuna ComunaToBeUpdated = comunaRepository.GetAll().Where(id => id.IdComuna == comuna.IdComuna).FirstOrDefault();
                if (ComunaToBeUpdated != null) {
                    ComunaToBeUpdated.Nombre = comuna.Nombre;
                    comunaRepository.Update(ComunaToBeUpdated);
                }
            }
            if (comunaRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ComunaResponse> ActualizarComuna(ComunaBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (ActualizarComunas(objBodyObjectRequest.Comunas, comunaRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al actualizar comunas";
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
