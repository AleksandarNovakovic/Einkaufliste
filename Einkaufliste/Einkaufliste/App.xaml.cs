using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Einkaufliste
{
    public partial class App : Application
    {
        public const string artikellistKey = "Artikelliste";
        

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            //DeleteArtikelliste();
            LadenArtikelliste();
        }

        async private void LadenArtikelliste()
        {
            string jsonString;

            if (!Application.Current.Properties.ContainsKey(App.artikellistKey))
            {
                jsonString = JsonConvert.SerializeObject(ArtikellistePage.artikelliste);
                Application.Current.Properties[App.artikellistKey] = jsonString;
                await Application.Current.SavePropertiesAsync();
            }

            jsonString = Application.Current.Properties[App.artikellistKey] as string;
            ArtikellistePage.artikelliste = JsonConvert.DeserializeObject<ObservableCollection<Artikel>>(jsonString);

            if (ArtikellistePage.artikelliste.Count == 0)
            {
                ArtikellistePage.artikelliste.Add(new Artikel() { Name = "Milch", Beschreibung = "1 Liter", Bild = "milch" });
                ArtikellistePage.artikelliste.Add(new Artikel() { Name = "Brot", Beschreibung = "1 Stück", Bild = "brot" });
                ArtikellistePage.artikelliste.Add(new Artikel() { Name = "Butter", Beschreibung = "1 Packung", Bild = "butter" });
                ArtikellistePage.artikelliste.Add(new Artikel() { Name = "Käse", Beschreibung = "200 Gramm", Bild = "kaese" });
                ArtikellistePage.artikelliste.Add(new Artikel() { Name = "Wurst", Beschreibung = "250 Gramm", Bild = "wurst" });

                jsonString = JsonConvert.SerializeObject(ArtikellistePage.artikelliste);
                Application.Current.Properties[App.artikellistKey] = jsonString;
                await Application.Current.SavePropertiesAsync();
            }
        }

        private void DeleteArtikelliste()
        {
            string jsonString = Application.Current.Properties[App.artikellistKey] as string;
            ArtikellistePage.artikelliste = JsonConvert.DeserializeObject<ObservableCollection<Artikel>>(jsonString);

            ArtikellistePage.artikelliste.Clear();

            jsonString = JsonConvert.SerializeObject(ArtikellistePage.artikelliste);
            Application.Current.Properties[App.artikellistKey] = jsonString;
            Application.Current.SavePropertiesAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
