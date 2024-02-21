using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarRegionModel
    {
        //Lógica Microservicio...
        public bool EliminarRegiones(List<Region_> regiones, IRepositoryEntityFrameworkCQRS<Region_> regionRepository){
            List<Region_> allRegiones = regionRepository.GetAll().ToList();
            foreach (Region_ region in regiones) {
                List<Region_> RegionesToBeDeleted = allRegiones.Where(id => id.Idregion == region.Idregion).ToList();
                foreach (Region_ regionToBeDeleted in RegionesToBeDeleted)
                {
                    regionRepository.Delete(regionToBeDeleted);
                }
            }
            if (regionRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<RegionResponse> EliminarRegion(RegionBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Region_> regionRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarRegiones(objBodyObjectRequest.Regiones, regionRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar usuario(s)";
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
