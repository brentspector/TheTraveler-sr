using UnityEngine;
using System.Collections;

namespace GSP
{
	public class DesertMapCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.m_mapSelection = "area01";
		} //end OnMouseDown()
	} //end DesertMapCollision
} //end namespace GSP
