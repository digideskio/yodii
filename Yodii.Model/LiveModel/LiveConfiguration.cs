﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;
using Yodii.Model.LiveModel;

namespace Yodii.Model
{
    class LiveConfiguration : ILiveConfiguration
    {
        List<ILivePluginInfo> _pluginLiveInfo;
        List<ILiveServiceInfo> _serviceLiveInfo;
        List<YodiiCommand> _yodiiCommands;

        internal LiveConfiguration( List<ILivePluginInfo> pluginLiveInfo, List<ILiveServiceInfo> serviceLiveInfo, List<YodiiCommand> YodiiCommands )
        {
            _pluginLiveInfo = pluginLiveInfo;
            _serviceLiveInfo = serviceLiveInfo;
            _yodiiCommands = YodiiCommands;
        }

        void AddYodiiCommand( YodiiCommand command )
        {
            Debug.Assert( command != null );
            //Adding any new item at the top of the list so that the most recent command gets handled first.
            _yodiiCommands.Insert( 0, command );
        }

        public IReadOnlyList<ILivePluginInfo> PluginLiveInfo { get { return _pluginLiveInfo.AsReadOnlyList(); } }

        public IReadOnlyList<ILiveServiceInfo> ServiceLiveInfo { get { return _serviceLiveInfo.AsReadOnlyList(); } }

        public IReadOnlyList<YodiiCommand> YodiiCommands { get { return _yodiiCommands.AsReadOnlyList(); } }
    }
}
