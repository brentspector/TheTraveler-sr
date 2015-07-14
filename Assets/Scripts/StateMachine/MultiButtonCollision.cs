using UnityEngine;
using System.Collections;

namespace GSP
{
	public class MultiButtonCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.ChangeMenuState (BrentsStateMachine.MENUSTATES.MULTI);
		} //end OnMouseDown()
	} //end MultiButtonCollision
} //end namespace GSP
