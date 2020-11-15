using System;
using RestSharp;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace StudentTestingApp.Model.DataAccess
{
    public class DnevnikApiAuthentificator
    {
        private const string ACCESS_TOKEN_KEY = "dnevnik-access-token";
        private const string EXPIRATION_DATE_KEY = "dnevnik-expiration-date";
        private const string LOGIN_KEY = "dnevnik-login";
        private const string USER_ID_KEY = "dnevnik-user-id";

        private readonly ISettings _settings;

        public DnevnikApiAuthentificator() =>
            _settings = CrossSettings.Current;

        public bool NeedToUpdateToken =>
            !_settings.Contains(ACCESS_TOKEN_KEY) ||
            _settings.GetValueOrDefault(EXPIRATION_DATE_KEY, DateTime.MinValue) < DateTime.Now;

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

        public void LogOut()
        {
            _settings.Remove(ACCESS_TOKEN_KEY);
            _settings.Remove(EXPIRATION_DATE_KEY);
            _settings.Remove(LOGIN_KEY);
            _settings.Remove(USER_ID_KEY);
        }

        public bool TryGetDnevnikApi(out DnevnikAPI api)
        {
            if (!NeedToUpdateToken)
            {
                api = new DnevnikAPI
                (
                    _settings.GetValueOrDefault(ACCESS_TOKEN_KEY, null),
                    _settings.GetValueOrDefault(USER_ID_KEY, 0)
                );
                return true;
            }
            else
            {
                api = null;
                return false;
            }
        }

        public bool TryGetLogin(out string username)
        {
            username = _settings.GetValueOrDefault(LOGIN_KEY, null);
            return _settings.Contains(LOGIN_KEY);
        }

        public bool TryGetExpirationDate(out DateTime date)
        {
            date = _settings.GetValueOrDefault(EXPIRATION_DATE_KEY, DateTime.MinValue);
            return _settings.Contains(EXPIRATION_DATE_KEY);
        }
    }
}