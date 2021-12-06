using Prueba.UnitTests.Core.Enveloped;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.UnitTests.Core.HandlingObjectGeneric
{
    public class ComposeObject : IComposeObject
    {
        public async Task<EnvelopedObject.Enveloped> GetObject<T>(T Obj, string iniDate)
        {

            string nameClass = string.Empty;

            try
            {
                nameClass = Obj.GetType().GetProperty("Item").PropertyType.Name;
            }
            catch
            {
                nameClass = Obj.GetType().Name;
            }

            dynamic DynamicObj;
            DynamicObj = new ExpandoObject();

            switch (nameClass)
            {
                case "Reembolso":
                    DynamicObj.Reembolso = Obj;
                    break;
                case "RespuestaSolicitud":
                    DynamicObj.RespuestaSolicitud = Obj;
                    break;
                case "TipoDocumento":
                    DynamicObj.TipoDocumento = Obj;
                    break;
                case "TipoPrestacion":
                    DynamicObj.TipoPrestacion = Obj;
                    break;
                default:
                    DynamicObj = Obj;
                    break;
            }

            return new EnvelopedObject.Enveloped
            {
                header = new EnvelopedObject.Header
                {
                    transactionData = new EnvelopedObject.Transactiondata
                    {
                        idTransaction = "WEB2019043000000100",
                        startDate = iniDate,
                        endDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                    }
                },
                body = DynamicObj
            };
        }
    }
}
