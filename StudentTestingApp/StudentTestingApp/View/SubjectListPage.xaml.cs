using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Presenter;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectListPage : ContentPage, ISubjectListView
    {
        public class SubjectView
        {
            public string Name { get; set; }
        }

        public event Action OnSelectSubject;
        public ObservableCollection<SubjectView> Subjects { get; }
        public string SelectedSubjectName { get; private set; }

        public SubjectListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Subjects = new ObservableCollection<SubjectView>();
            BindingContext = this;
        }

        public void Show()
        {
            App.Current.MainPage = new NavigationPage(this);
        }

        public void ShowSubject(string name)
        {
            Subjects.Add(new SubjectView() { Name = name });
        }

        private void SubjectTapped(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedSubjectName = ((SubjectView)((ListView)sender).SelectedItem).Name;
            OnSelectSubject?.Invoke();
        }
    }
}