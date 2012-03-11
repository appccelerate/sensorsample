//-------------------------------------------------------------------------------
// <copyright file="BlackHoleSensor.cs" company="Appccelerate">
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

namespace SensorSample.Sensors
{
    using System;

    using Appccelerate.EventBroker;

    using SensorSample.Sirius;

    public class BlackHoleSensor : ISensor
    {
        private readonly IVhptBlackHoleSubOrbitDetectionEngine detectionEngine;

        public BlackHoleSensor(IVhptBlackHoleSubOrbitDetectionEngine detectionEngine)
        {
            this.detectionEngine = detectionEngine;
        }

        [EventPublication(EventTopics.BlackHoleDetected)]
        public event EventHandler BlackHoleDetected = delegate { };

        public string Name
        {
            get { return "Black Hole Sensor"; }
        }

        public string Describe()
        {
            return "Detects black holes";
        }

        public void StartObservation()
        {
            this.detectionEngine.BlackHoleDetected += this.HandleBlackHoleDetected;
        }

        public void StopObservation()
        {
            this.detectionEngine.BlackHoleDetected -= this.HandleBlackHoleDetected;
        }

        private void HandleBlackHoleDetected(object sender, EventArgs e)
        {
            this.BlackHoleDetected(sender, e);
        }
    }
}