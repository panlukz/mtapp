using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class HistoryPageModel : FreshBasePageModel
    {
        public Command ShowHistoryDetails
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<HistoryDetailsPageModel>();
                });
            }
        }
    }
}
