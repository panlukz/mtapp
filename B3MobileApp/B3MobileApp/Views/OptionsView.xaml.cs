
using Xamarin.Forms;

namespace B3MobileApp.Views
{
    public partial class OptionsView : ContentPage
    {
        public OptionsView()
        {
            InitializeComponent();
            BindingContext = App.Locator.Options;
        }
    }
}
