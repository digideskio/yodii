﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using QuickGraph;
using Yodii.Lab.Mocks;
using Yodii.Model;

namespace Yodii.Lab
{
    public class YodiiGraphEdge : Edge<YodiiGraphVertex>, INotifyPropertyChanged
    {
        readonly YodiiGraphEdgeType _type;
        RunningRequirement _referenceRequirement;

        internal YodiiGraphEdge( YodiiGraphVertex source, YodiiGraphVertex target, YodiiGraphEdgeType type )
            : base( source, target )
        {
            _type = type;
        }

        internal YodiiGraphEdge( YodiiGraphVertex source, YodiiGraphVertex target, MockServiceReferenceInfo serviceRef )
            : this( source, target, YodiiGraphEdgeType.ServiceReference )
        {
            serviceRef.PropertyChanged += serviceRef_PropertyChanged;
            _referenceRequirement = serviceRef.Requirement;
        }

        void serviceRef_PropertyChanged( object sender, System.ComponentModel.PropertyChangedEventArgs e )
        {
            if( e.PropertyName == "Requirement" ) ReferenceRequirement = (sender as MockServiceReferenceInfo).Requirement;
        }

        public RunningRequirement ReferenceRequirement
        {
            get { return _referenceRequirement; }
            set
            {
                if( value != _referenceRequirement )
                {
                    _referenceRequirement = value;
                    RaisePropertyChanged( "ReferenceRequirement" );
                }
            }
        }
        public YodiiGraphEdgeType Type { get { return _type; } }

        public bool IsSpecialization { get { return Type == YodiiGraphEdgeType.Specialization; } }
        public bool IsImplementation { get { return Type == YodiiGraphEdgeType.Implementation; } }
        public bool IsServiceReference { get { return Type == YodiiGraphEdgeType.ServiceReference; } }

        public string Description
        {
            get
            {
                if( IsSpecialization )
                {
                    return String.Format( "Specialization:\n\nService {0} specializes service {1}.",
                        Source.LiveServiceInfo.ServiceInfo.ServiceFullName,
                        Target.LiveServiceInfo.ServiceInfo.ServiceFullName );
                }
                else if( IsImplementation )
                {
                    return String.Format( "Implementation:\n\nPlugin {0} implements service {1}.",
                        Source.LivePluginInfo.PluginInfo.Description,
                        Target.LiveServiceInfo.ServiceInfo.ServiceFullName );
                }
                else
                {
                    return String.Format( "Service reference ({0}):\n\nPlugin {1} references service {2} with requirement: {0}.",
                        ReferenceRequirement.ToString(),
                        Source.LivePluginInfo.PluginInfo.Description,
                        Target.LiveServiceInfo.ServiceInfo.ServiceFullName);
                }
            }
        }

        #region INotifyPropertyChanged utilities

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Throw new PropertyChanged.
        /// </summary>
        /// <param name="caller">Fill with Member name, when called from a property.</param>
        protected void RaisePropertyChanged( string caller )
        {
            Debug.Assert( caller != null );
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( caller ) );
            }
        }

        #endregion INotifyPropertyChanged utilities
    }
}
