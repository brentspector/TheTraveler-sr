using UnityEngine;
using System.Collections;

namespace GSP
{
	public class SnowMapCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.m_mapSelection = "area04";
		} //end OnMouseDown()
	} //end SnowMapCollision
} //end namespace GSP
