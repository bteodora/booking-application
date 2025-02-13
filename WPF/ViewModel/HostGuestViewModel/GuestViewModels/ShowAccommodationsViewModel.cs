﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Injector;
using BookingApp.Observer;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.View.Guest.GuestTools;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ShowAccommodationsViewModel : IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }


        public AccommodationViewModel SelectedAccommodation { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public User User { get; set; }

        public Frame Frame { get; set; }

        public AccommodationsPage AccommodationsPage { get; set; }

        public HostService HostService { get; set; }

        public AccommodationSearcher AccommodationSearcher { get; set; }



        public ObservableCollection<string> CountriesSearch { get; set; }
        public ObservableCollection<string> CitiesSearch { get; set; }

      

        private string citySearch;
        public string CitySearch
        {
            get
            {
                return citySearch;
            }
            set
            {
                if (value != citySearch)
                {
                    citySearch = value ?? string.Empty;
                    OnPropertyChanged(nameof(CitySearch));
                }
            }
        }

        private string countrySearch;
        public string CountrySearch
        {
            get
            {
                return countrySearch;
            }
            set
            {
                if (value != countrySearch)
                {
                    countrySearch = value ?? string.Empty;
                    LoadCitiesFromCSV();
                    OnPropertyChanged(nameof(CountrySearch));
                }
            }
        }

        // KOMANDE

        public GuestICommand SearchCommand { get; set; }

        public GuestICommand<object> ReserveCommand { get; set; }

        public GuestICommand AnywhereAnytimeCommand { get; set; }

        public GuestICommand<object> DetailsCommand { get; set; }

        public ShowAccommodationsViewModel(User user, Frame frame, AccommodationsPage accommodationsPage)
        {

            Accommodations = new ObservableCollection<AccommodationViewModel>();
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
           // AccommodationService.Subscribe(this);
            User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;

            Frame = frame;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            HostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            AccommodationsPage = accommodationsPage;
            AccommodationSearcher = new AccommodationSearcher(AccommodationService);
            CitiesSearch = new ObservableCollection<string>();
            CountriesSearch = new ObservableCollection<string>();
            SearchCommand = new GuestICommand(OnSearch);
            ReserveCommand = new GuestICommand<object>(OnReserve);
            AnywhereAnytimeCommand = new GuestICommand(OnAnywhereAnytime);
            DetailsCommand = new GuestICommand<object>(OnDetails);
            CountriesSearch.Add("");
            LoadCountriesFromCSV();
           
         

        }

        private void OnDetails(object sender)
        {
            Button button = sender as Button;
            SelectedAccommodation = button.DataContext as AccommodationViewModel;
            Frame.Content = new AccommodationDetailsPage(Frame, User, SelectedAccommodation);
        }

        private void OnAnywhereAnytime()
        {
            Frame.Content = new AnywhereAnytimePage(User, Frame);
        }

        private void OnReserve(object sender)
        {
            Button button = sender as Button;
            SelectedAccommodation = button.DataContext as AccommodationViewModel;
            Frame.Content = new ReservationInfoPage(SelectedAccommodation, User, Frame);
        }

        private void OnSearch()
        {
            List<string> queries = new List<string>();
            queries.Add(AccommodationsPage.txtSearchName.Text); //nameQuery
            queries.Add(CitySearch); //cityQuery
            queries.Add(CountrySearch); //countryQuery
            queries.Add(AccommodationsPage.txtSearchType.Text); //typeQuery
            queries.Add(AccommodationsPage.txtSearchGuestNumber.Text); //guestQuery
            queries.Add(AccommodationsPage.txtSearchReservationDays.Text); //reservationQuery

            AccommodationsPage.AccommodationListBox.ItemsSource = AccommodationSearcher.SearchAccommodations(queries);
        }

        public void Update()
        {
            Accommodations.Clear();
            List<AccommodationViewModel> superHostAccommodations = new List<AccommodationViewModel>();
            List<AccommodationViewModel> nonSuperHostAccommodations = new List<AccommodationViewModel>();

            SeparateAccommodations(AccommodationService, superHostAccommodations, nonSuperHostAccommodations);

            foreach (AccommodationViewModel superHostAccommodation in superHostAccommodations)
            {
                Accommodations.Add(superHostAccommodation);

            }

            foreach (AccommodationViewModel nonSuperHostAccommodation in nonSuperHostAccommodations)
            {
                Accommodations.Add(nonSuperHostAccommodation);

            }

        }
        public void SeparateAccommodations(AccommodationService accommodationService, List<AccommodationViewModel> superHostAccommodations, List<AccommodationViewModel> nonSuperHostAccommodations)
        {
            foreach (Accommodation accommodation in AccommodationService.GetAll())
            {

                AccommodationViewModel accommodationDTO = new AccommodationViewModel(accommodation);
                Host host = HostService.GetById(accommodation.HostId);
                HostService.BecomeSuperHost(host);
                accommodationDTO.IsSuperHost = host.IsSuperHost;

                if (accommodationDTO.IsSuperHost)
                    superHostAccommodations.Add(accommodationDTO);
                else
                    nonSuperHostAccommodations.Add(accommodationDTO);

                //MessageBox.Show(Accommodations[0].Type.ToString());
            }
        }

     

        private void LoadCountriesFromCSV()
        {
          
            
            string csvFilePath = "../../../Resources/Data/european_countries.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    CountriesSearch.Add(values[0]);
                }
            }
          
        }

        private void LoadCitiesFromCSV()
        {
            CitiesSearch.Clear();
            CitiesSearch.Add("");
            string csvFilePath = "../../../Resources/Data/european_cities_and_countries.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values[1].Equals(CountrySearch))
                        CitiesSearch.Add(values[0]);
                }
            }
            CitySearch = CountriesSearch[0];
        }

      


    }
}
