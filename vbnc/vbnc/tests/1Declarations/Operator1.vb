'operator overloading

class Operator1
	shared operator =(value1 as operator1, value2 as operator1) as boolean
	end operator
	shared operator <>(value1 as operator1, value2 as operator1) as boolean
	end operator
	shared operator +(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator -(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator +(value1 as operator1) as operator1
	end operator
	shared operator -(value1 as operator1) as operator1
	end operator
	shared operator ^(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator *(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator /(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator \(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator &(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator >=(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator <=(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator <(value1 as operator1, value2 as operator1) as boolean
	end operator
	shared operator >(value1 as operator1, value2 as operator1) as boolean
	end operator
	shared narrowing operator ctype(value1 as operator1) as integer
	end operator
	shared widening operator ctype(value1 as operator1) as long
	end operator
	shared operator <<(value1 as operator1, value2 as integer) as operator1
	end operator
	shared operator >>(value1 as operator1, value2 as integer) as operator1
	end operator
	shared operator istrue(value1 as operator1) as boolean
	end operator
	shared operator isfalse(value1 as operator1) as boolean
	end operator
	shared operator not(value1 as operator1) as operator1
	end operator
	shared operator like(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator and(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator or(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator xor(value1 as operator1, value2 as operator1) as operator1
	end operator
	shared operator mod(value1 as operator1, value2 as operator1) as operator1
	end operator
	'quite a list!
end class