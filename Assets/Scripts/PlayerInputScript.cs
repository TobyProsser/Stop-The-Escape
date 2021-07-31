using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInputScript : MonoBehaviour
{
    public GameObject Guard;
    public GameObject ElectricTower;
    public GameObject ElectricBox;

    public int ObjectInSelect = 1;
    public TextMeshProUGUI SwitchText;

    private int GuardPrice;

    private GameControllerScript GCS;

    public GameObject ShopPanel;
    public GameObject BetweenDayPanel;
    public GameObject MainPanel;

    public bool PlayMode = false;

    private void Awake()
    {
        GCS = this.GetComponent<GameControllerScript>();
        ShopPanel.SetActive(false);
        BetweenDayPanel.SetActive(false);
        MainPanel.SetActive(false);
        SwitchText.text = ObjectInSelect.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        GuardPrice = GameControllerScript.GaurdCost;
    }

    public void PrisonPlaced()
    {
        ShopPanel.SetActive(false);
        BetweenDayPanel.SetActive(true);
        MainPanel.SetActive(true);
        this.GetComponent<NewPlaceGateScript>().FindGates();
        PlayMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && PlayMode || Input.GetMouseButtonDown(0) && PlayMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Floor")
                {
                    if (!BetweenDayPanel.activeInHierarchy && MainPanel.activeInHierarchy) //If it is day and the game is running
                    {
                        if (GameControllerScript.Money > GuardPrice)
                        {
                            GameObject CurGuard = Instantiate(Guard, hit.point, Quaternion.identity);
                            GameControllerScript.Money -= GuardPrice;
                        }
                    }
                    else if (ObjectInSelect == 1)
                    {
                        Vector3 BoxSpawnLoc = new Vector3(hit.point.x -9.6f, hit.point.y, hit.point.z + 7.07f);
                        GameObject CurElectricBox = Instantiate(ElectricBox, BoxSpawnLoc, Quaternion.identity);
                    }
                    else if (ObjectInSelect == 2)
                    {
                        Vector3 TowerSpawnLoc = new Vector3(hit.point.x - 2.037f, hit.point.y, hit.point.z + 12.177f);
                        GameObject CurElectricTower = Instantiate(ElectricTower, TowerSpawnLoc, Quaternion.identity);
                    }
                }
            }
        }
    }

    public void ShopButton()
    {
        BetweenDayPanel.SetActive(false);
        MainPanel.SetActive(false);
        ShopPanel.SetActive(true);
    }

    public void SwitchTrapsButton()
    {
        if (ObjectInSelect == 1)
        {
            ObjectInSelect = 2;
            SwitchText.text = ObjectInSelect.ToString();
        }
        else if (ObjectInSelect == 2)
        {
            ObjectInSelect = 1;
            SwitchText.text = ObjectInSelect.ToString();
        }
    }
}
