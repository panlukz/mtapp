
using Xamarin.Forms;

namespace B3MobileApp.Views
{
    public partial class ActivityView : ContentPage
    {
        public ActivityView()
        {
            InitializeComponent();
            BindingContext = App.Locator.Activity;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
