using System;
using Phoneword.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace Phoneword.Droid
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            try
            {
                Device.OpenUri(new Uri("tel:" + number));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}