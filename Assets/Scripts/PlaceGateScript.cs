using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGateScript : MonoBehaviour
{
    public GameObject[] Gate = new GameObject[1];
    private int GatePrice;

    private Vector3 TGatePos = new Vector3(0,1,54);
    private Vector3 BGatePos = new Vector3(0,1,-24);
    private Vector3 BLGatePos = new Vector3(-15, 1, -1);
    private Vector3 TLGatePos = new Vector3(-15, 1, 31);
    private Vector3 BRGatePos = new Vector3(15, 1, -1);
    private Vector3 TRGatePos = new Vector3(15, 1, 31);
    public Transform FRGatePos;
    public Transform FLGatePos;
    public Transform FTGatePos;
    public Transform FBGatePos;

    [HideInInspector]
    public bool TGate, BGate, BLGate, TLGate, BRGate, TRGate, FRGate, FLGate, FTGate, FBGate;

    private GameObject Prison;
    // Start is called before the first frame update
    void Start()
    {
        GatePrice = GameControllerScript.GateCost;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "TGP" && !TGate)
                {
                    Debug.Log("Top gate");
                    PlaceGate(1);
                }
                else if (hit.transform.tag == "BGP" && !BGate)
                {
                    Debug.Log("Bottom Gate");
                    PlaceGate(2);
                }
                else if (hit.transform.tag == "BLGP" && !BLGate)
                {
                    Debug.Log("Bottom Left Gate");
                    PlaceGate(3);
                }
                else if (hit.transform.tag == "TLGP" && !TLGate)
                {
                    Debug.Log("Top Left Gate");
                    PlaceGate(4);
                }
                else if (hit.transform.tag == "BRGP" && !BRGate)
                {
                    Debug.Log("Bottom Right Gate");
                    PlaceGate(5);
                }
                else if (hit.transform.tag == "TRGP" && !TRGate)
                {
                    Debug.Log("Top Right Gate");
                    PlaceGate(6);
                }
                else if (hit.transform.tag == "FRGP" && !FRGate)
                {
                    Debug.Log("First Right Gate");
                    PlaceGate(7);
                }
                else if (hit.transform.tag == "FLGP" && !FLGate)
                {
                    Debug.Log("First Left Gate");
                    PlaceGate(8);
                }
                else if (hit.transform.tag == "FTGP" && !FTGate)
                {
                    Debug.Log("First Top Gate");
                    PlaceGate(9);
                }
                else if (hit.transform.tag == "FBGP" && !FBGate)
                {
                    Debug.Log("FirstBottom Gate");
                    PlaceGate(10);
                }
            }
        }
    }

    private void PlaceGate(int GateNum)
    {
        Prison = GameObject.FindGameObjectWithTag("Prison");
        if (GameControllerScript.Money > GatePrice)
        {
            GameControllerScript.Money -= GatePrice;
            if (GateNum == 1)
            {
                GameObject CurGate = Instantiate(Gate[0], TGatePos, Quaternion.identity);
                CurGate.name = "TGateN";
                TGate = true;
            }
            else if (GateNum == 2)
            {
                GameObject CurGate = Instantiate(Gate[0], BGatePos, Quaternion.identity);
                CurGate.name = "BGateN";
                BGate = true;
            }
            else if (GateNum == 3)
            {
                GameObject CurGate = Instantiate(Gate[0], BLGatePos, Quaternion.identity);
                CurGate.transform.Rotate(0, 90, 0);
                CurGate.name = "BLGateN";
                BLGate = true;
            }
            else if (GateNum == 4)
            {
                GameObject CurGate = Instantiate(Gate[0], TLGatePos, Quaternion.identity);
                CurGate.transform.Rotate(0, 90, 0);
                CurGate.name = "TLGateN";
                TLGate = true;
            }
            else if (GateNum == 5)
            {
                GameObject CurGate = Instantiate(Gate[0], BRGatePos, Quaternion.identity);
                CurGate.transform.Rotate(0, 90, 0);
                CurGate.name = "BRGateN";
                BRGate = true;
            }
            else if (GateNum == 6)
            {
                GameObject CurGate = Instantiate(Gate[0], TRGatePos, Quaternion.identity);
                CurGate.transform.Rotate(0, 90, 0);
                CurGate.name = "TRGateN";
                TRGate = true;
            }
            else if (GateNum == 7)
            {
                GameObject CurGate = Instantiate(Gate[0], Vector3.zero, Quaternion.identity);
                CurGate.transform.SetParent(Prison.transform);
                CurGate.transform.rotation = FRGatePos.rotation;
                CurGate.transform.position = FRGatePos.position;
                CurGate.name = "FRGateN";
                FRGate = true;
            }
            else if (GateNum == 8)
            {
                GameObject CurGate = Instantiate(Gate[0], Vector3.zero, Quaternion.identity);
                CurGate.transform.SetParent(Prison.transform);
                CurGate.transform.rotation = FLGatePos.rotation;
                CurGate.transform.position = FLGatePos.position;
                CurGate.name = "FLGateN";
                FLGate = true;
            }
            else if (GateNum == 9)
            {
                GameObject CurGate = Instantiate(Gate[0], Vector3.zero, Quaternion.identity);
                CurGate.name = "FTGateN";
                CurGate.transform.SetParent(Prison.transform);
                CurGate.transform.rotation = FTGatePos.rotation;
                CurGate.transform.position = FTGatePos.position;
                FTGate = true;
            }
            else if (GateNum == 10)
            {
                GameObject CurGate = Instantiate(Gate[0], Vector3.zero, Quaternion.identity);
                CurGate.transform.SetParent(Prison.transform);
                CurGate.transform.rotation = FBGatePos.rotation;
                CurGate.transform.position = FBGatePos.position;
                CurGate.name = "FBGateN";
                FBGate = true;
            }
        }
    }

}
