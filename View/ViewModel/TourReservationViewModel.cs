﻿using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit;

namespace BookingApp.ViewModel
{
    public class TourReservationViewModel : INotifyPropertyChanged
    {
        private readonly TourParticipantService _tourParticipantService;
        private readonly TourReservationService _tourReservationService;
        private readonly TouristService _touristService;

        public TourReservationViewModel TourReservation { get; set; }


        public List<TourParticipantViewModel> TourParticipantDTOs { get; set; }
        public List<TourParticipantViewModel> TourParticipantsListBox { get; set; }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private List<int> _participantIds = new List<int>();
        public List<int> ParticipantIds
        {
            get { return  _participantIds; }
            set
            {
                if(_participantIds != value)
                {
                    _participantIds = value;
                    OnPropertyChanged(nameof(ParticipantIds));
                }
            }
        }

        private int _tourId;
        public int TourId
        {
            get { return _tourId; }
            set
            {
                if (_tourId != value)
                {
                    _tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

        private int _touristId;
        public int TouristId
        {
            get { return _touristId; }
            set
            {
                if (_touristId != value)
                {
                    _touristId = value;
                    OnPropertyChanged(nameof(TouristId));
                }
            }
        }

        private int _startCheckpoint;

        public int StartCheckpoint
        {
            get { return _startCheckpoint; }
            set
            {
                if (_startCheckpoint != value)
                {
                    _startCheckpoint = value;
                    OnPropertyChanged(nameof(StartCheckpoint));
                }
            }
        }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private int participantCount;
        public string ParticipantCount
        {
            get
            {
                return participantCount.ToString();
            }
            set
            {
                if (value != participantCount.ToString())
                {
                    participantCount = Convert.ToInt32(value);
                    OnPropertyChanged(nameof(participantCount));
                }
            }
        }

        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if(_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private List<TourParticipantViewModel> _participantsListBox = new List<TourParticipantViewModel>();
        public List<TourParticipantViewModel> ParticipantsListBox
        {
            get
            {
                return _participantsListBox;
            }
            set
            {
                if(value != _participantsListBox)
                {
                    _participantsListBox = value;
                    OnPropertyChanged(nameof(ParticipantsListBox));
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if(_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private string _age;
        public string Age
        {
            get
            {
                return _age;
            }
            set
            {
                if(_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        private TourParticipantViewModel _selectedParticipant;
        public TourParticipantViewModel SelectedParticipant
        {
            get
            {
                return _selectedParticipant;
            }
            set
            {
                if(_selectedParticipant != value)
                {
                    _selectedParticipant = value;
                    OnPropertyChanged(nameof(SelectedParticipant));
                }
            }
        }

                //<xctk:IntegerUpDown x:Name="Participants"
                //Text="{Binding Path=TourReservation.SelectedTour.ParticipantCount, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
                //IsEnabled="False"
                //Grid.Row="2"
                //Grid.Column="4"
                //Minimum="0"
                //Maximum="{Binding MaximumValuePeoples}"
                //Value="0"
                //TextAlignment="Justify"
                //HorizontalAlignment="Left"
                //FontSize="20"
                //Height="35"
                //Width="100"/>

                        //<TextBox x:Name="years"
                        // Grid.Column="1"
                        // Grid.Row="3"
                        // HorizontalAlignment="Left"
                        // TextAlignment="Justify"
                        // Width="31"
                        // Height="25"
                        // PreviewTextInput="years_PreviewTextInput"
                        // LostFocus="years_LostFocus" Margin="0,10,0,10"/>

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetupForNextParticipantInput()
        {
            ParticipantsListBox = null;
            ParticipantsListBox = TourParticipantsListBox;

            Name = string.Empty;
            LastName = string.Empty;
            Age = "0";
        }

        public void AddParticipant()
        {
            if (!AllFieldsFilled(Age, Name, LastName))
            {
                System.Windows.MessageBox.Show("All fields must be filled");
            }
            else
            {
                TourParticipantViewModel tourParticipantViewModel = _tourParticipantService.saveParticipantToDTO(Name, LastName, Age);
                TourParticipantDTOs.Add(tourParticipantViewModel);

                TourParticipantsListBox.Add(tourParticipantViewModel);

                SetupForNextParticipantInput();
            }
        }

        private bool AllFieldsFilled(string years, string name, string lastName)
        {
            if (years == string.Empty || name == string.Empty || lastName == string.Empty)
            {
                return false;
            }

            return true;
        }

        public void RemoveParticipant()
        {
            TourParticipantDTOs.Remove(SelectedParticipant);

            TourParticipantsListBox.Remove(SelectedParticipant);
            ParticipantsListBox = null;
            ParticipantsListBox = TourParticipantsListBox;
        }
        private void saveParticipants()
        {
            int reservationId = _tourReservationService.NextReservationId();
            foreach (TourParticipantViewModel tp in TourParticipantDTOs)
            {
                _tourParticipantService.saveParticipant(tp, reservationId);
            }
        }

        private void saveReservation()
        {
            _tourReservationService.saveReservation(TourReservation, SelectedTour, UserId);
        }

        public void Book()
        {
            int participantCount = Convert.ToInt32(ParticipantCount);
            if (participantCount < TourParticipantDTOs.Count)
            {
                System.Windows.MessageBox.Show("Too many participants in the list of participants", "Participants error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (participantCount > TourParticipantDTOs.Count)
            {
                System.Windows.MessageBox.Show("Too less participants in the list of participants", "Participants error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                saveParticipants();
                saveReservation();

                ReduceNumberOfAvailablePlaces();
                System.Windows.MessageBox.Show("Tour " + "\"" + SelectedTour.Name + "\"" + " booked!");
            }
        }

        private void ReduceNumberOfAvailablePlaces()
        {
            try
            {
                _touristService.UpdateAvailablePlaces(SelectedTour, TourParticipantDTOs.Count);
            }
            catch (ArgumentNullException)
            {
                System.Windows.MessageBox.Show("something wrong happened");
            }
        }

        public TourReservationViewModel() { 
            _tourParticipantService = new TourParticipantService();
            _tourReservationService = new TourReservationService();
            _touristService = new TouristService();
            TourReservation = new TourReservationViewModel();

            TourParticipantDTOs = new List<TourParticipantViewModel>();
            TourParticipantsListBox = new List<TourParticipantViewModel>();
        }

        public TourReservationViewModel(TourReservation tourReservation)
        {
            _id = tourReservation.Id;
            _participantIds = tourReservation.ParticipantIds;
            _tourId= tourReservation.TourId;
            _startCheckpoint = tourReservation.StartCheckpoint;
        }

        public TourReservation ToTourReservation()
        {
            TourReservation tourReservation = new TourReservation(Id, _tourId, _touristId, _startCheckpoint);
            tourReservation.Id = _id;
            tourReservation.TourId = _tourId;
            tourReservation.TouristId = _touristId;
            tourReservation.StartCheckpoint = _startCheckpoint;
            tourReservation.ParticipantIds = _participantIds;
            return tourReservation;
        }
    }
}
