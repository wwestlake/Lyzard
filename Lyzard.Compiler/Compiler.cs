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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.Linq;
using System.IO;

namespace Lyzard.Compiler
{
    public class CSharpCompiler : Compiler
    {
        public CSharpCompiler() : base(new CSharpCodeProvider()) { }
    }

    public class VBCompiler : Compiler
    {
        public VBCompiler() : base(new VBCodeProvider()) { }
    }

    public abstract class Compiler
    {
        private readonly CodeDomProvider _provider;
        private readonly CompilerParameters _parameters;

        public Compiler(CodeDomProvider provider)
        {
            _provider = provider;
            _parameters = new CompilerParameters();
            _parameters.GenerateInMemory = false;
            _parameters.GenerateExecutable = false;
            AddReference("System.Core.dll");
        }

        public void AddReference(string path)
        {
            _parameters.ReferencedAssemblies.Add(path);
        }

        public CompilerResults Compile(string name, string outputPath, string code)
        {
            _parameters.OutputAssembly = $"{name}.dll";
            _parameters.CompilerOptions = $" /out:{outputPath}/{name}.dll";
            return _provider.CompileAssemblyFromSource(_parameters, new string[] { code });
        }

        public CompilerResults Compile(string name, string outputPath, IEnumerable<string> files)
        {
            _parameters.OutputAssembly = $"{name}.dll";
            _parameters.CompilerOptions = $" /out:{outputPath}/{name}.dll";
            return _provider.CompileAssemblyFromFile(_parameters, files.ToArray());
        }




    }
}
