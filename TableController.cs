using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public static TableController instance;

    [SerializeField] BoxController box;
    [SerializeField] BoxController[] table = new BoxController[64];

    [SerializeField] Transform[] teams = new Transform[2];

    [SerializeField] Transform piece;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                BoxController _box = Instantiate(box, new Vector3(column, row, 0) + transform.position, Quaternion.identity, gameObject.transform);
                _box.name += row * 8 + column;
                table[row * 8 + column] = _box;

                if ((column + row) % 2 == 0)
                    _box.Initialize(Color.grey, column, row);
                else
                    _box.Initialize(Color.white, column, row);
            }
        }

        GeneratePieces();
    }

    void GeneratePieces()
    {
        Piece _piece;
        GameObject goPiece;
        //Instantiate pawns
        for (int column = 0; column < 8; column++)
        {
            for (int row = 1; row < 7; row += 5)
            {
                goPiece = Instantiate(piece.gameObject, new Vector3(column, row, 0) + transform.position, Quaternion.identity, teams[row == 1 ? 0 : 1]);
                _piece = goPiece.AddComponent<Pawn>();
                _piece.GetComponent<Pawn>().Initialize(row == 1 ? Piece.Team.Black : Piece.Team.White);
                table[row * 8 + column].SetPiece(_piece);
            }

            for (int row = 0; row < 8; row += 7)
            {
                goPiece = Instantiate(piece.gameObject, new Vector3(column, row, 0) + transform.position, Quaternion.identity, teams[row == 0 ? 0 : 1]);
                switch (column)
                {
                    case 0:
                    case 7:
                        _piece = goPiece.AddComponent<Tower>();
                        _piece.GetComponent<Tower>().Initialize(row == 0 ? Piece.Team.Black : Piece.Team.White);
                        break;
                    case 1:
                    case 6:
                        _piece = goPiece.AddComponent<Horse>();
                        _piece.GetComponent<Horse>().Initialize(row == 0 ? Piece.Team.Black : Piece.Team.White);
                        break;
                    case 2:
                    case 5:
                        _piece = goPiece.AddComponent<Bishop>();
                        _piece.GetComponent<Bishop>().Initialize(row == 0 ? Piece.Team.Black : Piece.Team.White);
                        break;
                    case 3:
                        _piece = goPiece.AddComponent<King>();
                        _piece.GetComponent<King>().Initialize(row == 0 ? Piece.Team.Black : Piece.Team.White);
                        break;
                    case 4:
                        _piece = goPiece.AddComponent<Queen>();
                        _piece.GetComponent<Queen>().Initialize(row == 0 ? Piece.Team.Black : Piece.Team.White);
                        break;
                    default:
                        _piece = goPiece.AddComponent<Pawn>();
                        break;
                }
                table[row * 8 + column].SetPiece(_piece);
            }
        }
    }

    public BoxController GetBox(int column, int row)
    {
        if (!(column < 8 && column >= 0 && row < 8 && row >= 0))
            return null;

        int position = row * 8 + column;
        if (position < table.Length && position >= 0)
            return table[position];
        else return null;
    }
}
