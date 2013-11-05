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
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    This file has automatically been generated                            
'    Any change in this file will be lost!!                                
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Enum Messages
    ''' <summary>
    ''' VBNC = "file not found: {0}"
    ''' VB   = "file '%1!ls!' could not be found"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2001 = 2001

    ''' <summary>
    ''' VBNC = "response file '{0}' included multiple times"
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2003 = 2003

    ''' <summary>
    ''' VBNC = "Option '{0}' requires ':{1}'"
    ''' VB   = "option '%s' requires ':%s'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2006 = 2006

    ''' <summary>
    ''' VBNC = "the response file '{0}' could not be opened"
    ''' VB   = "unable to open response file '%s'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC2007 = 2007

    ''' <summary>
    ''' VBNC = "the option {0} was not recognized - ignored"
    ''' VB   = "unrecognized option '%1!ls!'; ignored"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC2009 = 2009

    ''' <summary>
    ''' VBNC = "No files to compile! Cannot do anything!"
    ''' VB   = "no input sources specified"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2011 = 2011

    ''' <summary>
    ''' VBNC = "can't open '{0}' for writing"
    ''' VB   = "can't open '%s' for writing"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2012 = 2012

    ''' <summary>
    ''' VBNC = "the value '{0}' is invalid for option '{1}'"
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2014 = 2014

    ''' <summary>
    ''' VBNC = "Code page '{0}' is invalid or not supported"
    ''' VB   = "code page '%s' is invalid or not installed"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2016 = 2016

    ''' <summary>
    ''' VBNC = "The library '{0}' could not be found."
    ''' VB   = "could not find library '%s'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2017 = 2017

    ''' <summary>
    ''' VBNC = "the option '{0}' cannot have the value '{1}'"
    ''' VB   = "the value '%2!ls!' is invalid for option '%1!ls!'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC2019 = 2019

    ''' <summary>
    ''' VBNC = "	'{0}': not most specific."
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC20001 = 20001

    ''' <summary>
    ''' VBNC = "There cannot be any statements in a namespace."
    ''' VB   = "Statement is not valid in a namespace."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30001 = 30001

    ''' <summary>
    ''' VBNC = "Type '{0}' is not defined."
    ''' VB   = "Type '|1' is not defined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30002 = 30002

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Next' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30003 = 30003

    ''' <summary>
    ''' VBNC = "Character constants must contain exactly one character."
    ''' VB   = "Character constant must contain exactly one character."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30004 = 30004

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to assembly '|1' containing the definition for event '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30005 = 30005

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to module '|1' containing the definition for event '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30006 = 30006

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to assembly '|1' containing the base class '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30007 = 30007

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to module '|1' containing the base class '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30008 = 30008

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to assembly '|1' containing the implemented interface '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30009 = 30009

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to module '|1' containing the implemented interface '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30010 = 30010

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Internal compiler error: code generator received malformed input."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30011 = 30011

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#If' block must end with a matching '#End If'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30012 = 30012

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#ElseIf', '#Else', or '#End If' must be preceded by a matching '#If'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30013 = 30013

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#ElseIf' must be preceded by a matching '#If' or '#ElseIf'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30014 = 30014

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Inheriting from 'System.|1' is not valid."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30015 = 30015

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Labels are not valid outside methods."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30016 = 30016

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Delegates cannot implement interface methods."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30018 = 30018

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Delegates cannot handle events."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30019 = 30019

    ''' <summary>
    ''' VBNC = "'Is' operator does not accept operands of type '{0}'. Operands must be reference or nullable types."
    ''' VB   = "'Is' requires operands that have reference types, but this operand has the value type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30020 = 30020

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'TypeOf ... Is' requires its left operand to have a reference type, but this operand has the value type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30021 = 30021

    ''' <summary>
    ''' VBNC = "Properties declared 'ReadOnly' cannot have a 'Set'."
    ''' VB   = "Properties declared 'ReadOnly' cannot have a 'Set'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30022 = 30022

    ''' <summary>
    ''' VBNC = "Properties declared 'WriteOnly' cannot have a 'Get'."
    ''' VB   = "Properties declared 'WriteOnly' cannot have a 'Get'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30023 = 30023

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement is not valid inside a method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30024 = 30024

    ''' <summary>
    ''' VBNC = "Property must end with 'End Property'."
    ''' VB   = "Property missing 'End Property'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30025 = 30025

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Sub' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30026 = 30026

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Function' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30027 = 30027

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#Else' must be preceded by a matching '#If' or '#ElseIf'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30028 = 30028

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Derived classes cannot raise base class events."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30029 = 30029

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Try must have at least one 'Catch' or a 'Finally'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30030 = 30030

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Fully qualified name for type cannot be longer than |2 characters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30031 = 30031

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Events cannot have a return type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30032 = 30032

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Identifier is too long."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30033 = 30033

    ''' <summary>
    ''' VBNC = "An escaped identifier must end with ']'."
    ''' VB   = "Bracketed identifier is missing closing ']'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30034 = 30034

    ''' <summary>
    ''' VBNC = "Syntax error."
    ''' VB   = "Syntax error."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30035 = 30035

    ''' <summary>
    ''' VBNC = "Overflow."
    ''' VB   = "Overflow."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30036 = 30036

    ''' <summary>
    ''' VBNC = "Symbol is not valid."
    ''' VB   = "Character is not valid."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30037 = 30037

    ''' <summary>
    ''' VBNC = "Option Strict On prohibits operands of type Object for operator '{0}'."
    ''' VB   = "Option Strict On prohibits operands of type Object for operator '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30038 = 30038

    ''' <summary>
    ''' VBNC = "Loop control variable cannot be a property or a late-bound indexed array."
    ''' VB   = "Loop control variable cannot be a property or a late-bound indexed array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30039 = 30039

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "First statement of a method body cannot be on the same line as the method declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30040 = 30040

    ''' <summary>
    ''' VBNC = "Too many errors."
    ''' VB   = "Maximum number of errors has been exceeded."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30041 = 30041

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is valid only within an instance method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30043 = 30043

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid within a structure."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30044 = 30044

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute constructor has a parameter of type '|1', which is not an integral, floating-point, or Enum type or one of Char, String, Boolean, or System.Type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30045 = 30045

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method 	 a ParamArray and Optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30046 = 30046

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' statement requires an array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30049 = 30049

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "ParamArray parameter must be an array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30050 = 30050

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "ParamArray parameter must be a one-dimensional array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30051 = 30051

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array exceeds the limit of 32 dimensions."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30052 = 30052

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Arrays cannot be declared with 'New'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30053 = 30053

    ''' <summary>
    ''' VBNC = "Too many arguments to '{0}'."
    ''' VB   = "Too many arguments to '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30057 = 30057

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statements and labels are not valid between 'Select Case' and first 'Case'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30058 = 30058

    ''' <summary>
    ''' VBNC = "The expression is not a constant expression."
    ''' VB   = "Constant expression is required."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30059 = 30059

    ''' <summary>
    ''' VBNC = "Cannot convert from '{0}' to '{1}' in a constant expression."
    ''' VB   = "Conversion from '|1' to '|2' cannot occur in a constant expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30060 = 30060

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Me' cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30062 = 30062

    ''' <summary>
    ''' VBNC = "'ReadOnly' variable cannot be the target of an assignment."
    ''' VB   = "'ReadOnly' variable cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30064 = 30064

    ''' <summary>
    ''' VBNC = "It is not valid to use 'Exit Sub' in a Function or Property."
    ''' VB   = "'Exit Sub' is not valid in a Function or Property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30065 = 30065

    ''' <summary>
    ''' VBNC = "It is not valid to use 'Exit Property' in a Function or Sub."
    ''' VB   = "'Exit Property' is not valid in a Function or Sub."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30066 = 30066

    ''' <summary>
    ''' VBNC = "It is not valid to use 'Exit Function' in a Sub or Property."
    ''' VB   = "'Exit Function' is not valid in a Sub or Property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30067 = 30067

    ''' <summary>
    ''' VBNC = "Expression is a value and therefore cannot be the target of an assignment."
    ''' VB   = "Expression is a value and therefore cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30068 = 30068

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "For loop control variable '|1' already in use by an enclosing For loop."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30069 = 30069

    ''' <summary>
    ''' VBNC = "Next control variable does not match For loop control variable '{0}'."
    ''' VB   = "Next control variable does not match For loop control variable '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30070 = 30070

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Case Else' can only appear inside a 'Select Case' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30071 = 30071

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Case' can only appear inside a 'Select Case' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30072 = 30072

    ''' <summary>
    ''' VBNC = "Constant cannot be the target of an assignment."
    ''' VB   = "Constant cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30074 = 30074

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Named arguments are not valid as array subscripts."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30075 = 30075

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'If' must end with a matching 'End If'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30081 = 30081

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'While' must end with a matching 'End While'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30082 = 30082

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Do' must end with a matching 'Loop'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30083 = 30083

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'For' must end with a matching 'Next'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30084 = 30084

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'With' must end with a matching 'End With'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30085 = 30085

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Else' must be preceded by a matching 'If' or 'ElseIf'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30086 = 30086

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End If' must be preceded by a matching 'If'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30087 = 30087

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Select' must be preceded by a matching 'Select Case'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30088 = 30088

    ''' <summary>
    ''' VBNC = "This 'Exit Do' statement is not contained within a 'Do' statement."
    ''' VB   = "'Exit Do' can only appear inside a 'Do' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30089 = 30089

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End While' must be preceded by a matching 'While'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30090 = 30090

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Loop' must be preceded by a matching 'Do'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30091 = 30091

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Next' must be preceded by a matching 'For'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30092 = 30092

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End With' must be preceded by a matching 'With'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30093 = 30093

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Label '|1' is already defined in the current method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30094 = 30094

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Select Case' must end with a matching 'End Select'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30095 = 30095

    ''' <summary>
    ''' VBNC = "This 'Exit For' statement is not contained within a 'For' statement."
    ''' VB   = "'Exit For' can only appear inside a 'For' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30096 = 30096

    ''' <summary>
    ''' VBNC = "This 'Exit While' statement is not contained within a 'While' statement."
    ''' VB   = "'Exit While' can only appear inside a 'While' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30097 = 30097

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ReadOnly' property '|1' cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30098 = 30098

    ''' <summary>
    ''' VBNC = "'Exit Select' can only appear inside a 'Select' statement."
    ''' VB   = "'Exit Select' can only appear inside a 'Select' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30099 = 30099

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Branching out of a 'Finally' is not valid."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30101 = 30101

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'!' requires its left operand to have a class or interface type, but this operand has the type '|1''."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30103 = 30103

    ''' <summary>
    ''' VBNC = "Number of indices is less than the number of dimensions of the indexed array."
    ''' VB   = "Number of indices is less than the number of dimensions of the indexed array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30105 = 30105

    ''' <summary>
    ''' VBNC = "Number of indices exceeds the number of dimensions of the indexed array."
    ''' VB   = "Number of indices exceeds the number of dimensions of the indexed array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30106 = 30106

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is an Enum type and cannot be used as an expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30107 = 30107

    ''' <summary>
    ''' VBNC = "Expression is a type and cannot be used as an expression."
    ''' VB   = "'|1' is a type and cannot be used as an expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30108 = 30108

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is a class type and cannot be used as an expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30109 = 30109

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is a structure type and cannot be used as an expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30110 = 30110

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is an interface type and cannot be used as an expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30111 = 30111

    ''' <summary>
    ''' VBNC = "Expression is a namespace and cannot be used as an expression."
    ''' VB   = "'|1' is a namespace and cannot be used as an expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30112 = 30112

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not a valid name and cannot be used as the root namespace name."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30113 = 30113

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Inherits' can appear only once within a 'Class' statement and can only specify one class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30121 = 30121

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Stop' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30122 = 30122

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30123 = 30123

    ''' <summary>
    ''' VBNC = "Property without a 'ReadOnly' or 'WriteOnly' specifier must provide both a 'Get' and a 'Set'."
    ''' VB   = "Property without a 'ReadOnly' or 'WriteOnly' specifier must provide both a 'Get' and a 'Set'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30124 = 30124

    ''' <summary>
    ''' VBNC = "'WriteOnly' property must provide a 'Set'."
    ''' VB   = "'WriteOnly' property must provide a 'Set'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30125 = 30125

    ''' <summary>
    ''' VBNC = "'ReadOnly' property must provide a 'Get'."
    ''' VB   = "'ReadOnly' property must provide a 'Get'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30126 = 30126

    ''' <summary>
    ''' VBNC = "Attribute '{0}' is not valid: {1}"
    ''' VB   = "Attribute '|1' is not valid: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30127 = 30127

    ''' <summary>
    ''' VBNC = "Security attribute '{0}' is not valid: {1}"
    ''' VB   = "Security attribute '|1' is not valid: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30128 = 30128

    ''' <summary>
    ''' VBNC = "Assembly attribute '{0}' is not valid: {1}"
    ''' VB   = "Assembly attribute '|1' is not valid: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30129 = 30129

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Module attribute '|1' is not valid: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30130 = 30130

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Security attribute '|1' cannot be applied to a module."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30131 = 30131

    ''' <summary>
    ''' VBNC = "Label '{0}' is not defined."
    ''' VB   = "Label '|1' is not defined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30132 = 30132

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'GoTo' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30133 = 30133

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Labels are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30134 = 30134

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'SyncLock' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30135 = 30135

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Error creating Win32 resources: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30136 = 30136

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Error saving temporary Win32 resource file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30137 = 30137

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to create temp file in path '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30138 = 30138

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Error setting assembly manifest option: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30139 = 30139

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Error creating assembly manifest: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30140 = 30140

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to create Assembly Linker object: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30141 = 30141

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to generate a reference to file '|1' (use TLBIMP utility to reference COM DLLs): |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30142 = 30142

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to embed resource file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30143 = 30143

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to link to resource file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30144 = 30144

    ''' <summary>
    ''' VBNC = "Unable to emit assembly: {0}"
    ''' VB   = "Unable to emit assembly: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30145 = 30145

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to sign assembly: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30146 = 30146

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Return' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30147 = 30147

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "First statement of this 'Sub New' must be a call to 'MyBase.New' or 'MyClass.New' because base class '|1' of '|2' does not have an accessible 'Sub New' that can be called with no arguments."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30148 = 30148

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' must implement '|2' for interface '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30149 = 30149

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' must implement '|2' for interface '|3'. Implementing property must have matching 'ReadOnly'/'WriteOnly' specifiers."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30154 = 30154

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Leading '.' or '!' can only appear inside a 'With' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30157 = 30157

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Cannot create an instance of Module '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30166 = 30166

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' and |3 '|2', declared in '|4' conflict in |5 '|6'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30175 = 30175

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Only one of 'Public', 'Private', 'Protected', 'Friend', or 'Protected Friend' can be specified."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30176 = 30176

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Only one of 'NotOverridable', 'MustOverride', or 'Overridable' can be specified."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30177 = 30177

    ''' <summary>
    ''' VBNC = "The modifier '{0}' is specified more than once."
    ''' VB   = "Specifier is duplicated."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30178 = 30178

    ''' <summary>
    ''' VBNC = "{0} '{1}' and {2} '{3}' conflict in {4} '{5}'."
    ''' VB   = "|1 '|2' and |3 '|2' conflict in |4 '|5'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30179 = 30179

    ''' <summary>
    ''' VBNC = "Keyword does not name a type."
    ''' VB   = "Keyword does not name a type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30180 = 30180

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Specifiers valid only at the beginning of a declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30181 = 30181

    ''' <summary>
    ''' VBNC = "Type expected."
    ''' VB   = "Type expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30182 = 30182

    ''' <summary>
    ''' VBNC = "Keyword is not valid as an identifier."
    ''' VB   = "Keyword is not valid as an identifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30183 = 30183

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Enum' must be preceded by a matching 'Enum'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30184 = 30184

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Enum' must end with a matching 'End Enum'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30185 = 30185

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Using' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30186 = 30186

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Declaration expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30188 = 30188

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "End of parameter list expected. Cannot define parameters after a paramarray parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30192 = 30192

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Specifiers and attributes are not valid on this statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30193 = 30193

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expected one of 'Dim', 'Const', 'Public', 'Private', 'Protected', 'Friend', 'Shadows', 'ReadOnly' or 'Shared'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30195 = 30195

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Comma expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30196 = 30196

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'As' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30197 = 30197

    ''' <summary>
    ''' VBNC = "Expected ')'."
    ''' VB   = "')' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30198 = 30198

    ''' <summary>
    ''' VBNC = "Expected '('."
    ''' VB   = "'(' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30199 = 30199

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'New' is not valid in this context."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30200 = 30200

    ''' <summary>
    ''' VBNC = "Expected expression."
    ''' VB   = "Expression expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30201 = 30201

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Optional' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30202 = 30202

    ''' <summary>
    ''' VBNC = "Identifier expected."
    ''' VB   = "Identifier expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30203 = 30203

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Integer constant expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30204 = 30204

    ''' <summary>
    ''' VBNC = "End of statement expected."
    ''' VB   = "End of statement expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30205 = 30205

    ''' <summary>
    ''' VBNC = "'Option' must be followed by 'Compare', 'Explicit', or 'Strict'."
    ''' VB   = "'Option' must be followed by 'Compare', 'Explicit', or 'Strict'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30206 = 30206

    ''' <summary>
    ''' VBNC = "'Option Compare' must be followed by 'Text' or 'Binary'."
    ''' VB   = "'Option Compare' must be followed by 'Text' or 'Binary'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30207 = 30207

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Compare' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30208 = 30208

    ''' <summary>
    ''' VBNC = "An 'As' clause is required if Option Strict is in effect."
    ''' VB   = "Option Strict On requires all variable declarations to have an 'As' clause."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30209 = 30209

    ''' <summary>
    ''' VBNC = "This function or property requires a return type because 'Option Strict' is in effect."
    ''' VB   = "Option Strict On requires all function and property declarations to have an 'As' clause."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30210 = 30210

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Comma or ')' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30213 = 30213

    ''' <summary>
    ''' VBNC = "Expected 'Sub' or 'Function'"
    ''' VB   = "'Sub' or 'Function' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30215 = 30215

    ''' <summary>
    ''' VBNC = "String constant expected."
    ''' VB   = "String constant expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30217 = 30217

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Lib' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30218 = 30218

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Delegate class '|1' has no Invoke method, so an expression of this type cannot be the target of a method call."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30220 = 30220

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Is' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30224 = 30224

    ''' <summary>
    ''' VBNC = "Option {0} statement can only appear once per file."
    ''' VB   = "'Option |1' statement can only appear once per file."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30225 = 30225

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Inherits' not valid in Modules."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30230 = 30230

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Implements' not valid in Modules."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30231 = 30231

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Implemented type must be an interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30232 = 30232

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a constant declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30233 = 30233

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a WithEvents declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30234 = 30234

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a member variable declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30235 = 30235

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Parameter already declared with name '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30237 = 30237

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Loop' cannot have a condition if matching 'Do' has one."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30238 = 30238

    ''' <summary>
    ''' VBNC = "Expected relational operator."
    ''' VB   = "Relational operator expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30239 = 30239

    ''' <summary>
    ''' VBNC = "Expected something else (like 'Sub', 'Function', 'Property', 'Do', 'For', 'While', 'Select' or 'Try'."
    ''' VB   = "'Exit' must be followed by 'Sub', 'Function', 'Property', 'Do', 'For', 'While', 'Select', or 'Try'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30240 = 30240

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Named argument expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30241 = 30241

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a method declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30242 = 30242

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on an event declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30243 = 30243

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a Declare."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30244 = 30244

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a local constant declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30246 = 30246

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a local variable declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30247 = 30247

    ''' <summary>
    ''' VBNC = "'If', 'ElseIf', 'Else', 'End If', 'Const', or 'Region' expected."
    ''' VB   = "'If', 'ElseIf', 'Else', 'End If', 'Const', or 'Region' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30248 = 30248

    ''' <summary>
    ''' VBNC = "'=' expected."
    ''' VB   = "'=' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30249 = 30249

    ''' <summary>
    ''' VBNC = "Type '{0}' has no constructors."
    ''' VB   = "Type '|1' has no constructors."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30251 = 30251

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Interface' must be preceded by a matching 'Interface'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30252 = 30252

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Interface' must end with a matching 'End Interface'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30253 = 30253

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "\n    '|1' inherits from '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30256 = 30256

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' cannot inherit from itself: |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30257 = 30257

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Classes can inherit only from other classes."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30258 = 30258

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is already declared as '|2' in this |3."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30260 = 30260

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they have different access levels."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30266 = 30266

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because it is declared 'NotOverridable'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30267 = 30267

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because it is declared 'Shared'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30268 = 30268

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' has multiple definitions with identical signatures."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30269 = 30269

    ''' <summary>
    ''' VBNC = "An interface cannot have '{0}'."
    ''' VB   = "'|1' is not valid on an interface method declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30270 = 30270

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not a parameter of '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30272 = 30272

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on an interface property declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30273 = 30273

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Parameter '|1' of '|2' already has a matching argument."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30274 = 30274

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on an interface event declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30275 = 30275

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type character '|1' does not match declared data type '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30277 = 30277

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Sub' or 'Function' expected after 'Delegate'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30278 = 30278

    ''' <summary>
    ''' VBNC = "Enum '{0}' must contain at least one member."
    ''' VB   = "Enum '|1' must contain at least one member."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30280 = 30280

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Structure '|1' must contain at least one instance member variable or Event declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30281 = 30281

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Constructor call is valid only as the first statement in an instance constructor."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30282 = 30282

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Sub New' cannot be declared 'Overrides'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30283 = 30283

    ''' <summary>
    ''' VBNC = "'Overrides' can't be used on {0}, because there is no such member in a base class."
    ''' VB   = "|1 '|2' cannot be declared 'Overrides' because it does not override a |1 in a base |3."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30284 = 30284

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'.' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30287 = 30287

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Local variable '|1' is already declared in the current block."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30288 = 30288

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement cannot appear within a method body. End of method assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30289 = 30289

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Local variable cannot have the same name as the function containing it."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30290 = 30290

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "\n    '|1' contains '|2' (variable '|3')."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30293 = 30293

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Structure '|1' cannot contain an instance of itself: |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30294 = 30294

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Interface '|1' cannot inherit from itself: |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30296 = 30296

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|0\n    '|1' calls '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30297 = 30297

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Constructor '|1' cannot call itself:"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30298 = 30298

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot inherit from |3 '|2' because '|2' is declared 'NotInheritable'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30299 = 30299

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30300 = 30300

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by return types."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30301 = 30301

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type character '|1' cannot be used in a declaration with an explicit type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30302 = 30302

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type character cannot be used in a 'Sub' declaration because a 'Sub' doesn't return a value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30303 = 30303

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by the default values of optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30305 = 30305

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array subscript expression missing."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30306 = 30306

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they differ by the default values of optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30307 = 30307

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they differ by optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30308 = 30308

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Cannot refer to '|1' because it is a member of the value-typed field '|2' of class '|3' which has 'System.MarshalByRefObject' as a base class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30310 = 30310

    ''' <summary>
    ''' VBNC = "Value of type '{0}' cannot be converted to '{1}'."
    ''' VB   = "Value of type '|1' cannot be converted to '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30311 = 30311

    ''' <summary>
    ''' VBNC = "'Case' cannot follow a 'Case Else' in the same 'Select' statement."
    ''' VB   = "'Case' cannot follow a 'Case Else' in the same 'Select' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30321 = 30321

    ''' <summary>
    ''' VBNC = "Value of type '{0}' cannot be converted to '{1}' because '{2}' is not derived from '{3}'."
    ''' VB   = "Value of type '|1' cannot be converted to '|2' because '|3' is not derived from '|4'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30332 = 30332

    ''' <summary>
    ''' VBNC = "Value of type '{0}' cannot be converted to '{1}' because '{2}' is not a reference type."
    ''' VB   = "Value of type '|1' cannot be converted to '|2' because '|3' is not a reference type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30333 = 30333

    ''' <summary>
    ''' VBNC = "'For' loop control variable cannot be of the type '{0}'"
    ''' VB   = "'For' loop control variable cannot be of type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30337 = 30337

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Select' expression cannot be of type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30338 = 30338

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by parameters declared 'ByRef' or 'ByVal'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30345 = 30345

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Interface can inherit only from another interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30354 = 30354

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Inherits' statements must precede all declarations in an interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30357 = 30357

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Default' can be applied to only one property name in a |1."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30359 = 30359

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property '|1' must be declared 'Default' because it overrides a default property on the base |2 '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30360 = 30360

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because only one is declared 'Default'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30361 = 30361

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they differ by 'ReadOnly' or 'WriteOnly'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30362 = 30362

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Sub New' cannot be declared in an interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30363 = 30363

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Sub New' cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30364 = 30364

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by 'ReadOnly' or 'WriteOnly'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30366 = 30366

    ''' <summary>
    ''' VBNC = "Class '{0}' cannot be indexed because it has no default property."
    ''' VB   = "Class '|1' cannot be indexed because it has no default property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30367 = 30367

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by parameters declared 'ParamArray'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30368 = 30368

    ''' <summary>
    ''' VBNC = "Cannot refer to an instance member of a class from within a shared method or shared member initializer without an explicit instance of the class."
    ''' VB   = "Cannot refer to an instance member of a class from within a shared method or shared member initializer without an explicit instance of the class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30369 = 30369

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'}' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30370 = 30370

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Module '|1' cannot be used as a type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30371 = 30371

    ''' <summary>
    ''' VBNC = "'New' cannot be used on an interface."
    ''' VB   = "'New' cannot be used on an interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30375 = 30375

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'New' cannot be used on class '|1' because it contains a 'MustOverride' member that has not been overridden."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30376 = 30376

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method '|1' is already declared in interface '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30377 = 30377

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Catch' cannot appear after 'Finally' within a 'Try' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30379 = 30379

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Catch' cannot appear outside a 'Try' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30380 = 30380

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Finally' can only appear once in a 'Try' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30381 = 30381

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Finally' cannot appear outside a 'Try' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30382 = 30382

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Try' must be preceded by a matching 'Try'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30383 = 30383

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Try' must end with a matching 'End Try'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30384 = 30384

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a Delegate declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30385 = 30385

    ''' <summary>
    ''' VBNC = "The class '{0}' must define a constructor since no default constructor can be created."
    ''' VB   = "Class '|1' must declare a 'Sub New' because its base class '|2' does not have an accessible 'Sub New' that can be called with no arguments."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30387 = 30387

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not accessible in this context because it is '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30389 = 30389

    ''' <summary>
    ''' VBNC = "'{0}.{1}' is not accessible because it is '{2}'."
    ''' VB   = "'|1.|2' is not accessible in this context because it is '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30390 = 30390

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Catch' cannot catch type '|1' because it is not 'System.Exception' or a class that inherits from 'System.Exception'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30392 = 30392

    ''' <summary>
    ''' VBNC = "This 'Exit Try' statement is not contained within a 'Try' statement."
    ''' VB   = "'Exit Try' can only appear inside a 'Try' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30393 = 30393

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on a Structure declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30395 = 30395

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on an Enum declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30396 = 30396

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid on an Interface declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30397 = 30397

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they differ by a parameter that is marked as 'ByRef' versus 'ByVal'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30398 = 30398

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'MyBase' cannot be used with method '|1' because it is declared 'MustOverride'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30399 = 30399

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot implement '|2' because there is no matching |3 on interface '|4'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30401 = 30401

    ''' <summary>
    ''' VBNC = "Method '{0}' does not have the same signature as delegate '{1}'."
    ''' VB   = "Method '|1' does not have the same signature as delegate '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30408 = 30408

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'WithEvents' variables must have an 'As' clause."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30412 = 30412

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'WithEvents' variables can only be typed as classes or interfaces."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30413 = 30413

    ''' <summary>
    ''' VBNC = "Value of type '{0}' cannot be converted to '{1}' because the array types have different number of dimensions."
    ''' VB   = "Value of type '|1' cannot be converted to '|2' because the array types have different numbers of dimensions."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30414 = 30414

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ReDim' cannot change the number of dimensions of an array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30415 = 30415

    ''' <summary>
    ''' VBNC = "Could not find a 'Sub Main' in '{0}'."
    ''' VB   = "'Sub Main' was not found in '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30420 = 30420

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Optional parameters cannot be declared as the type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30423 = 30423

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Constants must be an intrinsic or enumerated type, not a class, structure, or array type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30424 = 30424

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Sub' must be preceded by a matching 'Sub'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30429 = 30429

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Function' must be preceded by a matching 'Function'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30430 = 30430

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Property' must be preceded by a matching 'Property'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30431 = 30431

    ''' <summary>
    ''' VBNC = "Module methods cannot be '{0}'."
    ''' VB   = "Methods in a Module cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30433 = 30433

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Events in a Module cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30434 = 30434

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Members in a Structure cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30435 = 30435

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Members in a Module cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30436 = 30436

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they differ by their return types."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30437 = 30437

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Constants must have a value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30438 = 30438

    ''' <summary>
    ''' VBNC = "The constant expression cannot be represented in the type '{0}'."
    ''' VB   = "Constant expression not representable in type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30439 = 30439

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Catch' must end with a matching 'End Try'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30441 = 30441

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Finally' must end with a matching 'End Try'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30442 = 30442

    ''' <summary>
    ''' VBNC = "'Get' is already declared."
    ''' VB   = "'Get' is already declared."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30443 = 30443

    ''' <summary>
    ''' VBNC = "'Set' is already declared."
    ''' VB   = "'Set' is already declared."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30444 = 30444

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Const declaration cannot have an array initializer."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30445 = 30445

    ''' <summary>
    ''' VBNC = "'{0}' is not declared. It may be inaccessible due to its protection level."
    ''' VB   = "Name '|1' is not declared."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30451 = 30451

    ''' <summary>
    ''' VBNC = "Operator '{0}' is not defined for types '{1}' and '{2}'."
    ''' VB   = "Operator '|1' is not defined for types '|2' and '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30452 = 30452

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expression is not a method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30454 = 30454

    ''' <summary>
    ''' VBNC = "Argument not specified for parameter '{0}' of '{1}'."
    ''' VB   = "Argument not specified for parameter '|1' of '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30455 = 30455

    ''' <summary>
    ''' VBNC = "'{0}' is not a member of '{1}'."
    ''' VB   = "'|1' is not a member of '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30456 = 30456

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Types are not available in this context."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30458 = 30458

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Class' must be preceded by a matching 'Class'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30460 = 30460

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Class' or 'Module' cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30461 = 30461

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Namespace or type '|1' in the project-level Imports '|2' cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30464 = 30464

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Imports' statements must precede any declarations."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30465 = 30465

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Namespace or type '|1' for the Imports '|2' cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30466 = 30466

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' for the Imports '|2' does not refer to a Namespace, Class, Structure, Enum or Module."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30467 = 30467

    ''' <summary>
    ''' VBNC = "Type declaration characters are not valid in this context."
    ''' VB   = "Type declaration characters are not valid in this context."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30468 = 30468

    ''' <summary>
    ''' VBNC = "Reference to a non-shared member requires an object reference."
    ''' VB   = "Reference to a non-shared member requires an object reference."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30469 = 30469

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'MyClass' cannot be used outside of a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30470 = 30470

    ''' <summary>
    ''' VBNC = "Only arrays and methods can have argument lists."
    ''' VB   = "Expression is not an array or a method, and cannot have an argument list."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30471 = 30471

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because it is a 'Declare' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30474 = 30474

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'WithEvents' variables cannot be typed as arrays."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30476 = 30476

    ''' <summary>
    ''' VBNC = "A structure can't have a shared constructor with parameters."
    ''' VB   = "Shared 'Sub New' cannot have any parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30479 = 30479

    ''' <summary>
    ''' VBNC = "Shared 'Sub New' cannot be declared '{0}'."
    ''' VB   = "Shared 'Sub New' cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30480 = 30480

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Class' statement must end with a matching 'End Class'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30481 = 30481

    ''' <summary>
    ''' VBNC = "Operator '{0}' is not defined for type '{1}'."
    ''' VB   = "Operator '|1' is not defined for type '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30487 = 30487

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Default' cannot be combined with '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30490 = 30490

    ''' <summary>
    ''' VBNC = "Expression does not produce a value."
    ''' VB   = "Expression does not produce a value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30491 = 30491

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Constructor must be declared as a Sub, not as a Function."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30493 = 30493

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Line is too long."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30494 = 30494

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Exponent is not valid."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30495 = 30495

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Sub New' cannot handle events."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30497 = 30497

    ''' <summary>
    ''' VBNC = "Constant {0} is recursive, but it cannot depend on itself."
    ''' VB   = "Constant '|1' cannot depend on its own value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30500 = 30500

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Shared' cannot be combined with '|1' on a method declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30501 = 30501

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Shared' cannot be combined with '|1' on a property declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30502 = 30502

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Properties in a Module cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30503 = 30503

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property '|1' cannot be declared 'Default' because it overrides a Property on the base |2 '|3' that is not default."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30504 = 30504

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Methods or events that implement interface members cannot be declared 'Shared'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30505 = 30505

    ''' <summary>
    ''' VBNC = "Handles clause requires a variable declared with WithEvents."
    ''' VB   = "Handles clause requires a WithEvents variable."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30506 = 30506

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Implicitly generated property for handling the WithEvents variable '|1' conflicts with property '|2' defined in this class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30507 = 30507

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot expose type '|2' in |3 '|4' through |5 '|6'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30508 = 30508

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot inherit from |2 '|3' because it expands the access of the base |2 to the scope of |4 '|5'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30509 = 30509

    ''' <summary>
    ''' VBNC = "Option Strict On disallows implicit conversions from '{0}' to '{1}'."
    ''' VB   = "Option Strict On disallows implicit conversions from '|1' to '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30512 = 30512

    ''' <summary>
    ''' VBNC = "Overload resolution failed because no accessible '{0}' accepts this number of arguments."
    ''' VB   = "Overload resolution failed because no accessible '|1' accepts this number of arguments."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30516 = 30516

    ''' <summary>
    ''' VBNC = "Overload resolution failed because no '{0}' is accessible."
    ''' VB   = "Overload resolution failed because no '|1' is accessible."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30517 = 30517

    ''' <summary>
    ''' VBNC = "Overload resolution failed because no accessible '{0}' can be called with these arguments:{1}"
    ''' VB   = "Overload resolution failed because no accessible '|1' can be called with these arguments:|2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30518 = 30518

    ''' <summary>
    ''' VBNC = "Overload resolution failed because no accessible '{0}' can be called without a narrowing conversion:{1}"
    ''' VB   = "Overload resolution failed because no accessible '|1' can be called without a narrowing conversion:|2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30519 = 30519

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Argument matching parameter '|1' narrows from '|2' to '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30520 = 30520

    ''' <summary>
    ''' VBNC = "Overload resolution failed because no most specific '{0}' was found:"
    ''' VB   = "Overload resolution failed because no accessible '|1' is most specific for these arguments:|2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30521 = 30521

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Not most specific."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30522 = 30522

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "\n    '|1': |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30523 = 30523

    ''' <summary>
    ''' VBNC = "Property '{0}' is 'WriteOnly'."
    ''' VB   = "Property '|1' is 'WriteOnly'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30524 = 30524

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property '|1' is 'ReadOnly'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30526 = 30526

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1-dimensional array of |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30528 = 30528

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "All parameters must be explicitly typed if any are."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30529 = 30529

    ''' <summary>
    ''' VBNC = "Parameter cannot have the same name as its defining function."
    ''' VB   = "Parameter cannot have the same name as its defining function."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30530 = 30530

    ''' <summary>
    ''' VBNC = "Conversion from 'Date' to 'Double' requires calling the 'Date.ToOADate' method."
    ''' VB   = "Conversion from 'Date' to 'Double' requires calling the 'Date.ToOADate' method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30532 = 30532

    ''' <summary>
    ''' VBNC = "Conversion from 'Double' to 'Date' requires calling the 'Date.FromOADate' method."
    ''' VB   = "Conversion from 'Double' to 'Date' requires calling the 'Date.FromOADate' method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30533 = 30533

    ''' <summary>
    ''' VBNC = "Division by zero occured."
    ''' VB   = "Division by zero occurred while evaluating this expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30542 = 30542

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method cannot contain both a 'Try' statement and an 'On Error' or 'Resume' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30544 = 30544

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property access must assign to the property or use its value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30545 = 30545

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Interface '|1' cannot be indexed because it has no default property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30547 = 30547

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute '|1' cannot be applied to an assembly."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30548 = 30548

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute '|1' cannot be applied to a module."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30549 = 30549

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Constructors cannot implement interface methods."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30550 = 30550

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is ambiguous."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30554 = 30554

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Default member '|1' is not a property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30555 = 30555

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is ambiguous in the namespace '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30560 = 30560

    ''' <summary>
    ''' VBNC = "'{0}' is ambiguous, imported from the namespaces or types '{1}'."
    ''' VB   = "'|1' is ambiguous, imported from the namespaces or types '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30561 = 30561

    ''' <summary>
    ''' VBNC = "'{0}' is ambiguous between declarations in Modules '{1}' and '{2}'."
    ''' VB   = "'|1' is ambiguous between declarations in Modules '|2' and '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30562 = 30562

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is ambiguous in the application objects '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30563 = 30563

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array initializer has too few dimensions."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30565 = 30565

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array initializer has too many dimensions."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30566 = 30566

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array initializer has |1 too few elements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30567 = 30567

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array initializer has |1 too many elements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30568 = 30568

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'New' cannot be used on a class that is declared 'MustInherit'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30569 = 30569

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Alias '|1' is already declared."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30572 = 30572

    ''' <summary>
    ''' VBNC = "Late binding is not allowed when Option Strict On is in effect."
    ''' VB   = "Option Strict On disallows late binding."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30574 = 30574

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement does not declare a 'Get' or 'Set' method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30576 = 30576

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'AddressOf' operand must be the name of a method; no parentheses are needed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30577 = 30577

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#End ExternalSource' must be preceded by a matching '#ExternalSource'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30578 = 30578

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#ExternalSource' statement must end with a matching '#End ExternalSource'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30579 = 30579

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#ExternalSource' directives cannot be nested."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30580 = 30580

    ''' <summary>
    ''' VBNC = "An 'AddressOf' expression can't be converted to '{0}', because '{0}' is not a delegate type."
    ''' VB   = "'AddressOf' expression cannot be converted to '|1' because '|1' is not a delegate type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30581 = 30581

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'SyncLock' operand cannot be of type '|1' because '|1' is not a reference type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30582 = 30582

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1.|2' cannot be implemented more than once."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30583 = 30583

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be inherited more than once."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30584 = 30584

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Event '|1' cannot be handled because it is not accessible from '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30585 = 30585

    ''' <summary>
    ''' VBNC = "Named argument cannot match a ParamArray parameter."
    ''' VB   = "Named argument cannot match a ParamArray parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30587 = 30587

    ''' <summary>
    ''' VBNC = "Omitted argument cannot match a ParamArray parameter."
    ''' VB   = "Omitted argument cannot match a ParamArray parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30588 = 30588

    ''' <summary>
    ''' VBNC = "Cannot find the event '{0}'."
    ''' VB   = "Event '|1' cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30590 = 30590

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'WithEvents' variable does not raise any events."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30591 = 30591

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Variables in Modules cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30593 = 30593

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Events of shared WithEvents variables cannot be handled by non-shared methods."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30594 = 30594

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Reflection.Missing.Value', necessary for the implementation of omitted parameters, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30597 = 30597

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Threading.Monitor', necessary for the implementation of the 'SyncLock' statement, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30598 = 30598

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Threading.Monitor.|1', necessary for the implementation of the 'SyncLock' statement, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30599 = 30599

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'WithEvents' variable does not raise any instance events that are accessible to '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30600 = 30600

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'-' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30601 = 30601

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Interface members must be methods, properties, events, or type definitions."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30602 = 30602

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement cannot appear within an interface body."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30603 = 30603

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement cannot appear within an interface body. End of interface assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30604 = 30604

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'NotInheritable' classes cannot have members declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30607 = 30607

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' must either be declared 'MustInherit' or override the following inherited 'MustOverride' member(s): |2."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30610 = 30610

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array dimensions cannot have a negative size."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30611 = 30611

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.CompilerServices.StringType.MidStmtStr', necessary for the implementation of the 'Mid' statement, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30613 = 30613

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'MustOverride' method '|1' cannot be called with 'MyClass'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30614 = 30614

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End' statement cannot be used in class library projects."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30615 = 30615

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Variable '|1' hides a variable in an enclosing block."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30616 = 30616

    ''' <summary>
    ''' VBNC = "'Module' can only occur at file or namespace level."
    ''' VB   = "'Module' statents can occur only at file or namespace level."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30617 = 30617

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Namespace' statements can occur only at file or namespace level."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30618 = 30618

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement cannot appear within an Enum body. End of Enum assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30619 = 30619

    ''' <summary>
    ''' VBNC = "'Option Strict' must be followed by 'On' or 'Off'."
    ''' VB   = "'Option Strict' can be followed only by 'On' or 'Off'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30620 = 30620

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Structure' must be preceded by a matching 'Structure'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30621 = 30621

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Module' must be preceded by a matching 'Module'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30622 = 30622

    ''' <summary>
    ''' VBNC = "'End Namespace' must be preceded by a matching 'Namespace'."
    ''' VB   = "'End Namespace' must be preceded by a matching 'Namespace'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30623 = 30623

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Structure' statement must end with a matching 'End Structure'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30624 = 30624

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Module' statement must end with a matching 'End Module'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30625 = 30625

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Namespace' statement must end with a matching 'End Namespace'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30626 = 30626

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Option' statements must precede any declarations or 'Imports' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30627 = 30627

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Structures cannot have 'Inherits' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30628 = 30628

    ''' <summary>
    ''' VBNC = "A structure can't declare a non-shared constructor with no parameters."
    ''' VB   = "Structures cannot declare a non-shared 'Sub New' with no parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30629 = 30629

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Get' must be preceded by a matching 'Get'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30630 = 30630

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Get' statement must end with a matching 'End Get'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30631 = 30631

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Set' must be preceded by a matching 'Set'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30632 = 30632

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Set' statement must end with a matching 'End Set'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30633 = 30633

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement cannot appear within a property body. End of property assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30634 = 30634

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ReadOnly' and 'WriteOnly' cannot be combined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30635 = 30635

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'&gt;' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30636 = 30636

    ''' <summary>
    ''' VBNC = "Assembly or Module attributes can only be specified on file level."
    ''' VB   = "Assembly or Module attribute statements must precede any declarations in a file."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30637 = 30637

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array bounds cannot appear in type specifiers."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30638 = 30638

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Properties cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30639 = 30639

    ''' <summary>
    ''' VBNC = "'Option Explicit' must be followed by 'On' or 'Off'."
    ''' VB   = "'Option Explicit' can be followed only by 'On' or 'Off'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30640 = 30640

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ByVal' and 'ByRef' cannot be combined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30641 = 30641

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Optional' and 'ParamArray' cannot be combined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30642 = 30642

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property '|1' is of an unsupported type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30643 = 30643

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute cannot be used on '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30644 = 30644

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute '|1' cannot be applied to a method with optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30645 = 30645

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Return' statement in a Sub or a Set cannot return a value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30647 = 30647

    ''' <summary>
    ''' VBNC = "String constants must end with a double quote."
    ''' VB   = "String constants must end with a double quote."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30648 = 30648

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is an unsupported type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30649 = 30649

    ''' <summary>
    ''' VBNC = "Enums must be declared as an integral type."
    ''' VB   = "Enums must be declared as an integral type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30650 = 30650

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 parameters cannot be declared 'ByRef'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30651 = 30651

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to assembly '|1' containing the type '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30652 = 30652

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference required to module '|1' containing the type '|2'. Add one to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30653 = 30653

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Return' statement in a Function or a Get must return a value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30654 = 30654

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to find required file '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30655 = 30655

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Field '|1' is of an unsupported type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30656 = 30656

    ''' <summary>
    ''' VBNC = "'{0}' has a return type or a parameter type that are not supported."
    ''' VB   = "'|1' has a return type that is not supported or parameter types that are not supported."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30657 = 30657

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property '|1' with no parameters cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30658 = 30658

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property or field '|1' does not have a valid attribute type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30659 = 30659

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attributes cannot be applied to local variables."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30660 = 30660

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Field or property '|1' is not found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30661 = 30661

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute '|1' cannot be applied to '|2' because the attribute is not valid on this declaration type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30662 = 30662

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute '|1' cannot be applied multiple times."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30663 = 30663

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversions from '|1' to '|2' must be explicit."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30664 = 30664

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Throw' operand must derive from 'System.Exception'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30665 = 30665

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Throw' statement cannot omit operand outside a 'Catch' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30666 = 30666

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "ParamArray parameters must be declared 'ByVal'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30667 = 30667

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is obsolete: '|2'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30668 = 30668

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ReDim' statements require a parenthesized list of the new bounds of each dimension of the array."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30670 = 30670

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Explicit initialization is not permitted with multiple variables declared with a single type specifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30671 = 30671

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Explicit initialization is not permitted for arrays declared with explicit bounds."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30672 = 30672

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End SyncLock' must be preceded by a matching 'SyncLock'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30674 = 30674

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'SyncLock' statement must end with a matching 'End SyncLock'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30675 = 30675

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not an event of '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30676 = 30676

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'AddHandler' or 'RemoveHandler' statement event operand must be a dot-qualified expression or a simple name."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30677 = 30677

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End' statement not valid."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30678 = 30678

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array initializers are valid only for arrays, but the type of '|1' is '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30679 = 30679

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#End Region' must be preceded by a matching '#Region'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30680 = 30680

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#Region' statement must end with a matching '#End Region'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30681 = 30681

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Inherits' statement must precede all declarations in a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30683 = 30683

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is ambiguous across the inherited interfaces '|2' and '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30685 = 30685

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Default property access is ambiguous between the inherited interface members '|1' of interface '|2' and '|3' of interface '|4'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30686 = 30686

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Keyword cannot have a type character."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30687 = 30687

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Events in interfaces cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30688 = 30688

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement cannot appear outside of a method body."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30689 = 30689

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Structure '|1' cannot be indexed because it has no default property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30690 = 30690

    ''' <summary>
    ''' VBNC = "'{1}.{0}' is a type and cannot be used as an expression."
    ''' VB   = "'|1' is a type in '|2' and cannot be used as an expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30691 = 30691

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.String.Concat', necessary for the implementation of the '&amp;' operator, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30694 = 30694

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' must be declared 'Shadows' because another member with this name is declared 'Shadows'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30695 = 30695

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by the types of optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30696 = 30696

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they differ by the types of optional parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30697 = 30697

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to write temporary file because temporary path is not available."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30698 = 30698

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared or the module containing it is not loaded in the debugging session."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30699 = 30699

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Side effects not valid during expression evaluation in this context."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30700 = 30700

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Nothing' cannot be evaluated."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30701 = 30701

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Index '|1' for dimension '|2' is out of range."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30702 = 30702

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Run-time exception thrown : |1 - |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30703 = 30703

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Run-time exception thrown."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30704 = 30704

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Referenced object '|1' has a value of 'Nothing'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30705 = 30705

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expression or statement is not valid in debug windows."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30706 = 30706

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to evaluate expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30707 = 30707

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Loop statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30708 = 30708

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Variable declaration statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30709 = 30709

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "End of expression expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30710 = 30710

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Cannot set the value of a local variable for a method that is not at the top of the stack."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30711 = 30711

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to load information for class '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30712 = 30712

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Exit' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30713 = 30713

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Resume' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30714 = 30714

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Catch' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30715 = 30715

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Finally' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30716 = 30716

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Try' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30717 = 30717

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Select' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30718 = 30718

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Case' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30719 = 30719

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'On Error' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30720 = 30720

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Evaluation of expression or statement stopped."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30721 = 30721

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Evaluation of expression or statement timed out."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30722 = 30722

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method call did not return a value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30723 = 30723

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Error' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30724 = 30724

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Throw' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30725 = 30725

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'With' contexts and statements are not valid in debug windows."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30726 = 30726

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Methods declared in structures cannot handle events."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30728 = 30728

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Methods declared 'Overrides' cannot be declared 'Overridable' because they are implicitly overridable."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30730 = 30730

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'AddressOf' expressions are not valid in debug windows."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30731 = 30731

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Web methods cannot be evaluated in debug windows."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30732 = 30732

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is already declared by '|2', which was generated for this |3."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30733 = 30733

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is already declared as a parameter of this method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30734 = 30734

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type in a Module cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30735 = 30735

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expression cannot be evaluated at this time."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30736 = 30736

    ''' <summary>
    ''' VBNC = "No accessible 'Main' method with an appropriate signature was found in '{0}'."
    ''' VB   = "No accessible 'Main' method with an appropriate signature was found in '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30737 = 30737

    ''' <summary>
    ''' VBNC = "More than one 'Sub Main' was found in '{0}'."
    ''' VB   = "'Sub Main' is declared more than once in '|1': |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30738 = 30738

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'RaiseEvent' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30739 = 30739

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be converted to '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30741 = 30741

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Value '|1' cannot be converted to '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30742 = 30742

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' is not defined or the module containing it is not loaded in the debugging session."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30743 = 30743

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Referenced array element has a value of 'Nothing'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30744 = 30744

    ''' <summary>
    ''' VBNC = "Internal compiler error."
    ''' VB   = "Internal compiler error."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30747 = 30747

    ''' <summary>
    ''' VBNC = "Cannot convert '{0}' to '{1}'."
    ''' VB   = "Cannot convert to '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30748 = 30748

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to access member."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30749 = 30749

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Information for the type of '|1' has not been loaded into the runtime."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30750 = 30750

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to get type information for '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30751 = 30751

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'On Error' statements are not valid within 'SyncLock' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30752 = 30752

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Option Strict On disallows implicit conversions from '|1' to '|2'; the Visual Basic 6.0 collection type is not compatible with the .NET Framework collection type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30753 = 30753

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'GoTo |1' is not valid because '|1' is inside a 'Try', 'Catch' or 'Finally' statement that does not contain this statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30754 = 30754

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'GoTo |1' is not valid because '|1' is inside a 'SyncLock' statement that does not contain this statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30755 = 30755

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'GoTo |1' is not valid because '|1' is inside a 'With' statement that does not contain this statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30756 = 30756

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'GoTo |1' is not valid because '|1' is inside a 'For' or 'For Each' statement that does not contain this statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30757 = 30757

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute cannot be used because it does not have a Public constructor."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30758 = 30758

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Referenced '|1' has a value of 'Nothing'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30759 = 30759

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Referenced object has a value of 'Nothing'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30760 = 30760

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Project '|1' cannot reference project '|2' because '|2' directly or indirectly references '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30761 = 30761

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The targeted version of the .NET Compact Framework does not support latebinding."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30762 = 30762

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The targeted version of the .NET Compact Framework does not support using the Ansi, Auto or Unicode modifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30763 = 30763

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The targeted version of the .NET Compact Framework does not support latebound overload resolution."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30764 = 30764

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "All projects in a Visual Basic solution must target the same platform, but the project you are trying to add targets a platform other than the one specified by your solution."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30765 = 30765

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. File I/O functionality is normally available in the 'Microsoft.VisualBasic' namespace, but the targeted version of the .NET Compact Framework does not support it."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30766 = 30766

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Get' statements are no longer supported. File I/O functionality is normally available in the 'Microsoft.VisualBasic' namespace, but the targeted version of the .NET Compact Framework does not support it."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30767 = 30767

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Line' statements are no longer supported. File I/O functionality is normally available as 'Microsoft.VisualBasic.FileSystem.LineInput', but the targeted version of the .NET Compact Framework does not support it. The graphics functionality is available as 'System.Drawing.Graphics.DrawLine'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30768 = 30768

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The targeted version of the .NET Compact Framework does not support the 'End' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30769 = 30769

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The '|1' event specified by the 'DefaultEvent' attribute is not a publicly accessible event for this class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30770 = 30770

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The 'WebMethod' attribute will not have any effect on this member because its containing class is not exposed as a web service."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30771 = 30771

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The 'NonSerialized' attribute will not have any effect on this member because its containing class is not exposed as 'Serializable'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30772 = 30772

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Continue' statements are not valid in the Command Window."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30780 = 30780

    ''' <summary>
    ''' VBNC = "Expected something else (like 'Do', 'For' or 'While')"
    ''' VB   = "'Continue' must be followed by 'Do', 'For' or 'While'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30781 = 30781

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Continue Do' can only appear inside a 'Do' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30782 = 30782

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Continue For' can only appear inside a 'For' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30783 = 30783

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Continue While' can only appear inside a 'While' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30784 = 30784

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Parameter specifier is duplicated."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30785 = 30785

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Declare' statements in a Module cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30786 = 30786

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "No corresponding main |1 '|2' found in current project for this |1 to expand."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30787 = 30787

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expanding class cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30788 = 30788

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expanding structure cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30789 = 30789

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expanding class cannot have 'Inherits' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30790 = 30790

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Declare' statements in a structure cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30791 = 30791

    ''' <summary>
    ''' VBNC = "None of the accessible 'Main' methods with the appropriate signature found in '{0}' can be the startup method since they are all either generic or nested in generic types."
    ''' VB   = "None of the accessible 'Main' methods with the appropriate signatures found in '|1' can be the startup method since they are all either generic or nested in generic types."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30796 = 30796

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method arguments must be enclosed in parentheses."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30800 = 30800

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Labels that are numbers must be followed by colons."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30801 = 30801

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Type' statements are no longer supported; use 'Structure' statements instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30802 = 30802

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Currency' is no longer a supported type; use the 'Decimal' type instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30803 = 30803

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Variant' is no longer a supported type; use the 'Object' type instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30804 = 30804

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array declarations cannot specify lower bounds."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30805 = 30805

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Let' and 'Set' assignment statements are no longer supported."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30807 = 30807

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property Get/Let/Set are no longer supported; use the new Property declaration syntax."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30808 = 30808

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Wend' statements are no longer supported; use 'End While' statements instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30809 = 30809

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Methods cannot be declared 'Static'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30810 = 30810

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ReDim' statements can no longer be used to declare array variables."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30811 = 30811

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Optional parameters must specify a default value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30812 = 30812

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'GoSub' statements are no longer supported."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30814 = 30814

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. File I/O functionality is available in the 'Microsoft.VisualBasic' namespace."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30815 = 30815

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. Debug object functionality is available in 'System.Diagnostics.Debug' in the 'System' assembly."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30816 = 30816

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'On GoTo' and 'On GoSub' statements are no longer supported."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30817 = 30817

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. Function has moved to the 'Microsoft.VisualBasic' namespace."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30818 = 30818

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. Function has moved to the 'System.Math' class and is named '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30819 = 30819

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. 'LSet' statements are no longer supported; use 'Microsoft.VisualBasic.LSet' instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30820 = 30820

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. 'RSet' statements are no longer supported; use 'Microsoft.VisualBasic.RSet' instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30821 = 30821

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. 'Null' constant is no longer supported; use 'System.DBNull' instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30822 = 30822

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not declared. 'Empty' constant is no longer supported; use 'Nothing' instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30823 = 30823

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'EndIf' statements are no longer supported; use 'End If' instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30826 = 30826

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'D' can no longer be used to indicate an exponent, use 'E' instead."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30827 = 30827

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'As Any' is not supported in 'Declare' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30828 = 30828

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Get' statements are no longer supported. File I/O functionality is available in the 'Microsoft.VisualBasic' namespace."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30829 = 30829

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Line' statements are no longer supported. File I/O functionality is available as 'Microsoft.VisualBasic.FileSystem.LineInput' and the graphics functionality is available as 'System.Drawing.Graphics.DrawLine'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30830 = 30830

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because they differ by parameters declared 'ParamArray'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30906 = 30906

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "This inheritance causes circular dependencies between |1 '|2' and its nested |3 '|4'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30907 = 30907

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' cannot inherit from a type nested within it."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30908 = 30908

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot expose type '|2' outside the project through |3 '|4'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30909 = 30909

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot inherit from |2 '|3' because it expands the access of the base |2 outside the assembly."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30910 = 30910

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' accessor of '|2' is obsolete: '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30911 = 30911

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' accessor of '|2' is obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30912 = 30912

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot expose the underlying delegate type '|2' of the event it is implementing to |3 '|4' through |5 '6'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30914 = 30914

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot expose the underlying delegate type '|2' of the event it is implementing outside the project through |3 '|4'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30915 = 30915

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' is not supported because it either directly or indirectly inherits from itself."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30916 = 30916

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' must declare a 'Sub New' because the '|2' in its base class '|3' is marked obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30917 = 30917

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' must declare a 'Sub New' because the '|2' in its base class '|3' is marked obsolete: '|4'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30918 = 30918

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "First statement of this 'Sub New' must be an explicit call to 'MyBase.New' or 'MyClass.New' because the '|1' in the base class '|2' of '|3' is marked obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30919 = 30919

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "First statement of this 'Sub New' must be an explicit call to 'MyBase.New' or 'MyClass.New' because the '|1' in the base class '|2' of '|3' is marked obsolete: '|4'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30920 = 30920

    ''' <summary>
    ''' VBNC = "Type of '{0}' is ambiguous because the loop bounds and the step clause do not convert to the same type."
    ''' VB   = "Type of 'i' is ambiguous because the loop bounds and the step clause do not convert to the same type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC30983 = 30983

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to open module file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31007 = 31007

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to load referenced library '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31011 = 31011

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to load DLL '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31013 = 31013

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to find entry point '|1' in DLL '|2': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31014 = 31014

    ''' <summary>
    ''' VBNC = "Unable to write to output file '{0}': {1}"
    ''' VB   = "Unable to write to output file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31019 = 31019

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to write output to memory."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31020 = 31020

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Visual Basic .NET compiler is unable to recover from the following error: |0\nSave your work and restart Visual Studio .NET."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31021 = 31021

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to parse XML: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31023 = 31023

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to create a .NET Runtime interface: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31024 = 31024

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to open key file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31025 = 31025

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to create strong-named assembly from key file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31026 = 31026

    ''' <summary>
    ''' VBNC = "The file '{0}' does not exist."
    ''' VB   = "Unable to open file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31027 = 31027

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to sign file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31028 = 31028

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method '|1' cannot handle Event '|2' because they do not have the same signature."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31029 = 31029

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Project-level conditional compilation constant '|2' is not valid: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31030 = 31030

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Project-level conditional compilation constant is not valid: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31031 = 31031

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Interface '|1' can be implemented only once by this type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31033 = 31033

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Interface '|1' is not implemented by this class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31035 = 31035

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' exists in multiple base interfaces. Use the name of the interface that declares '|1' in the 'Implements' clause instead of the name of the derived interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31040 = 31040

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement is not valid in an interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31041 = 31041

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Sub New' cannot implement interface members."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31042 = 31042

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Arrays declared as structure members cannot be declared with an initial size."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31043 = 31043

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Events declared with an 'As' clause must have a delegate type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31044 = 31044

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Protected types can only be declared inside of a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31047 = 31047

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Properties with no required parameters cannot be declared 'Default'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31048 = 31048

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Initializers on structure members are valid only for 'Shared' members and constants."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31049 = 31049

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Namespace '|1' has already been imported."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31051 = 31051

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Modules cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31052 = 31052

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Implements' statements must follow any 'Inherits' statement and precede all declarations in a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31053 = 31053

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' is not a valid attribute type and so cannot be used on a module."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31054 = 31054

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' is not a valid attribute type and so cannot be used on an assembly."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31055 = 31055

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conflicts with '|1', which is implicitly declared for '|2' in |3 '|4'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31058 = 31058

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' implicitly defines '|3', which conflicts with a member implicitly declared for |4 '|5' in |6 '|7'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31059 = 31059

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' implicitly defines '|3', which conflicts with a member of the same name in |4 '|5'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31060 = 31060

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' conflicts with a member implicitly declared for |3 '|4' in |5 '|6'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31061 = 31061

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Set' method cannot have more than one parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31063 = 31063

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Set' parameter must have the same type as the containing property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31064 = 31064

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Set' parameter cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31065 = 31065

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method in a Module cannot be declared 'Protected' or 'Protected Friend'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31066 = 31066

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method in a structure cannot be declared 'Protected' or 'Protected Friend'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31067 = 31067

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Delegate in an interface cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31068 = 31068

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Enum in an interface cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31069 = 31069

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class in an interface cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31070 = 31070

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Structure in an interface cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31071 = 31071

    ''' <summary>
    ''' VBNC = "Warning treated as error : {0}"
    ''' VB   = "Warning treated as error : |1"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31072 = 31072

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' conflicts with a |3 by the same name declared in base |4 '|5'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31073 = 31073

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Delegate constructor for type '|1', necessary for the implementation of delegates, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31074 = 31074

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31075 = 31075

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is a module and cannot be referenced as an assembly."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31076 = 31076

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is an assembly and cannot be referenced as a module."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31077 = 31077

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operator '|1' is not defined for types '|2' and '|3'. Use 'Is' operator to compare two reference types."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31080 = 31080

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not a local variable or parameter, and so cannot be used as a 'Catch' variable."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31082 = 31082

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Methods in a Module cannot implement interface members."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31083 = 31083

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Events cannot be declared with a delegate type that has a return type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31084 = 31084

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Date constant is not valid."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31085 = 31085

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because it is not declared 'Overridable'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31086 = 31086

    ''' <summary>
    ''' VBNC = "Array modifiers cannot be specified on both a variable and its type."
    ''' VB   = "Array modifiers cannot be specified on both a variable and its type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31087 = 31087

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'NotOverridable' cannot be specified for methods that do not override another method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31088 = 31088

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Types declared 'Private' must be inside another type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31089 = 31089

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' statement within a 'Catch' handler cannot branch into a 'Try' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31090 = 31090

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Import of type '|2' from assembly or module '|1' failed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31091 = 31091

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "ParamArray parameters must have an array type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31092 = 31092

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.IDisposable.Dispose', necessary for the implementation of 'For Each' statements, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31093 = 31093

    ''' <summary>
    ''' VBNC = "Implemnting class '{0}' cannot be found."
    ''' VB   = "Implementing class '|1' cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31094 = 31094

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Reference to object under construction is not valid when calling another constructor."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31095 = 31095

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Implicit reference to object under construction is not valid when calling another constructor."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31096 = 31096

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Member '|1' cannot be found in class '|2'. This condition is usually the result of a mismatched 'Microsoft.VisualBasic.dll'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31097 = 31097

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' cannot be found. This condition is usually the result of a mismatched 'Microsoft.VisualBasic.dll'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31098 = 31098

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property accessors cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31099 = 31099

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The access modifier '|1' is not legal. The access modifier of 'get' or 'set' can only restrict the property access."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31100 = 31100

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Only one of the 'Get' or 'Set' members of the property can have access modifiers."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31101 = 31101

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Set' accessor of property '|1' is not accessible."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31102 = 31102

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Get' accessor of property '|1' is not accessible."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31103 = 31103

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'WriteOnly' properties cannot have an access modifier on 'Set'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31104 = 31104

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ReadOnly' properties cannot have an access modifier on 'Get'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31105 = 31105

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property accessors cannot be declared '|1' in a 'NotOverridable' property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31106 = 31106

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property accessors cannot be declared '|1' in a 'Default' property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31107 = 31107

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property cannot be declared '|1' because it contains a 'private' accessor."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31108 = 31108

    ''' <summary>
    ''' VBNC = "Expected 'AddHandler', 'RemoveHandler' or 'RaiseEvent'."
    ''' VB   = "Statement cannot appear within an event body. End of event assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31112 = 31112

    ''' <summary>
    ''' VBNC = "'{0}' can only be declared once."
    ''' VB   = "'|1' is already declared."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31127 = 31127

    ''' <summary>
    ''' VBNC = "The custom event '{0}' must specify a '{1}'."
    ''' VB   = "'|1' definition missing for event '|2'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31130 = 31130

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Local variables within methods of structures cannot be declared 'Static'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31400 = 31400

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Static local variable '|1' is already declared."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31401 = 31401

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Imports alias '|1' conflicts with '|2' declared in the root namespace."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31403 = 31403

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot shadow a method declared 'MustOverride'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31404 = 31404

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Optional parameters cannot have structure types."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31405 = 31405

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override a method that has been shadowed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31406 = 31406

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Event '|1' cannot implement event '|3.|2' because its delegate type does not match the delegate type of another event implemented by '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31407 = 31407

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot be combined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31408 = 31408

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' must be declared 'Overloads' because another '|2' is declared 'Overloads'/'Overrides'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31409 = 31409

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Overloading methods declared in multiple base interfaces is not valid."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31410 = 31410

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' must be declared 'MustInherit' because it contains methods declared 'MustOverride'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31411 = 31411

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Handles' in classes must specify a 'WithEvents' variable, 'MyBase', 'MyClass' or 'Me' qualified with a single identifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31412 = 31412

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1', implicitly declared for |2 '|3', cannot shadow a 'MustOverride' method in the base |4 '|5'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31413 = 31413

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1 |2' cannot implement '|3' because '|3' is a reserved name."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31415 = 31415

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot shadow a 'MustOverride' method implicitly declared for |2 '|3' in the base |4 '|5'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31416 = 31416

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because it is not accessible in this context."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31417 = 31417

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Handles' in modules must specify a 'WithEvents' variable qualified with a single identifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31418 = 31418

    ''' <summary>
    ''' VBNC = "'IsNot' requires operands that have reference types, but this operand has the value type '{0}'."
    ''' VB   = "'IsNot' requires operands that have reference types, but this operand has the value type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31419 = 31419

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' conflicts with the reserved member by this name that is implicitly declared in all enums."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31420 = 31420

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is already declared in this |2."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31421 = 31421

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Shared' attribute property '|1' cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31500 = 31500

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ReadOnly' attribute property '|1' cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31501 = 31501

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Resource name '|1' cannot be used more than once."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31502 = 31502

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be used as an attribute because it is not a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31503 = 31503

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be used as an attribute because it does not inherit from 'System.Attribute'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31504 = 31504

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be used as an attribute because it does not have a 'System.AttributeUsageAttribute' attribute."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31505 = 31505

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be used as an attribute because it is declared 'MustInherit'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31506 = 31506

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be used as an attribute because it has 'MustOverride' methods that have not been overridden."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31507 = 31507

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Cannot find .NET Framework directory: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31508 = 31508

    ''' <summary>
    ''' VBNC = "Unable to open resource file '{0}': {1}"
    ''' VB   = "Unable to open resource file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31509 = 31509

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute constant '|1' cannot be the target of an assignment."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31510 = 31510

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute member '|1' cannot be the target of an assignment because it is not declared 'Public'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31511 = 31511

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.STAThreadAttribute' and 'System.MTAThreadAttribute' cannot both be applied to the same method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31512 = 31512

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.STAThreadAttribute' and 'System.MTAThreadAttribute' cannot both be applied to '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31513 = 31513

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Project '|1' cannot generate a reference to file '|2'. You may need to delete and re-add this reference, and then do a full rebuild of the solution."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31514 = 31514

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Project '|1' makes an indirect reference to assembly '|2', which contains '|3'. Add a reference to '|2' to your project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31515 = 31515

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' cannot be used in an attribute because it is not declared 'Public'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31516 = 31516

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' cannot be used in an attribute because its container '|2' is not declared 'Public'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31517 = 31517

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Assembly '|1' cannot be created because its path is longer than 259 characters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31518 = 31518

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be referenced because it is not an assembly."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31519 = 31519

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to apply security attribute(s) to '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31520 = 31520

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be applied more than once to an assembly."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31521 = 31521

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Runtime.InteropServices.DllImportAttribute' cannot be applied to a 'Sub' or 'Function' with a non-empty body."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31522 = 31522

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Runtime.InteropServices.DllImportAttribute' cannot be applied to a 'Declare'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31523 = 31523

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Runtime.InteropServices.DllImportAttribute' cannot be applied to a 'Get' or 'Set'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31524 = 31524

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|2' is imported from different versions of assembly |1.  Different versions of the same strong-named assembly cannot be used in the same project."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31525 = 31525

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Runtime.InteropServices.DllImportAttribute' cannot be applied to a generic 'Sub' or 'Function'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC31526 = 31526

    ''' <summary>
    ''' VBNC = "Local variable '{0}' cannot be referred to before it is declared."
    ''' VB   = "Local variable '|1' cannot be referred to before it is declared."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32000 = 32000

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not valid within a Module."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32001 = 32001

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is valid only within a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32002 = 32002

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is declared in project '|2', which is not referenced by project '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32004 = 32004

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Statement cannot end a block outside of a line 'If' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32005 = 32005

    ''' <summary>
    ''' VBNC = "'Char' values cannot be converted to '{0}'. Use 'Microsoft.VisualBasic.AscW' to interpret a character as a Unicode value or 'Microsoft.VisualBasic.Val' to interpret it as a digit."
    ''' VB   = "'Char' values cannot be converted to '|1'. Use 'Microsoft.VisualBasic.AscW' to interpret a character as a Unicode value or 'Microsoft.VisualBasic.Val' to interpret it as a digit."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32006 = 32006

    ''' <summary>
    ''' VBNC = "'{0}' values cannot be converted to 'Char'. Use 'Microsoft.VisualBasic.ChrW' to interpret a numeric value as a Unicode character or first convert it to 'String' to produce a digit."
    ''' VB   = "'|1' values cannot be converted to 'Char'. Use 'Microsoft.VisualBasic.ChrW' to interpret a numeric value as a Unicode character or first convert it to 'String' to produce a digit."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32007 = 32007

    ''' <summary>
    ''' VBNC = "'{0}' is a delegate type. Delegate construction permits only a single AddressOf expression as an argument list. Often an AddressOf expression can be used instead of a delegate construction."
    ''' VB   = "'|1' is a delegate type. Delegate construction permits only a single AddressOf expression as an argument list. Often an AddressOf expression can be used instead of a delegate construction."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32008 = 32008

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method declaration statements must be the first statement on a logical line."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32009 = 32009

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be named as a parameter in an attribute specifier because it is not a field or property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32010 = 32010

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.CompilerServices.|1', necessary for compiling this construct, cannot be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32012 = 32012

    ''' <summary>
    ''' VBNC = "Option Strict On disallows operands of type Object for operator '{0}'. Use the 'Is' operator to test for object identity."
    ''' VB   = "Option Strict On disallows operands of type Object for operator '|1'. Use the 'Is' operator to test for object identity."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32013 = 32013

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Bounds can be specified only for the top-level array when initializing an array of arrays."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32014 = 32014

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Assembly' or 'Module' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32015 = 32015

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' has no parameters and its return type cannot be indexed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32016 = 32016

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Comma, ')', or a valid expression continuation expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32017 = 32017

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Resume' or 'GoTo' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32019 = 32019

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'=' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32020 = 32020

    ''' <summary>
    ''' VBNC = "Parameter '{0}' in '{1}' already has a matching omitted argument."
    ''' VB   = "Parameter '|1' in '|2' already has a matching omitted argument."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32021 = 32021

    ''' <summary>
    ''' VBNC = "Expression is an event, and cannot be called directly. Use a 'RaiseEvent' statement to raise an event."
    ''' VB   = "'|1' is an event, and cannot be called directly. Use a 'RaiseEvent' statement to raise an event."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32022 = 32022

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Expression is of type '|1', which is not a collection type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32023 = 32023

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Default values cannot be supplied for parameters that are not declared 'Optional'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32024 = 32024

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#Region' and '#End Region' statements are not valid within method bodies."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32025 = 32025

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Specifiers and attributes are not valid on 'Namespace' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32026 = 32026

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'MyBase' must be followed by '.' and an identifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32027 = 32027

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'MyClass' must be followed by '.' and an identifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32028 = 32028

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Option Strict On disallows narrowing from type '|2' to type '|3' in copying the value of 'ByRef' parameter '|1' back to the matching argument."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32029 = 32029

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'#ElseIf' cannot follow '#Else' as part of a '#If' block."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32030 = 32030

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Sub' must be the first statement on a line."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32031 = 32031

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Function' must be the first statement on a line."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32032 = 32032

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Get' must be the first statement on a line."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32033 = 32033

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Set' must be the first statement on a line."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32034 = 32034

    ''' <summary>
    ''' VBNC = "Attributes can only appear just before a declaration."
    ''' VB   = "Attribute specifier is not a complete statement. Use a line continuation to apply the attribute to the following statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32035 = 32035

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' must declare a 'Sub New' because its base class '|2' has more than one accessible 'Sub New' that can be called with no arguments."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32036 = 32036

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Next' statement names more variables than there are matching 'For' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32037 = 32037

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "First statement of this 'Sub New' must be a call to 'MyBase.New' or 'MyClass.New' because base class '|1' of '|2' has more than one accessible 'Sub New' that can be called with no arguments."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32038 = 32038

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array declared as for loop control variable cannot be declared with an initial size."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32039 = 32039

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "The '|1' keyword is used to overload inherited members; do not use the '|1' keyword when overloading 'Sub New'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32040 = 32040

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type character cannot be used in a type parameter declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32041 = 32041

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Too few type arguments to '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32042 = 32042

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Too many type arguments to '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32043 = 32043

    ''' <summary>
    ''' VBNC = "Type argument '{0}' does not inherit from or implement '{1}'."
    ''' VB   = "Type argument '|1' does not inherit from or implement '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32044 = 32044

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' has no type parameters and so cannot have type arguments."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32045 = 32045

    ''' <summary>
    ''' VBNC = "'New' cannot be used on a type parameter not declared 'As New()'"
    ''' VB   = "'New' cannot be used on a type parameter not declared 'As New()'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32046 = 32046

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type parameter '|1' has more than one constraint that is a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32047 = 32047

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type constraint '|1' is not a class or interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32048 = 32048

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type parameter already declared with name '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32049 = 32049

    ''' <summary>
    ''' VBNC = "Type parameter '{0}' for '{1}' cannot be inferred."
    ''' VB   = "Type parameter '|1' cannot be determined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32050 = 32050

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type argument inference fails for this argument."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32051 = 32051

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Is' operand of type '|1' can be compared only to 'Nothing' because '|1' is a type parameter with no class constraint."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32052 = 32052

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type parameter '|1' has the same name as a type parameter of an enclosing type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32053 = 32053

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' has the same name as a type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32054 = 32054

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' cannot inherit from a type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32055 = 32055

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Implemented type cannot be a type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32056 = 32056

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Optional parameters cannot have type parameter types."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32057 = 32057

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type arguments cannot be supplied for the expression '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32058 = 32058

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Array lower bounds can be only '0'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32059 = 32059

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type constraint cannot be a 'NotInheritable' class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32060 = 32060

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be used as a type constraint."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32061 = 32061

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type parameter cannot be used by itself as a type constraint."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32062 = 32062

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Cannot specify type parameters on an expanding class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32063 = 32063

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Cannot specify type parameters on an expanding structure."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32064 = 32064

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Cannot specify type parameters on a constructor."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32065 = 32065

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type arguments unexpected because attributes cannot be generics."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32066 = 32066

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attributes cannot be generics or types nested inside generics."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32067 = 32067

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Local variables within generic methods cannot be declared 'Static'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32068 = 32068

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type parameters cannot be used in Declares."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32069 = 32069

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' implicitly defines a member '|3' which has the same name as a type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32070 = 32070

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Constraint type '|1' already specified for this type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32071 = 32071

    ''' <summary>
    ''' VBNC = "Type argument '{0}' must have a public parameterless instance constructor to satisfy the 'New' constraint for type parameter '{1}'."
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32083 = 32083

    ''' <summary>
    ''' VBNC = "Type parameter '{0}' must have either a 'New' constraint or a 'Structure' constraint to satisfy the 'New' constraint for type parameter '{1}'."
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32084 = 32084

    ''' <summary>
    ''' VBNC = "Arguments cannot be passed when calling a type parameter constructor."
    ''' VB   = "Arguments cannot be passed to a 'New' used on a type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32085 = 32085

    ''' <summary>
    ''' VBNC = "Type parameters can't be used as a qualifier."
    ''' VB   = "Type parameters cannot be used as qualifiers."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32098 = 32098

    ''' <summary>
    ''' VBNC = "'Class' constraint cannot be specified multiple times for the same type parameter."
    ''' VB   = "'Class' constraint cannot be specified multiple times for the same type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32101 = 32101

    ''' <summary>
    ''' VBNC = "'Structure' constraint cannot be specified multiple times for the same type parameter."
    ''' VB   = "'Structure' constraint cannot be specified multiple times for the same type parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32102 = 32102

    ''' <summary>
    ''' VBNC = "'New' constraint and 'Structure' constraint cannot be used at the same time."
    ''' VB   = "'New' constraint and 'Structure' constraint cannot be combined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32103 = 32103

    ''' <summary>
    ''' VBNC = "'Class' constraint and 'Structure' constraint cannot be used at the same time."
    ''' VB   = "'Class' constraint and 'Structure' constraint cannot be combined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32104 = 32104

    ''' <summary>
    ''' VBNC = "Type argument '{0}' does not satisfy the 'Structure' constraint for type parameter '{1}'"
    ''' VB   = "Type argument '|1' does not satisfy the 'Structure' constraint for type parameter '|2'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32105 = 32105

    ''' <summary>
    ''' VBNC = "Type argument '{0}' does not satisfy the 'Class' constraint for type parameter '{1}'"
    ''' VB   = "Type argument '|1' does not satisfy the 'Class' constraint for type parameter '|2'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32106 = 32106

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be declared 'Shadows' outside of a class, structure, or interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32200 = 32200

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property parameters cannot have the name 'Value'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32201 = 32201

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot override '|2' because it expands the access of the base method."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32203 = 32203

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Event name length cannot exceed 1011 characters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32204 = 32204

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "WithEvents variable name length cannot exceed 1019 characters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32205 = 32205

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Object no longer exists due to compile error or deletion."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32300 = 32300

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Project has been closed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32301 = 32301

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "There is already a class named '|1'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32302 = 32302

    ''' <summary>
    ''' VBNC = "The 'DefaultMemberAttribute' defined on '{0}' specifies '{1}' as the default member, while the property marked as the default property is '{2}'."
    ''' VB   = "Conflict between the default property and the 'DefaultMemberAttribute' defined on '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32304 = 32304

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' could not be created: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32400 = 32400

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be applied because the format of the GUID '|2' is not correct."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32500 = 32500

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.ComClassAttribute' and '|1' cannot both be applied to the same class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32501 = 32501

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.ComClassAttribute' cannot be applied to '|1' because its container '|2' is not declared 'Public'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32504 = 32504

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Runtime.InteropServices.DispIdAttribute' cannot be applied to '|1' because 'Microsoft.VisualBasic.ComClassAttribute' reserves zero for the default property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32505 = 32505

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'System.Runtime.InteropServices.DispIdAttribute' cannot be applied to '|1' because 'Microsoft.VisualBasic.ComClassAttribute' reserves values less than zero."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32506 = 32506

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'InterfaceId' and 'EventsId' parameters for 'Microsoft.VisualBasic.ComClassAttribute' on '|1' cannot have the same value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32507 = 32507

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.ComClassAttribute' cannot be applied to a class that is declared 'MustInherit'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32508 = 32508

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.ComClassAttribute' cannot be applied to '|1' because it is not declared 'Public'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC32509 = 32509

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operator declaration must be one of:  +, -, *, \\, /, ^, &amp;, Like, Mod, And, Or, Xor, Not, &lt;&lt;, &gt;&gt;, =, &lt;&gt;, &lt;, &lt;=, &gt;, &gt;=, CType IsTrue, IsFalse."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33000 = 33000

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Widening' and 'Narrowing' cannot be combined."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33001 = 33001

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operator is not overloadable. Operator declaration must be one of:  +, -, *, \\, /, ^, &amp;, Like, Mod, And, Or, Xor, Not, &lt;&lt;, &gt;&gt;, =, &lt;&gt;, &lt;, &lt;=, &gt;, &gt;=, CType, IsTrue, IsFalse."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33002 = 33002

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Handles' is not valid on Operator declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33003 = 33003

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Implements' is not valid on Operator declaration."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33004 = 33004

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Operator' expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33005 = 33005

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Operator' must be the first statement on a line."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33006 = 33006

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Operator' must be preceded by a matching 'Operator'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33007 = 33007

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Exit Operator' is not valid. Use 'Return' to exit an Operator."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33008 = 33008

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 parameters cannot be declared 'ParamArray'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33009 = 33009

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 parameters cannot be declared 'Optional'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33010 = 33010

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operators must be declared 'Public'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33011 = 33011

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operators must be declared 'Shared'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33012 = 33012

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operators cannot be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33013 = 33013

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operator '|1' must have one parameter."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33014 = 33014

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operator '|1' must have two parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33015 = 33015

    ''' <summary>
    ''' VBNC = "Operator '{0}' must have one or two parameters."
    ''' VB   = "Operator '|1' must have either one or two parameters."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33016 = 33016

    ''' <summary>
    ''' VBNC = "CType operator must specifiy 'Narrowing' or 'Widening'."
    ''' VB   = "Conversion operators must be declared either 'Widening' or 'Narrowing'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33017 = 33017

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operators cannot be declared in Modules."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33018 = 33018

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Only conversion operators can be declared '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33019 = 33019

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Parameter type of this unary operator must be the enclosing type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33020 = 33020

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "At least one parameter type of this binary operator must be the enclosing type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33021 = 33021

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Either the parameter or return type of this conversion operator must be the enclosing type '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33022 = 33022

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Operator '|1' must have a return type of Boolean."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33023 = 33023

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert to the same type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33024 = 33024

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert to an interface type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33025 = 33025

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert to a base type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33026 = 33026

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert to a derived type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33027 = 33027

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert to Object."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33028 = 33028

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert from an interface type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33029 = 33029

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert from a base type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33030 = 33030

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert from a derived type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33031 = 33031

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Conversion operators cannot convert from Object."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33032 = 33032

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "A matching '|1' operator is required for '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33033 = 33033

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "To be applicable as a short circuit operator, the return and parameter types of '|1' must be '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33034 = 33034

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' must have a declaration of operator '|2' to be usable in a short circuit expression."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33035 = 33035

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' and type '|2' both declare '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33036 = 33036

    ''' <summary>
    ''' VBNC = "Type '{0}' must be a value type or a type argument constrained to 'Structure' in order to be used with 'Nullable' or nullable modifier '?'. "
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33101 = 33101

    ''' <summary>
    ''' VBNC = "'If' operator requires either two or three operands."
    ''' VB   = "'If' operator requires either two or three operands."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33104 = 33104

    ''' <summary>
    ''' VBNC = "Cannot infer a common type for the second and third operands of the 'If' operator. One must have a widening conversion to the other."
    ''' VB   = "Cannot infer a common type for the second and third operands of the 'If' operator. One must have a widening conversion to the other."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33106 = 33106

    ''' <summary>
    ''' VBNC = "First operand in a binary 'If' expression must be nullable or a reference type."
    ''' VB   = "First operand in a binary 'If' expression must be nullable or a reference type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33107 = 33107

    ''' <summary>
    ''' VBNC = "Cannot infer a common type for the first and second operands of the binary 'If' operator. One must have a widening conversion to the other."
    ''' VB   = "Cannot infer a common type for the first and second operands of the binary 'If' operator. One must have a widening conversion to the other."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC33110 = 33110

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Global' must be followed by '.' and an identifier."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36000 = 36000

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Global' not allowed in this context; identifier expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36001 = 36001

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Global' not allowed in handles; local name expected."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36002 = 36002

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' has the DefaultInstanceProperty attribute but no valid property '|2' was found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36003 = 36003

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|2' has the DefaultInstanceProperty attribute but the type '|3' of '|1' property doesn't match the class type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36004 = 36004

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'ElseIf' must be preceded by a matching 'If' or 'ElseIf'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36005 = 36005

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute constructor has a 'ByRef' parameter of type '|1'; cannot use constructors with byref parameters to apply the attribute."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36006 = 36006

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'End Using' must be preceded by a matching 'Using'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36007 = 36007

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Using' must end with a matching 'End Using'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36008 = 36008

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'GoTo |1' is not valid because '|1' is inside a 'Using' statement that does not contain this statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36009 = 36009

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Using' operand of type '|1' must implement System.IDisposable"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36010 = 36010

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Using' resource variable must have an explicit initialization."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36011 = 36011

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Using' resource variable type can not be array type."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36012 = 36012

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'On Error' statements are not valid within 'Using' statements."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36013 = 36013

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' specified in MyCollection attribute of '|2' can not be found."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36014 = 36014

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = " '|1' collection member conflicts with an existing member with the same name in class '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36015 = 36015

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Implicit variable '|1' is invalid because of '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36016 = 36016

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|2' has the DefaultInstanceProperty attribute but the property '|1' is not 'Shared'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36017 = 36017

    ''' <summary>
    ''' VBNC = "Data type(s) of the type parameter(s) in method '{0}' cannot be inferred from these arguments because more than one type is possible. Specifying the data type(s) explicitly might correct this error."
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36651 = 36651

    ''' <summary>
    ''' VBNC = "Data type(s) of the type parameter(s) in method '{0}' cannot be inferred from these arguments because they do not convert to the same type. Specifying the data type(s) explicitly might correct this error."
    ''' VB   = "(No corresponding vbc error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC36657 = 36657

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is obsolete: '|2'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40000 = 40000

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' shadows an overloadable member declared in the base |3 '|4'.  If you want to overload the base method, this method must be declared 'Overloads'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40003 = 40003

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' conflicts with |3 '|2' in the base |4 '|5' and so should be declared 'Shadows'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40004 = 40004

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' shadows an overridable method in a base class. To override the base method, this method must be declared 'Overrides'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40005 = 40005

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Default property '|1' conflicts with the default property '|2' in the base |3 '|4'. 'Shadows' of default assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40007 = 40007

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40008 = 40008

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Possible problem detected while building assembly: |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40009 = 40009

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Possible problem detected while building assembly '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40010 = 40010

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.ComClassAttribute' is specified for class '|1' but '|1' has no public members that can be exposed to COM; therefore, no COM interfaces are generated."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40011 = 40011

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' implicitly declares '|3', which conflicts with a member in the base |4 '|5', and so the |1 should be declared 'Shadows'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40012 = 40012

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' conflicts with a member implicitly declared for |3 '|4' in the base |5 '|6' and so should be declared 'Shadows'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40014 = 40014

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' implicitly declares '|3', which conflicts with a member implicitly declared for |4 '|5' in the base |6 '|7'. So the |1 should be declared 'Shadows'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40018 = 40018

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' accessor of '|2' is obsolete: '|3'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40019 = 40019

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' accessor of '|2' is obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40020 = 40020

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' conflicts with |3 '|2' in the base |4 '|5' and so should not be declared 'Overloads'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40021 = 40021

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' implicitly declares '|3', which conflicts with a member in the base |4 '|5', and so the |1 should not be declared 'Overloads'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40022 = 40022

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' conflicts with a member implicitly declared for |3 '|4' in the base |5 '|6' and so should not be declared 'Overloads'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40023 = 40023

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' implicitly declares '|3', which conflicts with a member implicitly declared for |4 '|5' in the base |6 '|7'. So the |1 should not be declared 'Overloads'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40024 = 40024

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type of member '|1' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40025 = 40025

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not CLS compliant because it derives from '|2', which is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40026 = 40026

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Return type of method '|1' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40027 = 40027

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type of parameter '|1' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40028 = 40028

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' is not CLS compliant because the interface '|2' it implements is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40029 = 40029

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' cannot be marked CLS compliant because its containing type '|3' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40030 = 40030

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Name '|1' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40031 = 40031

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Underlying type '|1' of enum is not CLS Compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40032 = 40032

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Non CLS compliant |1 is not allowed in a CLS compliant interface."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40033 = 40033

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Non CLS compliant mustoverride member is not allowed in a CLS compliant |1."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40034 = 40034

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' which overloads '|2' differs from it only by array of array parameter types or by the rank of the the array parameter types and so is not CLS Compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40035 = 40035

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property '|1' is not CLS Compliant because its 'Get' accessor does not have the same access level as the property itself."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40036 = 40036

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Property '|1' is not CLS Compliant because its 'Set' accessor does not have the same access level as the property itself."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40037 = 40037

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Root namespace '|1' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40038 = 40038

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Name '|1' in the root namespace '|2' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40039 = 40039

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Generic parameter constraint type '|1' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40040 = 40040

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Type '|1' is not CLS compliant."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40041 = 40041

    ''' <summary>
    ''' VBNC = "Namespace or type specified in the Imports '{0}' doesn't contain any public member or cannot be found. Make sure the namespace or the type is defined and contains at least one public member. Make sure the imported element name doesn't use any aliases."
    ''' VB   = "Namespace or type specified in the Imports 'Foo' doesn't contain any public member or cannot be found. Make sure the namespace or the type is defined and contains at least one public member. Make sure the imported element name doesn't use any aliases."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC40056 = 40056

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Attribute '|1' cannot be specified more than once in this project, even with identical parameter values."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC41000 = 41000

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' should declare a 'Sub New' because the '|2' in its base class '|3' is marked obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC41001 = 41001

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Class '|1' should declare a 'Sub New' because the '|2' in its base class '|3' is marked obsolete: '|4'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC41002 = 41002

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "First statement of this 'Sub New' should be an explicit call to 'MyBase.New' or 'MyClass.New' because the '|1' in the base class '|2' of '|3' is marked obsolete."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC41003 = 41003

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "First statement of this 'Sub New' should be an explicit call to 'MyBase.New' or 'MyClass.New' because the '|1' in the base class '|2' of '|3' is marked obsolete: '|4'"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC41004 = 41004

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "|1 '|2' conflicts with other members of the same name across the inheritance hierarchy and so should be declared 'Shadows'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42000 = 42000

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Class_Initialize' event is no longer supported. Use 'Sub New' to initialize a class."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42001 = 42001

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Class_Terminate' event is no longer supported. Use 'Sub Finalize' to free resources."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42002 = 42002

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' and '|2' cannot overload each other because they differ only by parameters declared 'ByRef' or 'ByVal'. 'Shadows' assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42003 = 42003

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Interface '|1' is already implemented by base class '|2'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42013 = 42013

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1.|2' from 'implements |3' is already implemented by the base class '|4'. Re-implementation of |5 assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42014 = 42014

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1.|2' is already implemented by the base class '|3'. Re-implementation of |4 assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42015 = 42015

    ''' <summary>
    ''' VBNC = "Variable declaration without an 'As' clause; type of Object assumed."
    ''' VB   = "Variable declaration without an 'As' clause; type of Object assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42020 = 42020

    ''' <summary>
    ''' VBNC = "Function without an 'As' clause; return type of Object assumed."
    ''' VB   = "Function without an 'As' clause; return type of Object assumed."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42021 = 42021

    ''' <summary>
    ''' VBNC = "Unused local variable: '{0}'."
    ''' VB   = "Unused local variable: '|1'."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42024 = 42024

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'Microsoft.VisualBasic.ComClassAttribute' on class '|1' implicitly declares |2 '|3', which conflicts with a member of the same name in |4 '|5'. Use 'Microsoft.VisualBasic.ComClassAttribute(InterfaceShadows:=True)' if you want to hide the name on the base |5."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42101 = 42101

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "'|1' cannot be exposed to COM as a property 'Let'. You will not be able to assign non-object values (such as numbers or strings) to this property from Visual Basic 6.0 using a 'Let' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42102 = 42102

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Variable '|1' is being used before it has been assigned a value."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42103 = 42103

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Variable '|1' is being used before it has been assigned a value. A null reference exception could result at runtime."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42104 = 42104

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Method '|1' has a link demand, but overrides or implements the following methods which do not have a link demand. A security vulnerability may exist: |2"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42200 = 42200

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment block must immediately precede the language element to which it applies."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42300 = 42300

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Only one XML comment block is allowed per language element."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42301 = 42301

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment must be the first statement on a line."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42302 = 42302

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment cannot appear within a method or a property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42303 = 42303

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML documentation parse error: |1"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42304 = 42304

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment tag '|1' appears with identical attributes more than once in the same XML comment block"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42305 = 42305

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment tag '|1' is not permitted on a '|2' language element."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42306 = 42306

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment parameter '|1' does not match a parameter on the corresponding '|2' statement."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42307 = 42307

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment parameter must have a 'name' attribute."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42308 = 42308

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment has a tag with a 'cref' attribute '|1' that could not be resolved."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42309 = 42309

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment tag 'include' must have a '|1' attribute."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42310 = 42310

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "Unable to create XML documentation file '|1': |0"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42311 = 42311

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment block cannot be associated with any valid language element"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42312 = 42312

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment tag 'returns' is not permitted on a 'WriteOnly' Property."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42313 = 42313

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment cannot appear on an expanding |1."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42314 = 42314

    ''' <summary>
    ''' VBNC = "CHANGEME"
    ''' VB   = "XML comment tag 'returns' is not permitted on a 'declare sub' language element."
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC42315 = 42315

    ''' <summary>
    ''' VBNC = "Newline character found in date constant."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90000 = 90000

    ''' <summary>
    ''' VBNC = "Unexpected end of file."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90001 = 90001

    ''' <summary>
    ''' VBNC = "'{0}' type character is not valid here."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90002 = 90002

    ''' <summary>
    ''' VBNC = "End of line found in string constant."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90003 = 90003

    ''' <summary>
    ''' VBNC = "End of file found in string constant."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90004 = 90004

    ''' <summary>
    ''' VBNC = "Invalid literal."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90005 = 90005

    ''' <summary>
    ''' VBNC = "Invalid {0} literal."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90006 = 90006

    ''' <summary>
    ''' VBNC = "Unexpected token: {0}."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90007 = 90007

    ''' <summary>
    ''' VBNC = "No more code?"
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90008 = 90008

    ''' <summary>
    ''' VBNC = "Cannot find '{0}'."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90009 = 90009

    ''' <summary>
    ''' VBNC = "Cannot have both '{0}' and '{1}'."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90010 = 90010

    ''' <summary>
    ''' VBNC = "Expected: '{0}'"
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90011 = 90011

    ''' <summary>
    ''' VBNC = "Could not load the assembly: '{0}'"
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90012 = 90012

    ''' <summary>
    ''' VBNC = "Error finding class '{0}' with the Main function, found {1} classes with this name."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90013 = 90013

    ''' <summary>
    ''' VBNC = "Expected 'While' or 'Until'."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90014 = 90014

    ''' <summary>
    ''' VBNC = "Can't use the modifier '{0}' here."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90015 = 90015

    ''' <summary>
    ''' VBNC = "Feature not implemented: {0}"
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90016 = 90016

    ''' <summary>
    ''' VBNC = "Invalid define: '{0}'"
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90017 = 90017

    ''' <summary>
    ''' VBNC = "Expected end of line."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90018 = 90018

    ''' <summary>
    ''' VBNC = "Expected '{0}'."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90019 = 90019

    ''' <summary>
    ''' VBNC = "Expected 'Get' or 'Set'."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90020 = 90020

    ''' <summary>
    ''' VBNC = "Invalid data type: '{0}'."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90021 = 90021

    ''' <summary>
    ''' VBNC = "Unexpected end of file in comment."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90022 = 90022

    ''' <summary>
    ''' VBNC = "There is no comparison possible between '{0}' and '{1}'."
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC90023 = 90023

    ''' <summary>
    ''' VBNC = "You've encountered something in the compiler which is not implemented. Please file a bug (see instructions here: http://mono-project.com/Bugs)"
    ''' VB   = "(no corresponding VB warning)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC99997 = 99997

    ''' <summary>
    ''' VBNC = "{0}"
    ''' VB   = "(no corresponding VB warning)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Warning)> VBNC99998 = 99998

    ''' <summary>
    ''' VBNC = "{0}"
    ''' VB   = "(no corresponding VB error)"
    ''' </summary>
    ''' <remarks></remarks>
    <Message(MessageLevel.Error)> VBNC99999 = 99999

End Enum
