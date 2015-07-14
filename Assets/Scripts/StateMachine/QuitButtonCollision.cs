using UnityEngine;
using System.Collections;

namespace GSP
{
	public class QuitButtonCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.ChangeMenuState (BrentsStateMachine.MENUSTATES.QUIT);
		} //end OnMouseDown()
	} //end QuitButtonCollision
} //end namespace GSP