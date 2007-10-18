imports system
'test interface nesting
friend interface Interface2
	interface INested1
		sub i
	end interface
	class CNested1
		sub i
		end sub
	end class
	structure SNested1
		dim field as string
	end structure
	enum l
		field
	end enum
end interface