using Project_C_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand {  get; set; }
        public RelayCommand WebAppsViewCommand { get; set; }

        private object _currentView;
        public HomeViewModel HomeVM { get; set; }
        public WebappsViewModel WebAppsVM { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                onPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            WebAppsVM = new WebappsViewModel();
            CurrentView = HomeVM;


            HomeViewCommand = new RelayCommand(ob =>
            {
                CurrentView = HomeVM;
            });

            WebAppsViewCommand = new RelayCommand(ob =>
            {
                CurrentView = WebAppsVM;
            });
        }
    }
}
