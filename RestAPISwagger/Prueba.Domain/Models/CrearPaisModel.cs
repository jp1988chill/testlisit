using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearPaisModel
    {
        //Lógica Microservicio...
        public bool CrearPaises(List<Pais> paises, IRepositoryEntityFrameworkCQRS<Pais> paisRepository){

            //Generamos nuevo PK para cada user automáticamente
            paisRepository.InsertMany(paises);
            if (paisRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<PaisResponse> CrearPais(PaisBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Pais> paisRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (CrearPaises(objBodyObjectRequest.Paises, paisRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar usuarios";
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
