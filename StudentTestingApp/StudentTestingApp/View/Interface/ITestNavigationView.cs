using System;
using System.Collections.Generic;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View.Interface
{
    public interface ITestNavigationView : IView
    {
        void ShowWithTimer(Test test);
        void AddQuestionView(IQuestionView questionView);
        event Action OnTestEnd;
    }
}