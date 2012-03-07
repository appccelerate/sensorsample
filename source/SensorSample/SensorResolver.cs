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

namespace SensorSample
{
    using Appccelerate.AsyncModule;
    using Appccelerate.Bootstrapper;
    using Appccelerate.StateMachine;

    using SensorSample.Sirius;

    public class SensorResolver : IExtensionResolver<ISensor>
    {
        private readonly IAsynchronousFileLogger fileLogger;

        public SensorResolver(IAsynchronousFileLogger fileLogger)
        {
            this.fileLogger = fileLogger;
        }

        public void Resolve(IExtensionPoint<ISensor> extensionPoint)
        {
            extensionPoint.AddExtension(new DoorSensor(new VhptDoor(), new ActiveStateMachine<States, Events>(), this.fileLogger));
            extensionPoint.AddExtension(new BlackHoleSensor(new VhptBlackHoleSubOrbitDetectionEngine()));
        }
    }
}