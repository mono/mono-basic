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

'#Const DEBUGRESOLVE = True

'Imports System.Reflection

''''' <summary>
''''' TypeEnum should not inherit from this class.
''''' ClassDeclaration, TypeStructure, TypeInterface, TypeModule should.
''''' </summary>
''''' <remarks></remarks>
'MustInherit Public Class ContainerType
'    Inherits AttributeBaseType
'    Sub New(ByVal Parent As IBaseObject)
'        MyBase.new(Parent)
'    End Sub
'    '    'Can contain other containers:
'    '    Private m_lstEnums As New BaseTypes(Compiler, m_Index)
'    '    Private m_lstStructures As New BaseTypes(Compiler, m_Index)
'    '    Private m_lstInterfaces As New BaseTypes(Compiler, m_Index)
'    '    Private m_lstClasses As New BaseTypes(Compiler, m_Index)
'    '    'Other types:
'    '    Private m_lstDelegates As New BaseTypes(Compiler, m_Index)

'    '    'Members:
'    '    Private m_lstMethods As New Members(Compiler, m_Index)
'    '    Private m_lstConstants As New Members(Compiler, m_Index)
'    '    Private m_lstVariables As New Members(Compiler, m_Index)

'    '    'An interface can inherit many interfaces, but save these in m_lstImplements
'    '    Private m_Inherits As TypeName
'    '    Private m_lstImplements As New BaseObjects(Compiler)

'    '    Private m_canInherit As Boolean
'    '    Private m_canImplement As Boolean
'    '    Private m_endKeyword As KS
'    '    Private m_startKeyword As KS

'    '    Protected m_tokStart As Token

'    '    Private m_all As New ArrayList(New BaseObjects() {m_lstEnums, m_lstStructures, _
'    '    m_lstInterfaces, m_lstClasses, m_lstDelegates, m_lstMethods, m_lstConstants, _
'    '    m_lstVariables, m_lstImplements})

'    '    ''' <summary>
'    '    ''' Returns the classes this container implements (or inherits, in the case of an interface).
'    '    ''' </summary>
'    '    ''' <value></value>
'    '    ''' <remarks></remarks>
'    '    ReadOnly Property [Implements]() As BaseObjects
'    '        Get
'    '            Return m_lstImplements
'    '        End Get
'    '    End Property
'    '    Protected ReadOnly Property [Inherits]() As TypeName
'    '        Get
'    '            Return m_Inherits
'    '        End Get
'    '    End Property

'    '    Protected Sub setBaseClass(ByVal BaseType As Type)
'    '        Helper.Assert(TypeOf Me Is ClassDeclaration)
'    '        Helper.Assert(m_Inherits Is Nothing)
'    '        m_Inherits = New TypeName(Me)
'    '        m_Inherits.SetType(BaseType)
'    '    End Sub

'    '    ''' <summary>
'    '    ''' Determines whether it's members have been defined.
'    '    ''' </summary>
'    '    ''' <remarks></remarks>
'    '    Protected m_bDefinedMembers As Boolean
'    '    ''' <summary>
'    '    ''' Determines whether DefineMember has been called.
'    '    ''' </summary>
'    '    ''' <value></value>
'    '    ''' <remarks></remarks>
'    '    Public Property DefinedMembers() As Boolean
'    '        Get
'    '            Return m_bDefinedMembers
'    '        End Get
'    '        Set(ByVal Value As Boolean)
'    '            m_bDefinedMembers = Value
'    '        End Set
'    '    End Property
'    '    ''' <summary>
'    '    ''' Sets the type this type inherits from.
'    '    ''' </summary>
'    '    ''' <param name="tp"></param>
'    '    ''' <remarks></remarks>
'    '    Sub SetInherits(ByVal tp As Type)
'    '        If canInherit Then
'    '            m_Inherits = New TypeName(Me)
'    '            m_Inherits.SetType(tp)
'    '        End If
'    '    End Sub


