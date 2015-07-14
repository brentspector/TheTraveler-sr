using UnityEngine;
using System.Collections;

namespace GSP
{
	public class CreditsButtonCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.ChangeMenuState (BrentsStateMachine.MENUSTATES.CREDITS);
		} //end OnMouseDown()
	} //end CreditsButtonCollision
} //end namespace GSP
