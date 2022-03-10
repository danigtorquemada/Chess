using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Position
{
    public Position(int _column, int _row)
    {
        Column = _column;
        Row = _row;
    }
    public int Column { get; private set; }
    public int Row { get; private set; }
}
public class Piece : MonoBehaviour
{
    [SerializeField] protected Team myTeam;
    [SerializeField] PieceType pieceType;

    int id;

    protected Position position;

    protected List<BoxController> possibleMovements = new List<BoxController>();

    public enum Team { White, Black }

    public enum PieceType { pawn, bishop, horse, tower, queen, king }

    public virtual void Initialize(Team _team, PieceType _pieceType = PieceType.pawn)
    {
        myTeam = _team;
        pieceType = _pieceType;

        id = (int)myTeam * 6 + (int)pieceType;

        SetImage();

        GameManager.singleton.AddPiece(this);
    }

    void SetImage()
    {
        if (TryGetComponent(out SpriteRenderer renderer))
        {
            renderer.sprite = SpriteManager.singleton.GetSprite(id);
        }
    }

    public Team GetTeam()
    {
        return myTeam;
    }

    public void SetPosition(int _column,int _row)
    {
        position = new Position(_column, _row);
    }

    public int GetId() { return id; }

    public virtual void PossibleMovements()
    {
        possibleMovements.Clear();
    }

    protected void AddPossibleMovement(BoxController _box)
    {
        possibleMovements.Add(_box);
    }

    protected void ShowPossibleMovements()
    {
        for (int i = 0; i < possibleMovements.Count; i++)
        {
            possibleMovements[i].ChangePossibleMovement();
        }
    }

    public virtual void HidePosibleMovement()
    {
        for (int i = 0; i < possibleMovements.Count; i++)
        {
            possibleMovements[i].DeselectBox();
        }
    }

    public virtual bool TryMove(BoxController _box)
    {
        if (possibleMovements.Contains(_box))
        {
            possibleMovements.Clear();
            return true;
        }
        else
            return false;
    }

    public void SelectPiece(BoxController box)
    {
        PossibleMovements();
        ComprobateCheckKing(box);
        ShowPossibleMovements();
    }

    public void ComprobateCheckKing(BoxController box)
    {
        box.SetPiece(null);

        Piece currentPiece = null;
        List<Piece> enemyPieces = GameManager.singleton.GetEnemyPieces();
        List<BoxController> finalPossibleMovement = new List<BoxController>();

        for (int i = 0; i < possibleMovements.Count; i++)
        {
            if (possibleMovements[i].GetPiece())
            {
                currentPiece = possibleMovements[i].GetPiece();
            }

            possibleMovements[i].SetPiece(this);

            for (int j = 0; j < enemyPieces.Count; j++)
            {
                if (currentPiece && enemyPieces[j] == currentPiece)
                    continue;

                enemyPieces[j].PossibleMovements();

                if (enemyPieces[j].IsKingBeenAttacking())
                {
                    goto RemovePossibleMovement;
                }
            }
            finalPossibleMovement.Add(possibleMovements[i]);

        RemovePossibleMovement:
            if (currentPiece)
                possibleMovements[i].SetPiece(currentPiece);
            else
                possibleMovements[i].SetPiece(null);

            currentPiece = null;
        }

        possibleMovements = finalPossibleMovement;
        box.SetPiece(this);
    }

    public bool IsKingBeenAttacking()
    {
        for (int i = 0; i < possibleMovements.Count; i++)
        {
            if (possibleMovements[i].GetPiece() && possibleMovements[i].GetPiece().pieceType == PieceType.king)
                return true;
        }

        return false;
    }
}
