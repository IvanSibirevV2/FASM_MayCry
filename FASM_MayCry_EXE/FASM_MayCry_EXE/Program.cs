using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASM_MayCry_EXE
{

    class Program
    {
        [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        private delegate int AssemblyAddFunction(int x, int y);
        static void Main(string[] args)
        {
            ExampleS.ExampleS.E2();
            System.Console.WriteLine("Oll Is Ok !!!");
            System.Console.ReadLine();
        }
    }
}
