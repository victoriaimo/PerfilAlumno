using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PerfilAlumno.ViewModels
{
    [QueryProperty(nameof(Nombre), "nombre")]
    [QueryProperty(nameof(Edad), "edad")]
    public class DetallePerfilViewModel : INotifyPropertyChanged
    {
        private string nombre = string.Empty;
        private string edad = string.Empty;

        public string Nombre
        {
            get => nombre;
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }

        public string Edad
        {
            get => edad;
            set
            {
                edad = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}