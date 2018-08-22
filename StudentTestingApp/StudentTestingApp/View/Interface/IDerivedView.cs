using System;

namespace StudentTestingApp.View.Interface
{
    public interface IDerivedView : IView
    {
        void Show(IParentView parentView);
        void Close();
    }
}