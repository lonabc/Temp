using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppLogin.Tools.InterfaceTools;

namespace WpfAppLogin.Tools.ToolsContext
{
   public class MathToolsFactory
    {

        public MathToolsFactory()
        { }


        public static IMathChangeTools<T> CreateMathTools<T>(string mathType)
        {
            switch (mathType.ToLower())
            {
                case "int":
                    return (IMathChangeTools<T>)new MathChangeTools();
                case "float":
                    return (IMathChangeTools<T>)new MathChangeFloat();
              
                default:
                    throw new ArgumentException("Invalid math type");
            }
        }
    }
}
