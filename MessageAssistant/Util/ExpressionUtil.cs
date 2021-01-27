using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;

namespace MessageAssistant.Util
{
    static class ExpressionUtil
    {
        public static object ComplierCode(string expression)
        {
            string code = WrapExpression(expression);

            CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();

            //编译的参数
            CompilerParameters compilerParameters = new CompilerParameters();
            //compilerParameters.ReferencedAssemblies.AddRange();
            compilerParameters.CompilerOptions = "/t:library";
            compilerParameters.GenerateInMemory = true;
            //开始编译
            CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, code);
            if (compilerResults.Errors.Count > 0)
            {
                // TODO:
                return null;
            }

            Assembly assembly = compilerResults.CompiledAssembly;
            Type type = assembly.GetType("ExpressionCalculate");
            MethodInfo method = type.GetMethod("Calculate");
            return method.Invoke(null, null);
        }

        static string WrapExpression(string expression)
        {
            string code = @"
                using System;

                class ExpressionCalculate
                {
                    public static DateTime start_dt = Convert.ToDateTime(""{start_dt}"");
                    public static DateTime end_dt = Convert.ToDateTime(""{end_dt}"");
                    public static DateTime current_dt = DateTime.Now;

                    public static object Calculate()
                    {
                        return {0};
                    }
                }
            ";

            return code.Replace("{0}", expression);
        }
    }
}
