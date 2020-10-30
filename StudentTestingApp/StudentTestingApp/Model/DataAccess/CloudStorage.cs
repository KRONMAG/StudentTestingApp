using System.IO;
using Dropbox.Api;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Реализация работы с облачным хранилищем
    /// </summary>
    public class CloudStorage
    {
        /// <summary>
        /// Токен доступа к облачному хранилищу
        /// </summary>
        private const string ACCESS_TOKEN = "QiNJ84AteaUAAAAAAAAAAWNpABuTzBfouVCnhUDdfzBtcmq5TvADh39UmsoRmssR";

        /// <summary>
        /// Загрузка файла из облачного хранилища
        /// </summary>
        /// <param name="remoteFilePath">Путь к файлу, расположенному в хранилище</param>
        /// <param name="localFilePath">Локальный путь, по которому будет размещен загружаемый файл</param>
        /// <returns>Истина, если файл успешно загружен, иначе - ложь</returns>
        public bool DownloadFile(string remoteFilePath, string localFilePath)
        {
            try
            {
                var client = new DropboxClient(ACCESS_TOKEN);
                var buffer = client.Files
                    .DownloadAsync(remoteFilePath)
                    .Result
                    .GetContentAsByteArrayAsync()
                    .Result;
                File.WriteAllBytes(localFilePath, buffer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Получение хеш-значения содержимого файла, расположенного в облачном хранилище
        /// </summary>
        /// <param name="filePath">Путь к файлу в облачном хранилище</param>
        /// <returns>
        /// Хеш-значение файла или значение null,
        /// если произошла ошибка при подключении к хранилищу
        /// </returns>
        public string GetFileHash(string filePath)
        {
            try
            {
                var client = new DropboxClient(ACCESS_TOKEN);
                return client.Files.GetMetadataAsync(filePath).Result.AsFile.ContentHash;
            }
            catch
            {
                return null;
            }
        }
    }
}