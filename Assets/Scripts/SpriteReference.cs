/*******************************************************************************
 *
 *  File Name: SpriteReference.cs
 *
 *  Description: Loads and deals with Sprites at runtime
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: SpriteReference
     * 
     * Description: Manages the references to any srpites loaded from Resources.
     * 
     *******************************************************************************/
    public static class SpriteReference
	{
		/*
		 * Sprite References
		 */

		// This is the sprite sheet for the buttons.
        static Sprite[] buttonSpritesheet = Resources.LoadAll<Sprite>("buttons_sprite_sheet");

		// This is the reference to the menu backgrond sprite.
		public static Sprite spriteMenuBackground = buttonSpritesheet[0];

		// This is the reference to the intro backgrond sprite.
		public static Sprite spriteIntroBackground = buttonSpritesheet[1];

		// This is the reference to the start button sprite.
		public static Sprite spriteStart = buttonSpritesheet[2];

		// This is the reference to the exit button sprite.
		public static Sprite spriteExit = buttonSpritesheet[3];

		// This is the reference to the option button sprite.
		public static Sprite spriteOptions = buttonSpritesheet[4];

		// This is the reference to the continue button sprite.
		public static Sprite spriteContinue = buttonSpritesheet[5];

		// This is the reference to the menu button sprite.
		public static Sprite spriteMenu = buttonSpritesheet[6];

		// This is the reference to the credit button sprite.
		public static Sprite spriteCredit = buttonSpritesheet[7];

		// This is the reference to the multi button sprite.
		public static Sprite spriteMulti = buttonSpritesheet[8];

		// This is the reference to the solo button sprite.
		public static Sprite spriteSolo = buttonSpritesheet[9];

		// This is the reference to the back button sprite.
		public static Sprite spriteBack = buttonSpritesheet[10];

		// This is the reference to the Help button sprite.
		public static Sprite spriteHelp = buttonSpritesheet[11];

		// This is the sprite sheet for the map thumbnails.
        static Sprite[] mapThumbnails = Resources.LoadAll<Sprite>("map_thumbnails");

		// This is the reference to the credit button sprite.
		public static Sprite spriteMapDesert = mapThumbnails[0];

		// This is the reference to the credit button sprite.
		public static Sprite spriteMapSnowy = mapThumbnails[1];

		// This is the reference to the credit button sprite.
		public static Sprite spriteMapMetro = mapThumbnails[2];

		// This is the reference to the credit button sprite.
		public static Sprite spriteMapEuro = mapThumbnails[3];
		
		//Resizes to fit screen
		public static void ResizeSpriteToScreen(GameObject theSprite, Camera theCamera, int fitToScreenWidth, int fitToScreenHeight)
		{
			// Get the sprite's SpriteRenderer component.
			SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();
			
			// Set the scale to normal.
            theSprite.transform.localScale = new Vector3(1, 1, 1);
			
			// Get the sprite's width and height.
            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;
			
			// Get the world width and height.
            float worldScreenHeight = (float)(theCamera.orthographicSize * 2.0);
            float worldScreenWidth = (float)(worldScreenHeight / Screen.height * Screen.width);
			
			// Set the scale to fit the sprite to the screen.
            if (fitToScreenWidth != 0) 
			{
				// Get the width scale needed.
                Vector2 sizeX = new Vector2(worldScreenWidth / spriteWidth / fitToScreenWidth, theSprite.transform.localScale.y);
                theSprite.transform.localScale = sizeX;
			} // end if
            if (fitToScreenHeight != 0)
			{
				// Get the height scale needed.
                Vector2 sizeY = new Vector2(theSprite.transform.localScale.x, worldScreenHeight / spriteHeight / fitToScreenHeight);
				theSprite.transform.localScale = sizeY;
			} // end if
		} // end ResizeSpriteToScreen
	} // end SpriteReference
} // end GSP

