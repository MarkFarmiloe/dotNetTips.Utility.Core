﻿// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 11-26-2019
// ***********************************************************************
// <copyright file="WebClientExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class WebClientExtensions.
    /// </summary>
    public static class WebClientExtensions
    {
        /// <summary>
        /// Gets the json.
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="url">The URL.</param>
        /// <returns>T.</returns>
        /// <exception cref="System.ArgumentNullException">URL cannot be empty or null.</exception>
        /// <exception cref="System.ArgumentException">URL cannot be empty or null.</exception>
        public static T ConvertFrom<T>(this WebClient client, string url)
            where T : class
        {
            if (client is null)
            {
                throw new System.ArgumentNullException(nameof(client));
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new System.ArgumentException("URL cannot be empty or null.", nameof(url));
            }

            var data = client.DownloadString(url);

            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var obj = (T)serializer.ReadObject(stream);

                stream.Flush();
                stream.Close();

                return obj;
            }
        }
    }
}