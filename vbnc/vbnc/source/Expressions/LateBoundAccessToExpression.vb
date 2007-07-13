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

Public MustInherit Class LateBoundAccessToExpression
    Inherits Expression

    Private m_LateBoundAccess As LateBoundAccessClassification

    ReadOnly Property LateBoundAccess() As LateBoundAccessClassification
        Get
            Return m_LateBoundAccess
        End Get
    End Property

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Throw New InternalException("A late bound expression does not have a constant value.")
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal LateBoundAccessClassification As LateBoundAccessClassification)
        MyBase.new(Parent)
        m_LateBoundAccess = LateBoundAccessClassification
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.NotImplemented()

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return Compiler.TypeCache.System_Object
        End Get
    End Property

    Protected Function EmitLateGet(ByVal Info As EmitInfo) As Boolean
        Return EmitLateGet(Info, LateBoundAccess)
    End Function

    Public Shared Function EmitLateGet(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True
        Dim copyBacks As Boolean() = Nothing

        'We need to emit a call to LateGet

        '1 - the instance expression
        result = LateBoundAccess.InstanceExpression.GenerateCode(Info) AndAlso result

        '2 - Type ??? - haven't found an example where this isn't nothing yet
        Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_Type))

        '3 - The member name
        Emitter.EmitLoadValue(Info, LateBoundAccess.Name)

        '4 - The arguments
        Emitter.EmitLoadI4Value(Info, 0)
        Emitter.EmitNewArr(Info, Info.Compiler.TypeCache.System_Object)
        'Implement argument emission.

        '5 - ArgumentNames
        Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_String_Array))

        '6 - TypeArguments
        If LateBoundAccess.TypeArguments IsNot Nothing Then
            Helper.NotImplemented("LateGet with type arguments.")
        Else
            Emitter.EmitLoadNull(Info.Clone(info.Compiler.TypeCache.System_Type_Array))
        End If

        '7 - CopyBack
        If copyBacks IsNot Nothing Then
            Helper.NotImplemented("LateGet with byref arguments.")
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_Boolean_Array))
        End If

        Emitter.EmitCall(Info, info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateGet_Object_Type_String_Array_Array_Array_Array)

        Return result
    End Function

    Protected Function EmitLateSet(ByVal Info As EmitInfo) As Boolean
        Return EmitLateSet(Info, LateBoundAccess)
    End Function

    Public Shared Function EmitLateSet(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True

        'We need to emit a call to LateSet

        '1 - the instance expression
        result = LateBoundAccess.InstanceExpression.GenerateCode(Info.Clone(True, False, LateBoundAccess.InstanceExpression.ExpressionType)) AndAlso result

        '2 - Type ??? - haven't found an example where this isn't nothing yet
        Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_Type))

        '3 - The member name
        Emitter.EmitLoadValue(Info, LateBoundAccess.Name)

        '4 - The arguments
        Emitter.EmitLoadI4Value(Info, 1)
        Emitter.EmitNewArr(Info, Info.Compiler.TypeCache.System_Object)
        Emitter.EmitDup(Info)
        Emitter.EmitLoadI4Value(Info, 0)
        result = Info.RHSExpression.GenerateCode(Info.Clone(True, False, Info.RHSExpression.ExpressionType)) AndAlso result
        If Info.RHSExpression.ExpressionType.IsValueType Then
            Emitter.EmitBox(Info, Info.RHSExpression.ExpressionType)
        End If
        Emitter.EmitStoreElement(Info, Info.Compiler.TypeCache.System_Object, Info.Compiler.TypeCache.System_Object_Array)

        '5 - ArgumentNames
        Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_String_Array))

        '6 - TypeArguments
        If LateBoundAccess.TypeArguments IsNot Nothing Then
            Helper.NotImplemented("LateSet with type arguments.")
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_Type_Array))
        End If

        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateSet_Object_Type_String_Array_Array_Array)

        Return result
    End Function


    Public Shared Function EmitLateCall(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True
        Dim copyBacks As Boolean() = Nothing

        'We need to emit a call to LateCall

        '1 - the instance expression
        result = LateBoundAccess.InstanceExpression.GenerateCode(Info.Clone(True, False, LateBoundAccess.InstanceExpression.ExpressionType)) AndAlso result

        '2 - Type ??? - haven't found an example where this isn't nothing yet
        Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_Type))

        '3 - The member name
        Emitter.EmitLoadValue(Info, LateBoundAccess.Name)

        '4 - The arguments
        Dim argCount As Integer
        Dim args As ArgumentList = LateBoundAccess.Arguments
        If args IsNot Nothing Then argCount = args.Count
        Emitter.EmitLoadI4Value(Info, argCount)
        Emitter.EmitNewArr(Info, Info.Compiler.TypeCache.System_Object)
        For i As Integer = 0 To argCount - 1
            Dim arg As Argument = args.Arguments(i)
            Emitter.EmitDup(Info)
            Emitter.EmitLoadI4Value(Info, i)
            result = arg.GenerateCode(Info.Clone(True, False, arg.Expression.ExpressionType)) AndAlso result
            If arg.Expression.ExpressionType.IsValueType Then
                Emitter.EmitBox(Info, Info.RHSExpression.ExpressionType)
            End If
            Emitter.EmitStoreElement(Info, Info.Compiler.TypeCache.System_Object, Info.Compiler.TypeCache.System_Object_Array)
        Next
        
        '5 - ArgumentNames
        Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_String_Array))

        '6 - TypeArguments
        If LateBoundAccess.TypeArguments IsNot Nothing Then
            Helper.NotImplemented("LateCall with type arguments.")
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_Type_Array))
        End If

        '7 - CopyBack
        If copyBacks IsNot Nothing Then
            Helper.NotImplemented("LateGet with byref arguments.")
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Compiler.TypeCache.System_Boolean_Array))
        End If

        '8 - Ignore return
        Emitter.EmitLoadI4Value(Info, True)
        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateCall_Object_Type_String_Array_Array_Array_Array_Boolean)

        Emitter.EmitPop(Info, Info.Compiler.TypeCache.System_Object)

        Return result
    End Function
End Class
