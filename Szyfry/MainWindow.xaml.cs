using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Szyfry
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page cez = new Cezar();
        private Page play = new Playfair();
        public MainWindow()
        {
            InitializeComponent();

            UstawPlayfair();
            
        }

        private void UstawCezar()
        {
            Glowna.Content = cez;
            Cezar.Background = Brushes.DarkGray;
            Playfair.Background = Brushes.LightGray;
        }

        private void UstawPlayfair()
        {
            Glowna.Content = play;
            Cezar.Background = Brushes.LightGray;
            Playfair.Background = Brushes.DarkGray;
        }  
        
        private void ZmienNaPlayfair(object sender, RoutedEventArgs e)
        {
            if (Glowna.Content == cez)
            {
                UstawPlayfair();

            }
        }

        private void ZmienNaCezar(object sender, RoutedEventArgs e)
        {
            if (Glowna.Content == play)
            {
                UstawCezar();
            }
        }
    }
}
