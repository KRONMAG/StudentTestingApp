using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ITestListView : IDerivedView
    {
        ICollection<Test> Tests { get; }
        Test SelectedTest { get; }
        event Action OnSelectTest;
    }
}