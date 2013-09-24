//-------------------------------------------------------------------------------
// <copyright file="SensorResolver.cs" company="Appccelerate">
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

namespace SensorSample.Bootstrapping
{
    using Appccelerate.Bootstrapper;
    using Appccelerate.EvaluationEngine;
    using Appccelerate.StateMachine;

    using SensorSample.Asynchronous;
    using SensorSample.Sensors;
    using SensorSample.Sirius;

    public class SensorResolver : IExtensionResolver<ISensor>
    {
        private readonly IAsynchronousFileLogger fileLogger;

        private readonly IEvaluationEngine evaluationEngine;

        private readonly IVhptDoor door;

        private readonly IVhptBlackHoleSubOrbitDetectionEngine blackHoleSubOrbitDetectionEngine;

        private readonly IVhptTravelCoordinator travelCoordinator;

        private readonly IStateMachine<States, Events> stateMachine;

        public SensorResolver(
            IAsynchronousFileLogger fileLogger, 
            IEvaluationEngine evaluationEngine,
            IVhptDoor door,
            IVhptBlackHoleSubOrbitDetectionEngine blackHoleSubOrbitDetectionEngine,
            IVhptTravelCoordinator travelCoordinator, 
            IStateMachine<States, Events> stateMachine)
        {
            this.fileLogger = fileLogger;
            this.evaluationEngine = evaluationEngine;
            this.door = door;
            this.blackHoleSubOrbitDetectionEngine = blackHoleSubOrbitDetectionEngine;
            this.travelCoordinator = travelCoordinator;
            this.stateMachine = stateMachine;
        }

        public void Resolve(IExtensionPoint<ISensor> extensionPoint)
        {
            extensionPoint.AddExtension(
                new DoorSensor(
                    this.door, 
                    this.stateMachine, 
                    this.fileLogger,
                    this.travelCoordinator,
                    this.evaluationEngine));
            extensionPoint.AddExtension(new BlackHoleSensor(this.blackHoleSubOrbitDetectionEngine));
        }
    }
}