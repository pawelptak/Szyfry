using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Szyfry
{
    /// <summary>
    /// Logika interakcji dla klasy Cezar.xaml
    /// </summary>
    public partial class Cezar : Page
    {    
        public Cezar()
        {
            InitializeComponent();
        }

        private void PrzyciskWczytaj(object sender, RoutedEventArgs e)
        {
            string szyfrogram = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true) szyfrogram = File.ReadAllText(openFileDialog.FileName);
            if (szyfrogram != null)
            {
                if (szyfrogram.Length == 0)
                {
                    MessageBox.Show("Plik tekstowy jest pusty.", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                Pole1.Text = szyfrogram;
            }

        }


        private String Szyfruj(String tekst, int klucz)
        {
            char[] tab = tekst.ToCharArray();
            if (tekst.Length == 0)
            {
                MessageBox.Show("Nie wczytano żadnego tekstu.", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                return null;
            }
            else
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    var isAlpha = char.IsLetter(tab[i]);
                    if (isAlpha) //jesli jest litera alfabetu
                    {
                        for (int j = 0; j < klucz; j++)
                        {
                            if (tab[i] == 'z') tab[i] = 'a';
                            else if (tab[i] == 'Z') tab[i] = 'A';
                            else tab[i]++;
                        }
                    }
                }
                String zwr = new String(tab);
                return zwr;
            }
        }

        private String Odszyfruj(String tekst, int klucz)
        {
            char[] tab = tekst.ToCharArray();
            if (tekst.Length == 0)
            {
                MessageBox.Show("Nie wczytano żadnego tekstu.", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                return null;
            }
            else
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    var isAlpha = char.IsLetter(tab[i]);
                    if (isAlpha) //jesli jest litera alfabetu
                    {
                        for (int j = 0; j < klucz; j++)
                        {
                            if (tab[i] == 'a') tab[i] = 'z';
                            else if (tab[i] == 'A') tab[i] = 'Z';
                            else tab[i]--;
                        }
                    }
                }
                String zwr = new String(tab);
                return zwr;
            }
        }

        private void PrzyciskSzyfruj(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Pole1.Text))
            {
                MessageBox.Show("Nie ma żadnego tekstu.", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else if (string.IsNullOrWhiteSpace(KluczBox.Text))
            {
                MessageBox.Show("Nie wybrano klucza", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else Pole2.Text = Szyfruj(Pole1.Text, Int32.Parse(KluczBox.Text));
        }

        private void PrzyciskOdszyfruj(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Pole1.Text))
            {
                MessageBox.Show("Nie ma żadnego tekstu.", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else if (string.IsNullOrWhiteSpace(KluczBox.Text))
            {
                MessageBox.Show("Nie wybrano klucza", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else Pole2.Text = Odszyfruj(Pole1.Text, Int32.Parse(KluczBox.Text));
        }

        private void Zamien(object sender, RoutedEventArgs e)
        {
            string tmp = Podpis1.Text;
            Podpis1.Text = Podpis2.Text;
            Podpis2.Text = tmp;

            switch (SzyfrujButon.IsVisible)
            {
                case true:
                    SzyfrujButon.Visibility = Visibility.Hidden;
                    OdszyfrujButon.Visibility = Visibility.Visible;
                    break;
                case false:
                    SzyfrujButon.Visibility = Visibility.Visible;
                    OdszyfrujButon.Visibility = Visibility.Hidden;
                    break;

            }
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //zezwol tylko na liczby
        private static bool CzyLiczby(string tekst)
        {
            return _regex.IsMatch(tekst);
        }

        private void TylkoLiczby(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CzyLiczby(e.Text);
        }
    }
}
