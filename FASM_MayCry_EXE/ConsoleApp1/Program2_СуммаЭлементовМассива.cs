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

namespace ConsoleApp1
{
    public class Student
    {
        public System.String Name = "Илья";
        public List<System.Int32> LabS = new List<System.Int32>();
        /////////////////////////
        public Student() { }
        private static Random __rand = new Random();
        public Student(System.String _Name) 
        {
            this.Name = _Name;
            int e = __rand.Next(1, 11);
            for (int i = 0; i < e; i++)this.LabS.Add(__rand.Next(2, 6));
        }
        public Student(System.String _Name, params System.Int32[] _LabS)
        {
            this.Name = _Name;
            this.LabS = _LabS.ToList();
        }
        /////////////////////////
        public Student Set(
            Student _this=null
            ,System.String _Name = null
            ,List<System.Int32> _LabS =null
        )
        {
            if (_this != null) this.Set(_this: null, _Name: _this.Name, _LabS: _this.LabS);
            if (_Name != null) this.Name = _Name;
            if (_LabS != null) this.LabS = _LabS;
            return this;
        }
        public Student WriteThis() 
        {
            System.Console.Write(this.Name + ";");
            foreach (int a in this.LabS) System.Console.Write(a + ";");
            System.Console.WriteLine();
            return this;
        }
        ///////////////////////
        public int Summ() 
        {
            int a = 0;
            //a = Summ(this.LabS.Count, this.LabS.ToArray());
            ;
            if (!false) 
            {
                //Стек функции int delSumm_For_Assembly( int _count, params int[] _ints);
                //count=3 //[ebp+8]
                //*int[]  //[ebp+12]
                //00000000000000000000
                FasmNet _FasmNet = new FasmNet();
                //Получение параметров из стека функции
                //int delSumm_For_Assembly( int _count, params int[] _ints);
                _FasmNet.AddLine("use32");
                _FasmNet.AddLine("xor eax, eax");
                _FasmNet.AddLine("xor ebx, ebx");
                _FasmNet.AddLine("xor ecx, ecx");
                _FasmNet.AddLine("mov ebx, dword [ebp+12]");//Указатель на первый элнемент массива
                _FasmNet.AddLine("metka_ForStart:");
                //Сумма всех элементов массива в лоб
                _FasmNet.AddLine("add eax, dword [ebx]");//Указатель на первый элнемент массива
                _FasmNet.AddLine("add ebx, 4");//Указатель на первый элнемент массива
                _FasmNet.AddLine("add ecx, 1");//Указатель на первый элнемент массива
                _FasmNet.AddLine("cmp ecx, dword [ebp+8]");
                _FasmNet.AddLine("je metka_Forend");
                _FasmNet.AddLine("jmp metka_ForStart");
                _FasmNet.AddLine("metka_Forend:");
                _FasmNet.AddLine("ret");
                System.Byte[] _ByteS = _FasmNet.Assemble();

                Process.NET.Memory.IAllocatedMemory _IAllocatedMemory =
                    new ProcessSharp(System.Diagnostics.Process.GetCurrentProcess(), Process.NET.Memory.MemoryType.Local)
                .MemoryFactory
                .Allocate(
                    name: "SamName", 
                    size: _ByteS.Length,
                    protection: MemoryProtectionFlags.ExecuteReadWrite 
                );
                _IAllocatedMemory.Write(0, _ByteS);

                delSumm_For_Assembly _delSumm_For_Assembly = Marshal.GetDelegateForFunctionPointer<delSumm_For_Assembly>(_IAllocatedMemory.BaseAddress);
                
                a= _delSumm_For_Assembly(this.LabS.Count, this.LabS.ToArray());
                
                _IAllocatedMemory.Dispose();

                ;
            }
            return a;
        }
        [System.Security.SuppressUnmanagedCodeSecurityAttribute]
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate int delSumm_For_Assembly( int _count, params int[] _ints);
        public int Summ( int _count, params int[] _ints)
        {
            ;
            int qwds = _ints[1];
            int _summ = 0;
            int i = 0;
            while (i < _count)
            {
                int a = _ints[i];
                _summ = _summ + a;
                i++;
            }
            return _summ;
        }
        

    }

    public class Program
    {
        static void Main(string[] args)
        {
            Student _Student = new Student("Иван",5,4,2,5,4,2);
            _Student.WriteThis();
            System.Console.WriteLine(_Student.Summ());
            System.Console.ReadLine();
        }
    }
}
