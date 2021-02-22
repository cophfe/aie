using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Common
{
	protected string _name = "";
	//protected is like private but classes that inherit can access it
	//Good practice to initialize variables (minimize bugs)

	public Common()
	{

	} 
	public Common(string name)
	{
		_name = name;
	}

	//virtual means inheriting classes can modify the function
	//abstract means their is no default, and each inheriting class has to override the function
	public virtual void PrintStats()
	{
		Console.WriteLine("Name: {0}", _name);
	}
}

