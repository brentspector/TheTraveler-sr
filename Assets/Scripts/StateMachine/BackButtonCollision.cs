using UnityEngine;
using System.Collections;

namespace GSP
{
	public class BackButtonCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.ChangeMenuState (BrentsStateMachine.MENUSTATES.HOME);
		} //end OnMouseDown()
	} //end BackButtonCollision
} //end namespace GSP
