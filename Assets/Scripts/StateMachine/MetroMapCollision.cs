using UnityEngine;
using System.Collections;

namespace GSP
{
	public class MetroMapCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.m_mapSelection = "area03";
		} //end OnMouseDown()
	} //end MetroMapCollision
} //end namespace GSP
