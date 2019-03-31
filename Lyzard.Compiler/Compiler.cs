using System;
using System.CodeDom.Compiler; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

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

        public Results Compile(string name, string code, bool loadAssembly = true)
        {
            var output = new Results();
            _parameters.OutputAssembly = $"{name}.dll";


            CompilerResults results = _provider.CompileAssemblyFromSource(_parameters, new string[] { code });
            if (!results.Errors.HasErrors)
            {
                if (loadAssembly) output.Assembly = results.CompiledAssembly;
                output.PathToAssembly = results.PathToAssembly;
            }

            foreach (CompilerError err in results.Errors)
            {
                output.Errors.Add(new Error
                {
                    ErrorNumber = err.ErrorNumber,
                    Column = err.Column,
                    ErrorText = err.ErrorText,
                    FileName = err.FileName,
                    IsWarning = err.IsWarning,
                    Line = err.Line
                });
            }

            return output;
        }



    }
}
