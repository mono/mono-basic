class T
	shared function Main () as integer
		return b("f"c)
	end function

	shared function B(byref c as char) as integer
		if c = "f"c then return 0
		return 1
	end function
end class