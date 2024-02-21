using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarRegionModel
    {
        //Lógica Microservicio...
        public bool ActualizarRegiones(List<Region_> regiones, IRepositoryEntityFrameworkCQRS<Region_> regionRepository){
            foreach (Region_ region in regiones) {
                Region_ RegionToBeUpdated = regionRepository.GetAll().Where(id => id.Idregion == region.Idregion).FirstOrDefault();
                if (RegionToBeUpdated != null) {
                    RegionToBeUpdated.Nombre = region.Nombre;
                    RegionToBeUpdated.Idcomuna = region.Idcomuna;
                    regionRepository.Update(RegionToBeUpdated);
                }
            }
            if (regionRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<RegionResponse> ActualizarRegion(RegionBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Region_> regionRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (ActualizarRegiones(objBodyObjectRequest.Regiones, regionRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al actualizar region(es)";
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
