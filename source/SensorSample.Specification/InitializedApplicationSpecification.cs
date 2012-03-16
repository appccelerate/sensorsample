//-------------------------------------------------------------------------------
// <copyright file="RunningApplicationSpecification.cs" company="Appccelerate">
//   Copyright (c) 2008-2012
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace SensorSample.Specification
{
    using System.Collections.Generic;

    using Appccelerate.Bootstrapper;

    using Machine.Specifications;

    using SensorSample.Sensors;
    using SensorSample.Sirius;

    public class InitializedApplicationSpecification
    {
        protected static DefaultBootstrapper<ISensor> bootstrapper;

        protected static TestableBootstrapperStrategy bootstrapperStrategy;

        protected static IVhptBlackHoleSubOrbitDetectionEngine blackHoleSubOrbitDetectionEngine;

        protected static IDictionary<string, IDictionary<string, string>> configuration;

        protected static IVhptDoor door;

        protected static IVhptFileLogger fileLogger;

        protected static IVhptTravelCoordinator travelCoordinator;

        Establish context = () =>
            {
                bootstrapperStrategy = new TestableBootstrapperStrategy();

                blackHoleSubOrbitDetectionEngine = bootstrapperStrategy.BlackHoleSubOrbitDetectionEngine;
                configuration = bootstrapperStrategy.Configuration;
                door = bootstrapperStrategy.Door;
                fileLogger = bootstrapperStrategy.FileLogger;
                travelCoordinator = bootstrapperStrategy.TravelCoordinator;

                bootstrapper = new DefaultBootstrapper<ISensor>();

                bootstrapper.Initialize(bootstrapperStrategy);
            };

        Cleanup stuff = () => bootstrapper.Dispose();

        protected static void RunApplication()
        {
            bootstrapper.Run();
        }
    }
}