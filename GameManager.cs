using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    [SerializeField] Piece.Team currentPlayer;
    [SerializeField] UIManager uiManager;

    int totalTurns = 0;

    [SerializeField] List<Piece> WhitePieces = new List<Piece>();
    [SerializeField] List<Piece> BlackPieces = new List<Piece>();

    [SerializeField] List<Piece> DeathWhitePieces = new List<Piece>();
    [SerializeField] List<Piece> DeathBlackPieces = new List<Piece>();

    Dictionary<Piece.Team, List<Piece>> pieces;
    Dictionary<Piece.Team, List<Piece>> deathPieces;

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
        deathPieces = new Dictionary<Piece.Team, List<Piece>>();
        pieces.Add(Piece.Team.Black, BlackPieces);
        pieces.Add(Piece.Team.White, WhitePieces);
        deathPieces.Add(Piece.Team.Black, DeathBlackPieces);
        deathPieces.Add(Piece.Team.White, DeathWhitePieces);
    }
    public Piece.Team GetTurn() { return currentPlayer; }

    public void ChangeTurn()
    {
        totalTurns++;
        currentPlayer = (Piece.Team)(totalTurns % 2);
        uiManager.ChangeTurn((int)currentPlayer);
    }

    public void AddPiece(Piece piece)
    {
        pieces[piece.GetTeam()].Add(piece);
    }

    public void DeathPiece(Piece piece)
    {
        Piece.Team deathPieceTeam = currentPlayer == Piece.Team.Black ? Piece.Team.White : Piece.Team.Black;
        pieces[deathPieceTeam].Remove(piece);
        deathPieces[deathPieceTeam].Add(piece);

        piece.gameObject.SetActive(false);
    }
}
