// <copyright file="WebCam.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace Monitor.Common
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Win32;

    /// <summary>
    /// WebCam Class.
    /// </summary>
    public class WebCam : IWebCam
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebCam"/> class.
        /// </summary>
        public WebCam()
        {
            if (IsWebCamInUse())
            {
                this.InUse = true;
            }
            else
            {
                this.InUse = false;
            }
        }

        /// <inheritdoc/>
        public bool InUse { get; set; } = false;

        /// <inheritdoc/>
        public string OnUri { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string OffUri { get; set; } = string.Empty;

        /// <inheritdoc/>
        public async Task<int> WebCamServiceWrapperAsync()
        {
            bool currentState = IsWebCamInUse();

            if (currentState != this.InUse)
            {
                if (currentState)
                {
                    Console.WriteLine($"Sending On Uri {this.OnUri}");
                    await this.WebCamPostAsync(this.OnUri);
                    this.InUse = true;
                }
                else
                {
                    Console.WriteLine($"Sending Off Uri {this.OffUri}");
                    await this.WebCamPostAsync(this.OffUri);
                    this.InUse = false;
                }
            }

            return 0;
        }

        /// <summary>
        /// Checks if the webcam is in use.
        /// </summary>
        private static bool IsWebCamInUse()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam"))
            {
                if (key is not null)
                {
                    foreach (var subKeyName in key.GetSubKeyNames())
                    {
                        using (var subKey = key.OpenSubKey(subKeyName))
                        {
                            if (subKey is not null)
                            {
                                if (subKey.GetValueNames().Contains("LastUsedTimeStop"))
                                {
                                    var endTime = subKey.GetValue("LastUsedTimeStop") is long ? (long)subKey.GetValue("LastUsedTimeStop") : -1;
                                    if (endTime <= 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the webcam is in use.
        /// </summary>
        /// <param name="uri">The Uri to send a post request to.</param>
        private async Task WebCamPostAsync(string uri)
        {
            HttpClient httpClient = new HttpClient();

            try
            {
                var response = await httpClient.PostAsync(uri, null);
                var responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Exception {ex.Message}");
                await Console.Out.WriteLineAsync($"Error sending HTTP POST to {uri}");
            }
        }
    }
}
