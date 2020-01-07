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

	//Should the patrol have a given amplitude
	bool isPatrolLimited
	{
		get;
		set;
	}

	//The amplitude of the limited patrol
	float patrolMagnitude
	{
		get;
		set;
	}
}