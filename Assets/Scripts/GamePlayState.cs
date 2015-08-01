/*******************************************************************************
 *
 *  File Name: GamePlayState.cs
 *
 *  Description: The enumeration for the main states of gameplay
 *
 *******************************************************************************/

namespace GSP
{
    // The states that the main FSM runs on
    public enum GamePlayState
    {
        BeginTurn,
        RollDice,
        CalcDistance,
        DisplayDistance,
        SelectPathToTake,
        DoAction,
        EndTurn,
        EndGame
    } //end GamePlayState
} // emd GSP
