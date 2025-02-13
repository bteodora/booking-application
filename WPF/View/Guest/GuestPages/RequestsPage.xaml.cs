﻿using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using System.Windows.Media.Animation;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        public ObservableCollection<DelayRequestViewModel> Requests { get; set; }
        public DelayRequestService DelayRequestService { get; set; }
        public User User { get; set; }
        public Frame Frame { get; set; }  

        public DelayRequestViewModel SelectedRequest { get; set; }
        
        public ShowRequestsViewModel ViewModel { get; set; }
        public RequestsPage(User user, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.Frame = frame;
            
          
            ViewModel = new ShowRequestsViewModel(User, Frame, this);
            
            DataContext = ViewModel;
            foreach (ComboBoxItem item in requestStatusBox.Items)
            {
                if (item.Content.ToString() == "Pending" || item.Content.ToString() == "Na čekanju")
                {
                    requestStatusBox.SelectedItem = item;
                    break;
                }
            }

            ViewModel.Update();
            Loaded += Page_Loaded;


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }




        private void RequestStatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.RequestStatusBox_SelectionChanged(sender, e);
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SelectedRequest = button.DataContext as DelayRequestViewModel;
            Frame.Content = new RequestDetailsPage(SelectedRequest, User, Frame);
        }
    }
}