'    '    ''' <summary>
'    '    ''' The base class for this Container (might be nothing).
'    '    ''' </summary>
'    '    ''' <value></value>
'    '    ''' <remarks></remarks>
'    '    Public ReadOnly Property BaseClass() As TypeName
'    '        Get
'    '            Return m_Inherits
'    '        End Get
'    '    End Property

'    '    MustOverride ReadOnly Property BaseType() As Type

'    '    Function GetSharedConstructor(Optional ByVal CreateIfInexistent As Boolean = False) As ConstructorMember
'    '        Helper.NotImplemented()
'    '        'For Each m As MethodMember In m_lstMethods
'    '        '    Dim c As ConstructorMember
'    '        '    c = TryCast(m, ConstructorMember)
'    '        '    If c IsNot Nothing Then
'    '        '        If c.IsShared Then
'    '        '            Helper.Assert(c.Parameters.Count = 0)
'    '        '            Return c
'    '        '        End If
'    '        '    End If
'    '        'Next

'    '        'If CreateIfInexistent Then
'    '        '    Dim newConstructor As ConstructorMember = ConstructorMember.CreateGenerated(Me, Nothing, MethodAttributes.Private Or MethodAttributes.Static)
'    '        '    AddMethodMember(newConstructor)
'    '        '    Return newConstructor
'    '        'End If

'    '        Return Nothing
'    '    End Function

'    '    Function GetConstructors() As ConstructorMember.MemberConstructorInfo()
'    '        Dim iCount As Integer
'    '        'Count first
'    '        For Each m As MethodMember In m_lstMethods
'    '            If TypeOf m Is ConstructorMember Then iCount += 1
'    '        Next
'    '        'Then create array
'    '        If iCount > 0 Then
'    '            Dim result(iCount - 1) As ConstructorMember.MemberConstructorInfo
'    '            Dim iConstrFound As Integer
'    '            For i As Integer = 0 To m_lstMethods.Count - 1
'    '                If TypeOf m_lstMethods(i) Is ConstructorMember Then
'    '                    result(iConstrFound) = CType(m_lstMethods(i), ConstructorMember).GetInfo
'    '                    iConstrFound += 1
'    '                End If
'    '            Next
'    '            Return result
'    '        Else
'    '            Return New ConstructorMember.MemberConstructorInfo() {}
'    '        End If
'    '    End Function
'    '    ''' <summary>
'    '    ''' Searches all the types contained herein for the specified defined Type.
'    '    ''' If nothing found, returns nothing.
'    '    ''' </summary>
'    '    ''' <param name="tp"></param>
'    '    ''' <returns></returns>
'    '    ''' <remarks></remarks>
'    '    Function FindBuildingType(ByVal tp As Type) As BaseType
'    '        Dim lst As ArrayList = m_Index.GetAllTypeBases

'    '        For Each no As NamedObject In lst
'    '            Dim tb As BaseType = TryCast(no, BaseType)
'    '            If tb IsNot Nothing AndAlso tb.TypeBuilder IsNot Nothing AndAlso tb.TypeBuilder.Equals(tp) Then
'    '                Return tb
'    '            ElseIf TypeOf tb Is ContainerType Then
'    '                FindBuildingType = CType(tb, ContainerType).FindBuildingType(tp)
'    '                If Not FindBuildingType Is Nothing Then
'    '                    Return FindBuildingType
'    '                End If
'    '            End If
'    '        Next
'    '        Return Nothing 'If nothing found, returns nothing.
'    '    End Function
'    '    Sub New(ByVal Parent As IBaseObject)
'    '        MyBase.New(Parent)
'    '        m_canInherit = canInherit
'    '        m_canImplement = canImplement
'    '        m_endKeyword = endKeyword
'    '        m_startKeyword = startKeyword
'    '    End Sub
'    '    Overrides Function DefineType() As Boolean
'    '        Dim ret As Boolean = True
'    '        Dim typeAttr As System.Reflection.TypeAttributes

