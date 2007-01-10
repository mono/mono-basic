imports system
'interface inheritance

interface Interface3
	sub a
end interface

interface Interface3Inherited
	inherits Interface3
	sub b
end interface

interface Interface3ToBeInherited
	sub c
end interface

interface Interface3MultiInherited
	inherits Interface3Inherited, Interface3ToBeInherited
	sub d
end interface