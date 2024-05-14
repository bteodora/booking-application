﻿using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;

namespace BookingApp.Application.Services.FeatureServices
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository AccommodationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            this.AccommodationRepository = accommodationRepository;
        }

        public List<Accommodation> GetAll()
        {
            return AccommodationRepository.GetAll();
        }

        public Accommodation Add(Accommodation accommodation)
        {
            return AccommodationRepository.Add(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {

            AccommodationRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return AccommodationRepository.Update(accommodation);
        }

        public Accommodation GetById(int id)
        {
            return AccommodationRepository.GetById(id);
        }

     /*  public void Subscribe(IObserver observer)
        {
            AccommodationRepository.AccommodationSubject.Subscribe(observer);
        }*/

        public void FreeDateRange(Accommodation accommodation, AccommodationReservationViewModel selectedReservation)
        {
            foreach (CalendarDateRange dateRange in accommodation.UnavailableDates)
            {
                if (dateRange.Start == selectedReservation.StartDate && dateRange.End == selectedReservation.EndDate)
                {
                    accommodation.UnavailableDates.Remove(dateRange);
                    break;
                }

            }

            AccommodationRepository.Update(accommodation);
        }

        public List<Accommodation> FindAvailableAccommodations(int dayNumber, int guestNumber, DateTime startDate, DateTime endDate)
        {
           List<Accommodation> result = new List<Accommodation>();
           foreach(Accommodation accommodation in this.GetAll())
            {
                if (CheckParameters(accommodation, dayNumber, guestNumber, startDate, endDate))
                    result.Add(accommodation);
            }
            return result;
        }

        private bool CheckParameters(Accommodation accommodation, int dayNumber, int guestNumber, DateTime startDate, DateTime endDate)
        {
            if (CheckDayGuestNumber(accommodation, dayNumber, guestNumber))
            {
                if (CheckDateRange(accommodation, startDate, endDate, dayNumber))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool CheckDateRange(Accommodation accommodation, DateTime startDate, DateTime endDate, int dayNumber)
        {
            return true;
        }

       

        private List<CalendarDateRange> GeneratePossibleRanges(DateTime startDate, DateTime endDate, int dayNumber)
        {
            List<CalendarDateRange> result = new List<CalendarDateRange>();
            while(startDate.AddDays(dayNumber) <=  endDate) {
                result.Add(new CalendarDateRange(startDate, startDate.AddDays(dayNumber)));
                startDate.AddDays(dayNumber);
            
            }
            return result;
        }

        private bool CheckDayGuestNumber(Accommodation accommodation, int dayNumber, int guestNumber)
        {
            if (accommodation.MinReservationDays <= dayNumber && accommodation.MaxGuestNumber >= guestNumber)
                return true;
            else
                return false;
        }
    }
}
