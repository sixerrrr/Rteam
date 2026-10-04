using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class Spectre
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DuplicateHandle(
        IntPtr hSourceProcessHandle,
        IntPtr hSourceHandle,
        IntPtr hTargetProcessHandle,
        out IntPtr lpTargetHandle,
        uint dwDesiredAccess,
        bool bInheritHandle,
        uint dwOptions);

    [DllImport("ntdll.dll")]
    private static extern int NtQuerySystemInformation(
        int systemInformationClass,
        IntPtr systemInformation,
        int systemInformationLength,
        ref int returnLength);

    [DllImport("ntdll.dll")]
    private static extern int NtQueryObject(
        IntPtr handle,
        int objectInformationClass,
        IntPtr objectInformation,
        int objectInformationLength,
        ref int returnLength);

    private const uint PROCESS_DUP_HANDLE = 0x0040;
    private const uint DUPLICATE_CLOSE_SOURCE = 0x00000001;
    private const int SystemHandleInformation = 16;
    private const int ObjectNameInformation = 1;

    [StructLayout(LayoutKind.Sequential)]
    private struct SYSTEM_HANDLE_INFORMATION
    {
        public uint ProcessId;
        public byte ObjectTypeNumber;
        public byte Flags;
        public ushort Handle;
        public IntPtr Object;
        public uint GrantedAccess;
    }

    public static bool CloseRobloxSingletonHandle()
    {
        Process[] processes = Process.GetProcessesByName("RobloxPlayerBeta");
        if (processes.Length == 0) return false;

        int robloxPid = processes[0].Id;
        IntPtr hRoblox = OpenProcess(PROCESS_DUP_HANDLE, false, robloxPid);

        if (hRoblox == IntPtr.Zero) return false;

        bool success = false;
        int status;
        int length = 0x10000;
        IntPtr ptrInfo = Marshal.AllocHGlobal(length);

        while ((status = NtQuerySystemInformation(SystemHandleInformation, ptrInfo, length, ref length)) == -1073741820)        {
            Marshal.FreeHGlobal(ptrInfo);
            ptrInfo = Marshal.AllocHGlobal(length);
        }

        if (status >= 0)
        {
            long handleCount = Marshal.ReadInt64(ptrInfo);
            int offset = sizeof(long);
            int structSize = Marshal.SizeOf<SYSTEM_HANDLE_INFORMATION>();

            for (long i = 0; i < handleCount; i++)
            {
                var handleInfo = Marshal.PtrToStructure<SYSTEM_HANDLE_INFORMATION>(ptrInfo + offset);
                offset += structSize;

                if (handleInfo.ProcessId == robloxPid)
                {
                    IntPtr hLocalTarget;
                    if (DuplicateHandle(hRoblox, (IntPtr)handleInfo.Handle, Process.GetCurrentProcess().Handle, out hLocalTarget, 0, false, 0))
                    {
                        string handleName = GetObjectName(hLocalTarget);
                        CloseHandle(hLocalTarget);
                        if (!string.IsNullOrEmpty(handleName) && handleName.Contains("ROBLOX_singletonEvent"))
                        {
                            DuplicateHandle(hRoblox, (IntPtr)handleInfo.Handle, IntPtr.Zero, out _, 0, false, DUPLICATE_CLOSE_SOURCE);
                            success = true;
                            break;
                        }
                    }
                }
            }
        }

        Marshal.FreeHGlobal(ptrInfo);
        CloseHandle(hRoblox);
        return success;
    }

    private static string GetObjectName(IntPtr handle)
    {
        int length = 0x400;
        IntPtr ptrName = Marshal.AllocHGlobal(length);
        int returnLength = 0;

        if (NtQueryObject(handle, ObjectNameInformation, ptrName, length, ref returnLength) >= 0)
        {
            short stringLength = Marshal.ReadInt16(ptrName);
            IntPtr buffer = Marshal.ReadIntPtr(ptrName + (IntPtr.Size == 8 ? 8 : 4));

            if (buffer != IntPtr.Zero && stringLength > 0)
            {
                string result = Marshal.PtrToStringUni(buffer, stringLength / 2);
                Marshal.FreeHGlobal(ptrName);
                return result;
            }
        }

        Marshal.FreeHGlobal(ptrName);
        return string.Empty;
    }
}
