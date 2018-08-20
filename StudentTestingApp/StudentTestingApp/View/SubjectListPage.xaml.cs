using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectListPage : ContentPage, ISubjectListView
    {
        private ObservableCollection<Subject> subjects;

        public SubjectListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            subjects = new ObservableCollection<Subject>();
            subjectstListView.ItemsSource = subjects;
        }

        private void subjectTapped(object sender, ItemTappedEventArgs e)
        {
            OnSelectSubject?.Invoke();
        }

        #region ISubjectListView
        public event Action OnSelectSubject;
        public Subject SelectedSubject
        {
            get
            {
                return (Subject)subjectstListView.SelectedItem;
            }
        }

        public void Show()
        {
            App.Current.MainPage = new NavigationPage(this) { BarBackgroundColor = Color.FromHex("212121") };
        }

        public void SetSubjects(IEnumerable<Subject> subjects)
        {
            this.subjects.Clear();
            subjects.ToList().ForEach((subject) =>
            {
                this.subjects.Add(subject);
            });
        }
        #endregion
    }
}