using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.tower)
    {
        base.Initialize(_team, _pieceType);
    }

    public override void ShowPosibleMovement(int row, int column)
    {
        base.ShowPosibleMovement(row, column);
        possibleMovements.Clear();

        bool IsRow;
        int incrementer;
        int position;
        BoxController _box;

        for (int z = 0; z < 2; z++)
        {
            IsRow = z == 0;
            position = IsRow ? row : column;

            for (int i = 0; i < 2; i++)
            {
                incrementer = i == 0 ? 1 : -1;

                for (int j = position + incrementer; j < 8 && j >= 0; j += incrementer)
                {

                    if (IsRow)
                    {
                        _box = TableController.instance.GetBox(j, column);
                    }
                    else
                    {
                        _box = TableController.instance.GetBox(row, j);
                    }

                    if (_box)
                    {
                        if (!_box.HasPiece())
                        {
                            AddPosibleMovement(_box);
                        }
                        else if(_box.GetPiece().GetTeam() != myTeam)
                        {
                            AddPosibleMovement(_box);
                            break;
                        }
                        else
                        { break; }
                    }
                }
            }
        }
    }
}
