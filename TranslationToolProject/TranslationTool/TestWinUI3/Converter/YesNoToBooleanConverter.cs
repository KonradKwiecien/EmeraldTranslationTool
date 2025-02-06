using Microsoft.UI.Xaml.Data;
using System;

namespace TestWinUI3.Converter;

public class YesNoToBooleanConverter : IValueConverter
{

  public object Convert(object value, Type targetType, object parameter, string language)
  {
    switch (value?.ToString()?.ToLower())
    {
      case "yes":
      case "tak":
        return true;
      case "no":
      case "nie":
        return false;
    }

    return false;
  }

  public object ConvertBack(object value, Type targetType, object parameter, string language)
  {
    if (value is bool)
    {
      if ((bool)value == true)
      {
        return "yes";
      } else
        return "no";
    }

    return "no";
  }
}
