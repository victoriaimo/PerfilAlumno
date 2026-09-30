using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PerfilAlumno.ViewModels
{
    [QueryProperty(nameof(Nombre), "nombre")]
    [QueryProperty(nameof(Email), "email")]
    [QueryProperty(nameof(Telefono), "telefono")]
    public class DetalleUsuarioViewModel : INotifyPropertyChanged
    {
        private string _nombre = string.Empty;
        private string _email = string.Empty;
        private string _telefono = string.Empty;

        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Telefono
        {
            get => _telefono;
            set
            {
                _telefono = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}