﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinBookStoreApp.ViewModels;
using XamarinBookStoreApp.Views;

namespace XamarinBookStoreApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(AddBookPage), typeof(AddBookPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
