﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Yodii.Model;

namespace Yodii.Engine
{
    partial class PluginData
    {
        RunningStatus? _dynamicStatus;
        PluginRunningStatusReason _dynamicReason;

        public RunningStatus? DynamicStatus { get { return _dynamicStatus; } }

        /// <summary>
        /// Called after Service DynamicResetState.
        /// </summary>
        public void DynamicResetState()
        {
            switch( FinalConfigSolvedStatus )
            {
                case SolvedConfigurationStatus.Disabled: 
                    {
                        _dynamicReason = PluginRunningStatusReason.StoppedByConfig;
                        _dynamicStatus = RunningStatus.Disabled;
                        break;
                    }
                case SolvedConfigurationStatus.Running: 
                    {
                        Debug.Assert( Service == null || (Service.Family.RunningPlugin == this) );
                        _dynamicReason = PluginRunningStatusReason.StartedByConfig;
                        _dynamicStatus = RunningStatus.RunningLocked;
                        break;
                    }
                default:
                    {
                        Debug.Assert( Service == null || !Service.Disabled );
                        _dynamicReason = PluginRunningStatusReason.None;
                        _dynamicStatus = null;
                        break;
                    }
            }
        }

        internal bool DynamicCanStart( StartDependencyImpact impact )
        {
            if( _dynamicStatus != null ) return _dynamicStatus.Value >= RunningStatus.Running;
            if( impact == StartDependencyImpact.Unknown ) impact = ConfigSolvedImpact;
            return DynTestCanStart( impact );
        }

        public bool DynamicStartByCommand( StartDependencyImpact impact )
        {
            if( _dynamicStatus != null ) return _dynamicStatus.Value >= RunningStatus.Running;
            if( impact == StartDependencyImpact.Unknown ) impact = ConfigSolvedImpact;
            if( !DynTestCanStart( impact ) ) return false;
            _dynamicStatus = RunningStatus.Running;
            _dynamicReason = PluginRunningStatusReason.StartedByCommand;
            DynamicStartBy( impact, PluginRunningStatusReason.StartedByCommand );
            return true;
        }

        bool DynTestCanStart( StartDependencyImpact impact )
        {
            foreach( var s in GetExcludedServices( impact ) )
            {
                if( s.DynamicStatus != null && s.DynamicStatus.Value >= RunningStatus.Running ) return false;
            }
            foreach( var s in GetIncludedServices( impact, false ) )
            {
                if( s.DynamicStatus != null && s.DynamicStatus.Value <= RunningStatus.Stopped ) return false;
            }
            return true;
        } 

        public PluginRunningStatusReason GetStoppedReasonForStoppedReference( DependencyRequirement requirement )
        {
            StartDependencyImpact impact = _configSolvedImpact;
            switch( requirement )
            {
                case DependencyRequirement.Running: return PluginRunningStatusReason.StoppedByRunningReference;
                case DependencyRequirement.RunnableRecommended:
                    if( impact >= StartDependencyImpact.StartRecommended )
                    {
                        return PluginRunningStatusReason.StoppedByRunnableRecommendedReference;
                    }
                    break;
                case DependencyRequirement.Runnable:
                    if( impact == StartDependencyImpact.FullStart )
                    {
                        return PluginRunningStatusReason.StoppedByRunnableReference;
                    }
                    break;
                case DependencyRequirement.OptionalRecommended:
                    if( impact >= StartDependencyImpact.StartRecommended )
                    {
                        return PluginRunningStatusReason.StoppedByOptionalRecommendedReference;
                    }
                    break;
                case DependencyRequirement.Optional:
                    if( impact == StartDependencyImpact.FullStart )
                    {
                        return PluginRunningStatusReason.StoppedByOptionalReference;
                    }
                    break;
            }
            return PluginRunningStatusReason.None;
        }

        public bool DynamicStopByCommand()
        {
            if( _dynamicStatus != null ) return _dynamicStatus.Value <= RunningStatus.Stopped;
            DynamicStopBy( PluginRunningStatusReason.StoppedByCommand );
            return true;
        }

        internal void DynamicStopBy( PluginRunningStatusReason reason )
        {
            Debug.Assert( _dynamicStatus == null );
            Debug.Assert( reason != PluginRunningStatusReason.None );
            _dynamicStatus = RunningStatus.Stopped;
            _dynamicReason = reason;
            if( Service != null )
            {
                Service.OnPluginStopped( true );
                Service.OnPostPluginStopped();
            }
        }

        internal void DynamicStartBy( StartDependencyImpact impact, PluginRunningStatusReason reason )
        {
            Debug.Assert( _dynamicStatus == null || _dynamicStatus.Value >= RunningStatus.Running );
            if( _dynamicStatus == null )
            {
                _dynamicStatus = RunningStatus.Running;
                _dynamicReason = reason;
            }
            if( Service != null ) Service.OnDirectPluginStarted( this );

            foreach( var sRef in PluginInfo.ServiceReferences )
            {
                ServiceData s = _solver.FindExistingService( sRef.Reference.ServiceFullName );
                s.DynamicStartByDependency( impact, sRef.Requirement );
            }
        }       

    }
}
