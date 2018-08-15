using System;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNavigationPage : TabbedPage, ITestNavigationView, INotifyPropertyChanged
    {
        public string ClockIconName { get; private set; }
        public int? RemainingSeconds { get; private set; }

        public TestNavigationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            QuestionViews = new ObservableCollection<IQuestionView>();
            ((ObservableCollection<IQuestionView>)QuestionViews).CollectionChanged += QuestionViewsChanged;
            BindingContext = this;
        }

        private void QuestionViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Children.Clear();
            foreach (var questionView in QuestionViews)
            {
                var page = (Page)questionView;
                Children.Add(page);
                page.Title = Children.Count.ToString();
            }
        }

        private async void OkClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Выход", "Вы действительно хотите завершить тестирование?", "Да", "Нет"))
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
            ((Page)parentView).Navigation.PushAsync(this);
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
        #endregion
    }
}