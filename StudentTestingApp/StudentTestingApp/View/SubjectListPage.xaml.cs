using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectListPage : ContentPage, ISubjectListView
    {
        public SubjectListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SubjectTapped(object sender, ItemTappedEventArgs e)
        {
            OnSelectSubject?.Invoke();
        }

        #region ISubjectListView

        public event Action OnSelectSubject;
        public int SelectedSubjectId => ((Tuple<int, string>) SubjectsListView.SelectedItem).Item1;

        public void Show()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new NavigationPage(this)
                    {BarBackgroundColor = Color.FromHex("212121")};
            });
        }

        public void Close()
        {
        }

        public void SetSubjects(IEnumerable<Tuple<int, string>> subjects)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SubjectsListView.ItemsSource = new ObservableCollection<Tuple<int, string>>(subjects);
            });
        }

        #endregion
    }
}