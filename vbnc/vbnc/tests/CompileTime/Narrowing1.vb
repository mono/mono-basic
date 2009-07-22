Option Strict Off
Imports system

class Narrowing1
	shared function Main as integer
		dim i as object
		i = math.round (i, 2)
	end function
end class