'    '        typeAttr = Me.TypeAttributes()
'    '        If TypeOf Me Is DelegateType Then
'    '            If IsNested Then
'    '                m_TypeBuilder = Parent.TypeBuilder.DefineNestedType(Name, typeAttr Or Reflection.TypeAttributes.Sealed)
'    '            Else
'    '                m_TypeBuilder = Compiler.ModuleBuilder.DefineType(FullName, typeAttr Or Reflection.TypeAttributes.Sealed)
'    '            End If
'    '        ElseIf TypeOf Me Is StructureDeclaration Then
'    '            If IsNested Then
'    '                m_TypeBuilder = Parent.TypeBuilder.DefineNestedType(Name, typeAttr, GetType(ValueType))
'    '            Else
'    '                m_TypeBuilder = Compiler.ModuleBuilder.DefineType(FullName, typeAttr, GetType(ValueType))
'    '            End If
'    '        ElseIf TypeOf Me Is ClassDeclaration Then
'    '            If IsNested Then
'    '                m_TypeBuilder = Parent.TypeBuilder.DefineNestedType(Name, typeAttr)
'    '            Else
'    '                m_TypeBuilder = Compiler.ModuleBuilder.DefineType(FullName, typeAttr)
'    '            End If
'    '        ElseIf TypeOf Me Is ModuleType Then
'    '            If IsNested Then
'    '                Compiler.Report.ShowMessage(Messages.VBNC30617, Me.m_tokStart.Span)
'    '            Else
'    '                m_TypeBuilder = Compiler.ModuleBuilder.DefineType(FullName, typeAttr)
'    '            End If
'    '        ElseIf TypeOf Me Is InterfaceDeclaration Then
'    '            If IsNested Then
'    '                m_TypeBuilder = Parent.TypeBuilder.DefineNestedType(Name, typeAttr)
'    '            Else
'    '                m_TypeBuilder = Compiler.ModuleBuilder.DefineType(FullName, typeAttr)
'    '            End If
'    '        Else
'    '            Throw New InternalException(Me)
'    '        End If

'    '        ret = m_lstEnums.DefineTypes AndAlso ret
'    '        ret = m_lstStructures.DefineTypes AndAlso ret
'    '        ret = m_lstInterfaces.DefineTypes AndAlso ret
'    '        ret = m_lstClasses.DefineTypes AndAlso ret
'    '        ret = m_lstDelegates.DefineTypes AndAlso ret

'    '        Return ret
'    '    End Function
'    '    Public Overrides Function DefineInheritsAndImplements() As Boolean
'    '        Dim result As Boolean = True

'    '        'ClassType defines the inherits clause.

'    '        'Implements clauses (or Inherits in the case of Interface, saved in m_lstImplements):
'    '        If TypeOf Me Is StructureDeclaration OrElse TypeOf Me Is ClassDeclaration OrElse TypeOf Me Is InterfaceDeclaration Then
'    '            Dim typeInterface As Type
'    '            For Each tn As TypeName In m_lstImplements
'    '                result = tn.Resolve(Me, False) AndAlso result
'    '                typeInterface = tn.ResolvedType
'    '                If Not typeInterface Is Nothing Then
'    '                    m_TypeBuilder.AddInterfaceImplementation(typeInterface)
'    '                Else
'    '                    Compiler.Report.ShowMessage(Messages.VBNC30002, tn.Name)
'    '                End If
'    '            Next
'    '        End If

'    '        'result = m_lstEnums.DefineInheritsAndImplements AndAlso result
'    '        result = m_lstStructures.DefineInheritsAndImplements AndAlso result
'    '        result = m_lstInterfaces.DefineInheritsAndImplements AndAlso result
'    '        result = m_lstClasses.DefineInheritsAndImplements AndAlso result
'    '        result = m_lstDelegates.DefineInheritsAndImplements AndAlso result

'    '        Return result
'    '    End Function

'    '    Overrides Function DefineMembers() As Boolean
'    '        Dim ret As Boolean = True

'    '        If m_bDefinedMembers = True Then
'    '            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "DefineMembers called on a defined member!")
'    '            Return True
'    '        End If

'    '        ret = m_lstVariables.DefineMembers AndAlso ret
'    '        ret = m_lstMethods.DefineMembers AndAlso ret
'    '        ret = m_lstConstants.DefineMembers AndAlso ret

