using UnityEngine;
using System.Collections;

namespace GSP
{
	public class EuroMapCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.m_mapSelection = "area02";
		} //end OnMouseDown()
	} //end EuroMapCollision
} //end namespace GSP
