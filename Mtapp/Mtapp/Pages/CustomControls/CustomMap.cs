using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Mtapp.Pages.CustomControls
{
    public class CustomMap : Map
    {
        #region Bindable properties

        public static readonly BindableProperty PositionsProperty = BindableProperty.Create(
            propertyName: "Positions",
            returnType: typeof(IList<ActivityPosition>),
            declaringType: typeof(CustomMap),
            defaultValue: new List<ActivityPosition>(),
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: PositionsChanged
            );

        private static void PositionsChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public IList<ActivityPosition> Positions
        {
            get { return (IList<ActivityPosition>)this.GetValue(PositionsProperty); }
            set { this.SetValue(PositionsProperty, value); }
        }

        public static readonly BindableProperty ActualPositionProperty = BindableProperty.Create(
            propertyName: "ActualPosition",
            returnType: typeof(ActivityPosition),
            declaringType: typeof(CustomMap),
            defaultValue: new ActivityPosition(),
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: ActualPositionsChanged
            );

        private static void ActualPositionsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //TODO make a follow functionality (MoveToRegion) possible to turn off
            var newPosition = (ActivityPosition)newvalue;
            var customMap = ((CustomMap)bindable);
            if (customMap.VisibleRegion != null)
                customMap.MoveToRegion(
                    MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(newPosition.Latitude, newPosition.Longitude),
                        customMap.VisibleRegion.Radius));
        }

        public ActivityPosition ActuallPosition
        {
            get { return (ActivityPosition)this.GetValue(ActualPositionProperty); }
            set { this.SetValue(ActualPositionProperty, value); }
        }

        #endregion

    }
}
