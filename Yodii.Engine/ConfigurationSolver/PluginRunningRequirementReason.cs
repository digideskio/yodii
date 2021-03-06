﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yodii.Model;

namespace Yodii.Engine
{
    enum PluginRunningRequirementReason
    {
        None = 0,

        /// <summary>
        /// Initialized by PluginData constructor.
        /// </summary>
        Config,

        /// <summary>
        /// Sets by ServiceData.RetrieveTheOnlyPlugin.
        /// </summary>
        FromServiceConfigToSinglePlugin,

        /// <summary>
        /// Sets by ServiceData.RetrieveTheOnlyPlugin and ServiceData.SetRunningRequirement.
        /// </summary>
        FromServiceToSinglePlugin,
        FromServiceToMultiplePlugin,

    }

}