'    '        'All the nested types should have their members defined as well
'    '        ret = m_lstEnums.DefineMembers AndAlso ret
'    '        ret = m_lstStructures.DefineMembers AndAlso ret
'    '        ret = m_lstInterfaces.DefineMembers AndAlso ret
'    '        ret = m_lstClasses.DefineMembers AndAlso ret
'    '        ret = m_lstDelegates.DefineMembers AndAlso ret

'    '        m_bDefinedMembers = True

'    '        Return ret
'    '    End Function

'    '    Public Overrides Function Resolve() As Boolean
'    '        Dim result As Boolean = True

'    '        Helper.NotImplemented()
'    '        '        result = Attributes.Resolve AndAlso result

'    '        '        result = m_lstVariables.Resolve AndAlso result
'    '        '        result = m_lstMethods.Resolve AndAlso result
'    '        '        result = m_lstConstants.Resolve AndAlso result

'    '        '        result = m_lstEnums.Resolve AndAlso result
'    '        '        result = m_lstStructures.Resolve AndAlso result
'    '        '        result = m_lstInterfaces.Resolve AndAlso result
'    '        '        result = m_lstClasses.Resolve AndAlso result
'    '        '        result = m_lstDelegates.Resolve AndAlso result
'    '        '#If DEBUGRESOLVE Then
'    '        '        If result = False Then Helper.Stop()
'    '        '#End If
'    '        Return result
'    '    End Function

'    '    Overrides Function GenerateCode(Info as EmitInfo) As Boolean
'    '        Dim result As Boolean = True

'    '        Helper.NotImplemented()
'    '        'Attributes.Emit(Me)

'    '        ''Can contain other containers:
'    '        ''        Private m_lstEnums As New typebases(Of TypeEnum)(m_lstIndex)
'    '        'result = m_lstStructures.GenerateCode() AndAlso result ' As New typebases(Of TypeStructure)(m_lstIndex)
'    '        ''        Private m_lstInterfaces As New typebases(Of TypeInterface)(m_lstIndex)
'    '        'result = m_lstClasses.GenerateCode() AndAlso result ' As New typebases(Of ClassDeclaration)(m_lstIndex)
'    '        ''Other types:
'    '        ''Private m_lstDelegates As New typebases(Of Compiler.TypeCache.Delegate)(m_lstIndex)

'    '        ''Members:
'    '        'If Not TypeOf Me Is InterfaceDeclaration Then
'    '        '    result = m_lstDelegates.GenerateCode() AndAlso result
'    '        '    result = m_lstMethods.GenerateCode AndAlso result ' As New typebases(Of MemberMethod)(m_lstIndex)
'    '        'End If
'    '        'Private m_lstConstants As New typebases(Of MemberConstant)(m_lstIndex)
'    '        'Private m_lstVariables As New TypeBases(Of MemberVariable)(m_lstIndex
'    '        Return result
'    '    End Function
'    '    Public Overridable Function ParseDeclaration() As Boolean
'    '        Dim ret As Boolean = True

'    '        ret = tm.AcceptIfNotError(startKeyword, Messages.VBNC90019, startKeyword.ToString) AndAlso ret
'    '        m_tokStart = tm.PeekToken(-1)
'    '        ret = setName(True) AndAlso ret
'    '        ret = tm.FindNewLineAndShowError() AndAlso ret


'    '        If TypeOf Me Is InterfaceDeclaration Then
'    '            While tm.Accept(KS.Inherits) 'Save inherits of interface to implements
'    '                Dim newInherit As TypeName
'    '                Do
'    '                    newInherit = New TypeName(Me)
'    '                    ret = newInherit.Parse AndAlso ret
'    '                    m_lstImplements.Add(newInherit)
'    '                Loop While tm.Accept(KS.Comma)
'    '                ret = tm.FindNewLineAndShowError() AndAlso ret
'    '            End While
'    '        ElseIf canInherit Then
'    '            If tm.Accept(KS.Inherits) Then
'    '                Dim newInherit As New TypeName(Me)
'    '                ret = newInherit.Parse() AndAlso ret
'    '                m_Inherits = newInherit
'    '                ret = tm.FindNewLineAndShowError() AndAlso ret
'    '            End If
'    '        End If

