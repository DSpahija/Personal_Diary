using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PersonalDiary.Custom
{
    class DisablingControl : Button
    {
        private Style _normalStyle;

        public static readonly BindableProperty DisabledStyleProperty =
            BindableProperty.Create(nameof(DisabledStyle), typeof(Style), typeof(DisablingControl), null, BindingMode.TwoWay, null, (obj, oldValue, newValue) => { });

        public Style DisabledStyle
        {
            get { return (Style)GetValue(DisabledStyleProperty); }
            set { SetValue(DisabledStyleProperty, value); }
        }

        public DisablingControl()
        {
            _normalStyle = Style;

            PropertyChanged += ExtendedButton_PropertyChanged;

        }

        private void ExtendedButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsEnabled) && DisabledStyle != null)
            {
                if (IsEnabled)
                    Style = _normalStyle;
                else
                    Style = DisabledStyle;
            }
        }
    }
}