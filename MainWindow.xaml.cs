using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FaustsAbbacus.Services;

namespace FaustsAbbacus
{
    public partial class MainWindow : Window
    {
        private readonly PoeNinjaService _poeNinjaService;
        private readonly Item _currentItem;

        public MainWindow()
        {
            InitializeComponent();
            _poeNinjaService = new PoeNinjaService();
            _currentItem = new Item();
            //TODO:
            // Leagues hinzufügen (diese sollten eigentlich auch von der API geholt werden)
            LeagueComboBox.Items.Add("Standard");
            LeagueComboBox.Items.Add("Settlers");
            LeagueComboBox.SelectedIndex = 0;
        }

        private async void LeagueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentItem.Name != null)
            {
                //TODO:
                // Update Preis wenn League geändert wird
                await UpdateItemPrice();
            }
        }

        private async void ItemNameAutoComplete_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO:
            // Implementiere hier die Item-Suche/Autocomplete-Logik
            // Dies sollte die verfügbaren Items von poe.ninja abrufen
        }

        private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(AmountTextBox.Text, out int amount))
            {
                _currentItem.AmountDropped = amount;
                UpdateDisplay();
            }
        }

        private async Task UpdateItemPrice()
        {
            string itemname = ItemName.SelectedText;
            string selectedLeague = LeagueComboBox.SelectedItem?.ToString();
            string itemtype = ItemType.SelectedText;
            if (selectedLeague != null)
            {
                decimal price = await _poeNinjaService.GetItemPrice(itemname, selectedLeague, itemtype);
                // Update UI
                PriceTextBlock.Text = price.ToString("N2");
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            TotalValueTextBlock.Text = _currentItem.TotalValue.ToString("N2");
        }
    }
}