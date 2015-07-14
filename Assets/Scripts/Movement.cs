using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public Vector3 MoveLeft(Vector3 position)
	{
		if(position.x > GSP.Tiles.TileManager.MinWidthUnits)
		{
			return new Vector3(-GSP.Tiles.TileManager.PlayerMoveDistance, 0, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveLeft(Vector3 position)

	public Vector3 MoveRight(Vector3 position)
	{
		if(position.x < GSP.Tiles.TileManager.MaxWidthUnits)
		{
			return new Vector3(GSP.Tiles.TileManager.PlayerMoveDistance, 0, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveRight(Vector3 position)

	public Vector3 MoveUp(Vector3 position)
	{
		if(position.y < GSP.Tiles.TileManager.MinHeightUnits)
		{
			return new Vector3(0, GSP.Tiles.TileManager.PlayerMoveDistance, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveUp(Vector3 position)

	public Vector3 MoveDown(Vector3 position)
	{
		if(position.y > GSP.Tiles.TileManager.MaxHeightUnits)
		{
			return new Vector3(0, -GSP.Tiles.TileManager.PlayerMoveDistance, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveDown(Vector3 position)
}
