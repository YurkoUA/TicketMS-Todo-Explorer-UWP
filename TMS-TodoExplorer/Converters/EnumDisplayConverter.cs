using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Data;

namespace TMS.TodoExplorer.Converters
{
    public class EnumDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var _enum = value.GetType();

            // Look for a 'Display' attribute.
            var _member = _enum
                .GetRuntimeFields()
                .FirstOrDefault(x => x.Name == value.ToString());

            if (_member == null)
            {
                return value;
            }

            var _attr = (DisplayAttribute)_member.GetCustomAttribute(typeof(DisplayAttribute));

            if (_attr == null)
            {
                return value;
            }

            return _attr.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value; // Never called
        }
    }
}
