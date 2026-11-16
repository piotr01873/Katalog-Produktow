
using System.ComponentModel;

namespace KatalogProduktow4K
{
    public class Produkt : INotifyPropertyChanged
    {
        private string nazwa = "";
        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; Zmiana(nameof(Nazwa)); }
        }

        private double cena;
        public double Cena
        {
            get { return cena; }
            set { cena = value < 0 ? 0 : value; Zmiana(nameof(Cena)); }
        }

        private int ilosc;
        
        public int Ilosc {
            get { return ilosc; }
            set { ilosc = value < 0 ? 0 : value; Zmiana(nameof(Ilosc)); }
        }

        private string kategoria = "";
        public string Kategoria
        {
            get { return kategoria; }
            set { kategoria = value; Zmiana(nameof(Kategoria)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Zmiana(string nazwaWlasciwosci)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwaWlasciwosci));
        }
    }
}
