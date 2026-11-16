
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KatalogProduktow4K
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Produkt> produkty { get; set; } = new ObservableCollection<Produkt>();

        public MainWindow()
        {
            InitializeComponent();

            KategorieComboBox.ItemsSource = new string[] { "Elektronika", "Odzież", "Książki", "Meble" };
            KategorieComboBox.SelectedIndex = 0;

            produkty.Add(new Produkt { Nazwa = "Laptop", Cena = 3500, Ilosc = 5, Kategoria = "Elektronika" });
            produkty.Add(new Produkt { Nazwa = "Koszula", Cena = 120, Ilosc = 10, Kategoria = "Odzież" });

            DataContext = this;
        }

        private void Oblicz(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(CenaTextBox.Text, out double cena) || cena < 0)
            {
                WynikTextBlock.Content = "Niepoprawna cena";
                return;
            }

            if (!int.TryParse(IloscTextBox.Text, out int ilosc) || ilosc < 0)
            {
                WynikTextBlock.Content = "Niepoprawna ilość";
                return;
            }

            string kategoria = KategorieComboBox.SelectedItem as string ?? "Brak";

            double wartosc = cena * ilosc;

            if (PromocjaCheckBox.IsChecked == true)
            {
                wartosc *= 0.9;
            }

            WynikTextBlock.Content = $"{NazwaTextBox.Text} ({kategoria}): {wartosc:F2} zł";
        }

        private void DodajProdukt(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(CenaTextBox.Text, out double cena) || cena < 0)
            {
                MessageBox.Show("Niepoprawna cena");
                return;
            }

            if (!int.TryParse(IloscTextBox.Text, out int ilosc) || ilosc < 0)
            {
                MessageBox.Show("Niepoprawna ilość");
                return;
            }

            if (string.IsNullOrWhiteSpace(NazwaTextBox.Text))
            {
                MessageBox.Show("Podaj nazwę produktu");
                return;
            }

            string kategoria = KategorieComboBox.SelectedItem as string ?? "Brak";

            produkty.Add(new Produkt
            {
                Nazwa = NazwaTextBox.Text,
                Cena = cena,
                Ilosc = ilosc,
                Kategoria = kategoria
            });
            WartoscMagazynu();
        }

        private void UsunProdukt(object sender, RoutedEventArgs e)
        {
            if (ListaProduktow.SelectedItem is Produkt wybranyProdukt)
            {
                MessageBoxResult wynik = 
                    MessageBox.Show($"Czy na pewno chcesz usunąć produkt: {wybranyProdukt.Nazwa}?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(wynik == MessageBoxResult.Yes)
                {
                    produkty.Remove(wybranyProdukt);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano produktu do usunięcia.");
            }
            WartoscMagazynu();
        }

        private void ListaProduktow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaProduktow.SelectedItem is Produkt produkt)
            {
                InformacjeProduktow.ItemsSource = new[]
                {
            new { Pole = "Nazwa", Wartosc = produkt.Nazwa },
            new { Pole = "Cena", Wartosc = produkt.Cena + " zł" },
            new { Pole = "Ilość", Wartosc = produkt.Ilosc.ToString() },
            new { Pole = "Kategoria", Wartosc = produkt.Kategoria },
            new { Pole = "Wartość", Wartosc = (produkt.Cena * produkt.Ilosc).ToString("F2") + " zł" }
        };
            }
            else
            {
                InformacjeProduktow.ItemsSource = null;
            }
        }

        private void PokazZaznaczony (object sednder, SelectionChangedEventArgs e)
        {
            InformacjeProduktow.Items.Clear();

            if (ListaProduktow.SelectedItem is Produkt produkt)
            {
                InformacjeProduktow.Items.Add(new { Pole = "Nazwa", Wartosc = produkt.Nazwa });
                InformacjeProduktow.Items.Add(new { Pole = "Cena", Wartosc = produkt.Cena.ToString() });
                InformacjeProduktow.Items.Add(new { Pole = "Ilość", Wartosc = produkt.Ilosc.ToString() });
                InformacjeProduktow.Items.Add(new { Pole = "Kategoria", Wartosc = produkt.Kategoria });
            }
            WartoscMagazynu();
        }

        private void Zapisz(object sender, RoutedEventArgs e)
        {
            if(ListaProduktow.SelectedItem is not Produkt p) return;
            if (!double.TryParse(CenaTextBox.Text, out double cena) || cena < 0) ;
            if (!int.TryParse(IloscTextBox.Text, out int ilosc) || ilosc < 0) ;
            string nazwa = NazwaTextBox.Text;
            WartoscMagazynu();
        }


        private void EdytujProdukt(object sender, RoutedEventArgs e)
        {
            if (ListaProduktow.SelectedItem is Produkt produkt)
            {
                if (double.TryParse(CenaTextBox.Text, out double cena) &&
                    int.TryParse(IloscTextBox.Text, out int ilosc))
                {
                    produkt.Nazwa = NazwaTextBox.Text;
                    produkt.Cena = cena;
                    produkt.Ilosc = ilosc;
                    produkt.Kategoria = KategorieComboBox.Text;

                    WynikTextBlock.Content = "Produkt zaktualizowany";
                }
                else
                {
                    MessageBox.Show("Podaj poprawną cenę i ilość");
                }
            }
            else
            {
                MessageBox.Show("Wybierz produkt z listy");
            }
            WartoscMagazynu();
        }

        private void WartoscMagazynu()
        {
            double suma = 0;

            foreach (Produkt produkt in produkty)
            {
                suma += produkt.Cena * produkt.Ilosc;
            }

            WartoscMagazynuText.Text = "Wartość magazynu: " + suma + " zł";
        }

    }
}
