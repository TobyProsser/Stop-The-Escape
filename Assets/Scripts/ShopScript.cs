using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class ShopScript : MonoBehaviour
{
    public NavMeshSurface ThisNavSurface;

    public TextMeshProUGUI PrisonPriceT;
    public TextMeshProUGUI GatePriceT;
    public TextMeshProUGUI GuardsPriceT;

    private int PrisonNo = 0;
    private int GateNo = 0;
    private int GuardNo = 0;

    private int PrisonPrice = 10000;
    private int GatePrice = 2000;
    private int GuardPrice = 3000;
    private float PriceMult = 1.75f;

    public GameObject[] Prisons = new GameObject[5];

    public GameObject ShopPanel;
    public GameObject GameInfoPanel;
    public GameObject BetweenDayPanel;

    private int JailCost = 1000;

    // Start is called before the first frame update
    void Start()
    {
        PrisonPriceT.text = "Cost: " + PrisonPrice.ToString();
        GatePriceT.text = "Cost: " + GatePrice.ToString();
        GuardsPriceT.text = "Cost: " + GuardPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpPrison()
    {
        PrisonPriceT.text = "Cost: " + PrisonPrice.ToString();
        if (GameControllerScript.Money >= GatePrice)
        {
            GameControllerScript.Money -= PrisonPrice;
            PrisonNo += 1;
            ChangePrison();
        }
    }

    public void UpGate()
    {
        GatePriceT.text = "Cost: " + GatePrice.ToString();
        if (GameControllerScript.Money >= GatePrice)
        {
            GameControllerScript.Money -= GatePrice;
            GateNo += 1;
            GatePrice = Mathf.RoundToInt(GatePrice * PriceMult);
        }
    }

    public void UpGuard()
    {
        GuardsPriceT.text = "Cost: " + GuardPrice.ToString();
        if (GameControllerScript.Money >= GuardPrice)
        {
            GameControllerScript.Money -= GuardPrice;
            GuardNo += 1;
            GuardPrice = Mathf.RoundToInt(GuardPrice * PriceMult);
        }
    }

    public void ExitButton()
    {
        ShopPanel.SetActive(false);
        GameInfoPanel.SetActive(true);
        BetweenDayPanel.SetActive(true);
    }

    private void ChangePrison()
    {
        if (PrisonNo < 5)
        {
            for (int i = 0; i < Prisons.Length; i++)
            {
                Prisons[i].SetActive(false);
            }
            Prisons[PrisonNo].SetActive(true);
            GameControllerScript.CurrentPrisonNo += 1;
            GameControllerScript.PrisonCost += JailCost; 
            ThisNavSurface.BuildNavMesh();
            PrisonPrice = Mathf.RoundToInt(PrisonPrice * PriceMult);
        }
    }
}
