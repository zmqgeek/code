		using System.Diagnostics;
		using System.Runtime.InteropServices;
		using System.Collections.Generic;
		using System.Linq;
		using System;
	---
		public static class Toolhelp32 {
			public const uint Inherit = 0x80000000;
			public const uint SnapModule32 = 0x00000010;
			public const uint SnapAll = SnapHeapList|SnapModule|SnapProcess|SnapThread;
			public const uint SnapHeapList = 0x00000001;
			public const uint SnapProcess = 0x00000002;
			public const uint SnapThread = 0x00000004;
			public const uint SnapModule = 0x00000008;
			[DllImport("kernel32.dll")]
			static extern bool CloseHandle(IntPtr handle);
			[DllImport("kernel32.dll")]
			static extern IntPtr CreateToolhelp32Snapshot(uint flags, int processId);
			public static IEnumerable<T> CreateSnapshot<T>(uint flags, int id) where T : IEntry, new() {
				using(var snap = new Snapshot(flags, id))
					for(IEntry entry = new T { }; entry.TryMoveNext(snap, out entry);)
						yield return (T)entry;
			}
			public interface IEntry {
				bool TryMoveNext(Toolhelp32.Snapshot snap, out IEntry entry);
			}
			public struct Snapshot:IDisposable {
				void IDisposable.Dispose() {
					Toolhelp32.CloseHandle(m_handle);
				}
				public Snapshot(uint flags, int processId) {
					m_handle=Toolhelp32.CreateToolhelp32Snapshot(flags, processId);
				}
				IntPtr m_handle;
			}
		}
	---
		[StructLayout(LayoutKind.Sequential)]
		public struct WinProcessEntry:Toolhelp32.IEntry {
			[DllImport("kernel32.dll")]
			public static extern bool Process32Next(Toolhelp32.Snapshot snap, ref WinProcessEntry entry);
			public bool TryMoveNext(Toolhelp32.Snapshot snap, out Toolhelp32.IEntry entry) {
				var x = new WinProcessEntry { dwSize=Marshal.SizeOf(typeof(WinProcessEntry)) };
				var b = Process32Next(snap, ref x);
				entry=x;
				return b;
			}
			public int dwSize;
			public int cntUsage;
			public int th32ProcessID;
			public IntPtr th32DefaultHeapID;
			public int th32ModuleID;
			public int cntThreads;
			public int th32ParentProcessID;
			public int pcPriClassBase;
			public int dwFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public String fileName;
			//byte fileName[260];
			//public const int sizeofFileName = 260;
		}
	---
		public static class Extensions {
			public static Process Parent(this Process p) {
				var entries = Toolhelp32.CreateSnapshot<WinProcessEntry>(Toolhelp32.SnapAll, 0);
				var parentid = entries.First(x => x.th32ProcessID==p.Id).th32ParentProcessID;
				return Process.GetProcessById(parentid);
			}
		}
