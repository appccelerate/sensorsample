//-------------------------------------------------------------------------------
// <copyright file="AsyncModuleReporter.cs" company="Appccelerate">
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
    using System.Reflection;

    using Appccelerate.AsyncModule;
    using Appccelerate.AsyncModule.Extensions;

    public class AsyncModuleReporter : IAsyncModuleLogExtension
    {
        public void AddedExtension(IModuleExtension extension, object controlledModule)
        {
            Console.WriteLine("Added extension {0} to module {1}.", extension, controlledModule);
        }

        public void ControllerAlreadyRunning(object controlledModule)
        {
            Console.WriteLine("Asynchronous controller of module {0} is already started.", controlledModule);
        }

        public void Starting(object controlledModule)
        {
            Console.WriteLine("Starting asynchronous controller of module {0}.", controlledModule);
        }

        public void Started(object controlledModule, int numberOfThreads)
        {
            Console.WriteLine("Started asynchronous controller of module {0} with {1} worker thread(s).", controlledModule, numberOfThreads);
        }

        public void StoppingAsync(object controlledModule)
        {
            Console.WriteLine("Stopping asynchronous controller of module {0}.", controlledModule);
        }

        public void AlreadyStopped(object controlledModule)
        {
            Console.WriteLine("Asynchronous controller of module {0} is already stopped.", controlledModule);
        }

        public void Stopping(object controlledModule, TimeSpan timeout)
        {
            Console.WriteLine("Stopping asynchronous controller of module {0} immediately with timeout {1}.", controlledModule, timeout);
        }

        public void AbortingThread(object controlledModule, string threadName, TimeSpan timeout)
        {
            Console.WriteLine("Aborting thread {0} because it did not terminate within the given time-out ({1} milliseconds)", threadName, timeout);
        }

        public void EnqueuedMessage(object controlledModule, object message)
        {
            Console.WriteLine("Enqueued message {0}", message);
        }

        public void UnhandledException(object controlledModule, object message, Exception exception)
        {
            Console.WriteLine("Unhandled exception in module {0}: {1} {2}", controlledModule, message, exception);
        }

        public void Stopped(object controlledModule)
        {
            Console.WriteLine("Stopped asynchronous controller of module {0}.", controlledModule);
        }

        public void NumberOfMessagesInQueue(int count, object controlledModule)
        {
            Console.WriteLine("{0} messages in queue of module {1}.", count, controlledModule);
        }

        public void WorkerThreadExit(string threadName)
        {
            Console.WriteLine("Worker thread '{0}' exited.", threadName);
        }

        public void SkippingNullMessage(object controlledModule)
        {
            Console.WriteLine("Skipping null message of module {0}.", controlledModule);
        }

        public void ConsumingMessage(object message, object controlledModule)
        {
            Console.WriteLine("Consuming message {0} of module {1}.", message, controlledModule);
        }

        public void RelayingMessage(object message, object controlledModule, string methodName)
        {
            Console.WriteLine("Relaying message {0} of module {1} to method {2}.", message, controlledModule, methodName);
        }

        public void ConsumedMessage(object message, object controlledModule)
        {
            Console.WriteLine("Consumed message {0} of module {1}.", message, controlledModule);
        }

        public void SkippedMessage(object message, object controlledModule)
        {
            Console.WriteLine("Skipped message {0} of module {1}", message, controlledModule);
        }

        public void SwallowedException(TargetInvocationException targetInvocationException, object message, object controlledModule)
        {
            Console.WriteLine("Swallowing exception {0} that occurred consuming message {1} of module {2}.", targetInvocationException, message, controlledModule);
        }

        public void NoHandlerFound(object message, object controlledModule)
        {
            Console.WriteLine("No handler method found for message {0} on module {1}.", message, controlledModule);
        }
    }
}