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
''' Every variable has an associated type, namely the declared type of the variable.
''' </summary>
''' <remarks></remarks>
Public Class VariableClassification
    Inherits ExpressionClassification

    Private m_ParameterInfo As Mono.Cecil.ParameterDefinition
    Private m_FieldInfo As Mono.Cecil.FieldReference
    Private m_LocalBuilder As Mono.Cecil.Cil.VariableDefinition

    Private m_InstanceExpression As Expression
    Private m_Parameter As Parameter
    Private m_Variable As VariableDeclaration
    Private m_LocalVariable As LocalVariableDeclaration
    Private m_TypeVariable As TypeVariableDeclaration
    Private m_Method As IMethod
    Private m_Expression As Expression

    Private m_ExpressionType As Mono.Cecil.TypeReference

    Private m_ArrayVariable As Expression
    Private m_Arguments As ArgumentList

    ReadOnly Property LocalVariable As LocalVariableDeclaration
        Get
            Return m_LocalVariable
        End Get
    End Property

    ReadOnly Property Method() As IMethod
        Get
            Return m_Method
        End Get
    End Property

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    ReadOnly Property ArrayVariable() As Expression
        Get
            Return m_ArrayVariable
        End Get
    End Property

    ReadOnly Property Arguments() As ArgumentList
        Get
            Return m_Arguments
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef value As Object, ByVal ShowError As Boolean) As Boolean
        If Me.FieldInfo IsNot Nothing Then
            Dim attrib As Object
            Dim dec As Decimal, dt As Date
            Dim constant As ConstantDeclaration
            Dim enumc As EnumMemberDeclaration

            attrib = FieldDefinition.Annotations(Compiler)

            If attrib IsNot Nothing Then
                enumc = TryCast(attrib, EnumMemberDeclaration)
                If enumc IsNot Nothing Then Return enumc.GetConstantValue(value)

                constant = TryCast(attrib, ConstantDeclaration)
                If constant IsNot Nothing Then Return constant.GetConstant(value, ShowError)
            End If

            If Me.FieldDefinition.IsLiteral Then
                value = Me.FieldDefinition.Constant
                Return True
            ElseIf Me.FieldDefinition.IsInitOnly Then
                If ConstantDeclaration.GetDecimalConstant(Compiler, FieldDefinition, dec) Then
                    value = dec
                    Return True
                ElseIf ConstantDeclaration.GetDateConstant(Compiler, FieldDefinition, dt) Then
                    value = dt
                    Return True
                Else
                    If ShowError Then Parent.Show30059()
                    Return False
                End If
            Else
                If ShowError Then Parent.Show30059()
                Return False
            End If
        ElseIf m_LocalVariable IsNot Nothing Then
            If m_LocalVariable.IsConst Then
                If m_LocalVariable.VariableInitializer.IsRegularInitializer Then
                    Return m_LocalVariable.VariableInitializer.AsRegularInitializer.GetConstant(value, ShowError)
                End If
                If ShowError Then Parent.Show30059()
                Return False
            End If
        Else
            If ShowError Then Parent.Show30059()
            Return False
        End If
    End Function

    ReadOnly Property ParameterInfo() As Mono.Cecil.ParameterDefinition
        Get
            Return m_ParameterInfo
        End Get
    End Property

    ReadOnly Property LocalBuilder() As Mono.Cecil.Cil.VariableDefinition
        Get
            If m_LocalVariable IsNot Nothing Then
                Return m_LocalVariable.LocalBuilder
            Else
                Return Nothing
            End If
        End Get
    End Property

    ReadOnly Property FieldDefinition() As Mono.Cecil.FieldDefinition
        Get
            If m_TypeVariable IsNot Nothing AndAlso m_TypeVariable.FieldBuilder IsNot Nothing Then
                Return m_TypeVariable.FieldBuilder
            ElseIf m_LocalVariable IsNot Nothing AndAlso m_LocalVariable.FieldBuilder IsNot Nothing Then
                Return m_LocalVariable.FieldBuilder
            Else
                Return CecilHelper.FindDefinition(m_FieldInfo)
            End If
        End Get
    End Property

    ReadOnly Property FieldInfo() As Mono.Cecil.FieldReference
        Get
            If m_TypeVariable IsNot Nothing AndAlso m_TypeVariable.FieldBuilder IsNot Nothing Then
                Return m_TypeVariable.FieldBuilder
            ElseIf m_LocalVariable IsNot Nothing AndAlso m_LocalVariable.FieldBuilder IsNot Nothing Then
                Return m_LocalVariable.FieldBuilder
            Else
                Return m_FieldInfo
            End If
        End Get
    End Property


    ''' <summary>
    ''' Loads the value of the variable.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GenerateCodeAsValue(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Info.DesiredType IsNot Nothing)

        If m_InstanceExpression IsNot Nothing Then
            result = m_InstanceExpression.GenerateCode(Info) AndAlso result
        End If

        If FieldInfo IsNot Nothing Then
            If Info.IsRHS Then
                Emitter.EmitLoadVariable(Info, FieldInfo)
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
        ElseIf LocalBuilder IsNot Nothing Then
            If Info.IsRHS Then
                Emitter.EmitLoadVariable(Info, LocalBuilder)
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
        ElseIf ParameterInfo IsNot Nothing Then
            Helper.Assert(m_InstanceExpression Is Nothing)
            If Info.IsRHS Then
                Emitter.EmitLoadVariable(Info, ParameterInfo)
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
        ElseIf m_ArrayVariable IsNot Nothing Then
            result = Helper.EmitLoadArrayElement(Info, m_ArrayVariable, m_Arguments) AndAlso result
        ElseIf m_Expression IsNot Nothing Then
            result = m_Expression.GenerateCode(Info) AndAlso result
        Else
            Throw New InternalException(Me)
        End If

        If CecilHelper.IsByRef(Info.DesiredType) Then
            Dim elementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(Info.DesiredType)
            Dim local As Mono.Cecil.Cil.VariableDefinition
            local = Emitter.DeclareLocal(Info, elementType)

            Emitter.EmitStoreVariable(Info, local)
            Emitter.EmitLoadVariableLocation(Info, local)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Stores at the address of the variable.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_Expression IsNot Nothing Then
            Return m_Expression.GenerateCode(Info)
        End If

        Helper.Assert(Info.IsRHS AndAlso Info.RHSExpression Is Nothing OrElse Info.IsLHS AndAlso Info.RHSExpression IsNot Nothing)

        If m_InstanceExpression IsNot Nothing Then
            Dim exp As Mono.Cecil.TypeReference = m_InstanceExpression.ExpressionType
            If CecilHelper.IsValueType(exp) AndAlso CecilHelper.IsByRef(exp) = False Then
                exp = Compiler.TypeManager.MakeByRefType(Me.Parent, exp)
            End If
            result = m_InstanceExpression.GenerateCode(Info.Clone(Parent, True, False, exp)) AndAlso result
        End If

        If FieldInfo IsNot Nothing Then
            If Info.IsRHS Then
                If CecilHelper.IsByRef(Info.DesiredType) Then
                    Emitter.EmitLoadVariableLocation(Info, FieldInfo)
                Else
                    Emitter.EmitLoadVariable(Info, FieldInfo)
                End If
            Else
                Dim rInfo As EmitInfo = Info.Clone(Parent, True, False, FieldInfo.FieldType)

                Helper.Assert(Info.RHSExpression IsNot Nothing)
                Helper.Assert(Info.RHSExpression.Classification.IsValueClassification)
                result = Info.RHSExpression.Classification.GenerateCode(rInfo) AndAlso result

                Emitter.EmitConversion(Info.RHSExpression.ExpressionType, FieldInfo.FieldType, Info.Clone(Parent, Info.RHSExpression.ExpressionType))
                Emitter.EmitStoreField(Info, FieldInfo)
            End If
        ElseIf LocalBuilder IsNot Nothing Then
            result = VariableExpression.Emit(Info, LocalBuilder) AndAlso result
        ElseIf ParameterInfo IsNot Nothing Then
            Dim isByRef As Boolean
            Dim isByRefStructure As Boolean
            Dim paramType As Mono.Cecil.TypeReference
            Dim paramElementType As Mono.Cecil.TypeReference = Nothing

            paramType = ParameterInfo.ParameterType
            isByRef = CecilHelper.IsByRef(paramType)
            If isByRef Then
                paramElementType = CecilHelper.GetElementType(paramType)
                isByRefStructure = CecilHelper.IsValueType(paramElementType)
            End If

            Helper.Assert(m_InstanceExpression Is Nothing)

            If Info.IsRHS Then
                If isByRef Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
                Else
                    Emitter.EmitLoadVariable(Info, ParameterInfo)
                End If
            Else
                Dim rInfo As EmitInfo
                Dim rhs As Expression = Info.RHSExpression
                Dim paramConsumed As Boolean

                If isByRefStructure Then
                    Emitter.EmitLoadVariable(Info.Clone(Parent, paramType), ParameterInfo)
                    If TypeOf rhs Is GetRefExpression Then rhs = DirectCast(rhs, GetRefExpression).Expression
                    'paramConsumed = TypeOf rhs Is NewExpression
                    If paramConsumed Then
                        rInfo = Info.Clone(Parent, True, False, paramType)
                    Else
                        rInfo = Info.Clone(Parent, True, False, paramElementType)
                    End If
                ElseIf isByRef Then
                    Emitter.EmitLoadVariableLocation(Info, ParameterInfo)
                    rInfo = Info.Clone(Parent, True, False, paramElementType)
                Else
                    rInfo = Info.Clone(Parent, True, False, paramType)
                End If

                Helper.Assert(rhs IsNot Nothing, "RHSExpression Is Nothing!")
                Helper.Assert(rhs.Classification.IsValueClassification)
                result = rhs.Classification.GenerateCode(rInfo) AndAlso result

                If Not paramConsumed Then
                    If isByRef = False Then
                        Emitter.EmitConversion(rhs.ExpressionType, paramType, Info)
                    End If
                    If isByRefStructure Then
                        Emitter.EmitStoreIndirect(Info, paramType)
                    Else
                        Emitter.EmitStoreVariable(Info, ParameterInfo)
                    End If
                End If
            End If
        ElseIf Me.m_Variable IsNot Nothing Then
            If Info.IsRHS Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            Else
                Dim rInfo As EmitInfo = Info.Clone(Parent, True, False, m_Variable.VariableType)

                Helper.Assert(Info.RHSExpression IsNot Nothing)
                Helper.Assert(Info.RHSExpression.Classification.IsValueClassification)
                result = Info.RHSExpression.Classification.GenerateCode(rInfo) AndAlso result

                Emitter.EmitConversion(Info.RHSExpression.ExpressionType, m_Variable.VariableType, Info)

                If Helper.CompareType(m_Variable.VariableType, Compiler.TypeCache.System_Object) AndAlso Helper.CompareType(Info.RHSExpression.ExpressionType, Compiler.TypeCache.System_Object) Then
                    Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
                End If

                Emitter.EmitStoreVariable(Info, LocalBuilder)
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
        ElseIf m_ArrayVariable IsNot Nothing Then
            If Info.IsRHS Then
                result = Me.GenerateCodeAsValue(Info) AndAlso result
            Else
                result = Helper.EmitStoreArrayElement(Info, m_ArrayVariable, m_Arguments) AndAlso result

            End If
        ElseIf m_Method IsNot Nothing Then
            If Info.IsRHS Then
                Emitter.EmitLoadVariable(Info, m_Method.DefaultReturnVariable)
            Else
                Helper.Assert(Info.RHSExpression IsNot Nothing, "RHSExpression Is Nothing!")
                Helper.Assert(Info.RHSExpression.Classification.IsValueClassification)
                result = Info.RHSExpression.Classification.GenerateCode(Info.Clone(Parent, True, False, m_Method.DefaultReturnVariable.VariableType)) AndAlso result
                Emitter.EmitStoreVariable(Info, m_Method.DefaultReturnVariable)
            End If
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    ReadOnly Property InstanceExpression() As Expression
        Get
            Return m_InstanceExpression
        End Get
    End Property

    <Obsolete()> Overloads Function ReclassifyToValue() As ValueClassification
        Return New ValueClassification(Me)
    End Function

    ''' <summary>
    ''' A variable declaration which refers to the implicitly declared local variable
    ''' for methods with return values (functions and get properties)
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="method"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal method As IMethod)
        MyBase.New(Classifications.Variable, Parent)
        Helper.Assert(TypeOf method Is FunctionDeclaration OrElse TypeOf method Is PropertyGetDeclaration)
        m_Method = method
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal parameter As Parameter)
        MyBase.New(Classifications.Variable, Parent)
        m_Parameter = parameter
        m_ParameterInfo = parameter.CecilBuilder
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal variable As VariableDeclaration, Optional ByVal InstanceExpression As Expression = Nothing)
        MyBase.New(Classifications.Variable, Parent)
        m_Variable = variable
        m_LocalVariable = TryCast(m_Variable, LocalVariableDeclaration)
        m_TypeVariable = TryCast(m_Variable, TypeVariableDeclaration)
        m_InstanceExpression = InstanceExpression
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression, ByVal ExpressionType As Mono.Cecil.TypeReference)
        MyBase.new(Classifications.Variable, Parent)
        m_Expression = Expression
        m_ExpressionType = ExpressionType
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal variable As Mono.Cecil.FieldReference, ByVal InstanceExpression As Expression)
        MyBase.New(Classifications.Variable, Parent)
        m_FieldInfo = variable
        m_InstanceExpression = InstanceExpression
        Helper.Assert(m_InstanceExpression Is Nothing OrElse m_InstanceExpression.IsResolved)
        Helper.Assert((Helper.IsShared(variable) AndAlso m_InstanceExpression Is Nothing) OrElse (Helper.IsShared(variable) = False AndAlso m_InstanceExpression IsNot Nothing))
    End Sub

    ''' <summary>
    ''' Creates a variable classification for an array access.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Arguments"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal ArrayVariableExpression As Expression, ByVal Arguments As ArgumentList)
        MyBase.New(Classifications.Variable, Parent)
        m_ArrayVariable = ArrayVariableExpression
        m_Arguments = Arguments
        Helper.Assert(ArrayVariable IsNot Nothing)
        Helper.Assert(Arguments IsNot Nothing)
    End Sub

    ReadOnly Property Type() As Mono.Cecil.TypeReference
        Get
            Dim result As Mono.Cecil.TypeReference
            If m_ExpressionType IsNot Nothing Then
                result = m_ExpressionType
            ElseIf m_Method IsNot Nothing Then
                result = m_Method.Signature.ReturnType
            ElseIf m_Variable IsNot Nothing Then
                result = m_Variable.VariableType
            ElseIf m_FieldInfo IsNot Nothing Then
                If Helper.IsEnum(Compiler, m_FieldInfo.DeclaringType) Then
                    result = m_FieldInfo.DeclaringType
                Else
                    result = m_FieldInfo.FieldType
                End If
            ElseIf m_Parameter IsNot Nothing Then
                result = m_Parameter.ParameterType
            ElseIf m_ArrayVariable IsNot Nothing Then
                result = CecilHelper.GetElementType(m_ArrayVariable.ExpressionType)
            Else
                Throw New InternalException(Me)
            End If
            Helper.Assert(result IsNot Nothing)
            Return result
        End Get
    End Property
End Class

