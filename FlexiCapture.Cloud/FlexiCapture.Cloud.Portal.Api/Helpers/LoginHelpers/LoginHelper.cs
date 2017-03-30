﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.Users;
using FlexiCapture.Cloud.Portal.Api.Users;
using Khingal.Models.Users;
using UserServiceDataHelper = FlexiCapture.Cloud.Portal.Api.Helpers.UserServiceDataHelpers.UserServiceDataHelper;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.LoginHelpers
{
    public static class LoginHelper
    {
        /// <summary>
        /// login
        /// </summary>
        /// <returns></returns>
        public static AuthUserModel LoginUser(UserLoginModel model)
        {
            try
            {
                DB.UserLogins userLogin = DBHelpers.LoginHelper.LoginUser(model);

                AuthUserModel aModel = new AuthUserModel();

                aModel.LoginData = new UserLoginModel();
                aModel.LoginData.Id = userLogin.Id;
                aModel.LoginData.UserLogin = model.UserLogin;
                aModel.LoginData.UserPassword = userLogin.UserPassword;
                aModel.LoginData.UserId = userLogin.Users.Id;
                aModel.LoginData.UserLoginStateId = userLogin.UserLoginStates.Id;
                aModel.LoginData.UserLoginStateName = userLogin.UserLoginStates.StateName;
                if (userLogin.LastLoginDate!=null)
                aModel.LoginData.LastLoginDate = userLogin.LastLoginDate.Value;
                if (userLogin.RegistrationDate!=null)
                aModel.LoginData.RegistrationDate = userLogin.RegistrationDate.Value;

                aModel.UserData = new UserModel();
                aModel.UserData.Id = userLogin.Users.Id;
                aModel.UserData.FirstName = userLogin.Users.FirstName;
                aModel.UserData.LastName = userLogin.Users.LastName;
                aModel.UserData.Email = userLogin.Users.Email;
                aModel.UserData.PhoneNumber = userLogin.Users.PhoneNumber;
                aModel.UserData.UserRoleId = userLogin.UserRoleId;
                aModel.UserData.LastActivityDate = userLogin.LastLoginDate.ToString();
                aModel.UserData.CompanyName = userLogin.Users.CompanyName;
                aModel.ServiceData = UserServiceDataHelper.GetToUserDataServices(userLogin.Users.Id);
                return aModel;
            }
            catch (Exception exception)
            {
                ExceptionHelper.TraceException(System.Reflection.MethodBase.GetCurrentMethod().Name,exception);
                return new AuthUserModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Login Error",
                        ShortDescription = "Error data",
                        FullDescription = "A user with this credentials was not found. Please check the credentials and try again."

                    }
                };
            }
        }
    }
}