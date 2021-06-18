using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    RaycastHit hit;

    [SerializeField]
    LayerMask layerPiece;

    BoxController box;
    BoxController lastBox;

    Piece piece;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward * 100, out hit, Mathf.Infinity, layerPiece))
            {
                if (hit.transform.TryGetComponent(out box))
                {
                    //First touch lastBox == null
                    if (!lastBox && box.HasPiece(out piece) && (int)piece.GetTeam() == (int)GameManager.singleton.GetTurn())
                    {
                        lastBox = box;
                        lastBox.SelectBox((int)GameManager.singleton.GetTurn());
                    }
                    else if (lastBox) //We have selected piece
                    {
                        lastBox.DeselectBox(false);

                        if (lastBox.GetPiece().TryMove(box))
                        {
                            if (box.HasPiece(out piece))
                            {
                                GameManager.singleton.DeathPiece(piece);
                            }

                            box.SetPiece(lastBox.GetPiece());
                            lastBox.SetPiece(null);
                            lastBox = null;

                            GameManager.singleton.ChangeTurn();
                            return;
                        }
                        else
                            lastBox = null;
                    }
                }
            }
        }
    }
}
