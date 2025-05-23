using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace FaustsAbbacus
{
    public class Item : INotifyPropertyChanged
    {
        private string _name;
        private decimal _price;
        private int _amountDropped;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                // Hier könnte der API-Call zu poe.ninja erfolgen
                UpdatePriceFromApi();
            }
        }

        public decimal Price
        {
            get => _price;
            private set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public int AmountDropped
        {
            get => _amountDropped;
            set
            {
                _amountDropped = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public decimal TotalValue => Price * AmountDropped;

        private async void UpdatePriceFromApi()
        {
            // TODO: Implementierung des API-Calls zu poe.ninja
            // Dies sollte den Preis basierend auf der ausgewählten League updaten
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}