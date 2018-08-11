using System;

namespace StudentTestingApp.View.Interface
{
    public interface IDerivedView
    {
        void Show(IParentView parentView);
    }
}