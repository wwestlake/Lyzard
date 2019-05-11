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
using Lyzard.Compiler;
using Lyzard.Config;
using Lyzard.FileSystem;
using Lyzard.IDE.Dialogs;
using Lyzard.MessageBus;
using NAudio.Wave;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lyzard.FsMath;

namespace Lyzard.TestConsole
{



    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //var gen1 = new Generators.SquareWaveGenerator(0.0f, 1.0f, 1000.0f, 44100.0f, 0.0f);
            //var gen2 = new Generators.SineWaveGenerator(0.0f, 1.0f, 2000.0f, 44100.0f, 0.0f);
            //var dsp = new SignalProcessing.Mixxers.DSP();
            //var gen3 = dsp.Clamp(0.0f, 1.0f, (new Generators.SquareWaveGenerator(0.0f, 1.0f, 1.0f, 44100.0f, 0.0f)).GenerateFloat());
            //var gen = dsp.Mix(dsp.Mix(gen1.GenerateFloat(), gen2.GenerateFloat()), gen3);
            //var samples = gen.Select(x => (float)x).Take(200000).ToArray();
            //var sampleBytes = new byte[sizeof(float) * samples.Length];
            //Buffer.BlockCopy(samples, 0, sampleBytes, 0, sampleBytes.Length);
            //var provider = new RawSourceWaveStream(sampleBytes, 0, sampleBytes.Length, new WaveFormat(44100, 32,1));
            //var waveOut = new WaveOut();
            //waveOut.DeviceNumber = -1;
            //waveOut.Init(provider);
            //waveOut.Play();
            //
            //let _waveOut = new WaveOut()
            //
            //_waveOut.Init(provider)
            //_waveOut.Play()

            Console.WriteLine(sizeof(float) * 8);

            //Thread.Sleep(2000);
            pause("press a key");
        }

        public static void TestDialogManager(string[] args)
        {
            var folder = DialogManager.SelectManagedFolder();

            if (folder != null)
                Console.WriteLine(folder.Path);

            pause("Press a key");
        }

        public static void TestState(string[] args)
        {
            var state = StateManager.SystemState;
            state.RecentFiles.Add("Test File");

            Console.WriteLine(state.RecentFiles[0]);


        }


        class MyMessage
        {
            public string Message { get; set; }
        }

        static void CompilerTest(string[] args)
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
