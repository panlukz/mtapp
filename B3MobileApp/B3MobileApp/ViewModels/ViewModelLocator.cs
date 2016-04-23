/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:B3MobileApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace B3MobileApp.ViewModels
{

    public class ViewModelLocator
    {
        public const string ActivityView = "ActivityView";
        public const string MainView = "MainView";
        public const string OptionsView = "OptionsView";

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ActivityViewModel>();
            SimpleIoc.Default.Register<OptionsViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ActivityViewModel Activity
        {
            get { return ServiceLocator.Current.GetInstance<ActivityViewModel>(); }
        }

        public OptionsViewModel Options
        {
            get { return ServiceLocator.Current.GetInstance<OptionsViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}