//-------------------------------------------------------------------------------
// <copyright file="UnregisterOnEventBrokerBehavior.cs" company="Appccelerate">
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
    using System.Collections.Generic;

    using Appccelerate.Bootstrapper;
    using Appccelerate.EventBroker;

    public class UnregisterOnEventBrokerBehavior : IBehavior<ISensor>
    {
        private readonly IEventBroker globalEventBroker;

        public UnregisterOnEventBrokerBehavior(IEventBroker globalEventBroker)
        {
            this.globalEventBroker = globalEventBroker;
        }

        public string Name
        {
            get { return "Unregister on global event broker behavior"; }
        }

        public string Describe()
        {
            return "unregisters sensors on the global event broker";
        }

        public void Behave(IEnumerable<ISensor> extensions)
        {
            foreach (var extension in extensions)
            {
                this.globalEventBroker.Unregister(extension);
            }
        }
    }
}