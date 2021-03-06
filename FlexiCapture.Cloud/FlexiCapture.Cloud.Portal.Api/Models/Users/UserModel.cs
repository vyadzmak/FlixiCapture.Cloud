﻿namespace FlexiCapture.Cloud.Portal.Api.Models.Users
{
    public class UserModel
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

       
        /// <summary>
        /// идентификатор роли пользователя
        /// </summary>
        public int UserRoleId { get; set; }

        /// <summary>
        /// имя пользователя
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// дата последней активности
        /// </summary>
        public string LastActivityDate { get; set; }

        /// <summary>
        /// company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        public string UserRoleName { get; set; }
        public string LoginState { get; set; }
        public string UserName { get; set; }
        public string RegistrationDate { get; set; }
        public int? ParentUserId { get; set; }
        public string ParentUserName { get; set; }
        /// <summary>
        /// get release and update notifications
        /// </summary>
        public bool GetReleaseUpdateNotif { get; set; }
        /// <summary>
        /// get Monthly use and payment notifications
        /// </summary>
        public bool GetUsePaymentNotif { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public UserModel()
        {
        }

    }
}