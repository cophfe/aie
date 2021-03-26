using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Animal
{
	private string _name;
	private float _speed, _health;
	public Animal(string name, float speed, float health)
	{
		_speed = speed;
		_name = name;
		_health = health;
	}

	public void Print()
	{
		Console.WriteLine($"Animal: {_name}\nSpeed: {_speed}\nHealth: {_health}\n");
	}

	public float GetSpeed()
	{
		return _speed;
	}

	public void SetSpeed(float speed)
	{
		_speed = speed;
	}

}

