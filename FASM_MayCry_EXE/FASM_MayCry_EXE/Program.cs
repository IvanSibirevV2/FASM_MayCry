using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///////////////////////////////////////////////////////////////
using Binarysharp.Assemblers.Fasm;
using Process.NET;
using Process.NET.Native.Types;
///////////////////////////////////////////////////////////////
using System.Runtime.InteropServices;
using System.Security;
///////////////////////////////////////////////////////////////
namespace FASM_MayCry_EXE
{

    public class Program
    {
        public static int qwer(int x) 
        {
            ;
            int c = x;
            return 5;
        }
        [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        private delegate int QWE(int x);
        static void Main(string[] args)
        {
            ;
            int asd(int x) 
            {
                return x;
            }
            //qwer(4);
            Process.NET.Memory.IAllocatedMemory _IAllocatedMemory =
/*
@"use32
mov eax, 4
mov eax, [ebp+8] +4
ret
"
*/
@"use32
mov eax, 4
mov eax, [ebp+8]
ret
"

.Get_FasmNet_Assemble().Get_Memory();
            QWE _QWE= _IAllocatedMemory.Get_Delegate<QWE>();
            ;
            int q = _QWE(2);
            //int q = asd(2);
            Console.WriteLine(q);
            
            ;
            _IAllocatedMemory.Dispose();
            //Console.WriteLine($"Example1 return value: {returnValue.ToInt32()}, expected: {1}"); // Prints 1
            //ExampleS.ExampleS.E2();
            System.Console.WriteLine("Oll Is Ok !!!");
            System.Console.ReadLine();
        }
    }
}
