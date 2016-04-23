using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = B3MobileApp.Model.Position;

namespace B3MobileApp.Views.CustomControls
{
    public class CustomMap : Map
    {
        #region Bindable properties

        public static readonly BindableProperty PositionsProperty = BindableProperty.Create(
            propertyName: "Positions",
            returnType: typeof (IList<Position>),
            declaringType: typeof (CustomMap),
            defaultValue: new List<Position>(),
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: PositionsChanged
            );

        private static void PositionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public IList<Position> Positions
        {
            get { return (IList<Position>) this.GetValue(PositionsProperty); }
            set { this.SetValue(PositionsProperty, value); }
        }

        public static readonly BindableProperty ActuallPositionProperty = BindableProperty.Create(
            propertyName: "ActuallPosition",
            returnType: typeof (Position),
            declaringType: typeof (CustomMap),
            defaultValue: new Position(),
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: ActuallPositionsChanged
            );

        private static void ActuallPositionsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //TODO make a follow functionality (MoveToRegion) possible to turn off
            var newPosition = (Position) newvalue;
            var customMap = ((CustomMap) bindable);
            if (customMap.VisibleRegion != null)
                customMap.MoveToRegion(
                    MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(newPosition.Latitude, newPosition.Longitude),
                        customMap.VisibleRegion.Radius));
        }

        public Position ActuallPosition
        {
            get { return (Position) this.GetValue(ActuallPositionProperty); }
            set { this.SetValue(ActuallPositionProperty, value); }
        }

        #endregion


        public CustomMap()
        {
        }

    }
}