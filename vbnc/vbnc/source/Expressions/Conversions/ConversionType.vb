' 
' Visual Basic.Net Compiler
' Copyright (C) 2011 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Public Enum CTypeConversionType
    Undetermined
    Identity
    Intrinsic
    ToNullable
    FromNullable
    NullableToNullable
    Castclass
    Unbox
    Unbox_Ldobj
    Unbox_Any
    Box
    Box_CastClass
    MS_VB_CS_Conversions_ToGenericParameter
    UserDefinedOperator
End Enum

