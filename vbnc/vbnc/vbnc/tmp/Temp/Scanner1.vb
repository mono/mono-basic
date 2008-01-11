public class Scanner1
'comment
rem comment
'identifiers:
	dim identifier as integer
'identifers with type characters. typos are there so that won't be a keyword
	'dim int% 
	'dim lng& 
	'dim deciml@ 
	'dim singl! 
	'dim dbl# 
	'dim str$
	'Type characters aren't supported at this time
'keywords:
	sub scannersub
		do while nothing is nothing or nothing isnot nothing and not nothing is nothing xor nothing is nothing
			'is this really an infinite loop?
		loop
		for i as integer = 0 to -1
			continue For
		next		
	end sub
'literals
' - boolean literals
	sub bool
		dim b as boolean
		b = true
		b = false
		b = true and false
		b = true xor false
		b = true or false
	end sub
' - integer literals
	sub int
		dim b as byte
		b = 0
		b = 255
		dim sb as sbyte
		sb = -128
		sb = 127
		dim s as short
		s = -32000
		s = 32000
		s = -32000S
		s = 32000s
		dim us as ushort
		us = 0
		us = 64000
		us = 0uS
		us = 64000US
		dim i as integer
		i = 2355
		i = 1455I
		i = 2355i
		i = -1245666I
		i = 123414%
		dim ui as uinteger
		ui = 0
		ui = 4000000
		ui = 0UI
		ui = 1500000Ui
		dim l as long
		l = -12535555112332
		l = 21454521355235
		l = -12341234124l
		l = 123444155234L
		l = 1233&
		dim ul as ulong
		ul = 0
		ul = 23412414441234
		ul = 0uL
		ul = 213515535324234Ul
		dim hex as integer
		hex = &H012345678
		hex = &HABCDEF
		hex = &hA9b9E7f
		hex = &h1S
		hex = &h2us
		hex = &h3i
		hex = &h4UI
		hex = &h5l
		hex = &h6Ul
		hex = &h7%
		dim oct as integer
		oct = &O01234567
		oct = &o1S
		oct = &o2us
		oct = &o3i
		oct = &o4UI
		oct = &o5l
		oct = &o6Ul
		oct = &O7%
		dim bin as integer
		bin = &B100110011101
		bin = &b1S
		bin = &b10us
		bin = &b101i
		bin = &b111UI
		bin = &B110l
		bin = &b011Ul
		bin = &b1001%
	end sub
' - floating point literals
	sub fp
		dim s as single
		s = 14421.123
		s = -12441.1441
		s = 1244.122!
		s = 1244.123E2
		s = 233e2
		s = 234e-2f
		s = 235E+55
		s = 1233.1E-2F
		s = 1233.2E+123
		s = .1233e4!
		s = .144e-3
		s = .344E+12F
		s = .1e1f
		s = 2E2F
		dim d as double
		d = 14421.123
		d = -12441.1441
		d = 1244.122R
		d = 1244.123E2
		d = 233e2
		d = 234e-2#
		d = 235E+55
		d = 1233.1E-2R
		d = 1233.2E+123
		d = .1233e4r
		d = .144e-3
		d = .344E+124R
		d = .1e1#
		d = 2E2R
		dim c as decimal
		c = 14421.123
		c = -12441.1441
		c = 1244.122D
		c = 1244.123E2
		c = 233e2
		c = 234e-2@
		c = 235E+5
		c = 1233.1E-2D
		c = 1233.2E+12
		c = .1233e4d
		c = .144e-3
		c = .344E+12D
		c = .1e1@
		c = 2E2D
	end sub
'string literals
	sub strltrls
		dim s as string
		s = "one"
		s = "two"
		s = "char""in string"
		s = """"
		s = "sev""""instr"
	end sub
'character literals
	sub chrltr
		dim c as char
		c = "c"c
		c = "D"C	
	end sub
'date literals
	sub dtltrls
		dim d as date
		d = # 1-1-1 #
		d = # 01-01-2000 #
		d = # 12-31-1999 #
		d = # 06-06-1950 #
		d = # 2/2/2 #
		d = # 12:12:12 #
		d = # 12:14:16 #
		d = # 12:12 #
		d = # 10:10:10 aM #
		d = # 1:1:1 Pm #
		d = # 1/1/1 9:9:9 AM #
		d = #	1/2/2003 4:4:4 pm#
		d = #1-2-3000		3:4:5	#
	end sub
'nothing literal
	sub nothng
		if nothing is nothing then 
		end if
	end Sub
'separators
'don't really know how to come up with compilable tests for this one...
'               ( ) ! # , . : 
	sub sep
		dim i as integer
		i = (2)
		'i = i!integer 'this is not equal to "i! integer" !!!
		'i = i#identifier
		'i = (new integer(){i,2,3,3})(2)
		i = 2:i=2:i=3
	end sub
'operators
	sub ops
		dim i as double
		i = i & i
		i = i*i
		i = 1+2
		i = 2-3
		i = 3/4
		i = 4 \ 	5
		i = 5 ^6
		i = 6 < 7
		i = 7 > 8
		i = 8 = 9
	end Sub
End class