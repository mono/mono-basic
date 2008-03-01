' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Public Class CallStatement
    Inherits Statement

    Private m_Target As Expression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Target As Expression)
        m_Target = Target
        m_Target.Parent = Me
    End Sub

    ReadOnly Property Target() As Expression
        Get
            Return m_Target
        End Get
    End Property

    Private Function IsExcluded() As Boolean
        Dim exp As InvocationOrIndexExpression
        Dim method As MethodInfo
        Dim classification As MethodGroupClassification
   
        If Not m_Target.Classification.IsVoidClassification Then Return False

        exp = TryCast(m_Target, InvocationOrIndexExpression)

        If exp Is Nothing Then Return False
        If exp.Expression Is Nothing Then Return False

        classification = TryCast(exp.Expression.Classification, MethodGroupClassification)
        If classification Is Nothing Then Return False

        method = classification.ResolvedMethodInfo

        If method Is Nothing Then Return False

        Return Compiler.Scanner.IsConditionallyExcluded(method, Me.Location)
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_Target.Classification.IsLateBoundClassification Then
            result = m_Target.GenerateCode(Info) AndAlso result

            Return result
        End If

        Helper.Assert(m_Target.Classification.IsValueClassification OrElse m_Target.Classification.IsVoidClassification)

        If IsExcluded() Then Return result

        result = m_Target.GenerateCode(Info.Clone(Me, True)) AndAlso result
        If m_Target.Classification.IsValueClassification Then
            If Helper.CompareType(m_Target.Classification.AsValueClassification.Type, Compiler.TypeCache.System_Void) = False Then
                Emitter.EmitPop(Info, m_Target.ExpressionType)
            End If
        End If

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Target.ResolveExpression(Info) AndAlso result

        If result = False Then Return result

        If m_Target.Classification.IsMethodGroupClassification Then
            Dim tmp As New InvocationOrIndexExpression(Me)
            tmp.Init(m_Target, New ArgumentList(tmp))
            result = tmp.ResolveExpression(Info) AndAlso result
            m_Target = tmp
        End If

        Return True
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Target.ResolveTypeReferences()
    End Function

#If DEBUG Then
    Public Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("Call ")
        m_Target.Dump(Dumper)
        Dumper.WriteLine("")
    End Sub
#End If
End Class
