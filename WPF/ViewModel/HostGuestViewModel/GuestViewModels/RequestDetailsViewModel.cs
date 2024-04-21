﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Model;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class RequestDetailsViewModel
    {


        public DelayRequestViewModel SelectedRequest { get; set; }

        public HostService HostService { get; set; }
        public string HostUsername { get; set; }

        public string OldDateRange { get; set; }
        public string NewDateRange { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfDays { get; set; }

        public string RequestHeader { get; set; }
        public AccommodationViewModel Accommodation { get; set; }

        public DelayRequestService DelayRequestService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public string Comment { get; set; }
        public ComboBox RequestStatusBox { get; set; }
        public RequestDetailsViewModel(DelayRequestViewModel selectedRequest)
        {

            SelectedRequest = selectedRequest;
            DelayRequestService = new DelayRequestService();
            AccommodationService = new AccommodationService();
            AccommodationReservationService = new AccommodationReservationService();
            HostService = new HostService();
            AccommodationReservation reservation = AccommodationReservationService.GetById(SelectedRequest.ReservationId);
            Accommodation = new AccommodationViewModel(AccommodationService.GetById(reservation.AccommodationId));
            HostUsername = HostService.GetById(SelectedRequest.HostId).Username;
            OldDateRange = selectedRequest.StartLastDate.ToString("MM/dd/yyyy") + " -> " + selectedRequest.EndLastDate.ToString("MM/dd/yyyy");
            NewDateRange = SelectedRequest.StartDate.ToString("MM/dd/yyyy") + " -> " + SelectedRequest.EndDate.ToString("MM/dd/yyyy");
            NumberOfPeople = reservation.NumberOfPeople;
            Comment = SelectedRequest.Comment;
            NumberOfDays = (reservation.EndDate - reservation.StartDate).Days + 1;
            RequestHeader = CreateRequestHeader(SelectedRequest);



        }
        private string? CreateRequestHeader(DelayRequestViewModel selectedRequest)
        {
            if (selectedRequest.Status == RequestStatus.PENDING)
            {
                Comment = "Waiting for host's response...";
                return "Your request is pending.";
            }

            else if (selectedRequest.Status == RequestStatus.APPROVED)
                return "Your request is approved.";
            else
                return "Your request is rejected";
        }
    }
}
