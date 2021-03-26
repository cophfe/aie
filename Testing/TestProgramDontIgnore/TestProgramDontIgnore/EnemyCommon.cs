using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EnemyCommon : Common
{
	protected int _health = 0;
	public EnemyCommon()
	{
	}
	public EnemyCommon(string name, int health) : base(name)
	{
		_health = health;
	}

	//sealed means it can not be overidden by inheriting classes
	//override means it is overriding an "abstract" or "virtual" function from a class it inherits from
	public override void PrintStats()
	{
		base.PrintStats();
		Console.WriteLine($"Health: {_health}");
	}
}
