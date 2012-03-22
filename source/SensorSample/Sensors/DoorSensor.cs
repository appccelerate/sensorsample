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
    using Appccelerate.StateMachine;

    using SensorSample.Sirius;

    public enum States
    {
        // TODO: add the states for the state machine here
    }

    public enum Events
    {
        // TODO: add the events for the state machine here
    }


    // TODO: make DoorSensor IInitializable and use Initialize method to define and initialize the state machine
    public class DoorSensor : ISensor
    {
        private readonly IVhptDoor door;

        private readonly IStateMachine<States, Events> stateMachine;

        public DoorSensor(
            IVhptDoor door, 
            IStateMachine<States, Events> stateMachine)
        {
            this.door = door;
            this.stateMachine = stateMachine;
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
            // TODO: start state machine

            this.door.Opened += this.HandleDoorOpened;
            this.door.Closed += this.HandleDoorClosed;
        }

        public void StopObservation()
        {
            this.door.Opened -= this.HandleDoorOpened;
            this.door.Closed -= this.HandleDoorClosed;

            // TODO: stop state machine
        }

        [EventSubscription(EventTopics.BlackHoleDetected, typeof(Publisher))]
        public void HandleBlackHoleDetection(object sender, EventArgs e)
        {
            // TODO: fire event onto state machine to tell it that a black hole was detected
        }

        private void HandleDoorOpened(object sender, EventArgs e)
        {
            // TODO: fire an event onto the state machine to tell it that the door is open
        }

        private void HandleDoorClosed(object sender, EventArgs e)
        {
            // TODO: fire an event onto the state machine to tell it that the door is closed
        }

        private void LogBlackHoleDetected()
        {
            this.Log("black hole detected! PANIC!!!");
        }

        private void LogDoorOpenedInNormalMode()
        {
            this.Log("door is open!");
        }

        private void LogDoorClosedInNormalMode()
        {
            this.Log("door is closed!");
        }

        private void LogDoorOpenedInPanicMode()
        {
            this.Log("door is open! PANIC!!!");
        }

        private void LogDoorClosedInPanicMode()
        {
            this.Log("door is closed! PANIC!!!");
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}