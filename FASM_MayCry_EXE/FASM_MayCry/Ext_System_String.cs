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

namespace System
{
    public static class Ext_System_String
    {
        /// <summary>new FasmNet().AddLineS_(_ASM_SourseCode).Assemble();</summary>
        /// <param name="_ASM_SourseCode">_ASM_SourseCode</param>
        /// <returns>System.Byte[] - Byte_ASM_Code</returns>
        public static System.Byte[] Get_FasmNet_Assemble(this System.String _ASM_SourseCode) => new FasmNet().AddLineS_(_ASM_SourseCode).Assemble();
    }
}
