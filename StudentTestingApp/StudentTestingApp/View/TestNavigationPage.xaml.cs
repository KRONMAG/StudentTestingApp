using System;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Model;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNavigationPage : TabbedPage, ITestNavigationView, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int RemainingSeconds { get; set; }

        public TestNavigationPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        #region ITestNavigationView
        public event Action OnTestEnd;
        public ICollection<IQuestionView> QuestionViews { get; }

        public void Show(IParentView parentView)
        {
            ((Page)parentView).Navigation.PushAsync(this);
        }

        public void ShowWithTimer(IParentView parentView, Test test)
        {
            if (test.Duration != null)
            {
                RemainingSeconds = (int)test.Duration;
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("RemainingSeconds"));
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