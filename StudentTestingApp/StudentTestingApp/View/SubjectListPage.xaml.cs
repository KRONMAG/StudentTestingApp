using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using System.Collections.Generic;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectListPage : ContentPage, ISubjectListView
    {
        public SubjectListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Subjects = new ObservableCollection<Subject>();
            BindingContext = this;
        }

        private void SubjectTapped(object sender, ItemTappedEventArgs e)
        {
            OnSelectSubject?.Invoke();
        }

        #region ISubjectListView
        public event Action OnSelectSubject;
        public ICollection<Subject> Subjects { get; }
        public Subject SelectedSubject { get; set; }

        public void Show()
        {
            App.Current.MainPage = new NavigationPage(this) { BarBackgroundColor = Color.FromHex("212121") };
        }
        #endregion
    }
}