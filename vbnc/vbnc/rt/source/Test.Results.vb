' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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

Partial Class Test
    Enum Results
        ''' <summary>
        ''' The test has not been run yet.
        ''' </summary>
        ''' <remarks></remarks>
        NotRun
        ''' <summary>
        ''' The test is running
        ''' </summary>
        ''' <remarks></remarks>
        Running
        ''' <summary>
        ''' Test failed.
        ''' </summary>
        ''' <remarks></remarks>
        Failed
        ''' <summary>
        ''' Test regressed.
        ''' </summary>
        ''' <remarks></remarks>
        Regressed
        ''' <summary>
        ''' Test succeeded.
        ''' </summary>
        ''' <remarks></remarks>
        Success
        ''' <summary>
        ''' Test is marked as known failure, but succeeded
        ''' </summary>
        ''' <remarks></remarks>
        KnownFailureSucceeded
        ''' <summary>
        ''' The test was skipped (doesn't apply for the current platform for instance)
        ''' </summary>
        ''' <remarks></remarks>
        Skipped
        ''' <summary>
        ''' Test is marked as known failure, and failed
        ''' </summary>
        ''' <remarks></remarks>
        KnownFailureFailed
    End Enum
End Class