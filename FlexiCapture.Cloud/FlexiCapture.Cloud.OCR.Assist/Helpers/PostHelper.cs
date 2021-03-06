﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.OCR.Assist.Models;

namespace FlexiCapture.Cloud.OCR.Assist.Helpers
{
    public static class PostHelper
    {
        /// <summary>
        /// make post request
        /// </summary>
        public static string MakePostRequest(string url, string json, ref string error)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
                error = "";
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                error = "Method Name: " + methodName + "Exception: " + exception.Message + " Inner exception:" +
                        innerException;
                return "";
            }
        }

        /// <summary>
        /// make post request
        /// </summary>
        public static string GetJobStatus(string url)
        {
            try
            {
                // Создаём объект WebClient
                using (var webClient = new WebClient())
                {
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    var response = webClient.DownloadString(url);
                    return response;
                }
            }
            catch (Exception exception)
            {
                return "";
            }
        }

        /// <summary>
        /// download path
        /// </summary>
        /// <param name="url"></param>
        /// <param name="downloadPath"></param>
        public static void DownloadFile(string url, string downloadPath, ref string error)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, downloadPath);
                }
                error = "";
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string exc = exception.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                error = "Exception: " + exc + "; Inner Exception: " + innerException + "; Method Name: " + methodName;
            }
        }
    }
}
