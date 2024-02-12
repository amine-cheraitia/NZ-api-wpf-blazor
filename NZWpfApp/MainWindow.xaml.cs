using Newtonsoft.Json;
using NZWpfApp.Models.Domain;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;

namespace NZWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7283/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            this.getAll();


        }



        public async void getAll()
        {
            
            var response = await client.GetStringAsync("Regions");
            var regions = JsonConvert.DeserializeObject<List<Models.Domain.Region>>(response);
            Console.WriteLine(regions);
            regionDG.ItemsSource = regions;
            labl.Content = "random"; 

        }

        private async void AddRegion(object sender, RoutedEventArgs e)
        {
            var newRegion = new Region
            {
                Name = nameRn.Text ,
                Code= codeRn.Text ,
                ReigionImageUrl= regionimgurlRn.Text
            };
            var json = JsonConvert.SerializeObject(newRegion);
            var response = await client.PostAsync("Regions", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Région ajoutée avec succès!");
                // Réinitialiser les données du formulaire
                var NewRegion = new Region();
                
                // Recharger la liste des régions après l'ajout
                getAll();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout de la région.");
            }


            

        }
    }
}