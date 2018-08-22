using System;
using System.Collections.Generic;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.View.Interface
{
    public interface ITestNavigationView : IDerivedView
    {
        void StartTimer(Test test);
        void SetQuestionViews(IEnumerable<IQuestionView> questionViews);
        event Action OnTestEnd;
    }
}