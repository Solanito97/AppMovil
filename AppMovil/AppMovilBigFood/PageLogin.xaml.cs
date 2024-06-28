using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppMovilBigFood
{
    public partial class PageLogin : ContentPage
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string login = loginEntry.Text;
            string password = passwordEntry.Text;

            using (var client = new HttpClient())
            {
                try
                {
                    // Realizar la solicitud GET para buscar al usuario por el login
                    HttpResponseMessage response = await client.GetAsync($"http://apibigfoodservice.somee.com/api/Usuario?login={Uri.EscapeDataString(login)}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var userList = JsonConvert.DeserializeObject<List<Usuario>>(responseContent);

                        // Verificar si se encontró algún usuario con el login especificado
                        if (userList != null && userList.Count > 0)
                        {
                            var validatedUser = userList[0]; // Tomamos el primer usuario encontrado

                            // Verificar la contraseña
                            if (validatedUser.ConfirmarPassword(password))
                            {
                                // Navigate to Products Page
                                await Navigation.PushAsync(new PageLista());
                            }
                            else
                            {
                                await DisplayAlert("Error", "Login o contraseña incorrectos", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Error", "Usuario no encontrado", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Fallo en el login", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
                }
            }
        }
    }
}

