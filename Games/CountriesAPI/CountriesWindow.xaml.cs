using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_C_.Games.CountriesAPI
{
    /// <summary>
    /// Interaction logic for CountriesWindow.xaml
    /// </summary>
    public partial class CountriesWindow : Window
    {
        public ObservableCollection<Country> Countries { get; set; } = new ObservableCollection<Country>();
        private ObservableCollection<Country> _allCountries = new ObservableCollection<Country>();

        public static HttpClient client = new HttpClient();
        public CountriesWindow()
        {
            InitializeComponent();
            LoadCountriesDataAsync();
             
        }

        private async Task LoadCountriesDataAsync()
        {
            try
            {
                string json = await client.GetStringAsync("https://restcountries.com/v3.1/all");
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                Countries = JsonSerializer.Deserialize<ObservableCollection<Country>>(json, options);

                foreach (Country c in Countries)
                {
                    _allCountries.Add(c);
                }
                CountriesDataGrid.ItemsSource = Countries;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            List<Country> filteredCountries = _allCountries
                .Where(c => c.Name.Common.ToLower().Contains(searchText))
                .ToList();

            UpdateCountriesCollection(filteredCountries);
        }

        private void RegionFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedRegion = (RegionFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedRegion == "All Regions")
            {
                UpdateCountriesCollection(_allCountries.ToList());
            }
            else
            {
                List<Country> filteredCountries = _allCountries
                    .Where(c => c.Region.ToLower() == selectedRegion.ToLower())
                    .ToList();

                UpdateCountriesCollection(filteredCountries);
            }
        }
        private void UpdateCountriesCollection(List<Country> countries)
        {
            Countries.Clear();
            foreach (Country country in countries)
            {
                Countries.Add(country);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove() ;
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HideButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
