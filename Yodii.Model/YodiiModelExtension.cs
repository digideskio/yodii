﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yodii.Model
{
    /// <summary>
    /// Extensions for Yodii interfaces.
    /// </summary>
    public static class YodiiModelExtension
    {

        /// <summary>
        /// </summary>
        /// <param name="this">This live plugin/service info.</param>
        /// <returns>The engine result.</returns>
        public static IYodiiEngineResult Start( this ILiveYodiiItem @this )
        {
            return @this.Start( null, StartDependencyImpact.Unknown );
        }

        /// <summary>
        /// </summary>
        /// <param name="this">This live plugin/service info.</param>
        /// <param name="callerKey">Caller key.</param>
        /// <returns>The engine result.</returns>
        public static IYodiiEngineResult Start( this ILiveYodiiItem @this, string callerKey )
        {
            return @this.Start( callerKey, StartDependencyImpact.Unknown );
        }

        /// <summary>
        /// </summary>
        /// <param name="this">This live plugin/service info.</param>
        /// <param name="impact">The impact.</param>
        /// <returns>The engine result.</returns>
        public static IYodiiEngineResult Start( this ILiveYodiiItem @this, StartDependencyImpact impact )
        {
            return @this.Start( null, impact );
        }

        /// <summary>
        /// </summary>
        /// <param name="this">This live plugin/service info.</param>
        /// <returns>The engine result.</returns>
        public static IYodiiEngineResult Stop( this ILiveYodiiItem @this )
        {
            return @this.Stop( null );
        }
    }
}
