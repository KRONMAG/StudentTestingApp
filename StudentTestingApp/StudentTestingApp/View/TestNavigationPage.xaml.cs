using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNavigationPage : TabbedPage, ITestNavigationView
    {
        public TestNavigationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void okClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Выход", "Вы действительно хотите завершить тестирование?", "Да", "Нет"))
                OnTestEnd?.Invoke();
        }

        #region ITestNavigationView
        public event Action OnTestEnd;

        public void Show()
        {
            App.Current.MainPage.Navigation.PushAsync(this);
            clockIconNameToolbarItem.Icon = "lock_clock.png";
        }

        public void ShowWithTimer(Test test)
        {
            App.Current.MainPage.Navigation.PushAsync(this);
            clockIconNameToolbarItem.Icon = "clock.png";
            remainingSecondsToolbarItem.Text = test.Duration.ToString();
            new Timer((state) =>
            {
                var remainingSeconds = int.Parse(remainingSecondsToolbarItem.Text) - 1;
                Device.BeginInvokeOnMainThread(() => remainingSecondsToolbarItem.Text = remainingSeconds.ToString());
                if (remainingSeconds == 0) OnTestEnd?.Invoke();
            }, null, 1000, 1000);
        }

        public void SetQuestionViews(IEnumerable<IQuestionView> questionViews)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Children.Clear();
                questionViews.ToList().ForEach((questionView) =>
                {
                    var page = (Page)questionView;
                    Children.Add(page);
                    page.Title = Children.Count.ToString();
                });
            });
        }
        #endregion
    }
}