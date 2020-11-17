using System;
using Xamarin.Essentials;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления настроек приложения
    /// </summary>
    public class SettingsPresenter : BasePresenter<ISettingsView>
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
        /// Средство авторизации в системе Дневник
        /// </summary>
        private readonly DnevnikApiAuthentificator _dnevnikApiAuthentificator;

        /// <summary>
        /// Загрузчик базы данных тестов
        /// </summary>
        private readonly TestsLoader _testsLoader;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление настроек приложения</param>
        /// <param name="messageDialog">Механизм показа диалоговых сообщений</param>
        /// <param name="waitingAnimation">Средство показа анимации ожидания</param>
        /// <param name="authentificator">Средство авторизации в системе Дневник</param>
        /// <param name="testsLoader">Загрузчик базы данных тестов</param>
        public SettingsPresenter
            (ApplicationController controller,
            ISettingsView view,
            IMessageDialog messageDialog,
            IWaitingAnimation waitingAnimation,
            DnevnikApiAuthentificator authentificator,
            TestsLoader testsLoader) :
            base(controller, view)
        {
            _messageDialog = messageDialog;
            _waitingAnimation = waitingAnimation;
            _dnevnikApiAuthentificator = authentificator;
            _testsLoader = testsLoader;

            view.UpdateTests += UpdateTests;
            view.TryLogInToDnevnik += TryLogInToDnevnik;
            view.LogOutFromDnevnik += LogOutFromDnevnik;
        }

        /// <summary>
        /// Обработчик события запроса обновления базы данных тестов
        /// </summary>
        private void UpdateTests()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                _messageDialog.ShowMessage("Невозможно обновить тесты: отсутствует интернет-соединение");
            else
            {
                _waitingAnimation.StartAnimation("Обновление тестов", out Guid guid);
                Worker.Run
                (
                    () =>
                    {
                        if (_testsLoader.HaveTestsBeenUpdated)
                        {
                            _messageDialog.ShowMessage("Загружена последняя версия тестов, обновление не требуется");
                            return false;
                        }
                        else if (!_testsLoader.LoadTests())
                        {
                            _messageDialog.ShowMessage("При обновлении тестов возникла ошибка");
                            return false;
                        }
                        else
                        {
                            _messageDialog.ShowMessage("Тесты успешно обновлены");
                            return true;
                        }
                    },
                    result => _waitingAnimation.StopAnimation(guid),
                    _ => { }
                ); ;
            }
        }

        /// <summary>
        /// Обработчик события запроса попытки входа в систему Дневник
        /// </summary>
        private void TryLogInToDnevnik()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                _messageDialog.ShowMessage("Отсутствует интернет-соединение, вход невозможен");
            else if (string.IsNullOrWhiteSpace(view.Login))
                _messageDialog.ShowMessage("Введите логин пользователя");
            else if (string.IsNullOrWhiteSpace(view.Password))
                _messageDialog.ShowMessage("Введите пароль пользователя");
            else
            {
                _waitingAnimation.StartAnimation("Вход в Дневник.ру", out Guid guid);
                Worker.Run
                (
                    () => _dnevnikApiAuthentificator.TryLogIn(view.Login, view.Password),
                    result =>
                    {
                        _waitingAnimation.StopAnimation(guid);
                        if (!result)
                            _messageDialog.ShowMessage
                            (
                                "Не удалось выполнить вход в Дневник.ру." +
                                "Проверьте правильность введенных данных"
                            );
                        else
                        {
                            _dnevnikApiAuthentificator.TryGetLogin(out string login);
                            _dnevnikApiAuthentificator.TryGetExpirationDate(out DateTime date);
                            view.Login = login;
                            view.Password = string.Empty;
                            view.ShowExpirationDate(date);
                            _messageDialog.ShowMessage("Вход в систему Дневник.ру выполнен успешно");
                        }
                    },
                    _ => { }
                );
            }
        }

        /// <summary>
        /// Обработчик запроса выхода из системы Дневник
        /// </summary>
        private void LogOutFromDnevnik()
        {
            _dnevnikApiAuthentificator.LogOut();
            view.Login = string.Empty;
            view.Password = string.Empty;
            view.ShowExpirationDate();
        }

        /// <summary>
        /// Отображение логина ранее авторизиванного пользовател и
        /// даты истечения авторизации в представлении, показ представления
        /// </summary>
        public override void Run()
        {
            if (_dnevnikApiAuthentificator.TryGetLogin(out string login) &&
                _dnevnikApiAuthentificator.TryGetExpirationDate(out DateTime date))
            {
                view.Login = login;
                view.ShowExpirationDate(date);
            }
            base.Run();
        }
    }
}