﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class RecommendationPageViewModel
    {
        public User User { get; }
        public Frame Frame { get; }
        public AccommodationReservationViewModel SelectedReservation { get; }
        public AccommodationViewModel SelectedAccommodation { get; }
        public AccommodationRateViewModel AccommodationRate { get; }

        public RenovationRecommendationService RenovationRecommendationService { get; set;  }
        public HostService HostService { get; set; }
        public AccommodationRateService AccommodationRateService { get; set; }
        public RenovationRecommendationViewModel Recommendation {  get; set; }

        // KOMANDE
        public GuestICommand SaveRateCommand { get; set; }
        public RecommendationPageViewModel(User user, Frame frame, AccommodationReservationViewModel selectedReservation, AccommodationViewModel selectedAccommodation, AccommodationRateViewModel accommodationRate)
        {
            User = user;
            Frame = frame;
            SelectedReservation = selectedReservation;
            SelectedAccommodation = selectedAccommodation;
            AccommodationRate = accommodationRate;
            Recommendation = new RenovationRecommendationViewModel();
            AccommodationRateService = new AccommodationRateService(Injector.Injector.CreateInstance<IAccommodationRateRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            HostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            RenovationRecommendationService = new RenovationRecommendationService(Injector.Injector.CreateInstance<IRenovationRecommendationRepository>());
            SaveRateCommand = new GuestICommand(OnSaveRate);
        }

        private void OnSaveRate()
        {
            Recommendation.ReservationId = SelectedReservation.Id;
            Recommendation.AccommodationId = SelectedAccommodation.Id;
            RenovationRecommendationService.Add(Recommendation.ToRecommendation());
            AccommodationRate.RecommendationId = RenovationRecommendationService.GetAll().Last().Id;
            AccommodationRateService.Add(AccommodationRate.ToAccommodationRate());
            AccommodationRate rate = AccommodationRate.ToAccommodationRate();
            Host host = HostService.GetById(rate.HostId);
            Frame.Content = new RateAccommodationSuccessfulPage(User, Frame, SelectedAccommodation, host.Username);
        }

      
    }
}
