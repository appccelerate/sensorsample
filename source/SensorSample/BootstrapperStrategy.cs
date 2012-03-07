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

namespace SensorSample
{
    using Appccelerate.AsyncModule;
    using Appccelerate.Bootstrapper;
    using Appccelerate.Bootstrapper.Syntax;
    using Appccelerate.EvaluationEngine;
    using Appccelerate.EventBroker;

    using SensorSample.Sirius;

    public class BootstrapperStrategy : AbstractStrategy<ISensor>
    {
        private readonly IEventBroker globalEventBroker;
        private readonly AsynchronousVhptFileLogger fileLogger;
        private readonly IEvaluationEngine evaluationEngine;

        public BootstrapperStrategy()
        {
            this.globalEventBroker = new EventBroker();
            this.fileLogger = new AsynchronousVhptFileLogger(new ModuleController(), new VhptFileLogger());
            this.evaluationEngine = new EvaluationEngine();
        }

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            return new SensorResolver(this.fileLogger, this.evaluationEngine);
        }

        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder
                .Execute(() => this.InitializeEvaluationEngine())
                .Execute(() => this.fileLogger.Initialize())
                .Execute(() => this.fileLogger.Start())
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

        private void InitializeEvaluationEngine()
        {
            this.evaluationEngine.SetLogExtension(new EvaluationEngineReporter());

            this.evaluationEngine.Load(new VhptOracle());
            this.evaluationEngine.Load(new PanicModeTargetLevelOracle());
        }
    }
}