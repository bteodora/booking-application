﻿using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RequestDetailsPage.xaml
    /// </summary>
    public partial class RequestDetailsPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public RequestDetailsViewModel ViewModel { get; set; }

        public DelayRequestViewModel SelectedRequest { get; set; }
        public RequestDetailsPage(DelayRequestViewModel selectedRequest, User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            SelectedRequest = selectedRequest;
            ViewModel = new RequestDetailsViewModel(SelectedRequest, this);
           
            DataContext = ViewModel;

            Loaded += Page_Loaded;


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }

        private void ContentChanged(object sender, EventArgs e)
        {
          
        }


    }
}
