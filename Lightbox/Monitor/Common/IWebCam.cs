// <copyright file="IWebCam.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace Monitor.Common
{
    /// <summary>
    /// Interface for the Webcam Class.
    /// </summary>
    public interface IWebCam
    {
        /// <summary>
        /// Gets or sets a value indicating whether InUse is on or off.
        /// </summary>
        bool InUse { get; set; }

        /// <summary>
        /// Gets or sets the OffUri parameter.
        /// </summary>
        string OffUri { get; set; }

        /// <summary>
        /// Gets or sets the OnUri parameter.
        /// </summary>
        string OnUri { get; set; }

        /// <summary>
        /// Starts the WebCam Service Wrapper.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the status of the webcam.</returns>
        Task<int> WebCamServiceWrapperAsync();
    }
}