﻿using System;
using System.Linq;
using System.Collections.Generic;
using RestSharp;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Взаимодействие с API Дневник.ру
    /// </summary>
    public class DnevnikAPI
    {
        /// <summary>
        /// REST-клиент
        /// </summary>
        private readonly RestClient _restClient;

        /// <summary>
        /// Идентификатор пользователя системы Дневник
        /// </summary>
        private readonly long _userId;

        /// <summary>
        /// Создание экземляра класса
        /// </summary>
        /// <param name="accessToken">Токен доступа к API Дневника</param>
        /// <param name="userId">Идентификатор пользователя, соответствущий токену</param>
        public DnevnikAPI(string accessToken, long userId)
        {
            _restClient = new RestClient("https://api.dnevnik.ru/v2.0");
            _restClient.AddDefaultHeader("Access-Token", accessToken);
            _userId = userId;
        }

        /// <summary>
        /// Попытка публикации записи о пройденном тесте на стене пользователя
        /// </summary>
        /// <param name="testResult">Результат тестирования</param>
        /// <returns>Истина, если публикация выполнена успешна, иначе - ложь</returns>
        public bool ShareTestResult(TestResult testResult)
        {
            var request = new RestRequest($"users/{_userId}/wallrecord", Method.POST, DataFormat.Json);
            var text = $"Завершил тест \"{testResult.TestName}\" " +
                        $"({testResult.SubjectName}) с результатом {testResult.Score}%";
            string fileUrl;
            if (testResult.Score < 51)
                fileUrl = @"https://avatars.mds.yandex.net/get-zen_doc/1888335/pub_5cc820c924de2d00b2ddce67_5cc8230c3e66cc00af04a2e4/scale_1200";
            else if (testResult.Score < 67)
                fileUrl = @"http://f2.24open.ru/aFOjOJ2PKQ.jpg";
            else if (testResult.Score < 85)
                fileUrl = @"https://vk.vkfaces.com/857124/v857124130/4b5fe/aYHWweZr5W8.jpg";
            else
                fileUrl = @"https://avatars.mds.yandex.net/get-zen_doc/99893/pub_5c93411a655b7c00b35068e2_5c93427188ee4b00b3f3bed9/scale_1200";
            request.AddJsonBody
            (
                new
                {
                    text = text,
                    fileUrl = fileUrl
                }
            );
            return _restClient.Execute(request).IsSuccessful;
        }

        /// <summary>
        /// Попытка получения количества оценок пользователя за все учебные года
        /// </summary>
        /// <param name="marks">Список с количеством полученных оценок за каждый учебный год</param>
        public bool TryGetMarks(out List<MarksStatistics> marksStatistics)
        {
            var contextResponse = _restClient
                .Execute(new RestRequest("users/me/context", Method.GET));
            
            if (!contextResponse.IsSuccessful)
            {
                marksStatistics = null;
                return false;
            }

            var personId = contextResponse.JsonContentToDynamic().personId;

            var eduGroupsResponse = _restClient.Execute
            (
                new RestRequest( $"persons/{personId}/edu-groups/all", Method.GET)
            );

            if (!eduGroupsResponse.IsSuccessful)
            {
                marksStatistics = null;
                return false;
            }

            var eduGroups = eduGroupsResponse.JsonContentToDynamic();

            var marksStatisticsByStudyYears = new Dictionary<int, MarksStatistics>();

            foreach (var eduGroup in eduGroups)
            {
                var studyYear = (int)eduGroup.studyyear;
                var periods = new[]
                {
                    new Tuple<DateTime, DateTime>
                    (
                        new DateTime(studyYear, 9, 1),
                        new DateTime(studyYear, 12, 31)
                    ),
                    new Tuple<DateTime, DateTime>
                    (
                        new DateTime(studyYear + 1, 1, 1),
                        new DateTime(studyYear + 1, 5, 31)
                    )
                };
                foreach (var period in periods)
                {
                    var marksResponse = _restClient
                        .Execute
                        (
                            new RestRequest
                            (
                                $"persons/{personId}/edu-groups/{eduGroup.id}/marks" +
                                $"/{period.Item1.Year}-{period.Item1.Month}-{period.Item1.Day}" +
                                $"/{period.Item2.Year}-{period.Item2.Month}-{period.Item2.Day}",
                                Method.GET
                            )
                        );

                    if (!marksResponse.IsSuccessful)
                    {
                        marksStatistics = null;
                        return false;
                    }

                    if (!marksStatisticsByStudyYears.ContainsKey(studyYear))
                    {
                        marksStatisticsByStudyYears.Add(studyYear, new MarksStatistics());
                        marksStatisticsByStudyYears[studyYear].StartYear = studyYear;
                        marksStatisticsByStudyYears[studyYear].EndYear = studyYear + 1;
                    }

                    var semesterMarks = marksResponse.JsonContentToDynamic();

                    foreach (var mark in semesterMarks)
                    {
                        switch (int.Parse(mark.value))
                        {
                            case 2:
                                marksStatisticsByStudyYears[studyYear].TwosCount++;
                                break;
                            case 3:
                                marksStatisticsByStudyYears[studyYear].ThreesCount++;
                                break;
                            case 4:
                                marksStatisticsByStudyYears[studyYear].FoursCount++;
                                break;
                            case 5:
                                marksStatisticsByStudyYears[studyYear].FivesCount++;
                                break;
                        }
                    }
                }
            }

            marksStatistics = marksStatisticsByStudyYears.Values.ToList();

            return true;
        }
    }
}