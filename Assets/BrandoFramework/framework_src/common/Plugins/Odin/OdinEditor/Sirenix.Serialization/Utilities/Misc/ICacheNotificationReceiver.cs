//-----------------------------------------------------------------------// <copyright file="ICacheNotificationReceiver.cs" company="Sirenix IVS"> // Copyright (c) Sirenix IVS. All rights reserved.// </copyright>//-----------------------------------------------------------------------
namespace Sirenix.Serialization.Utilities
{
    /// <summary>
    /// Provides notification callbacks for values that are cached using the <see cref="Cache{T}"/> class.
    /// </summary>
    internal interface ICacheNotificationReceiver
    {
        /// <summary>
        /// Called when the cached value is freed.
        /// </summary>
        void OnFreed();

        /// <summary>
        /// Called when the cached value is claimed.
        /// </summary>
        void OnClaimed();
    }
}