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
using Lyzard.Compiler;
using Lyzard.Core;
using Lyzard.Executive;
using Lyzard.FileSystem;
using Lyzard.Interfaces;
using Lyzard.MessageBus;
using Lyzard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Lyzard.TestConsole
{
    class Program
    {
        class dummy : IPlugin
        {

            public string Name => throw new NotImplementedException();

            public void Initialize()
            {
                throw new NotImplementedException();
            }

            public void StartPlugin()
            {
                throw new NotImplementedException();
            }
        }

        static Func<Error, string> converter = (err) => err.IsWarning ? "Warning" : "Error";

        static void Main(string[] args)
        {
            var oper = new MulInteger();
            oper.ConnectToInput("Left", new OutputConnector<int>(() => 3));
            oper.ConnectToInput("Right", new OutputConnector<int>(() => 4));

            var output = new TextOutput();
            output.ConnectToInput("Text", oper.GetOutput<int>("Result"));

            output.Execute();




            pause("Press a key");
        }

        static void FileSystemTest(string[] args)
        {
            FileSystemAccess.GetFileSystemItems("C:\\").ToList().ForEach(item => {
                Console.WriteLine($"{item.ModfiedOn}: {item.ItemType}: {item.Extension}: {item.Name}: {item.FullPath}");
            });


            pause("Hit the any key");
        }

        static void ProjectTest(string[] args)
        {
            //var project = Project.Create(Paths.Projects, "My Project");
            var path = $"{Paths.Projects}\\My Project\\My Project.lyzard";
            var project = Project.Load(path);
            FileSystemEventHandler handler = (s, e) => { Console.WriteLine($"{e.ChangeType}:{e.FullPath}"); };
            project.Created += handler;
            project.Deleted += handler;
            project.Renamed += handler;
            project.Changed += handler;

            project.References.Add("System.Core.dll");
            project.References.Add("Lyzard.Interfaces.dll");
            project.References.Add("Lyzard.MessageBus.dll");


            project.Save();

            Console.WriteLine($"{project.Guid}: {project.AssemblyName}");
            Console.WriteLine("Press 'q' to quit the sample.");
            while (Console.Read() != 'q') ;
        }


        static void AppDomainTest(string[] args)
        {
            Results results = CompileCode(false);
            if (!results.HasErrors)
            {
                var runner = new PluginWrapper("CompilerTest.dll", results.PathToAssembly, "Lyzard.TestConsole.CompilerTest");
                Console.WriteLine($"Plugin Name: {runner.Plugin.Name}");
                runner.Plugin.Initialize();
                runner.Plugin.StartPlugin();
                Helpers.PrintLoadedAssemblies();
            } else
            {
                PrintErrors(results);
            }
            pause("Press any key to continue");
        }

        private static void PrintErrors(Results results)
        {
            results.Errors.ToList().ForEach(
                err =>
                {
                    Console.WriteLine($"{converter(err)}: Line {err.Line}, Column {err.Column}, {err.ErrorText}");
                });
        }

        static void TestConnectors()
        {
            Results result = CompileCode(true);
            

            if (result.Errors.Count > 0)
            {
                PrintErrors(result);
            }
            else
            {
                try
                {
                    var obj = new ClassInstanceWrapper(result.Assembly, "Lyzard.TestConsole.CompilerTest");
                    var hello = new MethodWrapper(obj.Instance, "Hello");
                    hello.Invoke();
                    var test = new MethodWrapper(obj.Instance, "Test");
                    test.Invoke("This is a test");
                    var msg = new MethodWrapper(obj.Instance, "Message");
                    test.Invoke(msg.Invoke("This is a message"));

                    var prop = new PropertyWrapper(obj.Instance, "Property");
                    prop.Set("This is a propety test");
                    test.Invoke(prop.Get());

                    var field = new FieldWrapper(obj.Instance, "MyField");
                    field.Value = "This is from a field";
                    test.Invoke(field.Value);

                }
                catch (TypeNotFoundException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            pause("Press any key to continue");
        }

        private static Results CompileCode(bool loadAssembly)
        {
            var compiler = new CSharpCompiler();
            var file = new StreamReader(File.Open("CompilerTest.cs", FileMode.Open));
            var code = file.ReadToEnd();
            compiler.AddReference("Lyzard.Interfaces.dll");
            compiler.AddReference("Lyzard.Utilities.dll");
            var result = compiler.Compile("CompilerTest", code, loadAssembly);
            return result;
        }

        static void pause(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }

    }
}
