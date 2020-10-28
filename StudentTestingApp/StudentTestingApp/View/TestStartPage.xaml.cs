﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestStartPage : ContentPage, ITestStartView
    {
        public TestStartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void StartTestTapped(object sender, EventArgs e) =>
            TestStarted?.Invoke();

        #region ITestStartView

        public event Action TestStarted;

        public string StudentName => StudentNameEntry.Text;

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        public void SetTest(string name, int questionCount, int? duration)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NameLabel.Text = name;
                QuestionCountLabel.Text = $"Количество вопросов: {questionCount}";
                DurationLabel.Text = duration == null
                    ? "Продолжительность неограничена"
                    : $"Продолжительность: {duration} сек.";
            });
        }

        #endregion
    }
}