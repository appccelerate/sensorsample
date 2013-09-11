//-------------------------------------------------------------------------------
// <copyright file="IVhptTravelCoordinator.cs" company="Appccelerate">
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

namespace SensorSample.Sirius
{
    using System;

    public interface IVhptTravelCoordinator
    {
        void TravelTo(int level);
    }

    public class VhptTravelCoordinator : IVhptTravelCoordinator
    {
        public void TravelTo(int level)
        {
            if (level == 0)
            {
                using (SwitchColor.To(ConsoleColor.Red))
                {
                    Console.WriteLine("Too dangerous! Staying at level {0} for security reasons.", level);
                }
            }
            else
            {
                using (SwitchColor.To(ConsoleColor.Green))
                {
                    Console.WriteLine("Travelling to target level {0}.", level);
                }
            }
        }
    }

    public sealed class SwitchColor : IDisposable
    {
        private readonly ConsoleColor oldColor;

        private SwitchColor(ConsoleColor color)
        {
            this.oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public static IDisposable To(ConsoleColor color)
        {
            return new SwitchColor(color);
        }

        public void Dispose()
        {
            Console.ForegroundColor = this.oldColor;
        }
    }
}