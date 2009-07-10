' 
' Copyright (C) 2009 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

Imports Mono.Cecil
Imports System
Imports System.IO
Imports System.Reflection

Class Tuner
	Shared Function Main (args () As String) As Integer
		Dim a As AssemblyDefinition
		Dim source As String = args (0)
		Dim destination As String = args (1)
		
		source = Path.GetFullPath (source)
		destination = Path.GetFullPath (destination)

		a = AssemblyFactory.GetAssembly (source)
		a.MainModule.LoadSymbols ()

		Console.WriteLine ("Assembly successfully loaded from {0}", source)

		For i As Integer = a.MainModule.AssemblyReferences.Count - 1 To 0 Step -1
			Dim ref as AssemblyNameReference = a.MainModule.AssemblyReferences (i)

			Console.Write ("Assembly reference: {0}", ref.FullName)
			ref.Version = new Version (2, 0, 5, 0)
			ref.PublicKeyToken = new Byte () {&H7C, &HEC, &H85, &HD7, &HBE, &HA7, &H79, &H8E }
			Console.WriteLine (" => {0}", ref.FullName)
		Next

		a.MainModule.SaveSymbols ()
		AssemblyFactory.SaveAssembly (a, destination)

		Console.WriteLine ("Assembly successfully written to {0}", destination)
	End Function
End Class