using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Uplatnica
{
    /// <summary>
    /// Interaction logic for UplatnicaUserControl.xaml
    /// </summary>
    public partial class UplatnicaUserControl : UserControl
    {
        public UplatnicaUserControl()
        {
            InitializeComponent();
            //Parsira string odredjenog formata (broj sa dve decimale) u double vrednost
            //Vise informacija na https://www.wpf-tutorial.com/data-binding/the-stringformat-property/
            //Ili na https://docs.microsoft.com/en-us/dotnet/api/system.string.format?view=netframework-4.7.2
            iznosTextBox.Text = string.Format("{0:#,##0.00}", double.Parse("0")); 
        }
        //Ukoliko nam treba, mozemo da nesto radimo sa eventom kada se tekst promeni
        private void IznosTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
            }
            catch
            {
                //Ovde mozemo errore da ispisujemo na ekran kao naprimer MessageBox.Show("neki string koji opisuje problem")
            }
        }
        

        private void IznosTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //If u ovom slucaju proverava da li je ostavljeno prazno polje ili sadrzi samo razmake u iznosTextBox
            //Vise informacija na https://docs.microsoft.com/en-us/dotnet/api/system.string.isnullorwhitespace?view=netframework-4.7.2
            if (!string.IsNullOrWhiteSpace(iznosTextBox.Text))
            {
                //Vise informacija za double.Parse na https://docs.microsoft.com/en-us/dotnet/api/system.double.parse?view=netframework-4.7.2
                iznosTextBox.Text = string.Format("{0:#,##0.00}", double.Parse(iznosTextBox.Text));
            }
        }

        public void ResetFields()
        {
            uplatilacTextBox.Text = "";
            adresaUplatiocaTextBox.Text = "";
            svrhaTextBox.Text = "";
            primalacTextBox.Text = "";
            adresaPrimaocaTextBox.Text = "";
            sifraTextBox.Text = "";
            valutaTextBox.Text = "RSD";
            iznosTextBox.Text = string.Format("{0:#,##0.00}", double.Parse("0"));
            racunTextBox.Text = "";
            modelTextBox.Text = "";
            pozivTextBox.Text = "";
            mestoTextBox.Text = "";
            //DateTime.Today nam uzima trenutni datum iz racunara
            //Vise informacija na https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=netframework-4.7.2
            datum.SelectedDate = DateTime.Today;
        }

        public void SaveFields(UplatnicaTemp Temp)
        {
            Temp.UplatilacTextBox = uplatilacTextBox.Text;
            Temp.AdresaUplatiocaTextBox = adresaUplatiocaTextBox.Text;
            Temp.SvrhaTextBox = svrhaTextBox.Text;
            Temp.PrimalacTextBox = primalacTextBox.Text;
            Temp.AdresaPrimaocaTextBox = adresaPrimaocaTextBox.Text;
            Temp.SifraTextBox = sifraTextBox.Text;
            //U ovom slucaju valutu ne moramo da cuvamo zato sto smo stavili da ona bude staticna sa valutaTextBox.Text = "RSD";
            //Vise na https://docs.microsoft.com/en-us/dotnet/api/system.decimal.parse?view=netframework-4.7.2
            Temp.IznosTextBox = decimal.Parse(iznosTextBox.Text);
            Temp.RacunTextBox = racunTextBox.Text;
            Temp.ModelTextBox = modelTextBox.Text;
            Temp.PozivTextBox = pozivTextBox.Text;
            Temp.MestoTextBox = mestoTextBox.Text;
            Temp.Datum = datum.SelectedDate;
        }

        public void UpdateFields(UplatnicaTemp Temp)
        {

            uplatilacTextBox.Text = Temp.UplatilacTextBox;
            adresaUplatiocaTextBox.Text = Temp.AdresaUplatiocaTextBox;
            svrhaTextBox.Text = Temp.SvrhaTextBox;
            primalacTextBox.Text = Temp.PrimalacTextBox;
            adresaPrimaocaTextBox.Text = Temp.AdresaPrimaocaTextBox;
            sifraTextBox.Text = Temp.SifraTextBox;
            valutaTextBox.Text = Temp.ValutaTextBox;
            iznosTextBox.Text = string.Format("{0:#,##0.00}", double.Parse(Temp.IznosTextBox.ToString()));
            racunTextBox.Text = Temp.RacunTextBox;
            modelTextBox.Text = Temp.ModelTextBox;
            pozivTextBox.Text = Temp.PozivTextBox;
            mestoTextBox.Text = Temp.MestoTextBox;
            datum.SelectedDate = Temp.Datum.Value;
        }
        //Vrsi validaciju polja
        public bool TestFields()
        {
            //Ukoliko je sve dobro, verificationFields ostaje true i program dozvoljava da se upise u fajl
            bool verificationFields = true;
            //Ovo nam je brojac koji nam govori koliko nepravilno unesenih polja imamo, mada ovo se moze preskociti jer se ovo koristi najvise kod debugovanja
            int numberOfErrors = 0;

            //Leva strana
            if (string.IsNullOrWhiteSpace(uplatilacTextBox.Text))
            {
                //Ukoliko nije validno postavlja na rozikastu boju (FF)F9DEDE gde je FF Alpha tj. transparentnost
                //Vise informacija na https://docs.microsoft.com/en-us/dotnet/api/system.drawing.color.fromargb?view=netframework-4.7.2#System_Drawing_Color_FromArgb_System_Int32_System_Int32_System_Int32_System_Int32_
                uplatilacTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            //Vise informacija za SystemColors na https://devblogs.microsoft.com/wpf/systemcolors-reference/
            else { uplatilacTextBox.Background = SystemColors.HighlightTextBrush; }

            if (string.IsNullOrWhiteSpace(adresaUplatiocaTextBox.Text))
            {
                adresaUplatiocaTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { adresaUplatiocaTextBox.Background = SystemColors.HighlightTextBrush; }

            if (string.IsNullOrWhiteSpace(svrhaTextBox.Text))
            {
                svrhaTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { svrhaTextBox.Background = SystemColors.HighlightTextBrush; }

            if (string.IsNullOrWhiteSpace(primalacTextBox.Text))
            {
                primalacTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { primalacTextBox.Background = SystemColors.HighlightTextBrush; }

            if (string.IsNullOrWhiteSpace(adresaPrimaocaTextBox.Text))
            {
                adresaPrimaocaTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { adresaPrimaocaTextBox.Background = SystemColors.HighlightTextBrush; }

            // Desna strana
            if (string.IsNullOrWhiteSpace(sifraTextBox.Text))
            {
                sifraTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { sifraTextBox.Background = SystemColors.HighlightTextBrush; }
            try
            {
                //Ovde se vrsi dodatna provera da li je manji ili jednak nuli i ako jeste u catch bloku ispisujemo gresku
                if ((string.IsNullOrWhiteSpace(iznosTextBox.Text)) || (decimal.Parse(iznosTextBox.Text) <= 0))
                {
                    iznosTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                    numberOfErrors += 1;
                    verificationFields = false;
                }
                else { iznosTextBox.Background = SystemColors.HighlightTextBrush; }
            }
            catch
            {
                iznosTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                //Ovde moze da stoji opis greske koja god vama odgovara da saopstite korisniku ili sebi prilikom debugovanja
                MessageBox.Show("Error!");
            }

            if (string.IsNullOrWhiteSpace(racunTextBox.Text) || TestRacunTextBox() == false) 
            {
                racunTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { racunTextBox.Background = SystemColors.HighlightTextBrush; }

            /*if (string.IsNullOrWhiteSpace(txtRefModel.Text))
            {     txtRefModel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));       }
            else  {  txtRefModel.Background = SystemColors.HighlightTextBrush;   }*/

            if (string.IsNullOrWhiteSpace(pozivTextBox.Text))
            {
                pozivTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { pozivTextBox.Background = SystemColors.HighlightTextBrush; }

            if (string.IsNullOrWhiteSpace(mestoTextBox.Text))
            {
                mestoTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE));
                numberOfErrors += 1;
                verificationFields = false;
            }
            else { mestoTextBox.Background = SystemColors.HighlightTextBrush; }

            return verificationFields;
        }

        //Ovde za validaciju u startu ne dozvoljavamo korisniku da unese bilo sta drugo osim brojeva
        private void ModelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            //Za vise informacija kako Key funkcionise: https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.key?view=netframework-4.7.2
            e.Handled = !(key >= 34 && key <= 43 || //Key = 34 je 0, Key = 43 je 9
                          key >= 74 && key <= 83 || //Key = 74 je 0 na numerickoj tastaturi, Key = 83 je 9 na numerickoj tastaturi
                          e.Key == Key.Tab || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right);
        }
        //Tokom testiranja, izgleda da ova metoda ne radi kako treba
        private void PozivTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;

            e.Handled = !(key >= 34 && key <= 43 ||
                          key >= 74 && key <= 83 ||
                          //Key.Subtract nam je crtica (-)
                          e.Key == Key.Subtract && FindAllChar('-', pozivTextBox.Text) < 5 || //<-mora biti n crtica, mada bilo koji broj da stavim ovo nece iz nekog razloga raditi
                          e.Key == Key.Tab || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right);
        }

        private void RacunTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            if (racunTextBox.Text.Length - FindAllChar('-', racunTextBox.Text) < 18 || //<- mora biti 18 cifara
                      e.Key == Key.Tab || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right)
            {
                e.Handled = !(key >= 34 && key <= 43 ||
                            key >= 74 && key <= 83 ||
                            e.Key == Key.Subtract && FindAllChar('-', racunTextBox.Text) < 2 || //<-mora biti 2 crtice
                            e.Key == Key.Tab || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right);
            }
            else
            {
                e.Handled = true;
            }
        }

        private void SifraTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || 
                              key >= 74 && key <= 83 ||
                              e.Key == Key.Tab || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right);
        }

        private void IznosTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //CultureInfo je biblioteka koja sadrzi razne informacije o valutama, datumima itd. jedne drzave i dodeljujemo nacin pisanja valute na varijablu strTacka
            //Vise informacija na https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo?view=netframework-4.7.2
            string strTacka = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            char chrTacka = strTacka[0];

            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 ||
                          key >= 74 && key <= 83 ||
                          key == 88 && FindAllChar(chrTacka, iznosTextBox.Text) < 1 ||
                          key == 144 && FindAllChar(chrTacka, iznosTextBox.Text) < 1 ||
                          e.Key == Key.Tab || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right);
        }

        
        private void RacunTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TestRacunTextBox() == false)
            { racunTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xDE, 0xDE)); }
            else { racunTextBox.Background = SystemColors.HighlightTextBrush; }

        }
        //Metoda koja pretrazuje sve karaktere za zadati string
        //Ukoliko zelite vise informacija, ovaj link moze dosta pomoci https://stackoverflow.com/questions/2641326/finding-all-positions-of-substring-in-a-larger-string-in-c-sharp/2641383
        static int FindAllChar(Char target, String searched)
        {
            int startIndex = -1;
            int hitCount = 0;

            // Trazi sve u stringu.
            while (true)
            {
                startIndex = searched.IndexOf(
                    target, startIndex + 1,
                    searched.Length - startIndex - 1);

                // Izadji iz petlje kada nije nasao.
                if (startIndex < 0)
                    break;

                //Console.Write("{0}, ", startIndex);
                hitCount++;
            }

            return hitCount;
        }
        //Ukoliko Racun nije korektno upisan, polje ce se samo popuniti kako bi se zadovoljio uslov da sadrzi 18 cifara i 2 crtice
        private bool TestRacunTextBox()
        {
            try
            {
                //Prve 3 cifre su obavezne, a ostalo moze da se automatski popuni sa nulama
                string temp = racunTextBox.Text;
                int t;
                //c dobija broj crtica
                int c = FindAllChar('-', temp);
                if (c > 0)
                {
                    for (uint i = 0; i < c; i++)
                    {
                        //
                        t = temp.IndexOf('-');
                        temp = temp.Remove(t, 1);
                    }
                }
                //Gledamo razliku izmedju naseg stringa i ocekivanog stringa sa 18 karaktera
                int a = 18 - temp.Length;
                if (temp.Length > 2)
                {
                    //Sve sto nedostaje zamenjuje se sa nulama
                    for (uint i = 0; i < a; i++)
                    {
                        temp = temp.Insert(3, "0");
                    }
                    //Na kraju se na poziciji 3 i 17 stavljaju crtice
                    temp = temp.Insert(3, "-");
                    temp = temp.Insert(17, "-");
                }
                else
                {
                    return false;
                }
                //Zamenjujemo temp u racunTextBox ukoliko je nepravilno unesen racun, u suprotnom vracamo isti string
                racunTextBox.Text = temp;
                return true;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return false;
            }
        }

    }
}