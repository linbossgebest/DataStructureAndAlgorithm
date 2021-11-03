using AccessModifierDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifier
{
    class Demo1
    {
        private void Test()
        {
            Access access = new Access();
            access.publicMethod();
           
        }
    }
}
