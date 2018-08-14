using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuestionPage : ContentPage, IQuestionView
	{
		public QuestionPage ()
		{
			InitializeComponent ();
            Answers = new ObservableCollection<Answer>();
            SelectedAnswers = new Collection<Answer>();
            BindingContext = this;
        }

        #region IQuestionView
        public Question Question { get; set; }
        public ICollection<Answer> Answers { get; }
        public IEnumerable<Answer> SelectedAnswers { get; }
        #endregion
    }
}