//-------------------------------------------------------------------------------
// <copyright file="BootstrapperStrategy.cs" company="Appccelerate">
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
    using Appccelerate.Bootstrapper.Syntax;
    using Appccelerate.EventBroker;

    using SensorSample.Reporters;
    using SensorSample.Sensors;
    using SensorSample.Sirius;

    public class BootstrapperStrategy : AbstractStrategy<ISensor>
    {
        private IEventBroker globalEventBroker;

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            this.globalEventBroker = this.CreateGlobalEventBroker();
           
            return new SensorResolver(
                this.CreateDoor(),
                this.CreateBlackHoleSubOrbitDetectionEngine());
        }

        protected virtual EventBroker CreateGlobalEventBroker()
        {
            return new EventBroker();
        }

        protected virtual IVhptDoor CreateDoor()
        {
            return new VhptDoor();
        }

        protected virtual IVhptBlackHoleSubOrbitDetectionEngine CreateBlackHoleSubOrbitDetectionEngine()
        {
            return new VhptBlackHoleSubOrbitDetectionEngine();
        }

        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            // TODO: add behavior to register sensors on event broker
            builder
                .Execute(sensor => sensor.StartObservation());
        }

        protected override void DefineShutdownSyntax(ISyntaxBuilder<ISensor> builder)
        {
            // TODO: add behavior at end of shutdown that unregisters all sensors from the event broker.
            builder
                .Execute(sensor => sensor.StopObservation());
        }
    }
}