using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DigitalRuby.LightningBolt;

public class GuardController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody ThisRb;

    private List<GameObject> Prisoners = new List<GameObject>();

    private float ShortestDistance = 10000;
    private Vector3 GoTo;

    private Transform CurDoor;
    private float ShortestDoorDistance = 1000;
    private List<GameObject> DoorPositions = new List<GameObject>();
    private Vector3 GoToDoor;
    private bool PrisonerFound = false;

    private GameObject LightningObject;
    private LightningBoltScript LightningScript;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ThisRb = this.GetComponent<Rigidbody>();

        LightningObject = this.transform.GetChild(0).gameObject;
        LightningScript = LightningObject.GetComponent<LightningBoltScript>();
        LightningObject.SetActive(false);
    }

    void Start()
    {
        DoorPositions.AddRange(GameObject.FindGameObjectsWithTag("SpawnLoc"));
        print(DoorPositions[0].transform.position);
        print(DoorPositions[1].transform.position);
        FindPrisoners();
    }

    void LateUpdate()
    {
        if (!PrisonerFound)
        {
            FindPrisoners();
        }
    }

    private void FindPrisoners()
    {
        Prisoners.AddRange(GameObject.FindGameObjectsWithTag("Prisoner"));

        for (int i = 0; i < Prisoners.Count; i++)
        {
            if (Prisoners[i] != null)
            {
                float distance = Vector3.Distance(transform.position, Prisoners[i].transform.position);

                if (distance < ShortestDistance)
                {
                    ShortestDistance = distance;
                    GoTo = Prisoners[i].transform.position;
                }
            }
        }

        agent.SetDestination(GoTo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prisoner" && !PrisonerFound)
        {
            PrisonerFound = true;
            StartCoroutine(TazePrisoner(2, other.gameObject));
        }
    }

    private IEnumerator TazePrisoner(float waitTime, GameObject Prisoner)
    {
        Prisoner.GetComponent<PrisonerController>().Tazed();
        LightningObject.SetActive(true);
        LightningScript.StartObject = this.gameObject;
        LightningScript.EndObject = Prisoner;
        yield return new WaitForSeconds(waitTime);
        LightningObject.SetActive(false);
        Destroy(this.gameObject, 1);
    }
}
