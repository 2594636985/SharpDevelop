﻿// <file>
//     <copyright see="prj:///doc/copyright.txt">2002-2005 AlphaSierraPapa</copyright>
//     <license see="prj:///doc/license.txt">GNU General Public License</license>
//     <owner name="David Srbecký" email="dsrbecky@gmail.com"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

using DebuggerInterop.Core;
using DebuggerInterop.MetaData;

namespace DebuggerLibrary
{
	public class Module: RemotingObjectBase, IDisposable
	{
		string fullPath;
		ulong  baseAdress;
		int    isDynamic;
		int    isInMemory;
		
		int orderOfLoading = 0;
		readonly ICorDebugModule corModule;
		ISymbolReader symReader;
		IMetaDataImport metaDataInterface;
		
		internal MetaData MetaData {
			get {
				return new MetaData(metaDataInterface);
			}
		}
		
		public ISymbolReader SymReader {
			get {
				return symReader;
			}
		}
		
		public ICorDebugModule CorModule {
			get {
				return corModule;
			}
		}
		
		public ulong BaseAdress { 
			get {
				return baseAdress;
			} 
		}
		
		public bool IsDynamic { 
			get {
				return isDynamic == 1;
			} 
		}
		
		public bool IsInMemory { 
			get {
				return isInMemory == 1;
			} 
		}
		
		public string FullPath { 
			get {
				return fullPath;
			} 
		}
		
		public string Filename {
			get {
				if (IsDynamic || IsInMemory) return String.Empty;
				return System.IO.Path.GetFileName(FullPath);
			}
		}
		
		public string DirectoryName {
			get {
				if (IsDynamic || IsInMemory) return String.Empty;
				return System.IO.Path.GetDirectoryName(FullPath);
			}
		}
		
		public bool SymbolsLoaded { 
			get {
				return symReader != null;
			} 
		}
		
		public int OrderOfLoading { 
			get {
				return orderOfLoading;
			}
			set {
				orderOfLoading = value;
			}
		}
		
		public bool JMCStatus {
			set {
				uint unused = 0;
				((ICorDebugModule2)corModule).SetJMCStatus(value?1:0, 0, ref unused);
			}
		}
		
		internal Module(ICorDebugModule pModule)
		{
			corModule = pModule;
			
			pModule.GetBaseAddress(out baseAdress);
			
			pModule.IsDynamic(out isDynamic);
			
			pModule.IsInMemory(out isInMemory);
			
			Guid metaDataInterfaceGuid = new Guid("{ 0x7dac8207, 0xd3ae, 0x4c75, { 0x9b, 0x67, 0x92, 0x80, 0x1a, 0x49, 0x7d, 0x44 } }");
			object pMetaDataInterface;
			pModule.GetMetaDataInterface(ref metaDataInterfaceGuid, out pMetaDataInterface);
			
			metaDataInterface = (IMetaDataImport) pMetaDataInterface;
			
			uint pStringLenght = 0; // Terminating character included in pStringLenght
			IntPtr pString = IntPtr.Zero;
			pModule.GetName(pStringLenght,
							out pStringLenght, // real string lenght
			                pString);
			// Allocate string buffer
			pString = Marshal.AllocHGlobal((int)pStringLenght * 2);
			pModule.GetName(pStringLenght,
							out pStringLenght, // real string lenght
			                pString);
			fullPath = Marshal.PtrToStringUni(pString);
			Marshal.FreeHGlobal(pString);
			
			
			SymBinder symBinder = new SymBinder();
			IntPtr ptr = IntPtr.Zero;
			try {
				ptr = Marshal.GetIUnknownForObject(metaDataInterface);
				symReader = symBinder.GetReader(ptr, fullPath, string.Empty);
			} catch (System.Exception) {
				symReader = null;
			} finally {
				if (ptr != IntPtr.Zero) {
					Marshal.Release(ptr);
				}
			}
			
			JMCStatus = SymbolsLoaded;
		}
		
		public void ApplyChanges(byte[] metadata, byte[] il)
		{
			(corModule as ICorDebugModule2).ApplyChanges((uint)metadata.Length, metadata, (uint)il.Length, il);
		}
		
		public void Dispose()
		{
			if (symReader != null) {
				try {
					System.Reflection.MethodInfo m = symReader.GetType().GetMethod("{dtor}");
					m.Invoke(symReader, null);
				} catch {
					Console.WriteLine("symReader release failed. ({dtor})");
				} finally {
					symReader = null;
				}
			}
			
			try {
				Marshal.FinalReleaseComObject(metaDataInterface);
			} catch {
				Console.WriteLine("metaDataInterface release failed. (FinalReleaseComObject)");
			} finally {
				metaDataInterface = null;
			}
		}
	}
}