'    '        If canImplement Then
'    '            While tm.Accept(KS.Implements)
'    '                Dim newImplements As TypeName
'    '                Do
'    '                    newImplements = New TypeName(Me)
'    '                    ret = newImplements.Parse() AndAlso ret
'    '                    m_lstImplements.Add(newImplements)
'    '                Loop While tm.Accept(KS.Comma)
'    '                ret = tm.FindNewLineAndShowError() AndAlso ret
'    '            End While
'    '        End If

'    '        Return ret
'    '    End Function
'    '    Public Overrides Function ParseMain() As Boolean
'    '        Dim ret As Boolean = True

'    '        ret = ParseDeclaration() AndAlso ret
'    '        ret = ParseBody() AndAlso ret
'    '        ret = ParseEnd() AndAlso ret

'    '        Return ret
'    '    End Function
'    '    Public Overrides Function PostParse() As Boolean
'    '        Dim result As Boolean = True

'    '        result = m_lstEnums.PostParse AndAlso result
'    '        result = m_lstStructures.PostParse AndAlso result
'    '        result = m_lstInterfaces.PostParse AndAlso result
'    '        result = m_lstClasses.PostParse AndAlso result
'    '        result = m_lstDelegates.PostParse AndAlso result
'    '        result = m_lstMethods.PostParse AndAlso result
'    '        result = m_lstConstants.PostParse AndAlso result
'    '        result = m_lstVariables.PostParse AndAlso result

'    '        Return result
'    '    End Function
'    '    Overridable Function getNewEnum() As EnumDeclaration
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewStructure() As StructureDeclaration
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewInterface() As InterfaceDeclaration
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewClass() As ClassDeclaration
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewDelegate() As DelegateType
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewSub() As SubMember
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewFunction() As FunctionMember
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewProperty() As PropertyMember
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    Overridable Function getNewOperator() As OperatorMember
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function
'    '    ''' <summary>
'    '    ''' Returns a new MemberConstructor, if the container can contain a constructor (for classes , modules and structures)
'    '    ''' otherwise returns a nothing
'    '    ''' </summary>
'    '    ''' <returns></returns>
'    '    ''' <remarks></remarks>
'    '    Overridable Function getNewConstructor() As ConstructorMember
'    '        Debug.Fail("Method should be overridden!") : Return Nothing 'HACK: Should be overridden!
'    '    End Function

'    '    Sub Add(ByVal tpEnum As EnumDeclaration)
'    '        Helper.NotImplemented() 'm_lstEnums.Add(tpEnum)
'    '    End Sub
'    '    Sub Add(ByVal tpStructure As StructureDeclaration)
'    '        Helper.NotImplemented() 'm_lstStructures.Add(tpStructure)
'    '    End Sub
'    '    Sub Add(ByVal tpInterface As InterfaceDeclaration)
'    '        Helper.NotImplemented() 'm_lstInterfaces.Add(tpInterface)
'    '    End Sub
'    '    Sub Add(ByVal tpClass As ClassDeclaration)
'    '        Helper.NotImplemented() 'm_lstClasses.Add(tpClass)
'    '    End Sub
'    '    Sub Add(ByVal tpDelegate As DelegateType)
'    '        Helper.NotImplemented() 'm_lstDelegates.Add(tpDelegate)
'    '    End Sub
'    '    Sub AddVariableMembers(ByVal ParamArray members As VariableDeclarator())
'    '        m_lstVariables.AddRange(members)
'    '    End Sub
'    '    Sub AddConstantMembers(ByVal ParamArray members As ConstantDeclaration())
'    '        m_lstConstants.AddRange(members)
'    '    End Sub
'    '    Sub AddMethodMember(ByVal mbmProcedure As MethodMember)
'    '        m_lstMethods.Add(mbmProcedure)
'    '    End Sub
'    '    Overridable Function ParseBody() As Boolean
'    '        Dim result As Boolean = True
'    '        Dim iJump As Integer = 0
'    '        Dim iJumpNext As Integer
'    '        Dim endKword As KS = endKeyword
'    '        Dim TmpToken As Token
'    '        Dim lstAttribs As New AttributeMembers(Compiler)

