﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BookingApp.Model;


namespace BookingApp.DTO
{
    public class AccommodationDTO : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        private AccommodationType type;
        public AccommodationType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private int maxGuestNumber;
        public int MaxGuestNumber
        {
            get { return maxGuestNumber; }
            set
            {
                if (maxGuestNumber != value)
                {
                    
                    maxGuestNumber = value;
                    OnPropertyChanged("MaxGuestNumber");
                }
            }
        }

        private int minReservationDays;
        public int MinReservationDays
        {
            get { return minReservationDays; }
            set
            {
                if (minReservationDays != value)
                {
                    
                    minReservationDays = value;
                    OnPropertyChanged("MinReservationNumber");
                }
            }
        }

        private int reservationDaysLimit;
        public int ReservationDaysLimit
        {
            get { return reservationDaysLimit; }
            set
            {
                if (reservationDaysLimit != value)
                {
                    
                    reservationDaysLimit = value;
                    OnPropertyChanged("ReservationDaysLimit");
                }
            }
        }

        


        private List<string> images = new List<string>();
        public List<string> Images
        {
            get { return images; }
            set
            {
                if (images != value)
                {
                    images = value;
                    OnPropertyChanged("Images");
                }
            }
        }

        private string imagesWithComma;
        public string ImagesWithComma
        {
            get { return imagesWithComma; }
            set
            {
                if (imagesWithComma != value)
                {
                    imagesWithComma = value;
                    OnPropertyChanged("ImagesWithComma");
                }
            }
        }

        public string Error => null;

        
        private Regex _NumberRegex = new Regex("^[0-9]+$");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required";

                }
                
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Country is required";

                }
                else if (columnName == "MinReservationDays")
                {
                    Match match = _NumberRegex.Match(MinReservationDays.ToString());
                    if (!match.Success)
                        return "Min reservation days must be a number";
                    if (MinReservationDays < 0)
                        return "Min reservation days must be greater than 0";
                }
                else if (columnName == "MaxGuestNumber")
                {
                    Match match = _NumberRegex.Match(MaxGuestNumber.ToString());
                    if (!match.Success)
                        return "Max guest number must be a number";
                    if (MaxGuestNumber < 0)
                        return "Max guest number must be greater than 0";
                }
                else if (columnName == "ReservationDaysLimit")
                {
                    Match match = _NumberRegex.Match(ReservationDaysLimit.ToString());
                    if (!match.Success)
                        return "Reservation days limit must be a number";
                    if (ReservationDaysLimit < 1)
                        return "Reservation days limit must be greater than 1";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "City", "Name", "Country", "MinReservationDays", "MaxGuestNumbe", "ReservationDaysLimit" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationDTO()
        {}

        public AccommodationDTO(Accommodation accommodation)
        {
            id = accommodation.Id;
            name = accommodation.Name;
            city = accommodation.City;
            country = accommodation.Country;
            type = accommodation.Type;
            MaxGuestNumber = accommodation.MaxGuestNumber;
            MinReservationDays = accommodation.MinReservationDays;
            ReservationDaysLimit = accommodation.ReservationDaysLimit;



        }

        public Accommodation ToAccommodation()
        {
            if (!string.IsNullOrEmpty(imagesWithComma))
            {
                string imagesWithCommaCopy = imagesWithComma.Replace(" ", "");
                images = imagesWithCommaCopy.Split(",").ToList();
            }
            
            Accommodation a = new Accommodation(name, country, city, type, maxGuestNumber, minReservationDays, reservationDaysLimit);
            a.Id = id;
            a.Images = images;
            return a;

        }
    }

   // public enum AccommodationType { APARTMENT, HOUSE, COTTAGE }
}
