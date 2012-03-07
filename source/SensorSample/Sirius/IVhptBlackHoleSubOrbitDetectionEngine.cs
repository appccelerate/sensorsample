//-------------------------------------------------------------------------------
// <copyright file="IVhptBlackHoleSubOrbitDetectionEngine.cs" company="Appccelerate">
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
    using System.Reactive.Linq;

    public interface IVhptBlackHoleSubOrbitDetectionEngine
    {
        event EventHandler BlackHoleDetected;
    }

    public class VhptBlackHoleSubOrbitDetectionEngine : IVhptBlackHoleSubOrbitDetectionEngine, IDisposable
    {
        private readonly IDisposable engine;
        private bool isDisposed;

        public VhptBlackHoleSubOrbitDetectionEngine()
        {
            this.engine =
                Observable.Start(() => Observable.Interval(TimeSpan.FromSeconds(10)).First()).Subscribe(
                    interval => this.OnBlackHoleDetected(EventArgs.Empty));
        }

        public event EventHandler BlackHoleDetected;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnBlackHoleDetected(EventArgs e)
        {
            EventHandler handler = this.BlackHoleDetected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.engine.Dispose();

                this.isDisposed = true;
            }
        }
    }
}