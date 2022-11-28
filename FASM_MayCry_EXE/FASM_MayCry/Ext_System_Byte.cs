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
    public static class Ext_System_Byte
    {
        public static Process.NET.Memory.IAllocatedMemory Get_Memory(this System.Byte[] _this, System.String _name = "Example1")
        {
            return (Process.NET.Memory.IAllocatedMemory)
                new ProcessSharp(System.Diagnostics.Process.GetCurrentProcess(), Process.NET.Memory.MemoryType.Local)
                .MemoryFactory
                .Allocate(
                    name: _name, // only used for debugging; not really needed
                    size: _this.Length,
                    protection: MemoryProtectionFlags.ExecuteReadWrite /* It is important to mark the memory as executeable or we will get exceptions from DEP */
                )
                .Write_(0, _this);
            //return Marshal.GetDelegateForFunctionPointer<T>(allocatedCodeMemory.BaseAddress);
            //IAllocatedMemory.Dispose();
        }
    }
}
