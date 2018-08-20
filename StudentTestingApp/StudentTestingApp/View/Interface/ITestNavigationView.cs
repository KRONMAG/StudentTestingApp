using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ITestNavigationView : IView
    {
        void ShowWithTimer(Test test);
        void SetQuestionViews(IEnumerable<IQuestionView> questionViews);
        event Action OnTestEnd;
    }
}