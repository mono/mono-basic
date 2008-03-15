Imports System.Reflection.Emit
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Collections
Imports System.Collections.Generic

Namespace Ciloci.Flee

	Friend Class PairEqualityComparer
		Inherits EqualityComparer(Of ExpressionResultPair)

		Public Overloads Overrides Function Equals(ByVal x As ExpressionResultPair, ByVal y As ExpressionResultPair) As Boolean
			'Return String.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase)
		End Function

		Public Overloads Overrides Function GetHashCode(ByVal obj As ExpressionResultPair) As Integer
			'Return StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Name)
		End Function
	End Class

	Friend MustInherit Class ExpressionResultPair

	
	End Class

End Namespace
