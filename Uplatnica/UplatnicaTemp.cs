using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplatnica
{
    //Potrebno je staviti modifikator Public kako bi UplatnicaUserControl imala pristup ovoj klasi
    public class UplatnicaTemp
    {
        public string UplatilacTextBox;
        public string AdresaUplatiocaTextBox;
        public string SvrhaTextBox;
        public string PrimalacTextBox;
        public string AdresaPrimaocaTextBox;
        public string SifraTextBox;
        //Ovde nam valutaTextBox uvek vraca RSD
        public string ValutaTextBox
        {
            get { return "RSD"; }
        }
        public decimal IznosTextBox;
        public string RacunTextBox;
        public string ModelTextBox;
        public string PozivTextBox;
        public string MestoTextBox;
        public DateTime? Datum;

        //Troskovi obracuna (ovo moze tu da stoji, a i ne mora)
        public decimal Fee(decimal x)
        {
            return x * 0.01m;
        }
    }
}
