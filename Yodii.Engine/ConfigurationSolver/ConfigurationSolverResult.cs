﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using CK.Core;
using Yodii.Model;

namespace Yodii.Engine
{
    public class ConfigurationSolverResult : IConfigurationSolverResult
    {
        readonly List<IPluginSolved> _blockingPlugins;
        readonly List<IServiceSolved> _blockingServices;

        readonly List<IPluginSolved> _disabledPlugins;
        readonly int _availablePluginsCount;
        readonly List<IPluginSolved> _runningPlugins;

        internal ConfigurationSolverResult( List<IPluginSolved> blockingPlugins, List<IServiceSolved> blockingServices )
        {
            Debug.Assert( blockingPlugins != null || blockingServices != null );
            _blockingPlugins = blockingPlugins;
            _blockingServices = blockingServices;

            _blockingPlugins = blockingPlugins != null ? blockingPlugins : new List<IPluginSolved>();
            _blockingServices = blockingServices != null ? blockingServices : new List<IServiceSolved>();
        }

        public ConfigurationSolverResult( List<IPluginSolved> disabledPlugins, int availablePluginsCount, List<IPluginSolved> runningPlugins )
        {
            _disabledPlugins = disabledPlugins != null ? disabledPlugins : new List<IPluginSolved>();
            _availablePluginsCount = availablePluginsCount;
            _runningPlugins = runningPlugins != null ? runningPlugins : new List<IPluginSolved>();
        }

        public bool ConfigurationSuccess { get { return _blockingPlugins == null; } }

        public IReadOnlyList<IPluginSolved> BlockingPlugins { get { return ( _blockingPlugins != null ) ? _blockingPlugins.ToReadOnlyList() : null; } }

        public IReadOnlyList<IServiceSolved> BlockingServices { get { return ( _blockingServices != null ) ? _blockingServices.ToReadOnlyList() : null; } }

       
        //(Maybe) used later in tests, else TO BE REMOVED 
        public IReadOnlyCollection<IPluginSolved> DisabledPlugins
        {
            get { return (_disabledPlugins != null) ? _disabledPlugins.ToReadOnlyList() : null; }
        }

        public int AvailablePluginsCount
        {
            get { return _availablePluginsCount; }
        }

        //(Maybe) used later in tests, else TO BE REMOVED
        public IReadOnlyCollection<IPluginSolved> RunningPlugins
        {
            get { return (_runningPlugins != null) ? _runningPlugins.ToReadOnlyList() : null; }
        }
    }
}