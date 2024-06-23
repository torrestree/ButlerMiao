using Core.ViewModel.Common;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HelperWpf.Converter
{
    public class MessageType2TitleStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is VmMessage.MessageTypes messageType)
            {
                if (parameter is IEnumerable<Style> styles)
                {
                    return messageType switch
                    {
                        VmMessage.MessageTypes.Success => styles.ElementAt(1),
                        VmMessage.MessageTypes.Warning => styles.ElementAt(2),
                        VmMessage.MessageTypes.Failure => styles.ElementAt(3),
                        VmMessage.MessageTypes.Error => styles.ElementAt(3),
                        VmMessage.MessageTypes.OkCancel => styles.ElementAt(4),
                        VmMessage.MessageTypes.YesNoCancel => styles.ElementAt(4),
                        _ => styles.ElementAt(0),
                    };
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
