using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;

namespace Mtapp.Pages.Containers
{
    public class CustomMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        private ContentPage _menuPage;

        protected override void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            _menuPage = new ContentPage();
            _menuPage.Title = menuPageTitle;
            ListView listView = new ListView();
            listView.ItemsSource = (IEnumerable)PageNames;
            listView.ItemSelected += (EventHandler<SelectedItemChangedEventArgs>)((sender, args) =>
            {
                if (this.Pages.ContainsKey((string)args.SelectedItem))
                    this.Detail = Pages[(string)args.SelectedItem];
                this.IsPresented = false;
            });
            this._menuPage.Content = (View)listView;
            NavigationPage navigationPage1 = new NavigationPage((Page)this._menuPage);
            navigationPage1.Title = "Menu";

            NavigationPage navigationPage2 = navigationPage1;
            if (!string.IsNullOrEmpty(menuIcon))
                navigationPage2.Icon = (FileImageSource)menuIcon;
            this.Master = (Page)navigationPage2;
        }

        public override void AddPage<T>(string title, object data = null)
        {
            Page page = FreshPageModelResolver.ResolvePageModel<T>(data);
            PageExtensions.GetModel(page).CurrentNavigationServiceName = this.NavigationServiceName;
            Page containerPage = this.CreateContainerPage(page);
            Pages.Add(title, containerPage);
            PageNames.Add(title);
            if (this.Pages.Count != 1)
                return;
            this.Detail = containerPage;
        }
    }
}
