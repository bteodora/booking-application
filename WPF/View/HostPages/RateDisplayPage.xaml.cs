﻿using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Application.Services;
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
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
using BookingApp.Domain.Model.Features;

namespace BookingApp.WPF.View.HostPages
{
    /// <summary>
    /// Interaction logic for RateDisplayPage.xaml
    /// </summary>
    public partial class RateDisplayPage : Page
    {
        
        public RateDisplayPage(User user, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new RateDisplayPageViewModel(user, navigationService);
            
        }

    }
}
