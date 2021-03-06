﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using ICSharpCode.PythonBinding;
using ICSharpCode.SharpDevelop.Dom;
using NUnit.Framework;
using PythonBinding.Tests;

namespace PythonBinding.Tests.Parsing
{
	/// <summary>
	/// Tests that the PythonParser class has a class in the returned
	/// CompilationUnit.
	/// </summary>
	[TestFixture]
	public class ParseSingleClassTestFixture
	{
		ICompilationUnit compilationUnit;
		IClass c;
		
		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			string python = "class Test:\r\n" +
							"\tpass";
			
			DefaultProjectContent projectContent = new DefaultProjectContent();
			PythonParser parser = new PythonParser();
			compilationUnit = parser.Parse(projectContent, @"C:\Projects\Test\test.py", python);
			if (compilationUnit.Classes.Count > 0) {
				c = compilationUnit.Classes[0];
			}
		}
		
		[Test]
		public void OneClass()
		{
			Assert.AreEqual(1, compilationUnit.Classes.Count);
		}
		
		[Test]
		public void ClassName()
		{
			Assert.AreEqual("Test", c.Name);
		}
		
		/// <summary>
		/// The namespace of a class is the name of file containing the class excluding
		/// the file extension.
		/// </summary>
		[Test]
		public void FullyQualifiedClassName()
		{
			Assert.AreEqual("test.Test", c.FullyQualifiedName);
		}
		
		[Test]
		public void CompilationUnitFileName()
		{
			Assert.AreEqual(@"C:\Projects\Test\test.py", compilationUnit.FileName);
		}
		
		[Test]
		public void ClassBodyRegion()
		{
			int startLine = 1;
			int startColumn = 12;
			int endLine = 2;
			int endColumn = 6;
			DomRegion region = new DomRegion(startLine, startColumn, endLine, endColumn);
			Assert.AreEqual(region.ToString(), c.BodyRegion.ToString());
		}
		
		[Test]
		public void ClassRegion()
		{
			int startLine = 1;
			int startColumn = 1;
			int endLine = 2;
			int endColumn = 6;
			DomRegion region = new DomRegion(startLine, startColumn, endLine, endColumn);
			Assert.AreEqual(region.ToString(), c.Region.ToString());
		}		
	}
}
