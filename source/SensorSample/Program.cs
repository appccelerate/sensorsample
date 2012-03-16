//-------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Appccelerate">
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
    using System;

    using Appccelerate.Bootstrapper;

    using SensorSample.Bootstrapping;
    using SensorSample.Reporters;
    using SensorSample.Sensors;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var bootstrapper = new DefaultBootstrapper<ISensor>(new BootstrapperReporter()))
            {
                bootstrapper.Initialize(new BootstrapperStrategy());

                PrintHeader();

                bootstrapper.Run();

                PrintBody();

                bootstrapper.Shutdown();
            }

            PrintFooter();
        }

        private static void PrintHeader()
        {
            Console.WriteLine("Sirius Cybernetics Sensor Management v1.0");
            Console.WriteLine("Appccelerate");
            Console.WriteLine("=========================================================");
            Console.WriteLine("=========================================================");
            Console.WriteLine("Press any key to start sensors");
            Console.WriteLine("---------------------------------------------------------");
            Console.ReadLine();
        }

        private static void PrintBody()
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Press any key to stop sensors");
            Console.ReadLine();
        }

        private static void PrintFooter()
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Press any key to exit the sensor management.");
            Console.ReadLine();
        }
    }
}
