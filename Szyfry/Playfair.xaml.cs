using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Szyfry
{
    /// <summary>
    /// Logika interakcji dla klasy Playfair.xaml
    /// </summary>
    public partial class Playfair : Page
    {    
        public Playfair()
        {
            InitializeComponent();

            DataTable dt = new DataTable();

            //wyswietlenie pustej macierzy szyfrujacej
            for (int i = 0; i < 5; i++) 
            {
                dt.Columns.Add(i.ToString(), typeof(char));
            }


            for (int i = 0; i < 5; i++)
            {
               DataRow dr = dt.NewRow();           
               dt.Rows.Add(dr);
            }

            Macierz.ItemsSource = dt.DefaultView;
        }

        private string NaMaleLitery(string slowo) //zamienia duze litery na male w slowie
        {
            char[] tab = slowo.ToCharArray();

            for (int i=0; i < tab.Length; i++)
            {
                tab[i] = Char.ToLower(tab[i]);
            }

            String zwr = new String(tab);
            return zwr;
        }

        private string ZamienJnaI(string slowo) //zamienia 'J' na 'i' w slowie
        {
            char[] tab = slowo.ToCharArray();

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == 'j' || tab[i] == 'J') tab[i] = 'i';
            }

            String zwr = new String(tab);
            return zwr;
        }

        private string UsunPowtorzenia(string slowo)
        {   
            string noweSlowo = string.Join("",slowo.ToCharArray().Distinct());   
           
            return noweSlowo;
        }

        private string UsunZbedne(string slowo) //usuwa z wyrazu spacje oraz niepotrzebne znaki
        {       
            string noweSlowo = "";

            for (int i=0; i < slowo.Length; i++)
            {
                var isAlpha = char.IsLetter(slowo[i]);
                if (isAlpha)
                {
                    noweSlowo+=slowo[i];
                    
                }
            }
            
            return noweSlowo;
        }

        private string AlfabetBezPowtorzen(string slowo, string alfabet) //usuwa z alfabetu litery ze slowa
        {
            char[] tab = slowo.ToCharArray();        
            return String.Join("", alfabet.Split(tab));
        
        }

        private char[,] TworzMacierz(string slowo) //tworzy kwadrat szyfrujacy
        {
            slowo = NaMaleLitery(slowo);
            slowo = ZamienJnaI(slowo);              
            slowo = UsunPowtorzenia(slowo);
            slowo = UsunZbedne(slowo);

            string alfabet = "abcdefghiklmnopqrstuvwxyz";
            char[,] macierz = new char [5, 5];
            int i = 0;
            int j = 0;

            for (int w = 0; w < 5; w++)
            {

                for (int k = 0; k < 5; k++, i++)
                {
                    
                    if (i >= slowo.Length) //jesli wpisalem slowo w macierz to wpisuje alfabet
                    {
                        alfabet=AlfabetBezPowtorzen(slowo, alfabet);
                        macierz[w,k] = alfabet[j];
                        j++;
                    }
                    else macierz[w, k] = slowo[i];

                }
            }
            return macierz;
        }
      
        private void WyswietlMacierz(char[,] macierz) //wyswietlenie kwadratu szyfrujacego
        {
                  
            DataTable dt = new DataTable();


            for (int i = 0; i < 5; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(char));
            }


            for (int i = 0; i < 5; i++)
            {

                DataRow dr = dt.NewRow();
                for (int j = 0; j < 5; j++) dr[j] = macierz[i, j];
                dt.Rows.Add(dr);
            }

            Macierz.ItemsSource = dt.DefaultView;

        }

        private void Mieszaj (char[,] macierz, char a, char b) //przyjmuje macierz szyfrujaca oraz pare liter i szyfruje je
        {
           

            int wiersz_a = 0;
            int kolumna_a = 0;
            int wiersz_b = 0;
            int kolumna_b = 0;

            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 5; j++)
                {
                    if (macierz[i,j] == a)
                    {
                        wiersz_a = i;
                        kolumna_a = j;
                    }
                    if (macierz[i,j] == b)
                    {
                        wiersz_b = i;
                        kolumna_b = j;
                    }

                }

            }

            if (wiersz_a == wiersz_b && kolumna_a != kolumna_b) //1. przypadek - litery w jednym wierszu
            {


                if (kolumna_a == 4) a = macierz[wiersz_a,0];
                else a = macierz[wiersz_a,kolumna_a + 1];

                if (kolumna_b == 4) b = macierz[wiersz_b,0];
                else b = macierz[wiersz_b,kolumna_b + 1];
            }
            else if (kolumna_a == kolumna_b && wiersz_a != wiersz_b) //2. przypadek - litery w jednej kolumnie
            {


                if (wiersz_a == 4) a = macierz[0,kolumna_a];
                else a = macierz[wiersz_a + 1,kolumna_a];

                if (wiersz_b == 4) b = macierz[0,kolumna_b];
                else b = macierz[wiersz_b + 1,kolumna_b];
            }
            else //3. przypadek - w roznych kolumnach i wierszach
            {
                a = macierz[wiersz_a,kolumna_b];
                b = macierz[wiersz_b,kolumna_a];
            }
            pierwsza = a;
            druga = b;
        }

        private void Odmieszaj(char[,] macierz, char a, char b) //przyjmuje macierz szyfrujaca oraz pare liter i odszyfrowuje je
        {


            int wiersz_a = 0;
            int kolumna_a = 0;
            int wiersz_b = 0;
            int kolumna_b = 0;

            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 5; j++)
                {
                    if (macierz[i, j] == a)
                    {
                        wiersz_a = i;
                        kolumna_a = j;
                    }
                    if (macierz[i, j] == b)
                    {
                        wiersz_b = i;
                        kolumna_b = j;
                    }

                }

            }

            if (wiersz_a == wiersz_b && kolumna_a != kolumna_b) //1. przypadek - litery w jednym wierszu
            {


                if (kolumna_a == 0) a = macierz[wiersz_a, 4];
                else a = macierz[wiersz_a, kolumna_a - 1];

                if (kolumna_b == 0) b = macierz[wiersz_b, 4];
                else b = macierz[wiersz_b, kolumna_b - 1];
            }
            else if (kolumna_a == kolumna_b && wiersz_a != wiersz_b) //2. przypadek - litery w jednej kolumnie
            {


                if (wiersz_a == 0) a = macierz[4, kolumna_a];
                else a = macierz[wiersz_a - 1, kolumna_a];

                if (wiersz_b == 0) b = macierz[4, kolumna_b];
                else b = macierz[wiersz_b - 1, kolumna_b];
            }
            else //3. przypadek - w roznych kolumnach i wierszach
            {
                a = macierz[wiersz_a, kolumna_b];
                b = macierz[wiersz_b, kolumna_a];
            }
            pierwsza = a;
            druga = b;
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

        private char pierwsza;
        private char druga;
        private void Szyfruj(object sender, RoutedEventArgs e)
        {
            

            string klucz = KluczBox.Text;
            string tekst = Pole1.Text; //tekst do zaszyfrowania
            string szyfrogram = ""; //zaszyfrowany tekst wyjsciowy

            char[,] macierz = TworzMacierz(klucz);

            tekst = NaMaleLitery(tekst);
            tekst = ZamienJnaI(tekst);    
            tekst = UsunZbedne(tekst);

            if (tekst.Length % 2 != 0) tekst += 'x'; //jesli dlugosc tekstu nieparzysta to na koncu dodaje 'x'

            for (int p = 0; p < tekst.Length; p += 2) //pobieranie z tekstu liter parami
            {

                pierwsza = tekst[p];
                druga = tekst[p + 1];
                if (SzyfrujButon.IsVisible) Mieszaj(macierz, pierwsza, druga);
                else Odmieszaj(macierz, pierwsza, druga);
                szyfrogram += pierwsza;
                szyfrogram += druga;               
            }

            Pole2.Text = szyfrogram;
            WyswietlMacierz(macierz);
         
        }

        private void PrzyciskWczytaj(object sender, RoutedEventArgs e)
        {
            string szyfrogram=null;

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

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //zezwol tylko na liczby
        private static bool CzyLiczby(string tekst)
        {
            return _regex.IsMatch(tekst);
        }

        private void TylkoLitery(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !CzyLiczby(e.Text);
        }
    }


}
