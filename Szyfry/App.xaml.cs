﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Szyfry
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.DarkGray;
        }
    }
}
