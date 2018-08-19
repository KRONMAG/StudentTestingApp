using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ITestListView : IView
    {
        void AddTest(Test test);
        Test SelectedTest { get; }
        event Action OnSelectTest;
    }
}