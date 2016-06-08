using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Models;

namespace Mtapp.Services
{
    public interface IAuthService
    {
        Task<string> GetToken(UserAuth userAuth);
    }
}
