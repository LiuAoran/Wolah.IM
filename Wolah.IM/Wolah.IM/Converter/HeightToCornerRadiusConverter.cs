﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Wolah.IM.Converter
{
    class HeightToCornerRadiusConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double height = (double)value;
            double cornerRadius = height / 2; // 设置圆角半径为高度的一半

            return new CornerRadius(cornerRadius, cornerRadius, cornerRadius, cornerRadius);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
