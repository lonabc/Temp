using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using WpfAppLogin.Tools.InterfaceTools;

namespace WpfAppLogin.Tools
{
   public  class MathChangeTools : IMathChangeTools<int>
    {
      

        public int mathConversion(int start)
        {
            return 0;

        }
    }

    public class MathChangeFloat : IMathChangeTools<float>
    {
   

        public float mathConversion(float start)
        {
            float Rs = (3.3f - start) * 10.0f / start;
            float ratdio = Rs / 20.0f; //20.0f表示清洁空气中的示例数据
            
            float pollution = 100.0f - (ratdio*50.0f);

            pollution = Math.Clamp(pollution, 0.0f, 100.0f)==0 ? 6:pollution; // 直接限制在 [0, 100] 范围内;
                                                             // MessageBox.Show("ratdio" + ratdio.ToString() + " " + pollution);
        
            return pollution;
        }
    }



}
