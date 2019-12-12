using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatrollable
{	
	//Patrol state, 0 means stop, 1 means right and 2 left
	int patrolState
	{
		get;
		set;
	}
}