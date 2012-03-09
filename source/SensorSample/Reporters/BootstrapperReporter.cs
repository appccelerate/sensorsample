//-------------------------------------------------------------------------------
// <copyright file="BootstrapperReporter.cs" company="Appccelerate">
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

namespace SensorSample.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using Appccelerate.Bootstrapper;
    using Appccelerate.Bootstrapper.Reporting;

    public class BootstrapperReporter : IReporter
    {
        public void Report(IReportingContext context)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("== Bootstrapper report ==");
            Console.WriteLine();
            Console.WriteLine(Dump(context));
            Console.WriteLine();
        }

        private static string Dump(IReportingContext context)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Extensions:");
            context.Extensions.ToList().ForEach(e => Dump(e.Name, e.Description, builder, 0));
            
            builder.AppendLine();
            builder.AppendLine("Run syntax:");
            Dump(context.Run, builder);

            builder.AppendLine();
            builder.AppendLine("Shutdown syntax:");
            Dump(context.Shutdown, builder);

            return builder.ToString();
        }

        private static void Dump(IExecutionContext executionContext, StringBuilder sb)
        {
            Dump(executionContext.Name, executionContext.Description, sb, 3);

            Dump(executionContext.Executables, sb);
        }

        private static void Dump(IEnumerable<IExecutableContext> executableContexts, StringBuilder sb)
        {
            foreach (IExecutableContext executableContext in executableContexts)
            {
                Dump(executableContext.Name, executableContext.Description, sb, 6);

                executableContext.Behaviors.ToList().ForEach(b => Dump(b.Name, b.Description, sb, 9));
            }
        }

        private static void Dump(string name, string description, StringBuilder sb, int indent)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}[{3}{0}    Name = {1}{3}{0}    Description = {2}{3}{0}]", string.Empty.PadLeft(indent), name, description, Environment.NewLine));
        }
    }
}