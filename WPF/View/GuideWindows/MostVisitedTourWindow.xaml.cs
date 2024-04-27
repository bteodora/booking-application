﻿using BookingApp.Domain.Model.Features;
using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;

namespace BookingApp.View.GuideWindows
{
    public partial class MostVisitedTourWindow: Window
    {

        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private User Guide {  get; set; }
        private bool FirstTime {  get; set; }
        public string tourLabelString { get; set; }
        public string participantLabelString { get; set; }

        public MostVisitedTourWindow(User guide)
        {
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
            Guide = guide;
            FirstTime = true;
            InitializeComponent();
            DataContext = this;
        }

        private void TimePeriodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedPeriod = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedPeriod == "All Time")
            {
                ShowMostVisitedTourAllTime();
            }
            else
            {
                int selectedYear = int.Parse(((ComboBoxItem)comboBox.SelectedItem).Content.ToString());
                ShowMostVisitedTourPreviousYears(selectedYear);
            }
        }

        private void ShowMostVisitedTourAllTime()
        {
            Tour mostVisitedTourAllTime = _tourService.GetMostPopularTourForGuide(Guide.Id);

            if (FirstTime)
            {
                FirstTime = false;
            }
            else if (mostVisitedTourAllTime != null)
            {
                tourLabelString = mostVisitedTourAllTime.Name;
                int participantCount = _tourReservationService.GetNumberOfJoinedParticipants(mostVisitedTourAllTime.Id);
                participantLabelString = $"Participants: {participantCount}";
            }
            else
            {
                tourLabelString = "No data available";
                participantLabelString = "";
            }
        }

        private void ShowMostVisitedTourPreviousYears(int year)
        {
            Tour mostVisitedTourPreviousYears = _tourService.GetMostPopularTourForGuideInYear(Guide.Id, year);

            if (mostVisitedTourPreviousYears != null)
            {
                tourLabelString = mostVisitedTourPreviousYears.Name;
                int participantCount = _tourReservationService.GetNumberOfJoinedParticipants(mostVisitedTourPreviousYears.Id);
                participantLabelString = $"Participants: {participantCount}";
            }
            else
            {
                tourLabelString = "No data available";
                participantLabelString = "";
            }
        }

    }
}
