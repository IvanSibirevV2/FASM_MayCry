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
    public static class Ext_FasmNet
    {
        public static FasmNet AddLine_(this FasmNet _this, System.String _str, params object[] args)
        { _this.AddLine(_str, args); return _this; }
        /// <summary>
        /// Передача скопом всего ассемблерного кода в FasmNet.
        /// Передача паравметров функции рекомендуется только через стек вызова функции.
        /// int AssemblyAddFunction(int x, int y);  =>  
        /// fasmNet.AddLine("use32"); //Tell FASM.Net to use x86 (32bit) mode
        /// fasmNet.AddLine("push ebp"); // init stack frame
        /// fasmNet.AddLine("mov eax, [ebp+8]"); // set eax to second param (remember, in cdecl calling convention, params are pushed right-to-left)
        /// fasmNet.AddLine("mov edx, [ebp+12]"); // set edx to first param
        /// fasmNet.AddLine("add eax, edx"); //add edx (first param) to eax (second param) 
        /// fasmNet.AddLine("pop ebp"); // leave stack frame
        /// fasmNet.AddLine("ret");  // in cdecl calling convention, return value is stored in eax; so this will return both params added up
        /// </summary><param name="_this"></param><param name="_str"></param><returns></returns>
        public static FasmNet AddLineS_(this FasmNet _this, System.String _str)
        { _str.Split('\n').Select(a => a.Split('\r')[0]).ToList().ForEach(a => _this.AddLine(a)); return _this; }
    }
}
