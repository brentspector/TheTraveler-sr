using UnityEngine;
using System.Collections;

namespace GSP
{
	public class SoloButtonCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.ChangeMenuState (BrentsStateMachine.MENUSTATES.SOLO);
		} //end OnMouseDown()
	} //end SoloButtonCollision
} //end namespace GSP
