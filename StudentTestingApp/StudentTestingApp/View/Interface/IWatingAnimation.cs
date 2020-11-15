using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTestingApp.View.Interface
{
    public interface IWaitingAnimation
    {
        void StartAnimation(string message);

        void StopAnimation();
    }
}
