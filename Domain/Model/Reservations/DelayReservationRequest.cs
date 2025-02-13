﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BookingApp.Domain.Model.Reservations
{
    public enum RequestStatus { PENDING, APPROVED, REJECTED }
    public class DelayRequest : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int HostId { get; set; }

        public int ReservationId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime StartLastDate { get; set; }
        public DateTime EndLastDate { get; set; }
        public string DateRange => StartDate.ToString() + "-" + EndDate.ToString();

        public RequestStatus Status { get; set; }

        public string Comment { get; set; }

        public DateTime RepliedDate { get; set; }
        public DelayRequest()
        {

            Status = RequestStatus.PENDING;
            RepliedDate = DateTime.Now;
        }

        public DelayRequest(int guestId, int hostId, int reservationId, DateTime startDate, DateTime endDate, RequestStatus status, string comment, DateTime repliedDate, DateTime startLastDate, DateTime endLastDate)
        {

            GuestId = guestId;
            HostId = hostId;
            ReservationId = reservationId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Comment = comment;
            RepliedDate = repliedDate;
            StartLastDate = startLastDate;
            EndLastDate = endLastDate;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                HostId.ToString(),
                ReservationId.ToString(),
                DateRange,
                Comment,
                Status.ToString(),
                RepliedDate.ToString(),
                StartLastDate.ToString(),
                EndLastDate.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            HostId = Convert.ToInt32(values[2]);
            ReservationId = Convert.ToInt32(values[3]);
            string[] dateParts = values[4].Split('-');
            StartDate = DateTime.Parse(dateParts[0]);
            EndDate = DateTime.Parse(dateParts[1]);
            Comment = values[5];
            Status = StatusFromCsv(values[6]);
            RepliedDate = DateTime.Parse(values[7]);
            StartLastDate = DateTime.Parse(values[8]);
            EndLastDate = DateTime.Parse(values[9]);

        }

        private RequestStatus StatusFromCsv(string value)
        {
            RequestStatus status = RequestStatus.PENDING;

            switch (value)
            {
                case "PENDING":
                    status = RequestStatus.PENDING;
                    break;
                case "APPROVED":
                    status = RequestStatus.APPROVED;
                    break;
                case "REJECTED":
                    status = RequestStatus.REJECTED;
                    break;


            }

            return status;
        }
    }
}
