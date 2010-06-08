' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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

''' <summary>
''' ImportsClauses  ::= ImportsClause  | ImportsClauses  ","  ImportsClause
''' </summary>
''' <remarks></remarks>
Public Class ImportsClauses
    Inherits NamedBaseList(Of ImportsClause)

    Function GetNamespaces(ByVal FromWhere As BaseObject, ByVal Name As String) As Generic.List(Of [Namespace])
        Dim result As New Generic.List(Of [Namespace])
        For Each imp As ImportsClause In Me
            If imp.IsNamespaceClause AndAlso imp.AsNamespaceClause.IsNamespaceImport Then
                Dim ns As [Namespace]
                ns = FromWhere.Compiler.TypeManager.Namespaces.Item(imp.AsNamespaceClause.NamespaceImported, Name)
                If ns IsNot Nothing Then
                    result.Add(ns)
                End If
            End If
        Next
        Return result
    End Function

    ''' <summary>
    ''' Looks up all the modules imported by all the imports clauses.
    ''' </summary>
    ''' <param name="FromWhere"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetModules(ByVal FromWhere As BaseObject) As TypeList
        Dim result As TypeList = Nothing
        For Each imp As ImportsClause In Me
            If imp.IsNamespaceClause Then
                Dim ns As ImportsNamespaceClause = imp.AsNamespaceClause
                If ns.IsTypeImport Then
                    'A type cannot contain a module, nothing to do here.
                ElseIf ns.IsNamespaceImport Then
                    Dim modules As TypeDictionary = FromWhere.Compiler.TypeManager.GetModulesByNamespace(ns.NamespaceImported.ToString)
                    If modules IsNot Nothing AndAlso modules.Count > 0 Then
                        If result Is Nothing Then result = New TypeList
                        result.AddRange(modules.TypesAsArray)
                    End If
                Else
                    Continue For 'This import was not resolved correctly, don't use it.
                End If
            End If
        Next
        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    ''' <summary>
    ''' Returns true if a clause with the same imported namespace or alias exists.
    ''' </summary>
    ''' <param name="Clause"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Exists(ByVal Clause As ImportsClause) As Boolean
        For Each cl As ImportsClause In Me
            If cl.IsNamespaceClause Then
                If Clause.IsNamespaceClause Then
                    If Helper.CompareName(cl.AsNamespaceClause.Name, Clause.AsNamespaceClause.Name) Then
                        Return True
                    End If
                End If
            ElseIf cl.IsAliasClause Then
                If Clause.IsAliasClause Then
                    If Helper.CompareName(cl.AsAliasClause.Name, Clause.AsAliasClause.Name) Then
                        Return True
                    End If
                End If
            Else
                Throw New InternalException(Me)
            End If
        Next
        Return False
    End Function
End Class
