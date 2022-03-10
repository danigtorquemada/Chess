using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.tower)
    {
        base.Initialize(_team, _pieceType);
    }

    public override void PossibleMovements()
    {
        base.PossibleMovements();

        CheckVerticalMoves();
        CheckHorizontalMoves();
    }

    private void CheckVerticalMoves()
    {
        BoxController _box;

        for (int incrementer = -1; incrementer <= 1; incrementer += 2)
        {
            for (int j = position.Column + incrementer; j < 8 && j >= 0; j += incrementer)
            {
                _box = TableController.instance.GetBox(j, position.Row);

                if (_box)
                {
                    if (!_box.HasPiece())
                    {
                        AddPossibleMovement(_box);
                    }
                    else if (_box.GetPiece().GetTeam() != myTeam)
                    {
                        AddPossibleMovement(_box);
                        break;
                    }
                    else
                    { break; }
                }
            }
        }
    }

    private void CheckHorizontalMoves()
    {
        BoxController _box;

        for (int incrementer = -1; incrementer <= 1; incrementer += 2)
        {
            for (int j = position.Row + incrementer; j < 8 && j >= 0; j += incrementer)
            {
                _box = TableController.instance.GetBox(position.Column, j);

                if (_box)
                {
                    if (!_box.HasPiece())
                    {
                        AddPossibleMovement(_box);
                    }
                    else if (_box.GetPiece().GetTeam() != myTeam)
                    {
                        AddPossibleMovement(_box);
                        break;
                    }
                    else
                    { break; }
                }
            }
        }
    }


}
