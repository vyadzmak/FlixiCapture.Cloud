﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    /// <summary>
    /// email content
    /// </summary>
    public class EmailContentModel
    {
        #region constructor
        /// <summary>
        /// constructor to serialize
        /// </summary>
        public EmailContentModel()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// constructor
        /// </summary>
        public EmailContentModel(string textElements)
        {
            try
            {
                Elements = new List<EmailContentElementModel>();

                List<string> splitElements = textElements.Split(';').ToList();

                foreach (var splitElement in splitElements)
                {
                    List<string> separateElements = splitElement.Split('=').ToList();
                    if (separateElements.Count == 2)
                    {
                        Elements.Add(new EmailContentElementModel(){ Name = separateElements[0], Value = separateElements[1]});
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        
        /// <summary>
        /// model elements
        /// </summary>
        public List<EmailContentElementModel> Elements { get; set; } 
        #endregion
    }
}