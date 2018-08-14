using System;
using System.Collections.Generic;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View.Interface
{
    public interface ITestNavigationView : IDerivedView
    {
        void ShowWithTimer(IParentView parentView, Test test);
        ICollection<IQuestionView> QuestionViews { get; }
        event Action OnTestEnd;
    }
}