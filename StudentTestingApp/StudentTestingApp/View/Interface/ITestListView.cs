using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ITestListView : IView
    {
        void SetTests(IEnumerable<Test> tests);
        Test SelectedTest { get; }
        event Action OnSelectTest;
    }
}