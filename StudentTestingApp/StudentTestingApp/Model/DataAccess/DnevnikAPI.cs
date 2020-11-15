using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.Model.DataAccess
{
    public class DnevnikAPI
    {
        private RestClient _restClient;
        private long _userId;

        public DnevnikAPI(string accessToken, long userId)
        {
            _restClient = new RestClient("https://api.dnevnik.ru/v2.0");
            _restClient.AddDefaultHeader("Access-Token", accessToken);
            _userId = userId;
        }

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

        public void GetMarks()
        {
            var personId = (long)_restClient
                .Execute(new RestRequest("users/me/context", Method.GET))
                .JsonContentToDynamic()
                .personId;
            var marksByStudyYears = new Dictionary<long, List<int>>();
            var eduGroups = _restClient
                .Execute(new RestRequest($"persons/{personId}/edu-groups/all", Method.GET))
                .JsonContentToDynamic();

            foreach (var eduGroup in eduGroups)
            {
                var periods = new[]
                {
                    new Tuple<DateTime, DateTime>
                    (
                        new DateTime((int)eduGroup.studyyear, 9, 1),
                        new DateTime((int)eduGroup.studyyear, 12, 31)
                    ),
                    new Tuple<DateTime, DateTime>
                    (
                        new DateTime((int)eduGroup.studyyear + 1, 1, 1),
                        new DateTime((int)eduGroup.studyyear + 1, 5, 31)
                    )
                };
                foreach (var period in periods)
                {
                    var marks = _restClient
                        .Execute
                        (
                            new RestRequest
                            (
                                $"persons/{personId}/edu-groups/{eduGroup.id}/marks" +
                                $"/{period.Item1.Year}-{period.Item1.Month}-{period.Item1.Day}" +
                                $"/{period.Item2.Year}-{period.Item2.Month}-{period.Item2.Day}",
                                Method.GET
                            )
                        )
                        .JsonContentToDynamic();
                    foreach (var mark in marks)
                    {
                        if (!marksByStudyYears.ContainsKey(eduGroup.studyyear))
                            marksByStudyYears.Add(eduGroup.studyyear, new List<int>());
                        marksByStudyYears[eduGroup.studyyear].Add(int.Parse(mark.value));
                    }
                }
            }
            foreach (var pair in marksByStudyYears)
            {
                Console.WriteLine($"{pair.Key} {pair.Value.Count()}");
            }
        }
    }
}