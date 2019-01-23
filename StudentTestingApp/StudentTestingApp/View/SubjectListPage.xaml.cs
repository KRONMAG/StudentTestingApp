﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectListPage : ContentPage, ISubjectListView
    {
        public SubjectListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SubjectTapped(object sender, ItemTappedEventArgs e)
        {
            SubjectSelected?.Invoke();
        }

        #region ISubjectListView

        public event Action SubjectSelected;
        public int SelectedSubjectId => ((Tuple<int, string>)SubjectsListView.SelectedItem).Item1;

        public void Show()
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    if (Application.Current.MainPage == null)
                    {
                        Application.Current.MainPage = new NavigationPage(this);
                    }
                    else
                    {
                        Application.Current.MainPage.Navigation.PushAsync(this);
                    }
                });
        }

        public void Close()
        {

        }

        public void SetSubjects(IEnumerable<Tuple<int, string>> subjects)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SubjectsListView.ItemsSource = new ObservableCollection<Tuple<int, string>>(subjects);
            });
        }

        #endregion
    }
}