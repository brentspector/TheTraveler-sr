using UnityEngine;
using System.Collections;

namespace GSP
{
	public class HelpButtonCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.ChangeMenuState (BrentsStateMachine.MENUSTATES.HELP);
		} //end OnMouseDown()
	} //end HelpButtonCollision
} //end namespace GSP
