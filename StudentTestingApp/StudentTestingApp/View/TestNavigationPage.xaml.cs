using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNavigationPage : TabbedPage, ITestNavigationView
    {
        private Timer _timer;

        public TestNavigationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            ClockIconNameToolbarItem.Icon = "lock_clock.png";
        }

        private async void OkClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Выход", "Вы действительно хотите завершить тестирование?", "Да", "Нет"))
            {
                OnTestEnd?.Invoke();
            }
        }

        #region ITestNavigationView

        public event Action OnTestEnd;

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

        public void SetQuestionViews(IEnumerable<IQuestionView> questionViews)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Children.Clear();
                questionViews.ToList().ForEach(questionView =>
                {
                    Page page = (Page) questionView;
                    Children.Add(page);
                    page.Title = Children.Count.ToString();
                });
            });
        }

        public void StartTimer(int testDuration)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ClockIconNameToolbarItem.Icon = "clock.png";
                RemainingSecondsToolbarItem.Text = testDuration.ToString();
                _timer = new Timer(state =>
                {
                    int remainingSeconds = int.Parse(RemainingSecondsToolbarItem.Text) - 1;
                    Device.BeginInvokeOnMainThread(() =>
                        RemainingSecondsToolbarItem.Text = remainingSeconds.ToString());
                    if (remainingSeconds == 0)
                    {
                        OnTestEnd?.Invoke();
                    }
                }, null, 1000, 1000);
            });
        }

        #endregion
    }
}