using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Core.Domain;

namespace WS.Manager.Interfaces.Services
{
    public interface IJWTService
    {
        string CreateToken(AspNetUser aspNetUser);
    }
}
