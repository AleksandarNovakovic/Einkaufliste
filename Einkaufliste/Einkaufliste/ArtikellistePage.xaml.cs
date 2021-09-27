using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Einkaufliste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtikellistePage : ContentPage
    {
        public static ObservableCollection<Artikel> artikelliste = new ObservableCollection<Artikel>();

        public ArtikellistePage()
        {
            InitializeComponent();

            ListViewArtikelliste.ItemsSource = artikelliste.OrderBy(x => x.Name);
        }

        private void BtnNeueArtikel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NeueArtikelPage());
        }

        async private void ListViewArtikelliste_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Artikel neueArtikel = new Artikel();
            neueArtikel = (Artikel)e.Item;

            MainPage.einkaufsliste.Add(neueArtikel);

            string jsonString = JsonConvert.SerializeObject(MainPage.einkaufsliste);
            Application.Current.Properties[MainPage.einkaufslisteKey] = jsonString;
            await Application.Current.SavePropertiesAsync();
        }

        protected override void OnAppearing()
        {
            ListViewArtikelliste.ItemsSource = artikelliste.OrderBy(x => x.Name);
        }
    }
}