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

        readonly IReadOnlyCollection<IPluginInfo> _disabledPlugins;
        readonly IReadOnlyCollection<IPluginInfo> _stoppedPlugins;
        readonly IReadOnlyCollection<IPluginInfo> _runningPlugins;

        internal ConfigurationSolverResult( List<IPluginSolved> blockingPlugins, List<IServiceSolved> blockingServices )
        {
            Debug.Assert( blockingPlugins != null || blockingServices != null );
            _blockingPlugins = blockingPlugins;
            _blockingServices = blockingServices;

            //_blockingPlugins = blockingPlugins != null ? blockingPlugins.ToReadOnlyCollection() : CKReadOnlyListEmpty<IPluginInfo>.Empty;
            //_blockingServices = blockingServices != null ? blockingServices.ToReadOnlyCollection() : CKReadOnlyListEmpty<IServiceInfo>.Empty;
        }

        public ConfigurationSolverResult( List<IPluginInfo> disabledPlugins, List<IPluginInfo> stoppedPlugins, List<IPluginInfo> runningPlugins )
        {
            _disabledPlugins = disabledPlugins.ToReadOnlyCollection();
            _stoppedPlugins = stoppedPlugins.ToReadOnlyCollection();
            _runningPlugins = runningPlugins.ToReadOnlyCollection();
        }

        public bool ConfigurationSuccess { get { return _blockingPlugins == null; } }

        public IReadOnlyList<IServiceInfo> BlockingServices
        {
            get
            {
                List<IServiceInfo> l = new List<IServiceInfo>();
                for ( int i = 0; i < _blockingServices.Count; i++ )
                {
                    l.Add( _blockingServices[i].ServiceInfo );
                }
                return l.AsReadOnly();
            }
        }

        public IReadOnlyList<IPluginInfo> BlockingPlugins
        {
            get
            {
                List<IPluginInfo> l = new List<IPluginInfo>();
                for ( int i = 0; i < _blockingPlugins.Count; i++ )
                {
                    l.Add(_blockingPlugins[i].PluginInfo);
                }
                return l.AsReadOnly();
            }
        }

       //////

        public IReadOnlyCollection<IPluginInfo> DisabledPlugins
        {
            get { return _disabledPlugins; }
        }

        public IReadOnlyCollection<IPluginInfo> StoppedPlugins
        {
            get { return _stoppedPlugins; }
        }

        public IReadOnlyCollection<IPluginInfo> RunningPlugins
        {
            get { return _runningPlugins; }
        }
    }
}
