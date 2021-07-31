using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerScript : MonoBehaviour
{
    public static int Day = 0;
    public static int Money;
    public static int MoneyPerPrisoner = 600;
    public static int CurrentPrisonNo = 0;
    public static int PrisonCost = 1000;

    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI DayText;

    private int StartMoney = 5000;

    public static int GateCost = 250;
    public static int GaurdCost = 200;

    public GameObject ARInteractionObject;
    public GameObject ARPlacementIndicator;

    private void Awake()
    {
        ARInteractionObject.SetActive(true);
        ARPlacementIndicator.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        Money = StartMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if (Money <= 0)
        {
            print("You Lose, out of money");
        }

        MoneyText.text = "Money: " + Money.ToString();
        DayText.text = "Day: " + Day.ToString();
    }
}
