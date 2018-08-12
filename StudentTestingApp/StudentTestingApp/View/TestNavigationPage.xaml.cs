using System;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Model;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNavigationPage : TabbedPage, ITestNavigationView, INotifyPropertyChanged
    {
        public string ClockIconName { get; private set; }
        public int? RemainingSeconds { get; set; }

        public TestNavigationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            QuestionViews = new ObservableCollection<IQuestionView>();
            BindingContext = this;
        }

        private void OkClicked(object sender, EventArgs e)
        {
            if (DisplayAlert("Выход", "Вы действительно хотите завершить тестирование?", "Да", "Нет").Result)
                OnTestEnd?.Invoke();
        }

        #region INotifyPropertyChanged
        new public event PropertyChangedEventHandler PropertyChanged;

        new private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ITestNavigationView
        public event Action OnTestEnd;
        public ICollection<IQuestionView> QuestionViews { get; }

        public void Show(IParentView parentView)
        {
            ((Page)parentView).Navigation.PushAsync(this);
            ClockIconName = "lock_clock.png";
            OnPropertyChanged("ClockIconName");
        }

        public void ShowWithTimer(IParentView parentView, Test test)
        {
            Show(parentView);
            if (test.Duration != null)
            {
                ClockIconName = "clock.png";
                OnPropertyChanged("ClockIconName");
                RemainingSeconds = (int)test.Duration;
                OnPropertyChanged("RemainingSeconds");
                new Timer((o) =>
                {
                    RemainingSeconds -= 1;
                    PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("RemainingSeconds"));
                    if (RemainingSeconds == 0) OnTestEnd?.Invoke();
                }, null, 1000, 1000);
            }
        }
        #endregion
    }
}