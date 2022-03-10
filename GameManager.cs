using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    [SerializeField] Piece.Team currentPlayer;
    [SerializeField] UIManager uiManager;
    [SerializeField] Player player;

    int totalTurns = 0;

    [SerializeField] List<Piece> WhitePieces = new List<Piece>();
    [SerializeField] List<Piece> BlackPieces = new List<Piece>();

    [SerializeField] List<Piece> DeathWhitePieces = new List<Piece>();
    [SerializeField] List<Piece> DeathBlackPieces = new List<Piece>();

    Dictionary<Piece.Team, List<Piece>> pieces;
    Dictionary<Piece.Team, List<Piece>> poolPieces;
    bool bCanChangeTurn = true;
    Piece pawnToChange;
    BoxController boxPawnToChange;

    private void Awake()
    {
        if (!singleton)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        InitializeDictionaries();
    }
    void InitializeDictionaries()
    {
        pieces = new Dictionary<Piece.Team, List<Piece>>();
        poolPieces = new Dictionary<Piece.Team, List<Piece>>();

        pieces.Add(Piece.Team.Black, BlackPieces);
        pieces.Add(Piece.Team.White, WhitePieces);
        poolPieces.Add(Piece.Team.Black, DeathBlackPieces);
        poolPieces.Add(Piece.Team.White, DeathWhitePieces);
    }
    public Piece.Team GetTurn() { return currentPlayer; }

    public void ChangeTurn()
    {
        if (bCanChangeTurn)
        {
            totalTurns++;
            currentPlayer = (Piece.Team)(totalTurns % 2);
            uiManager.ChangeTurn((int)currentPlayer);
        }
    }

    public void AddPiece(Piece piece)
    {
        pieces[piece.GetTeam()].Add(piece);
    }

    public void DeathPiece(Piece piece)
    {
        Piece.Team deathPieceTeam = currentPlayer == Piece.Team.Black ? Piece.Team.White : Piece.Team.Black;
        pieces[deathPieceTeam].Remove(piece);
        poolPieces[deathPieceTeam].Add(piece);

        uiManager.DeathPiece(piece.GetId());

        piece.gameObject.SetActive(false);
    }


    public void PawnInLastRow(Piece.Team team, Piece pawn, BoxController _box)
    {
        uiManager.PawnInLastRow(team);
        pawnToChange = pawn;
        boxPawnToChange = _box;
        SetCanChangeTurn(false);
    }

    public void SetCanChangeTurn(bool canChange) { bCanChangeTurn = canChange; player.bChangeCanMove(canChange); }

    public void ChangePawn(DeathPiece _newPiece)
    {
        if (pawnToChange)
        {
            Transform piece = pawnToChange.transform;
            Piece.Team team = pawnToChange.GetTeam();
            Destroy(pawnToChange);
            int id = _newPiece.GetId() > 5 ? _newPiece.GetId() - 6 : _newPiece.GetId();

            Piece newPiece;
            switch (id)
            {
                case 1:
                    newPiece = piece.gameObject.AddComponent<Bishop>();
                    break;
                case 2:
                    newPiece = piece.gameObject.AddComponent<Horse>();
                    break;
                case 3:
                    newPiece = piece.gameObject.AddComponent<Tower>();
                    break;
                case 4:
                    newPiece = piece.gameObject.AddComponent<Queen>();
                    break;
                default:
                    newPiece = piece.gameObject.AddComponent<Pawn>();
                    break;
            }

            newPiece.Initialize(team, (Piece.PieceType)id);

            boxPawnToChange.SetPiece(newPiece);
            SetCanChangeTurn(true);
            ChangeTurn();
            pawnToChange = null;
            boxPawnToChange = null;
        }
    }

    public List<Piece> GetEnemyPieces()
    {
        return pieces[(currentPlayer == Piece.Team.Black) ? Piece.Team.White : Piece.Team.Black];
    }
}
