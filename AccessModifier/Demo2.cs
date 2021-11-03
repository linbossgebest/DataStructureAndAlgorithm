using AccessModifierDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifier
{
    class Demo2:Access
    {
        private void Test()
        {
            publicMethod();
            //privateMethod();
            protectedMethod();
            //internalMethod();
            protectedinternalMethod();
            //privateprotectedMethod();
            //protectedprivateMethod();
        }

        private void Test1()
        {
            Access access = new Access();
            access.publicMethod();
        }
    }
}
