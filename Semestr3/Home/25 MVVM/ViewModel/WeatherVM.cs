using _09_MVVM.Models;
using _09_MVVM.ViewModel.Commands;
using _09_MVVM.ViewModel.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace _09_MVVM.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();
        public City selectedCity;
        public CurrentConditions currentConditions;
        public FiveDayConditions fiveDayConditions;
        public SearchCommand SearchCommand { get; set; }
        public LoadFiveDayCommand LoadFiveDayCommand { set; get; }

        public bool showPopup = false;

        private string query;

        public WeatherVM()
        {
            query = "";
            SearchCommand = new SearchCommand(this);
            LoadFiveDayCommand = new LoadFiveDayCommand(this);
        }

        public bool ShowPopup
        {
            get => showPopup;
            set
            {
                showPopup = value;
                OnNotify();
            }
        }

        public string Query
        {
            get { return query; }
            set 
            {
                query = value;
                OnNotify();
            }
        }

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                GetConditionsAsync();
                OnNotify();

                if(ShowPopup)
                {
                    LoadFiveDayCommand?.Execute(null);
                }
            }
        }
        public CurrentConditions CurrentConditions
        {
            get => currentConditions;
            set
            {
                currentConditions = value;
                OnNotify();
            }
        }

        public FiveDayConditions FiveDayConditions
        {
            get => fiveDayConditions;
            set
            {
                fiveDayConditions = value;
                OnNotify();
            }
        }

        private async void GetConditionsAsync()
        {
            if (SelectedCity != null)
            {
                CurrentConditions = await WeatherHelper.GetCurrentConditionsAsync(SelectedCity.Key);
            }
            else
            {
                CurrentConditions = new CurrentConditions();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public async Task MakeRequestCitiesAsync()
        {
            var cities = await WeatherHelper.GetCitiesAsync(Query);
            Cities.Clear();

            foreach (var item in cities)
            {
                Cities.Add(item);
            }
        }

        public async void GetFiveDayConditionsAsync()
        {
            ShowPopup = true;
            if (SelectedCity != null)
            {
                FiveDayConditions = await WeatherHelper.GetFiveDeyConditionsAsync(SelectedCity.Key);
            }
            else
            {
                FiveDayConditions = new FiveDayConditions();
            }
        }
    }
}
