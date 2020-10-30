using System;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// ������������� ���������� ������������
    /// </summary>
    public interface ITestResultView : IView
    {
        /// <summary>
        /// ������� ������� �������� � ������������� �������� ���� ����������
        /// </summary>
        event Action GoToMainView;

        /// <summary>
        /// ����� ���������� ������������
        /// </summary>
        /// <param name="elapsedTime">����� ����������� ����� � ��������</param>
        /// <param name="score">������� ���������� �������</param>
        void ShowTestResult(int elapsedTime, decimal score);

        /// <summary>
        /// ����� ���������
        /// </summary>
        /// <param name="message">����� ���������</param>
        void ShowMessage(string message);
    }
}