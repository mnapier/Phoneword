using System;
using Xamarin.Forms;

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        private readonly Entry _phoneNumberEntry;
        private readonly Button _translateButton;
        private readonly Button _callButton;

        private string _translatedNumber;

        public MainPage()
        {
            Padding = new Thickness(20, Device.OnPlatform<double>(40, 20, 20), 20, 20);

            StackLayout panel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15,
            };

            panel.Children.Add(new Label
            {
                Text = "Enter a Phoneword:",
            });

            panel.Children.Add(_phoneNumberEntry = new Entry
            {
                Text = "1-855-XAMARIN"
            });

            panel.Children.Add(_translateButton = new Button
            {
                Text = "Translate"
            });

            panel.Children.Add(_callButton = new Button
            {
                Text = "Call",
                IsEnabled = false
            });

            
            _translateButton.Clicked += OnTranslate;
            _callButton.Clicked += OnCall;

            Content = panel;
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = _phoneNumberEntry.Text;
            _translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrWhiteSpace(_translatedNumber))
            {
                _callButton.IsEnabled = true;
                _callButton.Text = "Call " + _translatedNumber;
            }
            else
            {
                _callButton.IsEnabled = false;
                _callButton.Text = "Call";
            }
        }

        private async void OnCall(object sender, EventArgs e)
        {
            if (!await DisplayAlert(
                "Dial a Number",
                "Would you like to call " + _translatedNumber + "?",
                "Yes",
                "No"))
            {
                return;
            }

            var dialer = DependencyService.Get<IDialer>();

            dialer?.Dial(_translatedNumber);
        }
    }
}
