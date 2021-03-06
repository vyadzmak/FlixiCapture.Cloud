﻿using System;
using System.Collections.Generic;
using System.IO;
using FlexiCapture.Cloud.ServiceAssist;
using FlexiCapture.Cloud.ServiceAssist.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.OCR.Assist;
using FlexiCapture.Cloud.OCR.Assist.Models;
using Newtonsoft.Json;
using LogHelper = FlexiCapture.Cloud.ServiceAssist.DBHelpers.LogHelper;
using Tasks = FlexiCapture.Cloud.ServiceAssist.DB.Tasks;

namespace FlexiCapture.Cloud.EmailAttachmentService.Helpers.TaskHelpers
{
   public static class TaskHelper
    {
        /// <summary>
        /// execute task
        /// </summary>
        public static void ExecuteTask(Tasks task)
        {
            try
            {
                AssistProcessor assist = new AssistProcessor();
                Assist serviceAssist = new Assist();

                string planState = serviceAssist.CheckSubscriptionPlanAvailability(task.UserId);
                if (planState != "OK")
                {
                    serviceAssist.AddErrorToDocuments(task.Id, planState);
                    //update task
                    serviceAssist.UpdateTaskState(task.Id, 4);
                    //update documents
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);
                    return;
                }

                string url = serviceAssist.GetSettingValueByName("ApiUrl");
                string json = task.ProfileContent;
                string error = "";
                string response = assist.MakeOcr(url, json, ref error);
                if (string.IsNullOrEmpty(response))
                {
                    LogHelper.AddLog(error);
                    serviceAssist.AddErrorToDocuments(task.Id, error);
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);
                    serviceAssist.UpdateTaskState(task.Id, 4);

                    serviceAssist.EmailSettings = serviceAssist.GetToEmailConversionSettings(task.UserId);

                    if (serviceAssist.EmailSettings != null && serviceAssist.EmailSettings.ResponseSettings != null
                        && serviceAssist.EmailSettings.ResponseSettings.SendReply)
                    {
                        serviceAssist.SendEmailResponseFail(task.UserId, "DataCapture.Cloud received a conversion request form this e - mail address. Error occured while processing request.","");
                    }

                    return;
                }

                OcrResponseModel model = new OcrResponseModel();
                serviceAssist.UpdateTaskReponseContent(task.Id, response);
                model = JsonConvert.DeserializeObject<OcrResponseModel>(response);

