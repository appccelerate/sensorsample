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

    using Appccelerate.EvaluationEngine;
    using Appccelerate.EventBroker;
    using Appccelerate.EventBroker.Handlers;
    using Appccelerate.StateMachine;

    using SensorSample.Asynchronous;
    using SensorSample.Evaluation;
    using SensorSample.Sirius;

    public enum States
    {
        NormalMode,
        PanicMode,
        DoorOpenInNormalMode,
        DoorClosedInNormalMode,
        DoorOpenInPanicMode,
        DoorClosedInPanicMode
    }

    public enum Events
    {
        BlackHoleDetected,
        DoorOpened,
        DoorClosed
    }

    public class DoorSensor : ISensor, IInitializable
    {
        private readonly IVhptDoor door;

        private readonly IStateMachine<States, Events> stateMachine;

        private readonly IAsynchronousFileLogger fileLogger;

        // TODO: add a flag to keep track whether a black hole was detected and we are in panic mode

        // TODO: inject TravelCoordinator and EvaluationEngine
        public DoorSensor(
            IVhptDoor door, 
            IStateMachine<States, Events> stateMachine, 
            IAsynchronousFileLogger fileLogger)
        {
            this.door = door;
            this.stateMachine = stateMachine;
            this.fileLogger = fileLogger;
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
            this.stateMachine.Start();

            this.door.Opened += this.HandleDoorOpened;
            this.door.Closed += this.HandleDoorClosed;
        }

        public void StopObservation()
        {
            this.door.Opened -= this.HandleDoorOpened;
            this.door.Closed -= this.HandleDoorClosed;

            this.stateMachine.Stop();
        }

        [EventSubscription(EventTopics.BlackHoleDetected, typeof(Publisher))]
        public void HandleBlackHoleDetection(object sender, EventArgs e)
        {
            this.stateMachine.Fire(Events.BlackHoleDetected);
        }

        public void Initialize()
        {
            // TODO: add actions so that you know when a black hole was detected and to tell the travel coordinator where to go to.
            this.stateMachine.DefineHierarchyOn(States.NormalMode)
                .WithHistoryType(HistoryType.Deep)
                .WithInitialSubState(States.DoorClosedInNormalMode)
                .WithSubState(States.DoorOpenInNormalMode);

            this.stateMachine.DefineHierarchyOn(States.PanicMode)
                .WithHistoryType(HistoryType.Deep)
                .WithInitialSubState(States.DoorClosedInPanicMode)
                .WithSubState(States.DoorOpenInPanicMode);

            this.stateMachine
                .In(States.DoorClosedInNormalMode)
                .On(Events.BlackHoleDetected).Goto(States.DoorClosedInPanicMode)
                .On(Events.DoorOpened).Goto(States.DoorOpenInNormalMode).Execute(this.LogDoorOpenedInNormalMode);

            this.stateMachine
                .In(States.DoorOpenInNormalMode)
                .On(Events.BlackHoleDetected).Goto(States.DoorOpenInPanicMode)
                .On(Events.DoorClosed).Goto(States.DoorClosedInNormalMode).Execute(this.LogDoorClosedInNormalMode);

            this.stateMachine
                .In(States.DoorClosedInPanicMode)
                .On(Events.DoorOpened).Goto(States.DoorOpenInPanicMode).Execute(this.LogDoorOpenedInPanicMode);

            this.stateMachine
                .In(States.DoorOpenInPanicMode)
                .On(Events.DoorClosed).Goto(States.DoorClosedInPanicMode).Execute(this.LogDoorClosedInPanicMode);

            this.stateMachine
                .In(States.PanicMode)
                .ExecuteOnEntry(this.LogBlackHoleDetected)
                .On(Events.BlackHoleDetected);

            this.stateMachine.Initialize(States.NormalMode);
        }

        private void HandleDoorOpened(object sender, EventArgs e)
        {
            this.stateMachine.Fire(Events.DoorOpened);
        }

        private void HandleDoorClosed(object sender, EventArgs e)
        {
            this.stateMachine.Fire(Events.DoorClosed);
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
            this.fileLogger.Log(message);

            Console.WriteLine(message);
        }
    }
}