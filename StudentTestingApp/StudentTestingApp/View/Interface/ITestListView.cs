using System;
using System.Collections.Generic;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.View.Interface
{
    public interface ITestListView : IDerivedView
    {
        void SetTests(IEnumerable<Test> tests);
        Test SelectedTest { get; }
        event Action OnSelectTest;
    }
}