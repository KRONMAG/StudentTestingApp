using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Model.DataAccess;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления статистики по количеству оценок, полученных обучающимся
    /// </summary>
    public class MarksStatisticsPresenter : BasePresenter<IMarksView>
    {
        /// <summary>
        /// Механизм показа диалоговых сообщений
        /// </summary>
        private readonly IMessageDialog _messageDialog;

        /// <summary>
        /// Средство показа анимации ожидания
        /// </summary>
        private readonly IWaitingAnimation _waitingAnimation;

        /// <summary>
        /// Аутентификатор, обеспечивающий доступ к сети Дневника
        /// </summary>
        private readonly DnevnikApiAuthentificator _authentificator;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление статистики по количеству оценок</param>
        /// <param name="messageDialog">Механизм показа диалоговых сообщений</param>
        /// <param name="waitingAnimation">Средство показа анимации ожидания</param>
        /// <param name="authentificator">Аутентификатор, обеспечивающий доступ к сети Дневника</param>
        public MarksStatisticsPresenter
            (ApplicationController controller,
            IMarksView view,
            IMessageDialog messageDialog,
            IWaitingAnimation waitingAnimation,
            DnevnikApiAuthentificator authentificator) : base(controller, view)
        {
            _messageDialog = messageDialog;
            _waitingAnimation = waitingAnimation;
            _authentificator = authentificator;
        }

        /// <summary>
        /// Загрузка данных об оценках из системы Дневник и их отображение в представлении,
        /// показ представления
        /// </summary>
        public override void Run()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                _messageDialog.ShowMessage("Отсутствует интернет-соединение, просмотр оценок невозможен");
            else if (_authentificator.NeedToLogIn)
                _messageDialog.ShowMessage("Войдите в сеть Дневник.ру для просмотра оценок");
            else
            {
                _waitingAnimation.StartAnimation("Загрузка оценок", out Guid guid);
                Worker.Run
                (
                    () =>
                    {
                        _authentificator.TryGetDnevnikApi(out DnevnikAPI api);
                        var result = api.TryGetMarks(out List<MarksStatistics> marksStatistics);
                        return (result, marksStatistics);
                    },
                    arg =>
                    {
                        _waitingAnimation.StopAnimation(guid);
                        if (arg.result)
                            view.ShowMarksЫStatistics
                            (
                                arg.marksStatistics.Select(item => new Tuple<int, int, int, int, int, int>
                                (
                                    item.StartYear,
                                    item.EndYear,
                                    item.TwosCount,
                                    item.ThreesCount,
                                    item.FoursCount,
                                    item.FivesCount
                                ))
                                .OrderBy(item => item.Item1)
                                .ToList()
                            );
                        else
                            _messageDialog.ShowMessage("Не удалось загрузить данные об оценках");
                    },
                    _ => { }
                );
            }
            base.Run();
        }
    }
}