                if (model.Status.Equals("Submitted"))
                {
                    serviceAssist.UpdateTaskState(task.Id, 2);
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 2);

                }
                else
                {
                    string errorText = "";
                    foreach (var ocrError in model.Errors)
                    {
                        errorText += ocrError.ErrorName + ": " + ocrError.ErrorMessage;
                    }
                    serviceAssist.AddErrorToDocuments(task.Id, errorText);
                    serviceAssist.UpdateTaskState(task.Id, 4);
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);

                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }
        }

        /// <summary>
        /// check task 
        /// </summary>
        public static void CheckStateTask(Tasks task)
        {
            try
            {
                AssistProcessor assist = new AssistProcessor();
                Assist serviceAssist = new Assist();
                OcrResponseModel model = new OcrResponseModel();
                model = JsonConvert.DeserializeObject<OcrResponseModel>(task.ResponseContent);

                serviceAssist.EmailSettings = serviceAssist.GetToEmailConversionSettings(task.UserId);
                if (serviceAssist.EmailSettings == null || serviceAssist.EmailSettings.ResponseSettings == null)
                {
                    return;
                }

                string jobStatus = assist.GetJobStatus(model.JobUrl);

                model = JsonConvert.DeserializeObject<OcrResponseModel>(jobStatus);

                if (model.Status.Equals("Finished"))
                {
                    string planState = serviceAssist.CheckSubscriptionPlan(task.UserId, model.Statistics.PagesArea);
                    if (planState != "OK")
                    {
                        serviceAssist.AddErrorToDocuments(task.Id, planState);
                        //update task
                        serviceAssist.UpdateTaskState(task.Id, 4);
                        //update documents
                        serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);
                        return;
                    }

                    List<Tuple<int, string>> downloadIds = new List<Tuple<int, string>>();
                    List <Tuple<string, string>> attachmentsLinks = new List<Tuple<string, string>>();



                    string pathToDownload = serviceAssist.GetSettingValueByName("MainPath");
                    string resultFolder = serviceAssist.GetSettingValueByName("ResultFolder");
                    string jobPattern = serviceAssist.GetSettingValueByName("ApiUrlJobState");

                    string downloadPath = Path.Combine(pathToDownload, resultFolder);

                    foreach (var file in model.Download)
                    {
                        string jobId = model.JobUrl.Replace(jobPattern, string.Empty);
                        Documents document = serviceAssist.GetToDocumentByTaskId(task.Id);
                        if (document == null)
                        {
                            serviceAssist.UpdateTaskState(task.Id, 4);
                            return;
                        }
                        string originalName = Path.GetFileNameWithoutExtension(document.OriginalFileName);
                        string fileExt = serviceAssist.GetToFileExtension(file.OutputFormat);

                        Guid g = Guid.NewGuid();
                        string newName = g.ToString() + fileExt;

                        originalName = originalName + fileExt;
                        string filePath = Path.Combine(downloadPath, newName);
                        string error = "";
                        assist.DownloadFile(file.Uri, filePath, ref error);
                        if (!File.Exists(filePath))
                        {
                            LogHelper.AddLog(error);
                            serviceAssist.AddErrorToDocuments(task.Id, error);
                            //update task
                            serviceAssist.UpdateTaskState(task.Id, 4);
                            //update documents
                            serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);
                            return;
                        }
                        //add document
                        int resultDocumentId = serviceAssist.AddResultDocument(task.Id, g, originalName, newName, filePath);
                        if (serviceAssist.EmailSettings.ResponseSettings.AddAttachment)
                        {
                            attachmentsLinks.Add(new Tuple<string, string>(filePath, originalName));
                        }
                        if (serviceAssist.EmailSettings.ResponseSettings.AddLink)
                        {
                            downloadIds.Add(new Tuple<int, string>(resultDocumentId, originalName));
                        }

                    }
                    if (serviceAssist.EmailSettings.ResponseSettings.SendReply)
                    {
                        string text = "DataCapture.Cloud received a conversion request form this e-mail address.  Here is your conversion result:";
                        serviceAssist.SendEmailResponse(task.UserId, downloadIds, attachmentsLinks, 
                            serviceAssist.EmailSettings.ResponseSettings.CCResponse? serviceAssist.EmailSettings.ResponseSettings.Addresses:"",
                            text);
                    }
                    //update task
                    serviceAssist.UpdateTaskState(task.Id, 3);
                    //update documents
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 3);
                    // add statistics
                    serviceAssist.AddStatisctics(task.Id, model.Statistics);
                }
                else if (model.Status.Equals("Processing"))
                {
                }
                else if (!model.Status.Equals("Submitted"))
                {
                    LogHelper.AddLog("Error in JobStatus: " + jobStatus);
                    string errorText = "";
                    foreach (var ocrError in model.Errors)
                    {
                        errorText += ocrError.ErrorName + ": " + ocrError.ErrorMessage;
                    }
                    serviceAssist.AddErrorToDocuments(task.Id, errorText);
                    //update task
                    serviceAssist.UpdateTaskState(task.Id, 4);
                    //update documents
                    serviceAssist.UpdateDocumentStatesByTaskId(task.Id, 4);

                    if (serviceAssist.EmailSettings.ResponseSettings.SendReply)
                    {
                        serviceAssist.SendEmailResponseFail(task.UserId, "DataCapture.Cloud received a conversion request form this e - mail address. Error occured while processing request.", "");
                    }
                }

            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }
        }
    }
}
