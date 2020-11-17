using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Contracts;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Средство показа страницы с анимацией ожидания
    /// </summary>
    public class WaitingAnimation : IWaitingAnimation
    {
        /// <summary>
        /// Очередь сообщений для отображения рядом с анимацией ожидания
        /// </summary>
        private IDictionary<Guid, string> _messages;

        /// <summary>
        /// Навигатор всплывающих окон
        /// </summary>
        private IPopupNavigation _navigation;

        /// <summary>
        /// Страница с анимацией ожидания
        /// </summary>
        private WaitingAnimationPage _page;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        public WaitingAnimation()
        {
            _messages = new Dictionary<Guid, string>();
            _navigation = PopupNavigation.Instance;
            _page = new WaitingAnimationPage();
        }

        /// <summary>
        /// Показ анимации ожидания с заданным сообщением
        /// </summary>
        /// <param name="message">Сообщение, отображаемое рядом с анимацией ожидания</param>
        /// <param name="guid">Идентификатор анимации</param>
        public void StartAnimation(string message, out Guid guid)
        {
            var animationGuid = Guid.NewGuid();
            guid = animationGuid;
            Device.BeginInvokeOnMainThread
            (
                () =>
                {
                    if (_messages.Count == 0)
                        _navigation.PushAsync(_page).ConfigureAwait(true);
                    _page.Message = message;
                    _messages.Add(animationGuid, message);
                }
            );
        }

        /// <summary>
        /// Остановка анимации ожидания с указанным идентификатором
        /// </summary>
        /// <param name="guid">Идентификатор анимации</param>
        public void StopAnimation(Guid guid) =>
            Device.BeginInvokeOnMainThread
            (
                () =>
                {
                    if (_messages.ContainsKey(guid))
                    {
                        _messages.Remove(guid);
                        if (_messages.Count == 0)
                        {
                            if (_navigation.PopupStack.Count == 1)
                                _navigation.PopAsync().ConfigureAwait(true);
                        }
                        else
                            _page.Message = _messages.Last().Value;
                    }
                }
            );
    }
}