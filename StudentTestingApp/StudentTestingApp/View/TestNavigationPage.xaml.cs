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
        public TestNavigationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            ClockIconNameToolbarItem.IconImageSource = "lock_clock.png";
        }

        private async void OkClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Выход", "Вы действительно хотите завершить тестирование?", "Да", "Нет"))
                TestEnded?.Invoke();
        }

        #region ITestNavigationView

        public event Action TestEnded;

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

        public void StartTimer(int testDuration)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ClockIconNameToolbarItem.IconImageSource = "clock.png";
                RemainingSecondsToolbarItem.Text = testDuration.ToString();
            });
            new Timer(state =>
            {
                int remainingSeconds = int.Parse(RemainingSecondsToolbarItem.Text) - 1;
                Device.BeginInvokeOnMainThread(() =>
                    RemainingSecondsToolbarItem.Text = remainingSeconds.ToString());
                if (remainingSeconds == 0)
                    TestEnded?.Invoke();
            }, null, 1000, 1000);
        }

        #endregion
    }
}