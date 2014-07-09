using UnityEngine;
using System.Collections;

public class PlayerProfile : MonoBehaviour 
{
	private static PlayerProfile _instance ;
	public static PlayerProfile Instance 
	{
		get 
		{
			return _instance ;
		}
	}		
	private void Awake () 
	{			
		_instance = this ;
	}

	private uint score = 0;
	public uint Score
	{
		get 
		{
			return score;
		}
		set
		{
			score = value;
		}
	}

	private uint money = 0;
	public uint Money
	{
		get 
		{
			return money;
		}
		set
		{
			money = value;
		}
	}
}
