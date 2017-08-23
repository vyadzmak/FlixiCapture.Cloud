﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.Models;
using FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public static class FtpConversionSettingsHelper
    {
        public static FTPConversionSettingModel GetSettingsByUserId(int userId)
        {
            try
            {
                using (FCCPortalEntities2 db = new FCCPortalEntities2())
                {
                    var settings = db.FTPConversionSettings
                        .SingleOrDefault(x => x.UserId == userId);

                    if (settings == null)
                        return new FTPConversionSettingModel();

                    return new FTPConversionSettingModel(settings.Id, settings.AddProcessed,
                        settings.ReturnResults, settings.MirrorInput, settings.MoveProcessed, settings.UserId);

                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                throw;
            }
        }

        public static FTPConversionSettingModel AddSettings(FTPConversionSettingModel model)
        {
            try
            {
                using (FCCPortalEntities2 db = new FCCPortalEntities2())
                {
                    var settings = db.FTPConversionSettings
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (settings != null)
                    {
                        settings.UserId = model.UserId;
                        settings.AddProcessed = model.AddProcessed;
                        settings.MirrorInput = model.MirrorInput;
                        settings.MoveProcessed = model.MoveProcessed;
                        settings.ReturnResults = model.ReturnResults;

                        db.SaveChanges();
                    }
                    else
                    {
                        settings = new DB.FTPConversionSettings();
                        settings.UserId = model.UserId;
                        settings.AddProcessed = model.AddProcessed;
                        settings.MirrorInput = model.MirrorInput;
                        settings.MoveProcessed = model.MoveProcessed;
                        settings.ReturnResults = model.ReturnResults;

                        db.FTPConversionSettings.Add(settings);
                        db.SaveChanges();
                        model.Id = settings.Id;
                    }

                    return model;

                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }
    }
}