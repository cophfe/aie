using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------------
//--------------------------------------------------------------
class Player //this class is the 'blueprint' for a player object. define an instance of a player object based on the class with var p = new Player();
{
	//good practive is to never use public variables (debugging purposes- variables are only changed through functions so it only goes wrong inside 1 function, not in any random place)
	private bool m_alive = true; //variables are private by default
	private int m_health = 100, m_level = 1, m_xp = 0;
	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public Player(int health = 100, int level = 0)
	{
		Console.WriteLine($"Initialized with {health} health and {level} levels");
		m_health = health; //this.health is the health defined outside of this Constructor, health is the one within the object
		m_level = level;
	}
	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public void DealDamage(int damageAmt)
	{
		if (!m_alive)
			return;

		m_health -= damageAmt;
		if (m_health <= 0)
		{
			m_health = 0;
			m_alive = false;
		}
	}
	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public void XPChange(int amount)
	{
		if (!m_alive)
			return;

		m_xp += amount;


		while (m_xp >= 100)
		{
			m_xp -= 100;
			m_level++;
		}

		if (m_xp <= 0)
		{
			m_level--;
			m_xp += 100;
		}
	}
	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public void PrintStats()
	{
		Console.WriteLine("Health is "+ m_health +", Level is " + m_level + ", XP is " + m_xp);
	}
}

