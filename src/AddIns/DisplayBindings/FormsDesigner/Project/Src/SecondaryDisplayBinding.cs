﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.IO;

using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.NRefactory.Parser;
using ICSharpCode.FormsDesigner.Services;
using System.ComponentModel.Design;

namespace ICSharpCode.FormsDesigner
{
	public class FormsDesignerSecondaryDisplayBinding : ISecondaryDisplayBinding
	{
		/// <summary>
		/// When you return true for this property, the CreateSecondaryViewContent method
		/// is called again after the LoadSolutionProjects thread has finished.
		/// </summary>
		public bool ReattachWhenParserServiceIsReady {
			get {
				return true;
			}
		}
		
		public static bool IsInitializeComponentsMethodName(string name)
		{
			return name == "InitializeComponents" || name == "InitializeComponent";
		}
		
		public static IMethod GetInitializeComponents(IClass c)
		{
			c = c.GetCompoundClass();
			foreach (IMethod method in c.Methods) {
				if (IsInitializeComponentsMethodName(method.Name) && method.Parameters.Count == 0) {
					return method;
				}
			}
			return null;
		}

		public static bool BaseClassIsFormOrControl(IClass c)
		{
			// Simple test for fully qualified name
			c = c.GetCompoundClass();
			foreach (IReturnType baseType in c.BaseTypes) {
				if (baseType.FullyQualifiedName == "System.Windows.Forms.Form"
				    || baseType.FullyQualifiedName == "System.Windows.Forms.UserControl"
				    // also accept Form and UserControl when they could not be resolved
				    || baseType.FullyQualifiedName == "Form"
				    || baseType.FullyQualifiedName == "UserControl")
				{
					return true;
				}
			}
			
			IClass form = ProjectContentRegistry.WinForms.GetClass("System.Windows.Forms.Form");
			IClass userControl = ProjectContentRegistry.WinForms.GetClass("System.Windows.Forms.UserControl");
			if (form != null && c.IsTypeInInheritanceTree(form))
				return true;
			if (userControl != null && c.IsTypeInInheritanceTree(userControl))
				return true;
			return false;
		}

		public static bool IsDesignable(ParseInformation info)
		{
			if (info != null) {
				ICompilationUnit cu = (ICompilationUnit)info.BestCompilationUnit;
				foreach (IClass c in cu.Classes) {
					IMethod method = GetInitializeComponents(c);
					if (method != null) {
						return BaseClassIsFormOrControl(c);
					}
				}
			}
			return false;
		}
		
		public bool CanAttachTo(IViewContent viewContent)
		{
			if (viewContent is ITextEditorControlProvider) {
				ITextEditorControlProvider textAreaControlProvider = (ITextEditorControlProvider)viewContent;
				string fileExtension = String.Empty;
				string fileName      = viewContent.IsUntitled ? viewContent.UntitledName : viewContent.FileName;
				if (fileName == null)
					return false;
				
				fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
				
				switch (fileExtension) {
					case ".cs":
					case ".vb":
						ParseInformation info = ParserService.ParseFile(fileName, textAreaControlProvider.TextEditorControl.Document.TextContent, false, true);
						
						if (IsDesignable(info))
							return true;
						break;
					case ".xfrm":
						return true;
				}
			}
			return false;
		}
		
		public ISecondaryViewContent[] CreateSecondaryViewContent(IViewContent viewContent)
		{
			if (viewContent.SecondaryViewContents.Exists(delegate(ISecondaryViewContent c) { return c.GetType() == typeof(FormsDesignerViewContent); })) {
				return new ISecondaryViewContent[0];
			}
			
			string fileExtension = String.Empty;
			string fileName      = viewContent.IsUntitled ? viewContent.UntitledName : viewContent.FileName;
			
			fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
			
			IDesignerLoaderProvider loader;
			IDesignerGenerator generator;
			
			switch (fileExtension) {
				case ".cs":
					loader    = new NRefactoryDesignerLoaderProvider(SupportedLanguage.CSharp, ((ITextEditorControlProvider)viewContent).TextEditorControl);
					generator = new CSharpDesignerGenerator();
					break;
				case ".vb":
					loader    = new NRefactoryDesignerLoaderProvider(SupportedLanguage.VBNet, ((ITextEditorControlProvider)viewContent).TextEditorControl);
					generator = new VBNetDesignerGenerator();
					break;
				case ".xfrm":
					loader    = new XmlDesignerLoaderProvider(((ITextEditorControlProvider)viewContent).TextEditorControl);
					generator = new XmlDesignerGenerator();
					break;
				default:
					throw new ApplicationException("Cannot create content for " + fileExtension);
			}
			return new ISecondaryViewContent[] { new FormsDesignerViewContent(viewContent, loader, generator) };
		}
	}
}
