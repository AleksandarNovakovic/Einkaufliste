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

        public static ObservableCollection<Warenkorb> warenkorbliste = new ObservableCollection<Warenkorb>();

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

        //private void WarenkorbFuelen()
        //{
        //    warenkorbliste.Clear();
        //    const int anzahl = 1;
        //    Warenkorb wk = new Warenkorb();


        //    for (int i = 0; i < einkaufsliste.Count; i++)
        //    {
        //        if (einkaufsliste.Count == 0)
        //        {
        //            break;
        //        }

        //        if (warenkorbliste.Count == 0)
        //        {
        //            wk.Name = einkaufsliste[i].Name;
        //            wk.Beschreibung = einkaufsliste[i].Beschreibung;
        //            wk.Bild = einkaufsliste[i].Bild;
        //            wk.Anzahl = anzahl;
        //            warenkorbliste.Add(wk);

        //        }
        //        else
        //        {
        //            for (int j = 0; j < warenkorbliste.Count; j++)
        //            {
        //                if (warenkorbliste[j].Name == einkaufsliste[i].Name)
        //                {
        //                    warenkorbliste[j].Anzahl += 1;
                           
        //                }
        //                else
        //                {
        //                    wk.Name = einkaufsliste[i].Name;
        //                    wk.Beschreibung = einkaufsliste[i].Beschreibung;
        //                    wk.Bild = einkaufsliste[i].Bild;
        //                    wk.Anzahl = anzahl;
        //                    warenkorbliste.Add(wk);
        //                }
        //            }
        //        }
        //    }

        //}


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
            //WarenkorbFuelen();
            //ListViewEinkaufsliste.ItemsSource = warenkorbliste;

            ListViewEinkaufsliste.ItemsSource = einkaufsliste;
        }

        async private void LadeneEinkaufsliste()
        {
            if (await DisplayAlert("Question?", "Load Messages?", "Yes", "No"))
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

            //WarenkorbFuelen();
            //ListViewEinkaufsliste.ItemsSource = warenkorbliste;

            ListViewEinkaufsliste.ItemsSource = einkaufsliste;
        }
    }
}