'    '        Helper.NotImplemented()

'    '        'Do Until tm.CurrentToken() = endKword
'    '        '    'Select Case PeekToken(iJump).Type
'    '        '    TmpToken = tm.PeekToken(iJump)
'    '        '    If TmpToken.IsKeyword Then
'    '        '        Select Case TmpToken.AsKeyword.Keyword
'    '        '            Case KS.Enum
'    '        '                Dim newEnum As EnumDeclaration = getNewEnum()
'    '        '                result = newEnum.Parse() AndAlso result
'    '        '                Add(newEnum)
'    '        '            Case KS.Structure
'    '        '                Dim newStructure As StructureDeclaration = getNewStructure()
'    '        '                result = newStructure.Parse() AndAlso result
'    '        '                Add(newStructure)
'    '        '            Case KS.Interface
'    '        '                Dim newInterface As InterfaceDeclaration = getNewInterface()
'    '        '                result = newInterface.Parse() AndAlso result
'    '        '                Add(newInterface)
'    '        '            Case KS.Class
'    '        '                Dim newClass As ClassDeclaration = getNewClass()
'    '        '                result = newClass.Parse() AndAlso result
'    '        '                Add(newClass)
'    '        '            Case KS.Delegate
'    '        '                Dim newDelegate As DelegateType = getNewDelegate()
'    '        '                result = newDelegate.Parse() AndAlso result
'    '        '                Add(newDelegate)
'    '        '            Case KS.Sub
'    '        '                'Might be a constructor
'    '        '                If tm.PeekToken(iJump + 1).Equals(KS.[New]) Then
'    '        '                    Dim newSub As ConstructorMember
'    '        '                    newSub = getNewConstructor()
'    '        '                    If Not newSub Is Nothing Then
'    '        '                        result = newSub.Parse() AndAlso result
'    '        '                        AddMethodMember(newSub)
'    '        '                    Else
'    '        '                        tm.GotoNewline(True, False)
'    '        '                    End If
'    '        '                Else
'    '        '                    Dim newSub As SubMember
'    '        '                    newSub = getNewSub()
'    '        '                    result = newSub.Parse() AndAlso result
'    '        '                    AddMethodMember(newSub)
'    '        '                End If
'    '        '            Case KS.Function
'    '        '                Dim newFunction As FunctionMember = getNewFunction()
'    '        '                result = newFunction.Parse AndAlso result
'    '        '                AddMethodMember(newFunction)
'    '        '            Case KS.Property
'    '        '                Dim newProperty As PropertyMember = getNewProperty()
'    '        '                result = newProperty.Parse AndAlso result
'    '        '                AddMethodMember(newProperty)
'    '        '            Case KS.Operator
'    '        '                Dim newOperator As OperatorMember = getNewOperator()
'    '        '                result = newOperator.Parse AndAlso result
'    '        '                AddMethodMember(newOperator)
'    '        '            Case KS.Event, KS.CustomEvent
'    '        '                Helper.NotImplemented() '    Dim newEvent As EventMemberDeclaration = New EventMemberDeclaration(Me)
'    '        '                Helper.NotImplemented() 'result = newEvent.Parse AndAlso result
'    '        '                Helper.NotImplemented() 'AddMethodMember(newEvent)
'    '        '            Case KS.CustomEvent
'    '        '                Helper.NotImplemented() 'Dim newEvent As CustomEventMember = New CustomEventMember(Me)
'    '        '                Helper.NotImplemented() 'result = newEvent.Parse AndAlso result
'    '        '                Helper.NotImplemented() 'AddMethodMember(newEvent)
'    '        '            Case KS.Const
'    '        '                Dim newConst As ConstantMemberDeclaration = New ConstantMemberDeclaration(Me)
'    '        '                result = newConst.Parse AndAlso result
'    '        '                AddConstantMembers(newConst.ConstantDeclarations.Declarations)
'    '        '            Case Else
'    '        '                If AllContainerModifiers.Contains(TmpToken.AsKeyword.Keyword) Then
'    '        '                    iJumpNext = iJump + 1
'    '        '                Else
'    '        '                    Compiler.Report.ShowMessage(Messages.VBNC90007, TmpToken.Location, TmpToken.ToString)
'    '        '                    tm.GotoNewline(True, False)
'    '        '                    result = False
'    '        '                    iJumpNext = 0
'    '        '                End If
'    '        '        End Select
'    '        '    ElseIf TmpToken.IsIdentifier Then 'Case TokenType.Identifier
'    '        '        If iJump > 0 Then
'    '        '            Dim newVariable As VariableMemberDeclaration = New VariableMemberDeclaration(Me)
'    '        '            result = newVariable.Parse AndAlso result
'    '        '            AddVariableMembers(newVariable.VariableDeclarators.Declarations)
'    '        '        Else
'    '        '            Compiler.Report.ShowMessage(Messages.VBNC90007, TmpToken.Location, TmpToken.ToString)
'    '        '            tm.GotoNewline(True, False)
'    '        '        End If
'    '        '    ElseIf TmpToken.isEndOfCode Then
'    '        '        Compiler.Report.ShowMessage(Messages.VBNC90008, TmpToken.Location)
'    '        '        Return False
'    '        '    ElseIf TmpToken.isEndOfFile Then
'    '        '        Compiler.Report.ShowMessage(Messages.VBNC90001, TmpToken.Location)
'    '        '        Return False
'    '        '    ElseIf TmpToken.isEndOfLine Then
'    '        '        If lstAttribs.Count > 0 Then
'    '        '            Compiler.Report.ShowMessage(Messages.VBNC32035, lstAttribs.ToArray(0).Location)
'    '        '            lstAttribs.Clear()
'    '        '        End If
'    '        '        tm.GotoNewline(True, False)
'    '        '    ElseIf TmpToken = KS.LT Then
'    '        '        result = lstAttribs.Parse AndAlso result
'    '        '    Else
'    '        '        Compiler.Report.ShowMessage(Messages.VBNC90007, TmpToken.Location, TmpToken.ToString)
'    '        '        tm.GotoNewline(True, False)
'    '        '    End If
'    '        '    If iJump = iJumpNext Then
'    '        '        'A type / member was found
'    '        '        iJumpNext = 0
'    '        '    End If
'    '        '    iJump = iJumpNext

