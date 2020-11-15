using System;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;
using Xamarin.Essentials;

namespace StudentTestingApp.Presenter
{
    public class SettingsPresenter : BasePresenter<ISettingsView>
    {
        private readonly IMessageDialog _messageDialog;
        private readonly IWaitingAnimation _waitingAnimation;
        private readonly DnevnikApiAuthentificator _authentificator;
        private readonly TestsLoader _testsLoader;

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
            _authentificator = authentificator;
            _testsLoader = testsLoader;

            view.UpdateTests += UpdateTests;
            view.TryLogInToDnevnik += TryLogInToDnevnik;
            view.LogOutFromDnevnik += LogOutFromDnevnik;
        }

        private void UpdateTests()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                _messageDialog.ShowMessage("Невозможно обновить тесты: отсутствует интернет-соединение");
            else
            {
                _waitingAnimation.StartAnimation("Обновление тестов");
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
                    result =>
                    {
                        _waitingAnimation.StopAnimation();
                        if (result)
                            controller.CreatePresenter<MainPresenter>().Run();
                    },
                    _ => { }
                ); ;
            }
        }

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
                _waitingAnimation.StartAnimation("Вход в Дневник.ру");
                Worker.Run
                (
                    () => _authentificator.TryLogIn(view.Login, view.Password),
                    result =>
                    {
                        _waitingAnimation.StopAnimation();
                        if (!result)
                            _messageDialog.ShowMessage
                            (
                                "Не удалось выполнить вход в Дневник.ру." +
                                "Проверьте правильность введенных данных"
                            );
                        else
                        {
                            _authentificator.TryGetLogin(out string login);
                            _authentificator.TryGetExpirationDate(out DateTime date);
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

        private void LogOutFromDnevnik()
        {
            _authentificator.LogOut();
            view.Login = string.Empty;
            view.Password = string.Empty;
            view.ShowExpirationDate();
        }

        public override void Run()
        {
            if (_authentificator.TryGetLogin(out string login) &&
                _authentificator.TryGetExpirationDate(out DateTime date))
            {
                view.Login = login;
                view.ShowExpirationDate(date);
            }
            base.Run();
        }
    }
}