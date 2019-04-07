/* 
 * Lyzard Code Generation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Lyzard.FileSystem;
using Lyzard.MessageBus;
using Lyzard.ProjectManager;
using System;
using System.Linq;

namespace Lyzard.TestConsole
{
    class Program
    {

        class MyMessage
        {
            public string Message { get; set; }
        }

        static void Main(string[] args)
        {
            var source = new object();
            MessageBroker.Instance.Subscribe<MyMessage>((s) => {
                Console.WriteLine($"{s.TimeStamp} {s.Details.Message}");
            });

            MessageBroker.Instance.Publish<MyMessage>(source, new MyMessage { Message = "Object Sent Message" });



            pause("Press a key");
        }

        static void pause(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }

    }
}
