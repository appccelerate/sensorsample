//-------------------------------------------------------------------------------
// <copyright file="BlackHoleSpecifications.cs" company="Appccelerate">
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

namespace SensorSample.Specification
{
    using FakeItEasy;

    using Machine.Specifications;

    [Subject(Subjects.BlackHole)]
    public class When_a_black_hole_is_detected : RunningApplicationSpecification
    {
        Because of = () =>
            bootstrapperStrategy.BlackHoleSubOrbitDetectionEngine.BlackHoleDetected += Raise.WithEmpty().Now;

        It should_write_message_to_file_logger = () =>
            A.CallTo(() => bootstrapperStrategy.FileLogger.Log("black hole detected! PANIC!!!")).MustHaveHappened();
    }

    [Subject(Subjects.BlackHole)]
    public class When_a_door_opens_and_a_black_hole_was_detected : BlackHoleDetectedSpecification
    {
        Because of = () =>
            bootstrapperStrategy.Door.Opened += Raise.WithEmpty().Now;

        It should_write_panic_message_to_file_logger = () =>
            A.CallTo(() => bootstrapperStrategy.FileLogger.Log("door is open! PANIC!!!")).MustHaveHappened();
    }

    [Subject(Subjects.BlackHole)]
    public class When_a_door_closes_and_a_black_hole_was_detected : BlackHoleDetectedSpecification
    {
        Establish context = () =>
            bootstrapperStrategy.Door.Opened += Raise.WithEmpty().Now;

        Because of = () =>
            bootstrapperStrategy.Door.Closed += Raise.WithEmpty().Now;

        It should_write_panic_message_to_file_logger = () =>
            A.CallTo(() => bootstrapperStrategy.FileLogger.Log("door is closed! PANIC!!!")).MustHaveHappened();
    }

    public class When_a_door_closes_and_a_black_hole_was_detected_and_panic_mode_is_enabled :
        BlackHoleDetectedSpecification
    {
        private Establish context = () =>
        {
            bootstrapperStrategy.Door.Opened += Raise.WithEmpty().Now;
        };

        private Because of = () =>
            bootstrapperStrategy.Door.Closed += Raise.WithEmpty().Now;

        private It should_tell_travel_coordinator_to_move_to_ground_floor =
            () => A.CallTo(() => bootstrapperStrategy.TravelCoordinator.TravelTo(0)).MustHaveHappened();
    }

    [Subject(Subjects.BlackHole)]
    public class When_a_door_closes_and_a_black_hole_was_detected_and_panic_mode_is_disabled :
        BlackHoleDetectedSpecification
    {
        private Establish context = () =>
        {
            bootstrapperStrategy.Door.Opened += Raise.WithEmpty().Now;
        };

        private Because of = () =>
            bootstrapperStrategy.Door.Closed += Raise.WithEmpty().Now;

        private It should_tell_travel_coordinator_to_move_to_ground_floor =
            () => A.CallTo(() => bootstrapperStrategy.TravelCoordinator.TravelTo(42)).MustHaveHappened();
    }
}