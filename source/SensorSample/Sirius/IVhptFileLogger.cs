//-------------------------------------------------------------------------------
// <copyright file="IVhptFileLogger.cs" company="Appccelerate">
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
    using System.IO;
    using System.Threading;

    public interface IVhptFileLogger : IDisposable
    {
        void Log(string message);
    }

    public class VhptFileLogger : IVhptFileLogger
    {
        private const string DataBusFile = "SensorAppLogFile.txt";
        private readonly FileStream fileStream;
        private readonly StreamWriter streamWriter;

        private bool isDisposed;

        public VhptFileLogger()
        {
            if (File.Exists(DataBusFile))
            {
                File.Delete(DataBusFile);
            }
            
            this.fileStream = File.Create(DataBusFile);
            this.streamWriter = new StreamWriter(this.fileStream);
        }

        public void Log(string message)
        {
            Thread.Sleep(2000);

            this.streamWriter.WriteLine(message);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.streamWriter.Dispose();
                this.fileStream.Dispose();

                this.isDisposed = true;
            }
        }
    }
}