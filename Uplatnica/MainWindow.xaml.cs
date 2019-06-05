using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Pravljenje novog objekta koji ce se dalje koristiti
        UplatnicaTemp NalogZaUplatu = new UplatnicaTemp();

        public MainWindow()
        {
            InitializeComponent();
            UplatnicaUserControl2.valutaTextBox.Text = NalogZaUplatu.ValutaTextBox;
            UplatnicaUserControl2.datum.SelectedDate = DateTime.Today;
            UplatnicaUserControl2.iznosTextBox.Text = string.Format("{0:#,##0.00}", double.Parse("0"));

        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            UplatnicaUserControl2.ResetFields();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            UplatnicaUserControl2.ResetFields();
            NalogZaUplatu = ImportFromXML();
            if (NalogZaUplatu != null)
            {
                UplatnicaUserControl2.UpdateFields(NalogZaUplatu);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {

            if (UplatnicaUserControl2.TestFields())
            {
                UplatnicaUserControl2.SaveFields(NalogZaUplatu);
                ExportToXML(NalogZaUplatu);
                MessageBox.Show("Nalog za uplatu je uspešno poslat.");
            }
            else
            {
                MessageBox.Show("Popunite pravilno sva crvena polja.");
            }

        }


        //Kada god se koristi upis u neku vrstu fajla, potrebno je otvoriti stream ukoliko je to moguce, u suprotnom, u catch bloku izbaci error ili slicno
        private static void ExportToXML(UplatnicaTemp temp)
        {
            try
            {
                //Pravimo novi objekat pod imenom writer koji ce sva polja od UplatnicaTemp
                //Vise informacija na https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=netframework-4.7.2
                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(typeof(UplatnicaTemp));
                //Sa ovim cemo pokupiti trenutno vreme i u naziv fajla cemo ga ispisati
                string n = string.Format("text-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                //Sa ovim stavljamo putanju na desktop sa vec predefinisanim nazivom, vremenom i ekstenzijom
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//NalogZaUplatu" + n + ".xml";
                //Naredba za upisivanje u fajl
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, temp);
                file.Close();
            }
            catch
            {

            }
        }
        //Ucitavanj se isto radi kao i upisivanje stim sto se radi u obrnutom smeru mada vec C# ovo radi automatski za vas
        private static UplatnicaTemp ImportFromXML()
        {
            string filePath = String.Empty;
            UplatnicaTemp temp = null;
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    //Sa ovim filtriramo samo xml fajlove da na se prikazuju u dijalogu
                    Filter = "xml file (*.xml)|*.xml",
                    //Naziv dijaloga
                    Title = "Odaberite Nalog za uplatu"
                };
                //Ukoliko se otvorio dijalog, ucitaj mi podatke iz zadatog fajla
                if (openFileDialog1.ShowDialog() == true)
                {
                    filePath = openFileDialog1.FileName;
                    System.Xml.Serialization.XmlSerializer reader =
                           new System.Xml.Serialization.XmlSerializer(typeof(UplatnicaTemp));
                    //var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//NalogZaUplatu.xml";
                    System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                    temp = (UplatnicaTemp)reader.Deserialize(file);
                    file.Close();
                    return temp;
                }

                return temp;
            }
            catch
            {
                MessageBox.Show("Došlo je do greške prilikom učitavanja fajla!");
                temp = null;
                return temp;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UplatnicaUserControl2.TestFields();
        }

        private void UcPaymentOrderForm2_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
