using System;
using RestSharp;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Средство авторизации в API системы Дневник
    /// </summary>
    public class DnevnikApiAuthentificator
    {
        /// <summary>
        /// Ключ токена доступа к API Дневника
        /// </summary>
        private const string ACCESS_TOKEN_KEY = "dnevnik-access-token";

        /// <summary>
        /// Ключ даты истечения авторизации
        /// </summary>
        private const string EXPIRATION_DATE_KEY = "dnevnik-expiration-date";

        /// <summary>
        /// Ключ логина пользователя системы Дневник
        /// </summary>
        private const string LOGIN_KEY = "dnevnik-login";

        /// <summary>
        /// Ключ идентификатора авторизованного пользователя
        /// </summary>
        private const string USER_ID_KEY = "dnevnik-user-id";

        /// <summary>
        /// Настройки для хранения данных авторизации
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        public DnevnikApiAuthentificator() =>
            _settings = CrossSettings.Current;

        /// <summary>
        /// Необходимо ли выполнить вход в систему
        /// Ситуация возможна в нескольких случаях:
        /// 1) вход в Дневник ранее не производился
        /// 2) срок действия авторизации истек
        /// </summary>
        public bool NeedToLogIn =>
            !_settings.Contains(ACCESS_TOKEN_KEY) ||
            _settings.GetValueOrDefault(EXPIRATION_DATE_KEY, DateTime.MinValue) < DateTime.Now;

        /// <summary>
        /// Попытка входа в систему Дневник
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Истина, если авторизация выполнена успешна, иначе - ложь</returns>
        public bool TryLogIn(string login, string password)
        {
            var client = new RestClient("https://api.dnevnik.ru/v2.0");
            var request = new RestRequest("authorizations/bycredentials", Method.POST, DataFormat.Json);
            request.AddJsonBody
            (
                new
                {
                    client_id = "1d7bd105-4cd1-4f6c-9ecc-394e400b53bd",
                    client_secret = "5dcb5237-b5d3-406b-8fee-4441c3a66c99",
                    username = login,
                    password = password,
                    scope = "Wall, CommonInfo, EducationalInfo, FriendsAndRelatives"
                }
            );
            var response = client.Post(request);
            if (response.IsSuccessful)
            {
                var content = response.JsonContentToDynamic();
                _settings.AddOrUpdateValue(ACCESS_TOKEN_KEY, content.accessToken);
                _settings.AddOrUpdateValue
                (
                    EXPIRATION_DATE_KEY,
                    DateTime.Now.AddSeconds(content.expiresIn)
                );
                _settings.AddOrUpdateValue(LOGIN_KEY, login);
                _settings.AddOrUpdateValue(USER_ID_KEY, content.user);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Выход из системы Дневник
        /// </summary>
        public void LogOut()
        {
            _settings.Remove(ACCESS_TOKEN_KEY);
            _settings.Remove(EXPIRATION_DATE_KEY);
            _settings.Remove(LOGIN_KEY);
            _settings.Remove(USER_ID_KEY);
        }

        public bool TryGetDnevnikApi(out DnevnikAPI api)
        {
            if (!NeedToLogIn)
            {
                api = new DnevnikAPI
                (
                    _settings.GetValueOrDefault(ACCESS_TOKEN_KEY, null),
                    _settings.GetValueOrDefault(USER_ID_KEY, 0L)
                );
                return true;
            }
            else
            {
                api = null;
                return false;
            }
        }

        /// <summary>
        /// Попытка получения логина ранее авторизованного пользователя
        /// </summary>
        /// <param name="username">Логин пользователя, ранее вошедшего в  систему</param>
        /// <returns>Истина, если вход в систему ранее был совершен, иначе - ложь</returns>
        public bool TryGetLogin(out string username)
        {
            username = _settings.GetValueOrDefault(LOGIN_KEY, null);
            return _settings.Contains(LOGIN_KEY);
        }

        /// <summary>
        /// Получения даты, до которой действительная авторизация пользователя
        /// </summary>
        /// <param name="date">Дата, на момент которой авторизация пользователя истекат</param>
        /// <returns>Истина, если вход в систему ранее был совершен, иначе - ложь</returns>
        public bool TryGetExpirationDate(out DateTime date)
        {
            date = _settings.GetValueOrDefault(EXPIRATION_DATE_KEY, DateTime.MinValue);
            return _settings.Contains(EXPIRATION_DATE_KEY);
        }
    }
}