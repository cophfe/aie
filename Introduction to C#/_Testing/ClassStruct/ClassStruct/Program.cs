using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*//----------------------------------------
	Value type: float, int, bool, char  
	
		Assignment: copies a value
-------------------------------------------- 
	Reference type: class

		Assignment: copies a reference
*///----------------------------------------
class Program
{
	static void FunctionFunction(ClassClass cC, StructScruct sS)
	{
		cC._number = 50;
		sS._number = 50;
		Console.WriteLine($"Class : {cC._number}, Struct: {sS._number}");
	}
	static void Main(string[] args)
	{
		ClassClass _cC = new ClassClass();
		StructScruct _sS = new StructScruct();
		_cC._number = 100;
		_sS._number = 100;
		Console.WriteLine($"Class : {_cC._number}, Struct: {_sS._number}");
		FunctionFunction(_cC, _sS);
		Console.WriteLine($"Class : {_cC._number}, Struct: {_sS._number}");
		Console.ReadLine();
	}
}

