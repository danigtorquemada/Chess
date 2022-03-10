using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    bool bFirstMovement = true;

    public override void Initialize(Team _team, PieceType _pieceType = PieceType.pawn)
    {
        Debug.Log("HOLA");
        base.Initialize(_team, _pieceType);
    }

    public override void PossibleMovements()
    {
        base.PossibleMovements();

        int multiplierTeam = (myTeam == Team.Black ? 1 : -1);
        BoxController _box = TableController.instance.GetBox(position.Column, position.Row + multiplierTeam);

        if (_box && !_box.HasPiece())
        {
            AddPossibleMovement(_box);
        }

        if (bFirstMovement && !_box.HasPiece())
        {
            _box = TableController.instance.GetBox(position.Column, position.Row + multiplierTeam * 2);
            if (_box && !_box.HasPiece())
            {
                AddPossibleMovement(_box);
            }
        }

        for (int leftIncrementer = -1; leftIncrementer <= 1; leftIncrementer += 2)
        {
            _box = TableController.instance.GetBox(position.Column + leftIncrementer, position.Row + multiplierTeam);

            if (_box && _box.HasPiece() && _box.GetPiece().GetTeam() != myTeam)
            {
                AddPossibleMovement(_box);
            }
        }
    }

    public override bool TryMove(BoxController _box)
    {
        if (base.TryMove(_box))
        {
            bFirstMovement = false;


            int lastRow = myTeam == Team.Black ? 7 : 0;
            if (_box.position.Row == lastRow)
            {
                GameManager.singleton.PawnInLastRow(myTeam, this, _box);
            }
            return true;
        }
        else
            return false;
    }
}
