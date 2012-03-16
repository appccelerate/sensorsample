//-------------------------------------------------------------------------------
// <copyright file="AsynchronousVhptFileLogger.cs" company="Appccelerate">
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

namespace SensorSample.Asynchronous
{
    using Appccelerate.AsyncModule;

    using SensorSample.Sirius;

    public sealed class AsynchronousVhptFileLogger : IAsynchronousFileLogger
    {
        private readonly IModuleController moduleController;

        private readonly IVhptFileLogger decoratedVhptFileLogger;

        public AsynchronousVhptFileLogger(IModuleController moduleController, IVhptFileLogger decoratedVhptFileLogger)
        {
            this.moduleController = moduleController;
            this.decoratedVhptFileLogger = decoratedVhptFileLogger;
        }

        public void Initialize()
        {
            this.moduleController.Initialize(this);
        }

        public void Start()
        {
            this.moduleController.Start();
        }

        public void Log(string message)
        {
            this.moduleController.EnqueueMessage(message);
        }

        [MessageConsumer]
        public void ConsumeMessage(string message)
        {
            this.decoratedVhptFileLogger.Log(message);
        }

        public void Dispose()
        {
            this.moduleController.Stop();
            this.decoratedVhptFileLogger.Dispose();
        }
    }
}