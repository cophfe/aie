using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Horse : EnemyCommon
{
	protected float _speed = 0.0f;
	public Horse()
	{
		_speed = 100;
		_health = 100;
		_name = "horse";
	}
	public Horse(string name, int health, float speed) : base(name, health)
	{
		_speed = speed;
	}

	//sealed means it can not be overidden by inheriting classes
	public sealed override void PrintStats()
	{
		base.PrintStats();
		Console.WriteLine("Horse: neigh, i am supa fast");
	}
}

