using UnityEngine;
using System.Collections;
using GSP.Entities.Neutrals;
using GSP.Entities;

namespace GSP.Char
{
	public class Player : MonoBehaviour
	{
        Merchant m_merchant;    // Each player needs a merchant object.

        // Called before Start()
        void Awake()
        {
            //
        }
        
        // Use this for initialisation
        void Start()
        {
            //
        }

        public void GetMerchant(int ID)
        {
            // Get the merchant's reference.
            m_merchant = (Merchant)EntityManager.Instance.GetEntity(ID);
        }
        
        // Allows for collision on the market place to end the game.
        void OnCollisionEnter2D(Collision2D coll)
        {
            Debug.LogError("CALLED!");
            // Layer 8 is "Market"
            if (coll.gameObject.layer == 8)
            {
                // Get the game object with the game state machine tag.
                GameObject obj = GameObject.FindGameObjectWithTag("GamePlayStateMachineTag");

                // Now get the state machine script.
                var stateMachineScript = obj.GetComponent<GameplayStateMachine>();

                // Finally end the game by calling the end game function.
                stateMachineScript.EndGame();
            }
        }
        
        // Destroy the game object this script is attached to.
		public void DestroyGO()
		{
			Destroy(this.gameObject);
        }

        #region Wrapper for the merchant class

        // Gets the merchant's name.
        public string Name
        {
            get
            {
                return m_merchant.Name;
            }
        }

        // Gets the merchant's colour.
        public PlayerColours Colour
        {
            get
            {
                return m_merchant.Colour;
            }
        }

        // Gets the merchant's number of allies.
        public int NumAllies
        {
            get
            {
                return m_merchant.NumAllies;
            }
        }

        // Gets and Sets the merchant's position.
        public Vector3 Position
        {
            get
            {
                return m_merchant.Position;
            }
            set
            {
                m_merchant.Position = this.gameObject.transform.position;
            }
        }

        // Setup the character's sprite set. This is an array of sprites that will be used for the character.
        public void SetCharacterSprites(int playerNumber)
        {
            m_merchant.SetCharacterSprites(playerNumber);
        }

        // Faces the merchant in a given direction. This changes the merchant's sprite to match this.
        public void Face(FacingDirection facingDirection)
        {
            m_merchant.Face(facingDirection);
        }

        #endregion
    }
}
