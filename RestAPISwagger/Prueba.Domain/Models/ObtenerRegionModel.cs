using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerRegionModel
    {
        //Lógica Microservicio...
        public List<Region_> ObtenerRegionPorId(string Idregion, IRepositoryEntityFrameworkCQRS<Region_> regionRepository)
        {
            return regionRepository.GetAll().Where(id => id.Idregion == Convert.ToInt32(Idregion)).ToList();
        }

        public List<Region_> ObtenerRegiones(IRepositoryEntityFrameworkCQRS<Region_> regionRepository)
        {
            return regionRepository.GetAll().ToList();
        }

        public async Task<RegionResponse> ObtenerRegion(string Idregion, IRepositoryEntityFrameworkCQRS<Region_> regionRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Region_> Region = ObtenerRegionPorId(Idregion, regionRepository);
            if ((Region == null) || (Region.Count == 0)) {
                httpCod = 400;
                httpMsg = "No existe(n) Region(es) con Idregion (" + Idregion + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RegionResponse bodyResponse = new RegionResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Regiones = Region
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }

        public async Task<RegionResponse> ObtenerRegionesCollection(IRepositoryEntityFrameworkCQRS<Region_> regionRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Region_> Region_ = ObtenerRegiones(regionRepository);
            if (Region_ == null)
            {
                httpCod = 400;
                httpMsg = "No existe(n) Region(es)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RegionResponse bodyResponse = new RegionResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                Regiones = Region_
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
