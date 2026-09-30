using PerfilAlumno.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PerfilAlumno.ViewModels
{
    public class UsuariosViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        private string _mensajeEstado = "Presione el botón para cargar los usuarios.";

        public ObservableCollection<UsuarioApi> Usuarios { get; } = new();

        public string MensajeEstado
        {
            get => _mensajeEstado;
            set
            {
                _mensajeEstado = value;
                OnPropertyChanged();
            }
        }

        public ICommand CargarUsuariosCommand { get; }

        public UsuariosViewModel()
        {
            _httpClient = new HttpClient();
            CargarUsuariosCommand = new Command(async () => await CargarUsuariosAsync());
        }

        private async Task CargarUsuariosAsync()
        {
            try
            {
                MensajeEstado = "Cargando usuarios...";

                var usuarios = await _httpClient.GetFromJsonAsync<List<UsuarioApi>>(
                    "https://jsonplaceholder.typicode.com/users");

                Usuarios.Clear();

                if (usuarios != null)
                {
                    foreach (var usuario in usuarios)
                    {
                        Usuarios.Add(usuario);
                    }
                }

                MensajeEstado = "Usuarios cargados correctamente.";
            }
            catch (HttpRequestException)
            {
                MensajeEstado = "Error de conexión o respuesta del servidor.";
            }
            catch (Exception)
            {
                MensajeEstado = "Ocurrió un error al cargar los usuarios.";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}