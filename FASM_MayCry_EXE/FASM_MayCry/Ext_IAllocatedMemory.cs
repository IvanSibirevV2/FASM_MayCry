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
    public static class Ext_IAllocatedMemory
    {
        /// <summary>
        /// IntPtr returnValue = new IntPtr();
        /// ("use32"+"\n"+" mov eax, [ebp+4]"+"\n"+" ret").Get_FasmNet_Assemble().Get_Memory()
        /// .Set_ExecuteDelegate<AssemblyReadRegistersFunction>(_Delegate => {returnValue = _Delegate();})
        /// .Dispose();
        /// </summary>
        /// <typeparam name="TDelegate">
        /// [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        /// [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        /// </typeparam>
        /// <param name="_this"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Process.NET.Memory.IAllocatedMemory Set_ExecuteDelegate<TDelegate>(this Process.NET.Memory.IAllocatedMemory _this,System.Action<TDelegate> x) where TDelegate : System.Delegate
        {x(_this.Get_Delegate<TDelegate>());return _this;}
        /// <summary>
        /// var _Memory = ("use32"+"\n"+" mov eax, [ebp+4]"+"\n"+" ret").Get_FasmNet_Assemble().Get_Memory();
        /// int returnValue = _Memory.Get_Delegate<AssemblyConstantValueFunction>()();
        /// _Memory.Dispose();
        /// </summary>
        /// <typeparam name="TDelegate">
        /// [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        /// [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        /// </typeparam>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static TDelegate Get_Delegate<TDelegate>(this Process.NET.Memory.IAllocatedMemory _this) where TDelegate : System.Delegate
        { return Marshal.GetDelegateForFunctionPointer<TDelegate>(_this.BaseAddress); }
        /// <summary></summary>
        /// [System.Security.SuppressUnmanagedCodeSecurityAttribute] // disable security checks for better performance
        /// [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] // cdecl - let caller (.NET CLR) clean the stack
        /// <param name="_this"></param>
        /// <param name="_Delegate"></param>
        /// <returns></returns>
        public static Process.NET.Memory.IAllocatedMemory Get_Delegate<TDelegate>(this Process.NET.Memory.IAllocatedMemory _this, out TDelegate _Delegate) where TDelegate : System.Delegate
        {
            _Delegate = _this.Get_Delegate<TDelegate>();
            return _this;
        }
        public static Process.NET.Memory.IAllocatedMemory Write_(this Process.NET.Memory.IAllocatedMemory _this, int offset, byte[] toWrite)
        { _this.Write(offset, toWrite); return _this; }
    }

}
