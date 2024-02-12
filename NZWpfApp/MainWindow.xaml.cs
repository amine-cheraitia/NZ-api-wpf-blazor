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
        private Guid updatedID;
        private Guid DeletedID;
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
            try { 
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
                nameRn.Text = "";
                codeRn.Text = "";
                regionimgurlRn.Text = "";
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout de la région.");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de l'ajout de la région: " + ex.Message);
            }
        }


        private void UpdateRegion(object sender, RoutedEventArgs e)
        {
            Region selectedRegion =  ((FrameworkElement)sender).DataContext as Region;
            this.updatedID = selectedRegion.Id;
            nameRnUp.Text = selectedRegion.Name.ToString();
            codeRnUp.Text = selectedRegion.Code.ToString();
            regionimgurlRnUp.Text = selectedRegion.ReigionImageUrl.ToString();

        }

        private async void PutRegion(object sender, RoutedEventArgs e)
        {
            var newRegion = new Region
            {
                Name = nameRnUp.Text,
                Code = codeRnUp.Text,
                ReigionImageUrl = regionimgurlRnUp.Text
            };
            var json = JsonConvert.SerializeObject(newRegion);
            var response = await client.PutAsync($"Regions/{updatedID}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Région ajoutée avec succès!");
                // Réinitialiser les données du formulaire
                var NewRegion = new Region();

                // Recharger la liste des régions après l'ajout
                getAll();
                nameRnUp.Text = "";
                codeRnUp.Text = "";
                regionimgurlRnUp.Text = "";
                this.updatedID = Guid.Empty;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout de la région.");
            }


        }

        private async void DeleteRegion(object sender, RoutedEventArgs e)
        {
            Region selectedRegion = ((FrameworkElement)sender).DataContext as Region;
            this.DeletedID = selectedRegion.Id;

            var response = await client.DeleteAsync($"Regions/{DeletedID}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Région supprimée avec succès!");
                // Recharger la liste des régions après la suppression
                getAll();
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppression de la région.");
            }


        }
    }
}