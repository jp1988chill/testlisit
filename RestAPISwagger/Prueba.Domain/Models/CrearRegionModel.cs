using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearRegionModel
    {
        //Lógica Microservicio...
        public bool CrearRegiones(List<Region_> regiones, IRepositoryEntityFrameworkCQRS<Region_> regionRepository){

            //Generamos nuevo PK para cada user automáticamente
            regionRepository.InsertMany(regiones);
            if (regionRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<RegionResponse> CrearRegion(RegionBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Region_> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (CrearRegiones(objBodyObjectRequest.Regiones, userRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar regiones";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RegionResponse bodyResponse = new RegionResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Regiones = objBodyObjectRequest.Regiones
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
