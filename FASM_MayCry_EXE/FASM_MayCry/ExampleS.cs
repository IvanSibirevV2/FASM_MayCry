namespace ExampleS
{
    using System.Linq;
    using System.Text;
    using System;
    ///////////////////////////////////////////////////////////////
    using Binarysharp.Assemblers.Fasm;
    using Process.NET;
    using Process.NET.Native.Types;
    ///////////////////////////////////////////////////////////////
    using System.Runtime.InteropServices;
    using System.Security;
    ///////////////////////////////////////////////////////////////

    public static class ExampleS
    {

        

        [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        public delegate int AssemblyConstantValueFunction();
        /// <summary>
        /// Example 1: Function Returning Constant Value
        /// ExampleS.ExampleS._E1();
        /// </summary><returns>void</returns>
        public static void E1() 
        {
            var _Memory =
@"use32
mov eax, 1
ret
".Get_FasmNet_Assemble()
                .Get_Memory()
            ;
            int returnValue = _Memory.Get_Delegate<AssemblyConstantValueFunction>()();
            _Memory.Dispose();
            Console.WriteLine($"Example1 return value: {returnValue}, expected: {1}"); // Prints 1
        }
        [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        private delegate IntPtr AssemblyReadRegistersFunction();
        /// <summary>
        /// Example 2: Function Reading Registers
        /// ExampleS.ExampleS._E2();
        /// </summary><returns>void</returns>
        public static void E2()
        {
            IntPtr returnValue=new IntPtr();
@"use32
mov eax, [ebp+4]
ret
".Get_FasmNet_Assemble().Get_Memory()
                .Set_ExecuteDelegate<AssemblyReadRegistersFunction>(_Delegate => {returnValue = _Delegate(); })
                .Dispose();
            ;
            Console.WriteLine($"Example1 return value: {returnValue.ToInt32()}, expected: {1}"); // Prints 1
        }
        [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        [System.Runtime.InteropServices
            .UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        private delegate int AssemblyAddFunction(int x, int y);
        /// <summary>
        ///Example 3: Add Function With Parameters
        /// ExampleS.ExampleS._E3();
        /// </summary><returns>void</returns>
        public static void E3()
        {
            var _Memory =
@"use32
push ebp
mov eax, [ebp+8]
mov edx, [ebp+12]
add eax, edx
pop ebp
ret
".Get_FasmNet_Assemble()
                    .Get_Memory()
                ;
            int returnValue = _Memory.Get_Delegate<AssemblyAddFunction>()(3,2);
            _Memory.Dispose();
            Console.WriteLine($"Example1 return value: {returnValue}, expected: {1}"); // Prints 1
        }


        /// <summary>
        //// Example 4: Add Function With Parameters (Without Fasm.NET) 
        /// ExampleS.ExampleS._E3();
        /// </summary><returns>void</returns>
        public static void E4()
        {
            //For this example I have used https://defuse.ca/online-x86-assembler.htm
            var _Memory =
            (new System.Byte[] {
                0x55,               // 0 push ebp            ; init stack frame
                0x8B, 0x45, 0x08,   // 1 mov  eax, [ebp+8]   ; set eax to second param (remember, in cdecl calling convention, params are pushed right-to-left)
                0x8B, 0x55, 0x0C,   // 4 mov  edx, [ebp+12]  ; set edx to first param
                0x01, 0xD0,         // 7 add  eax, edx       ; add edx (first param) to eax (second param) 
                0x5D,               // 9 pop  ebp            ; leave stack frame
                0xC3                // A ret                 ; in cdecl calling convention, return value is stored in eax; so this will return both params added up
            }).Get_Memory()
                ;
            int returnValue = _Memory.Get_Delegate<AssemblyAddFunction>()(3, 2);
            _Memory.Dispose();
            Console.WriteLine($"Example1 return value: {returnValue}, expected: {1}"); // Prints 1
        }


        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
        /// <summary>
        ///Example 5: Add Function With Parameters (Without any dependencies)
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="flNewProtect"></param>
        /// <param name="lpflOldProtect"></param>
        /// <returns></returns>
        public static void E5()
        {
            var process = System.Diagnostics.Process.GetCurrentProcess();

            //You can use any x86 assembler
            //For this example I have used https://defuse.ca/online-x86-assembler.htm

            // Without FASM.Net I strongly suggest you to comment each instruction (e.g. "0 push ebp")
            byte[] assembledCode =
            {
                0x55,               // 0 push ebp            ; init stack frame
                0x8B, 0x45, 0x08,   // 1 mov  eax, [ebp+8]   ; set eax to second param (remember, in cdecl calling convention, params are pushed right-to-left)
                0x8B, 0x55, 0x0C,   // 4 mov  edx, [ebp+12]  ; set edx to first param
                0x01, 0xD0,         // 7 add  eax, edx       ; add edx (first param) to eax (second param) 
                0x5D,               // 9 pop  ebp            ; leave stack frame
                0xC3                // A ret                 ; in cdecl calling convention, return value is stored in eax; so this will return both params added up
            };

            int returnValue;
            unsafe
            {
                fixed (byte* ptr = assembledCode)
                {
                    var memoryAddress = (IntPtr)ptr;

                    // Mark memory as EXECUTE_READWRITE to prevent DEP exceptions
                    if (!VirtualProtectEx(process.Handle, memoryAddress,
                        (UIntPtr)assembledCode.Length, 0x40 /* EXECUTE_READWRITE */, out uint _))
                    {
                        throw new System.Exception("!!!");
                    }

                    AssemblyAddFunction myAssemblyFunction = Marshal.GetDelegateForFunctionPointer<AssemblyAddFunction>(memoryAddress);
                    returnValue = myAssemblyFunction(10, -15);
                }
            }

            // Note: We do not have to dispose memory ourself; the CLR will handle this.  
            Console.WriteLine($"Example3 (no dependencies) return value: {returnValue}, expected: -5"); // Prints -5
        }

    }
}
