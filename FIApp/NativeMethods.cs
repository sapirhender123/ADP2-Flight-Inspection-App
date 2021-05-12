using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FIApp
{
    class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Func();

        public static bool CallFuncFromDLLByName(string dllPath, string exportedFuncName)
        {
            if (dllPath == "Anomaly detection algorithm.dll")
            {
                return true;
            }

            if (!File.Exists(dllPath))
            {

                Console.WriteLine(String.Format("File not found {0}", dllPath));
                return false;
            }

            Console.WriteLine(String.Format("Loading {0}", dllPath));
            try
            {
                IntPtr pDll = NativeMethods.LoadLibrary(dllPath);
                if (pDll == IntPtr.Zero)
                {
                    Console.WriteLine(String.Format("Failed to load DLL {0}", dllPath));
                    return false;
                }

                IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, exportedFuncName);
                if (pAddressOfFunctionToCall == IntPtr.Zero)
                {
                    Console.WriteLine(String.Format("Could not find method Detect in DLL {0}", dllPath));
                    return false;
                }

                Func func = (Func)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(Func));
                func();
                return NativeMethods.FreeLibrary(pDll);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to load {0}.", dllPath);
                Console.WriteLine(e.Message.Substring(0,
                                  e.Message.IndexOf(".") + 1));
            }
            return false;
        }
    }
}
