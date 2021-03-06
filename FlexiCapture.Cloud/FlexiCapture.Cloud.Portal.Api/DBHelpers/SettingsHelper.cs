﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class SettingsHelper
    {
        /// <summary>
        /// get to settings value
        /// </summary>
        /// <returns></returns>
        public static string GetSettingsValueByName(string name)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.Settings.FirstOrDefault(x => x.SettingName.ToLower().Equals(name.ToLower())).SettingValue;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }
        }
    }
}