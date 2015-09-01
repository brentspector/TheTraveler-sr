﻿/*******************************************************************************
 *
 *  File Name: Player.cs
 *
 *  Description: Wrapper for the players
 *
 *******************************************************************************/
using GSP.Entities;
using GSP.Entities.Neutrals;
using UnityEngine;

namespace GSP.Char
{
    /*******************************************************************************
     *
     * Name: Player
     * 
     * Description: The wrapper to the merchant entity. This is what is placed in
     *              the scene.
     * 
     *******************************************************************************/
    public class Player : MonoBehaviour
	{
        Merchant merchant;    // Each Player needs a Merchant GameObject

        // Called before Start()
        void Awake()
        {
            //
        } // end Awake
        
        // Use this for initialisation
        void Start()
        {
            //
        } // end Start

        public void GetMerchant(int ID)
        {
            // Get the Merchant's reference
            merchant = (Merchant)EntityManager.Instance.GetEntity(ID);
        } // end GetMerchant

        // Updates the GameObject reference on the entity
        public void UpdateGameObject(GameObject obj)
        {
            merchant.UpdateGameObject(obj);
        } // end UpdateGameObject

        // Updates the script references on the entity
        public void UpdateScriptReferences()
        {
            merchant.UpdateScriptReferences();
        } // end UpdateScriptReferences
        
        // Allows for collision on the market place to end the game
        void OnCollisionEnter2D(Collision2D coll)
        {
			Debug.LogFormat("GameObject being collided with: {0}", coll.gameObject.name);
            // Layer 8 is the "Market"
            if (coll.gameObject.layer == 8)
            {
                // Get the GameplayStateMachine
				GameplayStateMachine stateMachineScript = GameObject.Find("Canvas").GetComponent<GameplayStateMachine>();

                // End the game by calling EndGame()
                stateMachineScript.EndGame();
            }
        } // end OnCollisionEnter2D
        
        // Destroy the GameObject this script is attached to
		public void DestroyGO()
		{
			Destroy(this.gameObject);
        } // end DestroyGO

        // Gets the player's entity
        // This is used to get the entity to do things with the implemented interfaces; just cast
        // back to the proper type first
        public Entity Entity
        {
            get { return merchant; }
        } // end Entity

        #region Wrapper for the merchant class

        // Setup the character's sprite set; This is an array of sprites that will be used for the character
        public void SetCharacterSprites(int playerNumber)
        {
            merchant.SetCharacterSprites(playerNumber);
        } // end SetCharacterSprites

        // Faces the merchant in a given direction; This changes the merchant's sprite to match this
        public void Face(FacingDirection facingDirection)
        {
            merchant.Face(facingDirection);
        } // end Face

        // Gets the Merchant's Name
        public string Name
        {
            get { return merchant.Name; }
        } // end Name

        // Gets the Merchant's Colour
        public InterfaceColors Color
        {
            get { return merchant.Color; }
        } // end Color

        // Gets the Merchant's number of allies
        public int NumAllies
        {
            get { return merchant.NumAllies; }
        } // end NumAllies

        // Gets and Sets the Merchant's Position
        public Vector3 Position
        {
            get { return merchant.Position; }
            set { merchant.Position = value; }
        }

        #endregion
    } // end Player
} // end GSP.Char
