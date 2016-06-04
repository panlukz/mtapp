using System.Threading.Tasks;
using Mtapp.Models;

namespace Mtapp.Services
{
    public interface IActivityDataService
    {
        Task Add(Activity activity);
    }
}