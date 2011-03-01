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

Imports OpCodes = Mono.Cecil.Cil.OpCodes

Public Class RegularEventHandlerDeclaration
    Inherits EventHandlerDeclaration

    Sub New(ByVal Parent As EventDeclaration, ByVal Modifiers As Modifiers, ByVal HandlerType As KS, ByVal EventName As Identifier)
        MyBase.new(Parent)

        Dim Code As ImplicitCodeBlock
        If (Parent.Modifiers.Is(ModifierMasks.MustOverride) OrElse DeclaringType.IsInterface) Then
            Code = Nothing
        Else
            Select Case HandlerType
                Case KS.AddHandler
                    Code = New ImplicitCodeBlock(Me, New ImplicitCodeBlock.CodeGenerator(AddressOf CreateAddHandlerCode))
                Case KS.RemoveHandler
                    Code = New ImplicitCodeBlock(Me, New ImplicitCodeBlock.CodeGenerator(AddressOf CreateRemoveHandlerCode))
                Case KS.RaiseEvent
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    Code = Nothing
                Case Else
                    Throw New InternalException(Me)
            End Select
        End If

        MyBase.Init(Modifiers, HandlerType, EventName, New ParameterList(Me), Code)

    End Sub

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        If DeclaringType.IsValueType Then
            Me.MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL Or Mono.Cecil.MethodImplAttributes.Managed
        ElseIf DeclaringType.IsInterface Then
            Me.MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL Or Mono.Cecil.MethodImplAttributes.Managed
        Else
            Me.MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL Or Mono.Cecil.MethodImplAttributes.Managed Or Mono.Cecil.MethodImplAttributes.Synchronized
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Helper.Assert(EventParent.EventType IsNot Nothing)
        If Signature.Parameters.Count = 0 Then
            Dim param As New Parameter(Signature.Parameters, "obj", EventParent.EventType)
            Signature.Parameters.Add(param)
            result = param.CreateDefinition AndAlso result
        End If

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Private Function CreateRemoveHandlerCode(ByVal Info As EmitInfo) As Boolean
        If Me.IsShared = False Then
            With Info.ILGen
                .Emit(OpCodes.Ldarg_0)
                .Emit(OpCodes.Ldarg_0)
                .Emit(OpCodes.Ldfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ldarg_1)
                .EmitCall(OpCodes.Call, Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Delegate__Remove), Nothing)
                .Emit(OpCodes.Castclass, Helper.GetTypeOrTypeBuilder(Compiler, [EventParent].EventType))
                .Emit(OpCodes.Stfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ret)
            End With
        Else
            With Info.ILGen
                .Emit(OpCodes.Ldsfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ldarg_0)
                .EmitCall(OpCodes.Call, Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Delegate__Remove), Nothing)
                .Emit(OpCodes.Castclass, Helper.GetTypeOrTypeBuilder(Compiler, [EventParent].EventType))
                .Emit(OpCodes.Stsfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ret)
            End With
        End If
        Return True
    End Function

    Private Function CreateAddHandlerCode(ByVal Info As EmitInfo) As Boolean
        Helper.Assert([EventParent].EventField IsNot Nothing)
        If Me.IsShared = False Then
            With Info.ILGen
                .Emit(OpCodes.Ldarg_0)
                .Emit(OpCodes.Ldarg_0)
                .Emit(OpCodes.Ldfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ldarg_1)
                .EmitCall(OpCodes.Call, Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Delegate__Combine), Nothing)
                .Emit(OpCodes.Castclass, Helper.GetTypeOrTypeBuilder(Compiler, [EventParent].EventType))
                .Emit(OpCodes.Stfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ret)
            End With
        Else
            With Info.ILGen
                .Emit(OpCodes.Ldsfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ldarg_0)
                .EmitCall(OpCodes.Call, Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Delegate__Combine), Nothing)
                .Emit(OpCodes.Castclass, Helper.GetTypeOrTypeBuilder(Compiler, [EventParent].EventType))
                .Emit(OpCodes.Stsfld, Helper.GetFieldOrFieldBuilder(Compiler, [EventParent].EventField))
                .Emit(OpCodes.Ret)
            End With
        End If
        Return True
    End Function

    Shadows ReadOnly Property EventParent() As RegularEventDeclaration
        Get
            Return DirectCast(MyBase.EventParent, RegularEventDeclaration)
        End Get
    End Property
End Class
