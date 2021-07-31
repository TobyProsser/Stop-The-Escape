using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlaceGateScript : MonoBehaviour
{
    private GameObject FRGate;
    private GameObject FLGate;
    private GameObject FTGate;
    private GameObject FBGate;

    private GameObject Prison;
    private bool PrisonFound = false;

    private int GatePrice = 250;

    void Start()
    {
        GatePrice = GameControllerScript.GateCost;

        //FindGates(); //Remove for AR
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && PrisonFound || Input.GetMouseButtonDown(0) && PrisonFound)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "FRGP" && !FRGate.activeInHierarchy)
                {
                    Debug.Log("First Right Gate");
                    if (GameControllerScript.Money > GatePrice)
                    {
                        GameControllerScript.Money -= GatePrice;
                        FRGate.SetActive(true);
                    }
                }
                else if (hit.transform.tag == "FLGP" && !FLGate.activeInHierarchy)
                {
                    Debug.Log("First Left Gate");
                    if (GameControllerScript.Money > GatePrice)
                    {
                        GameControllerScript.Money -= GatePrice;
                        FLGate.SetActive(true);
                    }
                }
                else if (hit.transform.tag == "FTGP" && !FTGate.activeInHierarchy)
                {
                    Debug.Log("First Top Gate");
                    if (GameControllerScript.Money > GatePrice)
                    {
                        GameControllerScript.Money -= GatePrice;
                        FTGate.SetActive(true);
                    }
                }
                else if (hit.transform.tag == "FBGP" && !FBGate.activeInHierarchy)
                {
                    Debug.Log("FirstBottom Gate");
                    if (GameControllerScript.Money > GatePrice)
                    {
                        GameControllerScript.Money -= GatePrice;
                        FBGate.SetActive(true);
                    }
                }
            }
        }
    }

    public void FindGates()
    {
        Prison = GameObject.FindGameObjectWithTag("Prison");
        if (Prison != null)
        {
            PrisonFound = true;
        }
        else
        {
            Debug.Log("Prison Not Found");
        }
        foreach (Transform t in Prison.transform)
        {
            if (t.name == "FRGate")
            {
                FRGate = t.gameObject;
            }
            else if (t.name == "FLGate")
            {
                FLGate = t.gameObject;
            }
            else if (t.name == "FTGate")
            {
                FTGate = t.gameObject;
            }
            else if (t.name == "FBGate")
            {
                FBGate = t.gameObject;
            }
        }
    }
}
