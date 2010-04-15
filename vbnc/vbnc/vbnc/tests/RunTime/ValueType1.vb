class S
	structure ST
		dim i as integer
	end structure
	shared sub test (v as valuetype)
	end sub
	shared sub Main ()
		dim i as st
		test (i)
	end sub
end class
