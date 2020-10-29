using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
            ClockToolbarItem.IconImageSource = "lock_clock.png";
        }

        private async void FinishTestEarlyClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Выход", "Вы действительно хотите завершить тестирование?", "Да", "Нет"))
                FinishTestEarlySelected?.Invoke();
        }

        #region ITestNavigationView

        public event Action FinishTestEarlySelected;

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = new NavigationPage(this)
                {
                    BarBackgroundColor = Color.FromHex("212121")
                });

        public void SetQuestionViews(IEnumerable<IQuestionView> questionViews)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Children.Clear();
                questionViews.ToList().ForEach(questionView =>
                {
                    Page page = (Page)questionView;
                    Children.Add(page);
                    page.Title = Children.Count.ToString();
                });
            });
        }

        public void SetRemainingTime(int seconds)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ClockToolbarItem.IconImageSource = "clock.png";
                RemainingSecondsToolbarItem.Text = seconds.ToString();
            });
        }

        #endregion
    }
}