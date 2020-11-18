using System;
using System.Linq;
using Xamarin.Essentials;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления списка результатов тестирования
    /// </summary>
    public class TestResultsPresenter : BasePresenter<ITestResultsView>
    {
        /// <summary>
        /// Средство показа анимации ожидания
        /// </summary>
        private readonly IWaitingAnimation _waitingAnimation;

        /// <summary>
        /// Механизм показа диалоговых сообщений
        /// </summary>
        private readonly IMessageDialog _messageDialog;

        /// <summary>
        /// Хранилище результатов тестирования
        /// </summary>
        private Repository<TestResult> _repository;

        /// <summary>
        /// Аутентификатор, обеспечивающий доступ к сети Дневника
        /// </summary>
        private readonly DnevnikApiAuthentificator _authentificator;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление списка результатов тестирования</param>
        /// <param name="messageDialog">Механизм показа диалоговых сообщений</param>
        /// <param name="waitingAnimation">Средство показа анимации ожидания</param>
        /// <param name="repository">Хранилище результатов тестирования</param>
        /// <param name="authentificator">Аутентификатор, использующийся для входа в систему Дневник</param>
        public TestResultsPresenter
            (ApplicationController controller,
            ITestResultsView view,
            IMessageDialog messageDialog,
            IWaitingAnimation waitingAnimation,
            Repository<TestResult> repository,
            DnevnikApiAuthentificator authentificator) :
            base(controller, view)
        {
            _messageDialog = messageDialog;
            _waitingAnimation = waitingAnimation;
            _repository = repository;
            _authentificator = authentificator;
            view.ShareTestResult += ShareTestResult;
            view.RemoveTestResult += RemoveTestResult;
            view.RemoveAllTestResults += RemoveAllTestResults;
        }

        /// <summary>
        /// Обработчик запроса распространения результата тестирования в сети Дневник
        /// </summary>
        /// <param name="id">Идентификатор результата, которым необходимо поделиться</param>
        public void ShareTestResult(int id)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                _messageDialog.ShowMessage("Отсутствует интернет-соединение, поделиться результатом невозможно");
            else if (_authentificator.NeedToLogIn)
                _messageDialog.ShowMessage("Необходимо войти в сеть Дневник.ру, чтобы поделиться результатом");
            else
            {
                _waitingAnimation.StartAnimation("Отправка результата", out Guid guid);
                Worker.Run
                (
                    () =>
                    {
                        var result = _repository.Get(id);
                        _authentificator.TryGetDnevnikApi(out DnevnikAPI api);
                        return api.ShareTestResult(result);
                    },
                    result =>
                    {
                        _waitingAnimation.StopAnimation(guid);
                        if (result)
                            _messageDialog.ShowMessage("Вы успешно поделились результатом");
                        else
                            _messageDialog.ShowMessage("Не удалось отправить результат тестирования");
                    },
                    _ => { }
                );
            }
        }

        /// <summary>
        /// Обработчик запроса удаления выбранного результата тестирования
        /// </summary>
        /// <param name="id">Идентификатор удаляемого результата</param>
        private void RemoveTestResult(int id)
        {
            _repository.Remove(id);
            LoadTestResults();
        }

        /// <summary>
        /// Обработчик запроса удаления всех результатов тестирования
        /// </summary>
        private void RemoveAllTestResults()
        {
            _repository.Clear();
            LoadTestResults();
        }

        /// <summary>
        /// Загрузка списка результатов тестирования в представление
        /// </summary>
        private void LoadTestResults()
        {
            view.ShowTestResults
            (
                _repository
                    .Get()
                    .OrderByDescending(result => result.StartDate)
                    .Select
                    (
                        result => new Tuple<int, string, string, DateTime, int, decimal>
                        (
                            result.Id,
                            result.SubjectName,
                            result.TestName,
                            result.StartDate,
                            (int)(result.EndDate - result.StartDate).TotalSeconds,
                            result.Score
                        )
                     )
                    .ToList()
             );
        }

        /// <summary>
        /// Показ результатов тестирования, представления
        /// </summary>
        public override void Run()
        {
            LoadTestResults();
            base.Run();
        }
    }
}