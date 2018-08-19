﻿using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestListPage : ContentPage, ITestListView
    {
        public ObservableCollection<Test> tests;

        public TestListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            tests = new ObservableCollection<Test>();
            testsListView.ItemsSource = tests;
        }

        private void testTapped(object sender, ItemTappedEventArgs e)
        {
            OnSelectTest?.Invoke();
        }

        #region ITestListView
        public event Action OnSelectTest;
        public Test SelectedTest
        {
            get
            {
                return (Test)testsListView.SelectedItem;
            }
        }

        public void Show()
        {
            App.Current.MainPage.Navigation.PushAsync(this);
        }

        public void AddTest(Test test)
        {
            tests.Add(test);
        }
        #endregion
    }
}