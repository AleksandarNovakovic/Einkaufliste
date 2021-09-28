using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using System.IO;

namespace Einkaufliste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NeueArtikelPage : ContentPage
    {
        public NeueArtikelPage()
        {
            InitializeComponent();
        }

        async private void BtnHinzufuegen_Clicked(object sender, EventArgs e)
        {
            Artikel neueArtikel = new Artikel();

            if (string.IsNullOrEmpty(EntryNeueArtikelName.Text) || string.IsNullOrEmpty(EntryNeueArtikelBeschreibung.Text))
            {
                await DisplayAlert("Fehler!", "Name und Beschreibung müssen ausgefüllt werden!", "OK");
            }
            else
            {
                neueArtikel.Name = EntryNeueArtikelName.Text;
                neueArtikel.Beschreibung = EntryNeueArtikelBeschreibung.Text;

                if (string.IsNullOrEmpty(EntryNeueImageLink.Text) || !EntryNeueImageLink.Text.EndsWith(".jpg") && !EntryNeueImageLink.Text.EndsWith(".png"))
                {
                    neueArtikel.Bild = "defaultBild";
                }
                else
                {
                    neueArtikel.Bild = EntryNeueImageLink.Text;
                }

                ArtikellistePage.artikelliste.Add(neueArtikel);

                string jsonString = JsonConvert.SerializeObject(ArtikellistePage.artikelliste);
                Application.Current.Properties[App.artikellistKey] = jsonString;
                await Application.Current.SavePropertiesAsync();

                if (await DisplayAlert("Info!", "Neue Artikel hinzugefügt. Möchten Sie weitere Artikel hinzufügen?", "Ja", "Nein"))
                {
                    EntryNeueArtikelName.Text = null;
                    EntryNeueArtikelBeschreibung.Text = null;
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
        }


        async private void BtnAuswaehlenPhoto_Clicked(object sender, EventArgs e)
        {
            var bild = await MediaPicker.PickPhotoAsync();

            if (bild != null)
            {
                //bild.FileName = EntryNeueArtikelBeschreibung.Text;

                var bildZeigen = await bild.OpenReadAsync();

                httpImageEigabe.IsVisible = false;
                EntryNeueImageLink.Text = bild.FullPath;

                ImageNeue.Source = ImageSource.FromStream(() => bildZeigen);
            }
        }

        async private void BtnAufnehmenPhoto_Clicked(object sender, EventArgs e)
        {
            var bild = await MediaPicker.CapturePhotoAsync();

            //var image = bild.FullPath;

            if (bild != null)
            {
                bild.FileName = EntryNeueArtikelBeschreibung.Text;

                var bildZeigen = await bild.OpenReadAsync();

                httpImageEigabe.IsVisible = false;
                EntryNeueImageLink.Text = bild.FullPath;

                ImageNeue.Source = ImageSource.FromStream(() => bildZeigen);
            }
        }
    }
}
