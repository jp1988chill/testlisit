using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain
{
    public interface IAppSettingsRepository
    {
        string GetRestAPIPath();
        string GetRestAPIStubUser();
        string GetRestAPIStubPassword();
    }
}
