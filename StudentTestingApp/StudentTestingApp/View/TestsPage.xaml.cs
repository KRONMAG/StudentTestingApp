﻿using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestsPage : ContentPage, ITestsView
    {
        public TestsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void TestTapped(object sender, ItemTappedEventArgs e) =>
            TestSelected?.Invoke();

        #region ITestListView

        public event Action TestSelected;

        public int SelectedTestId => ((Tuple<int, string>)TestsListView.SelectedItem).Item1;

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        public void SetTests(IReadOnlyList<Tuple<int, string>> tests)
        {
            var isEmpty = !tests.Any();
            Device.BeginInvokeOnMainThread(() =>
            {
                EmptyTestsLabel.IsVisible = isEmpty;
                TestsListView.IsVisible = !isEmpty;
                TestsListView.ItemsSource = tests;
            });
        }

        #endregion
    }
}