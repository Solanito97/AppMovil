using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;

namespace AppMovilBigFood
{
    public partial class PageLista : ContentPage
    {
        public PageLista()
        {
            InitializeComponent();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://apibigfoodservice.somee.com/api/Producto");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<Productos>>(responseContent);
                    productsListView.ItemsSource = products;
                }
                else
                {
                    await DisplayAlert("Error", "Error al cargar los productos", "OK");
                }
            }
        }
    }
}
