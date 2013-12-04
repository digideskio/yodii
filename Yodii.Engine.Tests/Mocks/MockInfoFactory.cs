﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yodii.Model;

namespace Yodii.Engine.Tests.Mocks
{
    internal static class MockInfoFactory
    {
        public static DiscoveredInfo CreateGraph001()
        {
            /**
             *                  +--------+                              +--------+
             *      +---------->|ServiceA+-------+   *----------------->|ServiceB|
             *      |           |        |       |   | Need Running     |        |   
             *      |           +---+----+       |   |                  +---+----+
             *      |               |            |   |                      |
             *      |               |            |   |                      |
             *      |               |            |   |                      |
             *  +---+-----+         |        +---+---*-+                    |
             *  |ServiceAx|     +---+-----+  |PluginA-2|                +---+-----+
             *  |         |     |PluginA-1|  |         |                |PluginB-1|
             *  +----+----+     |         |  +---------+                |         |
             *       |          +---------+                             +---------+
             *       |
             *  +----+-----+
             *  |PluginAx-1|
             *  |          |
             *  +----------+
             */

            var d = new DiscoveredInfo();

            d.ServiceInfos.Add( new ServiceInfo( "ServiceA", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "ServiceB", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "ServiceAx", d.DefaultAssembly ) );
            d.FindService( "ServiceAx" ).Generalization = d.FindService( "ServiceA" );

            d.PluginInfos.Add( new PluginInfo( "PluginA-1", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginA-2", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginAx-1", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginB-1", d.DefaultAssembly ) );

            d.FindPlugin( "PluginA-1" ).Service = d.FindService( "ServiceA" );
            d.FindPlugin( "PluginA-2" ).Service = d.FindService( "ServiceA" );
            d.FindPlugin( "PluginAx-1" ).Service = d.FindService( "ServiceAx" );
            d.FindPlugin( "PluginB-1" ).Service = d.FindService( "ServiceB" );

            d.FindPlugin( "PluginA-2" ).AddServiceReference( d.FindService( "ServiceB" ), DependencyRequirement.Running );

            return d;
        }

        public static DiscoveredInfo CreateGraph002()
        {
            /**
             *                 +--------+                              +--------+
             *     +---------->|ServiceA+-------+   *----------------->|ServiceB|
             *     |           +---+----+       |   | Need Running     +---+----+
             *     |               |            |   |                      |
             *     |               |            |   |                      |
             *     |               |            |   |                      |
             *     |               |            |   |                      |
             * +---+-----+     +---+-----+  +---+---*-+                +---+-----+
             * |ServiceAx|     |PluginA-1|  |PluginA-2|                |PluginB-1|
             * +----+----+     +---------+  +---------+                +---------+
             *      |
             *      |
             *      |--------|
             *      |   +----+-----+
             *      |   |PluginAx-1|
             *      |   +----------+
             *      |        
             *      |        
             *  +---+------+  
             *  |ServiceAxx|  
             *  +----+-----+  
             *       |      
             *       |      
             *       |      
             *       |      
             *  +---+-------+
             *  |PluginAxx-1|
             *  +-----------+
             */

            var d = new DiscoveredInfo();

            d.ServiceInfos.Add( new ServiceInfo( "ServiceA", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "ServiceB", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "ServiceAx", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "ServiceAxx", d.DefaultAssembly ) );
            d.FindService( "ServiceAx" ).Generalization = d.FindService( "ServiceA" );
            d.FindService( "ServiceAxx" ).Generalization = d.FindService( "ServiceAx" );

            d.PluginInfos.Add( new PluginInfo( "PluginA-1", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginA-2", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginAx-1", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginAxx-1", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginB-1", d.DefaultAssembly ) );

            d.FindPlugin( "PluginA-1" ).Service = d.FindService( "ServiceA" );
            d.FindPlugin( "PluginA-2" ).Service = d.FindService( "ServiceA" );
            d.FindPlugin( "PluginAx-1" ).Service = d.FindService( "ServiceAx" );
            d.FindPlugin( "PluginAxx-1" ).Service = d.FindService( "ServiceAxx" );
            d.FindPlugin( "PluginB-1" ).Service = d.FindService( "ServiceB" );

            d.FindPlugin( "PluginA-2" ).AddServiceReference( d.FindService( "ServiceB" ), DependencyRequirement.Running );

            return d;
        }

        public static DiscoveredInfo CreateGraph003()
        {
            /**
             *  +--------+
             *  |ServiceA+ ------+
             *  |        |       |
             *  +---+----+       |
             *      |            |
             *      |            |
             *      |            |
             *      |        +---+---*-+
             *  +---+-----+  |PluginA-2|
             *  |PluginA-1|  |         |
             *  |         |  +---------+
             *  +---------+
             */

            var d = new DiscoveredInfo();

            d.ServiceInfos.Add( new ServiceInfo( "ServiceA", d.DefaultAssembly ) );

            d.PluginInfos.Add( new PluginInfo( "PluginA-1", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginA-2", d.DefaultAssembly ) );

            d.FindPlugin( "PluginA-1" ).Service = d.FindService( "ServiceA" );
            d.FindPlugin( "PluginA-2" ).Service = d.FindService( "ServiceA" );

            return d;
        }

        public static DiscoveredInfo CreateGraph004()
        {
            /**
             *  +--------+
             *  |ServiceA+ ------+
             *  |        |       |
             *  +---+----+       |
             *      |            |
             *      |            |
             *      |            |
             *      |         +---+------+
             *  +---+------+  |ServiceAx2|
             *  |ServiceAx1|  |          |
             *  |          |  +----------+
             *  +----------+      |       
             *      |             |       
             *      |             |       
             *      |             |       
             *      |          +---+-------+
             *  +---+-------+  |PluginAx2-1|
             *  |PluginAx1-1|  |           |
             *  |           |  +-----------+
             *  +-----------+
             */

            var d = new DiscoveredInfo();

            d.ServiceInfos.Add( new ServiceInfo( "ServiceA", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "ServiceAx1", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "ServiceAx2", d.DefaultAssembly ) );
            d.FindService( "ServiceAx1" ).Generalization = d.FindService( "ServiceA" );
            d.FindService( "ServiceAx2" ).Generalization = d.FindService( "ServiceA" );

            d.PluginInfos.Add( new PluginInfo( "PluginAx1-1", d.DefaultAssembly ) );
            d.PluginInfos.Add( new PluginInfo( "PluginAx2-2", d.DefaultAssembly ) );

            d.FindPlugin( "PluginAx1-1" ).Service = d.FindService( "ServiceAx1" );
            d.FindPlugin( "PluginAx2-2" ).Service = d.FindService( "ServiceAx2" );

            return d;
        }

        public static DiscoveredInfo CreateGraph005()
        {
            #region graph
            /*
            *                  +--------+                            +--------+
            *      +-----------|Service1+                            |Service2|---------------+
            *      |           |Running |                            |Running |               |
            *      |           +---+----+                            +---+----+               |
            *      |               |                                      |                   |
            *      |               |                                      |                   |
            *      |               |                                      |                   |
            *  +---+-----+         |                                      |                   |
            *  |Plugin1  |     +---+-----+                            +---+-----+         +---+-----+
            *  |Optional |     |Plugin2  |                            |Plugin3  |         |Plugin4  |
            *  +----+----+     |Optional |                            |Optional |         |Optional |
            *       |          +---------+                            +---------+         +-----+---+
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
             *      |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |           +--------+            |                     |
            *       |                   |           |Service3+            |                     |
            *       |       +-----------|-----------|Optional|------------|------+--------------+-----------+
            *       |       |           |           +---+----+            |      |              |           |                
            *       |       |           |               |                 |      |              |           |                
            *       |       |           |               |                 |      |              |           |                
            *       |   +---+-------+   +-------->+-----+-----+           |  +---+-------+      |       +---+-------+        
            *       |   |Service3.1 |             |Service3.2 |           |  |Service3.3 |      |       |Service3.4 |        
            *       +-->|Optional   |             |Optional   |           +->|Optional   |<-----+       |Optional   |        
             *          +-----------+             +-----------+              +-----------+              +-----------+        
             *          |           |             |           |              |           |              |           |        
             *          |           |             |           |              |           |              |           |        
             *          |           |             |           |              |           |              |           |        
             *      +---+-----+ +---+-----+   +---+-----+ +---+-----+    +---+-----+ +---+-----+    +---+-----+ +---+-----+  
             *      |Plugin5  | |Plugin6  |   |Plugin7  | |Plugin8  |    |Plugin9  | |Plugin10 |    |Plugin11 | |Plugin12 |  
             *      |Optional | |Optional |   |Optional | |Optional |    |Optional | |Optional |    |Optional | |Optional |  
             *      +---------+ +---------+   +---------+ +---------+    +---------+ +---------+    +---------+ +---------+  
             * 
            */
            #endregion

            var d = new DiscoveredInfo();

            d.ServiceInfos.Add( new ServiceInfo( "Service1", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "Service2", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "Service3", d.DefaultAssembly ) );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.1", d.DefaultAssembly ) );
            d.FindService( "Service3.1" ).Generalization = d.FindService( "Service3" );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.2", d.DefaultAssembly ) );
            d.FindService( "Service3.2" ).Generalization = d.FindService( "Service3" );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.3", d.DefaultAssembly ) );
            d.FindService( "Service3.3" ).Generalization = d.FindService( "Service3" );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.4", d.DefaultAssembly ) );
            d.FindService( "Service3.4" ).Generalization = d.FindService( "Service3" );

            d.PluginInfos.Add( new PluginInfo( "Plugin1", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin1" ).Service = d.FindService( "Service1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin2", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin2" ).Service = d.FindService( "Service1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin3", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin3" ).Service = d.FindService( "Service2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin4", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin4" ).Service = d.FindService( "Service2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin5", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin5" ).Service = d.FindService( "Service3.1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin6", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin6" ).Service = d.FindService( "Service3.1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin7", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin7" ).Service = d.FindService( "Service3.2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin8", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin8" ).Service = d.FindService( "Service3.2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin9", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin9" ).Service = d.FindService( "Service3.3" );

            d.PluginInfos.Add( new PluginInfo( "Plugin10", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin10" ).Service = d.FindService( "Service3.3" );

            d.PluginInfos.Add( new PluginInfo( "Plugin11", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin11" ).Service = d.FindService( "Service3.4" );

            d.PluginInfos.Add( new PluginInfo( "Plugin12", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin12" ).Service = d.FindService( "Service3.4" );

            d.FindPlugin( "Plugin1" ).AddServiceReference( d.FindService( "Service3.1" ), DependencyRequirement.Running );
            d.FindPlugin( "Plugin2" ).AddServiceReference( d.FindService( "Service3.2" ), DependencyRequirement.Running );

            d.FindPlugin( "Plugin4" ).AddServiceReference( d.FindService( "Service3.3" ), DependencyRequirement.Running );
            d.FindPlugin( "Plugin3" ).AddServiceReference( d.FindService( "Service3.3" ), DependencyRequirement.Running );
         
            return d;
        }

        public static DiscoveredInfo CreateGraph005b()
        {
            #region graph
            /*
            *                  +--------+                            +--------+
            *      +-----------|Service1+                            |Service2|---------------+
            *      |           |Running |                            |Running |               |
            *      |           +---+----+                            +---+----+               |
            *      |               |                                      |                   |
            *      |               |                                      |                   |
            *      |               |                                      |                   |
            *  +---+-----+         |                                      |                   |
            *  |Plugin1  |     +---+-----+                            +---+-----+         +---+-----+
            *  |Optional |     |Plugin2  |                            |Plugin3  |         |Plugin4  |
            *  +----+----+     |Optional |                            |Optional |         |Optional |
            *       |          +---------+                            +---------+         +-----+---+
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |                                 |                     |
             *      |                   |                                 |                     |
            *       |                   |                                 |                     |
            *       |                   |           +--------+            |                     |
            *       |                   |           |Service3+            |                     |
            *       |       +-----------|-----------|Optional|------------|------+--------------+-----------+
            *       |       |           |           +---+----+            |      |              |           |                
            *       |       |           |               |                 |      |              |           |                
            *       |       |           |               |                 |      |              |           |                
            *       |   +---+-------+   +-------->+-----+-----+           |  +---+-------+      |       +---+-------+        
            *       |   |Service3.1 |             |Service3.2 |           |  |Service3.3 |      |       |Service3.4 |        
            *       +-->|Optional   |             |Optional   |           +->|Optional   |      +------>|Optional   |        
             *          +-----------+             +-----------+              +-----------+              +-----------+        
             *          |           |             |           |              |           |              |           |        
             *          |           |             |           |              |           |              |           |        
             *          |           |             |           |              |           |              |           |        
             *      +---+-----+ +---+-----+   +---+-----+ +---+-----+    +---+-----+ +---+-----+    +---+-----+ +---+-----+  
             *      |Plugin5  | |Plugin6  |   |Plugin7  | |Plugin8  |    |Plugin9  | |Plugin10 |    |Plugin11 | |Plugin12 |  
             *      |Optional | |Optional |   |Optional | |Optional |    |Optional | |Optional |    |Optional | |Optional |  
             *      +---------+ +---------+   +---------+ +---------+    +---------+ +---------+    +---------+ +---------+  
             * 
            */
            #endregion

            var d = new DiscoveredInfo();

            d.ServiceInfos.Add( new ServiceInfo( "Service1", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "Service2", d.DefaultAssembly ) );
            d.ServiceInfos.Add( new ServiceInfo( "Service3", d.DefaultAssembly ) );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.1", d.DefaultAssembly ) );
            d.FindService( "Service3.1" ).Generalization = d.FindService( "Service3" );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.2", d.DefaultAssembly ) );
            d.FindService( "Service3.2" ).Generalization = d.FindService( "Service3" );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.3", d.DefaultAssembly ) );
            d.FindService( "Service3.3" ).Generalization = d.FindService( "Service3" );

            d.ServiceInfos.Add( new ServiceInfo( "Service3.4", d.DefaultAssembly ) );
            d.FindService( "Service3.4" ).Generalization = d.FindService( "Service3" );

            d.PluginInfos.Add( new PluginInfo( "Plugin1", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin1" ).Service = d.FindService( "Service1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin2", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin2" ).Service = d.FindService( "Service1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin3", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin3" ).Service = d.FindService( "Service2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin4", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin4" ).Service = d.FindService( "Service2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin5", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin5" ).Service = d.FindService( "Service3.1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin6", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin6" ).Service = d.FindService( "Service3.1" );

            d.PluginInfos.Add( new PluginInfo( "Plugin7", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin7" ).Service = d.FindService( "Service3.2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin8", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin8" ).Service = d.FindService( "Service3.2" );

            d.PluginInfos.Add( new PluginInfo( "Plugin9", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin9" ).Service = d.FindService( "Service3.3" );

            d.PluginInfos.Add( new PluginInfo( "Plugin10", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin10" ).Service = d.FindService( "Service3.3" );

            d.PluginInfos.Add( new PluginInfo( "Plugin11", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin11" ).Service = d.FindService( "Service3.4" );

            d.PluginInfos.Add( new PluginInfo( "Plugin12", d.DefaultAssembly ) );
            d.FindPlugin( "Plugin12" ).Service = d.FindService( "Service3.4" );

            d.FindPlugin( "Plugin1" ).AddServiceReference( d.FindService( "Service3.1" ), DependencyRequirement.Running );
            d.FindPlugin( "Plugin2" ).AddServiceReference( d.FindService( "Service3.2" ), DependencyRequirement.Running );

            d.FindPlugin( "Plugin3" ).AddServiceReference( d.FindService( "Service3.3" ), DependencyRequirement.Running );
            d.FindPlugin( "Plugin4" ).AddServiceReference( d.FindService( "Service3.4" ), DependencyRequirement.Running );

            return d;
        }
    }
}
