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
    using Appccelerate.Bootstrapper.Configuration;
    using Appccelerate.Bootstrapper.Syntax;
    using Appccelerate.EvaluationEngine;
    using Appccelerate.EventBroker;
    using Appccelerate.StateMachine;

    using SensorSample.Asynchronous;
    using SensorSample.Evaluation;
    using SensorSample.Reporters;
    using SensorSample.Sensors;
    using SensorSample.Sirius;

    public class BootstrapperStrategy : AbstractStrategy<ISensor>
    {
        private IEventBroker globalEventBroker;

        private IEvaluationEngine evaluationEngine;

        private AsynchronousVhptFileLogger fileLogger;

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            this.globalEventBroker = this.CreateGlobalEventBroker();
            this.evaluationEngine = new EvaluationEngine();
            this.fileLogger = new AsynchronousVhptFileLogger(this.CreateFileLogger());

            return new SensorResolver(
                this.fileLogger,
                this.evaluationEngine,
                this.CreateDoor(),
                this.CreateBlackHoleSubOrbitDetectionEngine(),
                this.CreateTravelCoordinator(),
                this.CreateStateMachine());
        }

        protected virtual IStateMachine<States, Events> CreateStateMachine()
        {
            return new ActiveStateMachine<States, Events>();
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

        protected virtual IVhptTravelCoordinator CreateTravelCoordinator()
        {
            return new VhptTravelCoordinator();
        }

        protected virtual IVhptFileLogger CreateFileLogger()
        {
            return new VhptFileLogger();
        }

        protected virtual ExtensionConfigurationSectionBehavior CreateExtensionConfigurationSectionBehavior()
        {
            return new ExtensionConfigurationSectionBehavior();
        }

        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder
                .Begin
                    .With(this.CreateExtensionConfigurationSectionBehavior())
                .Execute(() => this.InitializeEventBroker())
                .Execute(() => this.InitializeEvaluationEngine())
                .Execute(sensor => sensor.StartObservation())
                    .With(new InitializeSensorBehavior())
                    .With(new RegisterOnEventBrokerBehavior(this.globalEventBroker));
        }

        protected override void DefineShutdownSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder
                .Execute(sensor => sensor.StopObservation())
            .End
                .With(new UnregisterOnEventBrokerBehavior(this.globalEventBroker));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.fileLogger.Dispose();
        }

        private void InitializeEventBroker()
        {
            this.globalEventBroker.AddExtension(new EventBrokerReporter());
        }

        private void InitializeEvaluationEngine()
        {
            this.evaluationEngine.SetLogExtension(new EvaluationEngineReporter());

            this.evaluationEngine.Load(new VhptOracle());
            this.evaluationEngine.Load(new PanicModeTargetLevelOracle());
        }
    }
}