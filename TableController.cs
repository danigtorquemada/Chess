using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField] BoxController box;
    [SerializeField] BoxController[] table = new BoxController[64];

    [SerializeField] Transform[] teams = new Transform[2];

    [SerializeField] Transform piece;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                BoxController _box = Instantiate(box, new Vector3(i, j, 0) + transform.position, Quaternion.identity, gameObject.transform);
                table[i * 8 + j] = _box;

                if ((i + j) % 2 == 0)
                    _box.Initialize(Color.grey, j, i);
                else
                    _box.Initialize(Color.white, j, i);
            }
        }

        GeneratePieces();
    }

    void GeneratePieces()
    {
        Piece _piece;
        GameObject goPiece;
        //Instantiate pawns
        for (int i = 0; i < 8; i++)
        {
            for (int j = 1; j < 7; j += 5)
            {
                goPiece = Instantiate(piece.gameObject, new Vector3(i, j, 0) + transform.position, Quaternion.identity, teams[j == 1 ? 0 : 1]);
                _piece = goPiece.AddComponent<Pawn>();
                _piece.GetComponent<Pawn>().Initialize(j == 1 ? Piece.Team.black : Piece.Team.white);
                table[i * 8 + j].SetPiece(_piece);
            }

            for (int j = 0; j < 8; j += 7)
            {
                goPiece = Instantiate(piece.gameObject, new Vector3(i, j, 0) + transform.position, Quaternion.identity, teams[j == 0 ? 0 : 1]);
                switch (i)
                {
                    case 0:
                    case 7:
                        _piece = goPiece.AddComponent<Tower>();
                        _piece.GetComponent<Tower>().Initialize(j == 0 ? Piece.Team.black : Piece.Team.white);
                        break;
                    case 1:
                    case 6:
                        _piece = goPiece.AddComponent<Horse>();
                        _piece.GetComponent<Horse>().Initialize(j == 0 ? Piece.Team.black : Piece.Team.white);
                        break;
                    case 2:
                    case 5:
                        _piece = goPiece.AddComponent<Bishop>();
                        _piece.GetComponent<Bishop>().Initialize(j == 0 ? Piece.Team.black : Piece.Team.white);
                        break;
                    case 3:
                        _piece = goPiece.AddComponent<King>();
                        _piece.GetComponent<King>().Initialize(j == 0 ? Piece.Team.black : Piece.Team.white);
                        break;
                    case 4:
                        _piece = goPiece.AddComponent<Queen>();
                        _piece.GetComponent<Queen>().Initialize(j == 0 ? Piece.Team.black : Piece.Team.white);
                        break;
                    default:
                        _piece = goPiece.AddComponent<Pawn>();
                        break;
                }
                table[i * 8 + j].SetPiece(_piece);
            }
        }
    }
}
