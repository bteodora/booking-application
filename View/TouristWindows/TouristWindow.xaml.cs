﻿using BookingApp.ViewModel;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TouristWindow.xaml
    /// </summary>
    public partial class TouristWindow : Window, INotifyPropertyChanged
    {
        public List<TourViewModel> Tours { get; set; }
        public Tour SelectedTour {  get; set; }
        private readonly TouristService _touristService;

        // trebace napraviti userService
        private readonly UserRepository _userRepository;

        #region Property
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        private int _maximumValuePeoples;


        public string MaximumValuePeoples
        {
            get
            {
                return _maximumValuePeoples.ToString();
            }
            set
            {
                if (value != _maximumValuePeoples.ToString())
                {
                    _maximumValuePeoples = Convert.ToInt32(value);
                    OnPropertyChanged(nameof(_maximumValuePeoples));
                }
            }
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
        public TouristWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            _touristService = new TouristService();
            _userRepository = new UserRepository();
            Tours = new List<TourViewModel>();
            Username = username;

            MainFrame.Content = new AllToursPage(getUserId());
            MaximumValuePeoples = _touristService.FindMaxNumberOfParticipants().ToString();

            Update();
        }

        public void Update()
        {
            Tours.Clear();
            List<TourViewModel> ToursViewModel = _touristService.GetAllTours();
            foreach(TourViewModel tour in ToursViewModel)
            {
                Tours.Add(tour);
            }
        }

        private void DurationSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            e.Handled = IsFloat(textBox, e);
        }

        private bool IsFloat(TextBox textBox, TextCompositionEventArgs e)
        {
            if((!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
                || (e.Text == "." && textBox.Text.Contains("."))
                || (e.Text == "." && textBox.Text.Length == 0))
            {
                return true;
            }
            return false;
        }

        private void DurationSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                return;
            }
        }

        private void PeopleSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsDigit(e);
        }

        private bool IsDigit(TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                return true;

            return false;
        }

        private void PeopleSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                return;
            }
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new AllToursPage(getUserId());
            AllToursButton.Background = darkerBlue();
        }

        private void MyToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new MyToursPage(getUserId());
            MyToursButton.Background = darkerBlue();
        }

        private void EndedToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new EndedToursPage(getUserId());
            EndedToursButton.Background = darkerBlue();
        }
        private void VouchersButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new VouchersPage(getUserId());
            VouchersButton.Background = darkerBlue();
        }

        private int getUserId()
        {
            return _userRepository.GetByUsername(Username).Id;
        }

        private void resetButtonColors()
        {
            AllToursButton.Background = lighterBlue();
            MyToursButton.Background = lighterBlue();
            EndedToursButton.Background = lighterBlue();
            RequestedToursButton.Background = lighterBlue();
            VouchersButton.Background = lighterBlue();
            LogOutButton.Background = lighterBlue();
        }

        private SolidColorBrush darkerBlue()
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#344299"));
        }
        private SolidColorBrush lighterBlue()
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5FD9"));
        }

    }
}
