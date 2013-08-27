//-------------------------------------------------------------------------------
// <copyright file="DoorSensor.cs" company="Appccelerate">
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
    using Appccelerate.EventBroker.Handlers;

    using SensorSample.Sirius;

    public class DoorSensor : ISensor
    {
        private readonly IVhptDoor door;

        public DoorSensor(
            IVhptDoor door)
        {
            this.door = door;
        }

        public string Name
        {
            get
            {
                return "Door sensor";
            }
        }

        public string Describe()
        {
            return "The door sensor detects opening and closing of doors";
        }

        public void StartObservation()
        {
            this.door.Opened += this.HandleDoorOpened;
            this.door.Closed += this.HandleDoorClosed;
        }

        public void StopObservation()
        {
            this.door.Opened -= this.HandleDoorOpened;
            this.door.Closed -= this.HandleDoorClosed;
        }

        [EventSubscription(EventTopics.BlackHoleDetected, typeof(OnPublisher))]
        public void HandleBlackHoleDetection(object sender, EventArgs e)
        {
            this.Log("black hole detected!");
        }

        private void HandleDoorOpened(object sender, EventArgs e)
        {
            this.Log("door open");
        }

        private void HandleDoorClosed(object sender, EventArgs e)
        {
            this.Log("door closed");
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}