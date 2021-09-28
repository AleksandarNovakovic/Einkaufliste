using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Einkaufliste
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static ObservableCollection<Artikel> einkaufsliste = new ObservableCollection<Artikel>();
        public const string einkaufslisteKey = "Einkaufsliste";

        public int Anzahl { get; set; }

        public MainPage()
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey(MainPage.einkaufslisteKey))
            {
                LadeneEinkaufsliste();
            }
        }

        private void BtnArtikelliste_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArtikellistePage());
        }

        async private void ListViewEinkaufsliste_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (einkaufsliste.Count > 1)
            {
                Artikel alteArtikel = new Artikel();

                alteArtikel = (Artikel)e.Item;
                einkaufsliste.Remove(alteArtikel);
                ListViewEinkaufsliste.ItemsSource = einkaufsliste;

                string jsonString = JsonConvert.SerializeObject(MainPage.einkaufsliste);
                Application.Current.Properties[MainPage.einkaufslisteKey] = jsonString;
            }
            else
            {
                einkaufsliste.Clear();
                Application.Current.Properties.Remove(MainPage.einkaufslisteKey);
            }
            await Application.Current.SavePropertiesAsync();
        }

        protected override void OnAppearing()
        {
            ListViewEinkaufsliste.ItemsSource = einkaufsliste;
        }

        async private void LadeneEinkaufsliste()
        {
            if (await DisplayAlert("Frage?", "Liste laden?", "JA", "NEIN"))
            {
                string jsonString = Application.Current.Properties[MainPage.einkaufslisteKey] as string;
                einkaufsliste = JsonConvert.DeserializeObject<ObservableCollection<Artikel>>(jsonString);
            }
            else
            {
                einkaufsliste.Clear();
                Application.Current.Properties.Remove(MainPage.einkaufslisteKey);
                await Application.Current.SavePropertiesAsync();
            }

            ListViewEinkaufsliste.ItemsSource = einkaufsliste;
        }
    }
}