'    '        'Loop

'    '        Return result
'    '    End Function

'    '    ''' <summary>
'    '    ''' Finds a method with the specified name and exact parameters.
'    '    ''' </summary>
'    '    ''' <param name="Name"></param>
'    '    ''' <param name="Parameters"></param>
'    '    ''' <returns></returns>
'    '    ''' <remarks></remarks>
'    '    Function GetMethod(ByVal Name As String, ByVal Parameters() As Type) As MethodMember
'    '        Dim result As MethodMember = Nothing
'    '        Dim tmp As ArrayList

'    '        tmp = Me.FindInMe(Name, False)
'    '        If tmp.Count = 0 Then
'    '            result = Nothing
'    '        Else
'    '            For Each obj As BaseObject In tmp
'    '                Dim method As MethodMember = TryCast(obj, MethodMember)
'    '                If method IsNot Nothing Then
'    '                    If Helper.CompareTypes(method.ParameterTypes, Parameters) Then
'    '                        Return method
'    '                    End If
'    '                End If
'    '            Next
'    '        End If

'    '        Return result
'    '    End Function

'    Function FindMethod(ByVal Method As MethodBase) As MethodMember
'        Helper.NotImplemented()
'        '        Helper.Assert(Method.IsConstructor = False)
'        '        For Each obj As MethodMember In m_lstMethods
'        '            If obj.MethodBuilder Is Method Then
'        '                Return obj
'        '            End If
'        '        Next
'        Return Nothing
'    End Function

'    '    Function FindMethodOrConstructor(ByVal Method As MethodBase) As MethodMember
'    '        If Method.IsConstructor Then
'    '            If TypeOf Method Is ConstructorMember.MemberConstructorInfo Then
'    '                Dim constructor As ConstructorMember.MemberConstructorInfo = DirectCast(Method, ConstructorMember.MemberConstructorInfo)
'    '                For Each obj As MethodMember In m_lstMethods
'    '                    If TypeOf obj Is ConstructorMember Then
'    '                        If constructor.Equals(obj.ConstructorBuilder) Then
'    '                            Return obj
'    '                        End If
'    '                    End If
'    '                Next
'    '            Else
'    '                Dim constructor As ConstructorInfo = DirectCast(Method, ConstructorInfo)
'    '                Helper.Stop() 'Check!
'    '                For Each obj As MethodMember In m_lstMethods
'    '                    If TypeOf obj Is ConstructorMember Then
'    '                        If constructor.Equals(obj.ConstructorBuilder) Then
'    '                            Return obj
'    '                        End If
'    '                    End If
'    '                Next
'    '            End If
'    '        Else
'    '            Return FindMethod(Method)
'    '        End If
'    '        Return Nothing
'    '    End Function
'    '    Overridable Function ParseEnd() As Boolean
'    '        Dim ret As Boolean = True

'    '        ret = tm.AcceptIfNotError(endKeyword, Messages.VBNC90019, endKeyword.ToString) AndAlso ret
'    '        ret = tm.AcceptNewLine(True, False) AndAlso ret
'    '        Return ret
'    '    End Function
'    '#If DEBUG Then
'    '    Public Sub Dump(ByVal Xml As System.Xml.XmlWriter)
'    '        Dim type As String
'    '        type = Me.GetType.ToString.Replace("vbnc.", "").Replace("Type", "")

'    '        Xml.WriteStartElement(type)
'    '        Xml.WriteAttributeString("Name", Name)
'    '        Xml.WriteAttributeString("BaseType", m_Inherits.ToString)
'    '        Me.DumpModifiers(Xml)
'    '        Xml.WriteAttributeString("CompilerGenerated", Me.CompilerGenerated.ToString)
'    '        DumpAttributes(Xml)
'    '        For Each obj As BaseObjects In Me.m_all
'    '            obj.Dump(Xml)
'    '        Next
'    '        Xml.WriteEndElement()
'    '    End Sub
'    '    Overrides Sub Dump()
'    '        DumpAttributes()
'    '        DumpModifiers()
'    '        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, startKeyword.ToString & " " & Name)
'    '        Debug.Indent()
'    '        If canInherit Then
'    '            If TypeOf Me Is InterfaceDeclaration Then
'    '                For Each im As TypeName In m_lstImplements
'    '                    Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Inherits " & im.Name)
'    '                Next
'    '            Else
'    '                If m_Inherits IsNot Nothing Then
'    '                    Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Inherits " & m_Inherits.Name)
'    '                End If
'    '            End If
'    '        End If
'    '        If canImplement Then
'    '            For Each im As TypeName In m_lstImplements
'    '                Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Implements " & im.Name)
'    '            Next
'    '        End If
'    '        m_lstVariables.Dump()

'    '        m_lstEnums.Dump()
'    '        m_lstStructures.Dump()
'    '        m_lstInterfaces.Dump()
'    '        m_lstClasses.Dump()
'    '        'Other types:
'    '        m_lstDelegates.Dump()

'    '        m_lstConstants.Dump()
'    '        m_lstMethods.Dump()


'    '        'Members:
'    '        Debug.Unindent()
'    '        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, endKeyword.ToString.Replace("_", " "))
'    '    End Sub
'    '#End If
'    '    MustOverride ReadOnly Property canInherit() As Boolean
'    '    MustOverride ReadOnly Property canImplement() As Boolean
'    '    MustOverride ReadOnly Property endKeyword() As KS
'    '    MustOverride ReadOnly Property startKeyword() As KS

'    Public Overrides Function ParseMain() As Boolean

'    End Function

'    Public Overrides Sub SetupModifiers()

'    End Sub
'End Class