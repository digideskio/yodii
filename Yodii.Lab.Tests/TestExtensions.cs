﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Yodii.Model;

namespace Yodii.Lab.Tests
{
    class TestExtensions
    {
        /// <summary>
        /// Assert equivalence between two IPluginInfo, in the context of Yodii.Lab.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void AssertPluginEquivalence( IPluginInfo a, IPluginInfo b, bool inspectServices = false )
        {
            if( a == null && b == null ) return;

            Assert.That( a != null && b != null );

            Assert.That( a.PluginId == b.PluginId );
            Assert.That( a.PluginFullName == b.PluginFullName );

            if( a.Service == null )
                Assert.That( b.Service == null );
            else if( inspectServices )
            {
                AssertServiceEquivalence( a.Service, b.Service, false );
            }

            Assert.That( a.ServiceReferences.Count == b.ServiceReferences.Count );
            foreach(var referenceB in b.ServiceReferences)
            {
                var referenceA = a.ServiceReferences.Where( x => x.Owner.PluginId == referenceB.Owner.PluginId &&
                    x.Reference.ServiceFullName == referenceB.Reference.ServiceFullName &&
                    x.Requirement == referenceB.Requirement ).FirstOrDefault();

                Assert.That( referenceA != null );

                AssertServiceEquivalence( referenceA.Reference, referenceB.Reference, false );
            }
        }

        /// <summary>
        /// Assert equivalence between two IServiceInfo, in the context of Yodii.Lab.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void AssertServiceEquivalence( IServiceInfo a, IServiceInfo b, bool inspectPlugins = false )
        {
            if( a == null && b == null ) return;

            Assert.That( a != null && b != null );
            Assert.That( a.ServiceFullName == b.ServiceFullName );
            AssertServiceEquivalence( a.Generalization, b.Generalization, inspectPlugins );

            Assert.That( a.Implementations.Count == b.Implementations.Count );
            if( inspectPlugins )
            {
                foreach( var pluginB in b.Implementations )
                {
                    Assert.That( a.Implementations.Where( x => x.PluginId == pluginB.PluginId ).Count() == 1 );
                    var pluginA = a.Implementations.Where( x => x.PluginId == pluginB.PluginId ).First();

                    AssertPluginEquivalence( pluginA, pluginB, false );
                }
            }
            
        }

        /// <summary>
        /// Assert equivalence between two IServiceReferenceInfo, in the context of Yodii.Lab.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void AssertServiceReferenceEquivalence( IServiceReferenceInfo a, IServiceReferenceInfo b )
        {
            if( a == null && b == null ) return;

            Assert.That( a != null && b != null );
        }
    }
}