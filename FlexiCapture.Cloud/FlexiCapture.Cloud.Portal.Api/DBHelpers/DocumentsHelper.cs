﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using System.Data.Entity;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.Models.Documents;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    /// documents helper
    /// </summary>
    public class DocumentsHelper
    {
        /// <summary>
        /// add document to database
        /// </summary>
        /// <returns></returns>
        public static int AddDocument(int taskId, HttpPostedFile file, Guid guid, string gFileName,string path, string md5)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    Documents document = new Documents()
                    {
                        Date = DateTime.Now,
                        DocumentStateId = 1,
                        DocumentTypeId = DocumentTypesHelper.GetToDocumentFileType(Path.GetExtension(file.FileName)),
                        FileName = gFileName,
                        FileSize = file.ContentLength,
                        Hash = md5,
                        Guid = guid,
                        Path = path,
                        OriginalFileName = file.FileName,
                        TaskId = taskId
                        

                    };
                    db.Documents.Add(document);
                    db.SaveChanges();
                    return document.Id;
                }

                return -1;
            }
            catch (Exception exception)
            {
                return -1;
            }
            
        }

        /// <summary>
        /// get to all documents by user id and service id
        /// </summary>
        /// <returns></returns>
        public static string GetDocumentsByUserServiceId(string baseUrl, int userId, int serviceId)
        {
            try
            {
                List<DocumentModel> models = new List<DocumentModel>();

                using (var db = new FCCPortalEntities())
                {
                    List<Documents> documents = db.Documents
                        .Include(x => x.DocumentStates)
                        .Include(x => x.DocumentTypes)
                        .Include(x => x.Tasks)
                        .Include(x=>x.Tasks)
                        .Where(x => x.Tasks.UserId == userId && x.Tasks.ServiceId == serviceId).ToList();

                    foreach (var document in documents)
                    {
                        DocumentModel model = new DocumentModel()
                        {
                            Id = document.Id,
                            DateTime = document.Date.ToString(),
                            FileSizeBytes = document.FileSize,
                            OriginalFileName = document.OriginalFileName,
                            FileSize =Math.Round((double)(document.FileSize)/(1024*1024),2),
                            StateName = document.DocumentStates.Name,
                            StateId = document.DocumentStateId,
                            TaskId = document.TaskId.ToString(),
                            TypeId = document.DocumentTypeId,
                            TypeName = document.DocumentTypes.Name,
                            Url = document.Path
                            
                        };
                        //этот момент переделать, чтобы не давать полный адрес к файлу
                        model.Url = baseUrl +"/"+ model.Url;
                        models.Add(model);

                    }
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(models);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}