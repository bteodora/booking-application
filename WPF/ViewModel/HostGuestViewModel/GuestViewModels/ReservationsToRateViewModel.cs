﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationsToRateViewModel : IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationReservationViewModel> Reservations { get; set; }
        public User User { get; set; }

       
        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public AccommodationRateService AccommodationRateService { get; set; }

        public Frame Frame { get; set; }

        // KOMANDE
        public GuestICommand RateCommand { get; set; }
        private AccommodationReservationViewModel selectedReservation;
        public AccommodationReservationViewModel SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    OnPropertyChanged("SelectedReservation");

                }
               
                RateCommand.RaiseCanExecuteChanged();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReservationsToRateViewModel(User user, Frame frame)
        {

            User = user;
            Frame = frame;
            // SelectedReservation = reservation;
            Reservations = new ObservableCollection<AccommodationReservationViewModel>();
            AccommodationRateService = new AccommodationRateService(Injector.Injector.CreateInstance<IAccommodationRateRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
           // SelectedReservation = selectedReservation;
            RateCommand = new GuestICommand(OnRate, CanRate);


        }

        private bool CanRate()
        {
            if (SelectedReservation == null)
                return false;
            else
                return true;
        }

        private void OnRate()
        {
            Frame.Content = new RateAccommodationForm(User, SelectedReservation, Frame);
        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {

                if (reservation.GuestId == User.Id && IsBeforeFiveDays(reservation) && !IsReservationRated(reservation))
                {
                    Reservations.Add(new AccommodationReservationViewModel(reservation));

                }
            }
            
        }

        private bool IsReservationRated(AccommodationReservation reservation)
        {
            bool isFound = false;
            foreach (AccommodationRate rate in AccommodationRateService.GetAll())
            {
                if (rate.ReservationId == reservation.Id)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;


        }

        private bool IsBeforeFiveDays(AccommodationReservation reservation)
        {
            int daysPassed = (DateTime.Now - reservation.EndDate).Days;
            if (daysPassed <= 5 && daysPassed > 0)
                return true;
            else
                return false;
        }

      
    }
}
