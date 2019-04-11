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
using Lyzard.AppDominaControl;
using Lyzard.Collections;
using Lyzard.Compiler;
using Lyzard.FileSystem;
using Lyzard.MessageBus;
using Lyzard.ProjectManager;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
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
            var compiler = new CSharpCompiler();
            var dllPath = Path.GetFullPath("TestClasses") + CommonFolders.Sep + "TestApp.dll";
            var results = compiler.Compile("TestApp", "TestClasses", new List<string> {
                Path.GetFullPath( "TestClasses/TestClass1.cs" ),
                Path.GetFullPath("TestClasses/TestClass2.cs")
            });

            foreach (CompilerError err in results.Errors)
            {
                Console.WriteLine($"{(err.IsWarning ? "WARNING :" : "ERROR  :")} {err.ErrorText}");
            }
            
            if (! results.Errors.HasErrors)
            {
                var loader = new AppDomainLoader("TestApp", dllPath);

                if (loader.IsLoaded)
                {
                    var testClass1 = loader.RunRemoteFunc<object>(() => {
                        Console.WriteLine(AppDomain.CurrentDomain.SetupInformation.ApplicationName);

                        return null;
                    });
                }

            }

            pause("Press a key");
        }

        static void MessageBrokerTests(string[] args)
        {
            var source = new object();
            MessageBroker.Instance.Subscribe<MyMessage>((s) => {
                Console.WriteLine($"{s.TimeStamp} {s.Details.Message}");
            });

            MessageBroker.Instance.Publish<MyMessage>(source, new MyMessage { Message = "Object Sent Message" });
        }

        static void pause(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }

    }
}
