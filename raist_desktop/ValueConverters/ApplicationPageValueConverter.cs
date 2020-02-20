
using raist_desktop.DataModel;
using System;
using System.Diagnostics;
using System.Globalization;

namespace raist_desktop.ValueConverters
{
    class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Cari page sesuai value
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Setup:
                    return 0;
                case ApplicationPage.Main:
                    return 1;
                case ApplicationPage.Login:
                    return 2;
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
