
Imports System
Imports Microsoft.VisualBasic.CompilerServices
Imports NUnit.Framework

<TestFixture()> _
Public Class ConversionsTests

    <Test(), ExpectedException(GetType(InvalidCastException))> _
    Sub ToCharArrayRankOne()
        Dim chars() As Char

        chars = Conversions.ToCharArrayRankOne(CType("dog", Object))
        Assert.AreEqual("dog".ToCharArray, chars)

        chars = Conversions.ToCharArrayRankOne("dog")
        Assert.AreEqual("dog".ToCharArray, chars)

        chars = Conversions.ToCharArrayRankOne(Nothing)
        Assert.AreEqual("".ToCharArray, chars)

        chars = Conversions.ToCharArrayRankOne(CType(Nothing, String))
        Assert.AreEqual("".ToCharArray, chars)

        Conversions.ToCharArrayRankOne(5) 'ExpectedException: InvalidCastException

    End Sub
End Class

