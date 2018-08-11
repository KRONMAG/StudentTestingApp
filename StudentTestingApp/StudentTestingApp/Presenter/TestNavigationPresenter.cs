using System;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestNavigationPresenter
    {
        public TestNavigationPresenter(IParentView parentView, ITestNavigationView testNavigationView)
        {
            testNavigationView.Show(parentView);
        }
    }
}