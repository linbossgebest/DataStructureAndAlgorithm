using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifierDemo
{
    class Demo2:Access
    {
        private void Test()
        {
           
            publicMethod();
            //privateMethod();
            protectedMethod();
            internalMethod();
            protectedinternalMethod();
            privateprotectedMethod();
            protectedprivateMethod();
        }
    }
}
