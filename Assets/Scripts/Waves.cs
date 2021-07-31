using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waves : MonoBehaviour
{
    private int Day = 0;

    private int PrisonersPerJail = 10;
    private int NumOfJails = 1;
    private int PrisonersAmount;
    private int AmountOfWaves = 1;
    private int PrisonerSpawnTime = 2;
    private int BetweenWaveTime = 10;

    public GameObject Prisoner;
    private List<GameObject> CurPrisoners = new List<GameObject>();

    public GameObject BetweenDayMenu;

    private List<GameObject> SpawnPositions = new List<GameObject>();

    public static int CaughtPrisoners = 0;
    public static int ExcapedPrisoners = 0;
    private bool DoneSpawning = false;

    public NavMeshSurface ThisNavSurface;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (CaughtPrisoners + ExcapedPrisoners >= PrisonersAmount && DoneSpawning)
        {
            if (NoPrisoners())
            {
                DayFinished();
            }
        }
    }

    private IEnumerator StartDay(float waitTime)
    {
        for (int i = 0; i < AmountOfWaves; i++)
        {
            StartCoroutine(SpawnWave(0));
            yield return new WaitForSeconds(BetweenWaveTime);
        }
        DoneSpawning = true;
    }

    private IEnumerator SpawnWave(float waitTime)
    {
        for (int i = 0; i < PrisonersAmount; i++)
        {
            Vector3 SpawnLoc = Vector3.zero;
            SpawnLoc = SpawnPositions[UnityEngine.Random.Range(0, SpawnPositions.Count)].transform.position;
            GameObject CurPrisoner = Instantiate(Prisoner, SpawnLoc, Quaternion.identity);
            yield return new WaitForSeconds(PrisonerSpawnTime);
        }
    }

    private void DailyMoney()
    {
        float CapturedPrisoners = PrisonersPerJail - ExcapedPrisoners;
        GameControllerScript.Money += Mathf.RoundToInt(CapturedPrisoners * GameControllerScript.MoneyPerPrisoner);
    }

    private void DayFinished()
    {
        DoneSpawning = false;
        DailyMoney();
        if (GameControllerScript.Money >= GameControllerScript.PrisonCost)
        {
            BetweenDayMenu.SetActive(true);
        }
        else
        {
            //Failed Because you couldnt pay for the prison
        }
    }

    public void StartDayWaves()
    {
        SpawnPositions.AddRange(GameObject.FindGameObjectsWithTag("SpawnLoc"));
        this.GetComponent<NewPlaceGateScript>().FindGates();
        ThisNavSurface.BuildNavMesh();
        DoneSpawning = false;
        BetweenDayMenu.SetActive(false);
        PrisonersAmount = PrisonersPerJail * NumOfJails;
        StartCoroutine(StartDay(0));
    }

    private bool NoPrisoners()
    {
        float PrisInScene = GameObject.FindGameObjectsWithTag("Prisoner").Length;
        if (PrisInScene == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
