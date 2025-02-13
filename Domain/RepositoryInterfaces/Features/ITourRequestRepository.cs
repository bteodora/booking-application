﻿using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        List<TourRequest> GetAllPending();
        List<TourRequest> GetAllPendingOrInvalid();
        void Add(TourRequest tourRequest);
        void Save();
        int NextId();
        TourRequest GetById(int id);
        List<TourRequest> GetByTouristId(int touristId);
        void UpdateRequest(TourRequest request);
        public List<TourRequest> GetAllForYear(int year);
        public List<TourRequest> GetRequestsBetweenDates(DateTime startDate, DateTime endDate);
    }
}
