using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class HistoryPageModel : FreshBasePageModel
    {
        #region Commands

        /// <summary>
        ///     Showing details of selected activity.
        /// </summary>
        public Command ShowHistoryDetails
        {
            get
            {
                return new Command(
                    async () => { await CoreMethods.PushPageModel<HistoryDetailsPageModel>(); }/*, ShowHistoryCanExecute*/);
            }
        }

        public Command ChangeCanExecuteCommand
        {
            get
            {
                return new Command(() =>
                {
                    Decision = true;
                    ShowHistoryDetails.ChangeCanExecute();
                } );
            }
        }

        public bool Decision { get; set; }

        private bool ShowHistoryCanExecute()
        {
            if (!Decision) return false;

            return true;
        }

        #endregion
    }
}