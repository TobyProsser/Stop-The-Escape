using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody ThisRb;

    private GameObject CurPrison;
    private Vector3[] GateArray = new Vector3[4];
    private float ShortestDistance = 100000;
    private Vector3 GoTo = Vector3.zero;
    private Vector3 SpawnLoc;

    public bool BreakGate;
    private GameObject GateBreaking;
    private String CurGateName;

    public float speed = 10000;
    public float AttackTime = 1;
    public float AttackForce = 10;

    private float CheckMovementTime = 3;

    private PlaceGateScript PGS;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ThisRb = this.GetComponent<Rigidbody>();

        PGS = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlaceGateScript>();
    }

    void Start()
    {
        MakeGateArray();
        StartCoroutine(NoMovementCheck(0));
        SpawnLoc = this.transform.position;
    }

    private void MakeGateArray()
    {
        CurPrison = GameObject.FindGameObjectWithTag("Prison");
        GateArray[0] = CurPrison.transform.GetChild(1).position;
        GateArray[1] = CurPrison.transform.GetChild(2).position;
        GateArray[2] = CurPrison.transform.GetChild(3).position;
        GateArray[3] = CurPrison.transform.GetChild(4).position;

        FindGate();
    }

    void LateUpdate()
    {
        
    }

    private void FindGate()
    {
        for (int i = 0; i < GateArray.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, GateArray[i]);

            if (distance < ShortestDistance)
            {
                ShortestDistance = distance;
                GoTo = GateArray[i];
            }
        }

        agent.SetDestination(GoTo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate")
        {
            agent.isStopped = true;
            GateBreaking = other.gameObject;
            BreakGate = true;
            StartCoroutine(DestroyGate(0));
        }

        if (other.tag == "ExcapedArea")
        {
            Waves.ExcapedPrisoners += 1;
            Destroy(this.gameObject);
        }

        if (other.tag == "ElectricBox")
        {
            if (other.gameObject.GetComponent<ElectricBoxScript>().ElectricOn)
            {
                Tazed();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Gate")
        {
            if (!BreakGate)
            {
                agent.isStopped = true;
                GateBreaking = collision.gameObject;
                BreakGate = true;
                StartCoroutine(DestroyGate(0));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Gate")
        {
            BreakGate = false;
            agent.isStopped = false;
            agent.SetDestination(GoTo);
        }
    }

    private IEnumerator DestroyGate(float waitTime)
    {
        OnGateScript CurGateScript = GateBreaking.GetComponent<OnGateScript>();
        CurGateName = GateBreaking.name;
        while (BreakGate)
        {
            CurGateScript.Health -= AttackForce;
            if (CurGateScript.Health == 0 || GateBreaking == null)
            {
                BreakGate = false;
                WhichGateBroke();
                agent.isStopped = false;
                agent.SetDestination(GoTo);
            }
            yield return new WaitForSeconds(AttackTime);
        }
    }

    private void WhichGateBroke()
    {
        if (CurGateName == "TGateN")
        {
            PGS.TGate = false;
        }
        else if (CurGateName == "BGateN")
        {
            PGS.BGate = false;
        }
        else if (CurGateName == "BLGateN")
        {
            PGS.BLGate = false;
        }
        else if (CurGateName == "TLGateN")
        {
            PGS.TLGate = false;
        }
        else if (CurGateName == "BRGateN")
        {
            PGS.BRGate = false;
        }
        else if (CurGateName == "TRGateN")
        {
            PGS.TRGate = false;
        }
        else if (CurGateName == "FRGateN")
        {
            PGS.FRGate = false;
        }
        else if (CurGateName == "FLGateN")
        {
            PGS.FLGate = false;
        }
        else if (CurGateName == "FTGateN")
        {
            PGS.FTGate = false;
        }
        else if (CurGateName == "FBGateN")
        {
            PGS.FBGate = false;
        }
    }

    private IEnumerator NoMovementCheck(float waitTime)
    {
        while (true)
        {
            Vector3 CurPos = this.transform.position;
            yield return new WaitForSeconds(CheckMovementTime);
            Vector3 NextPos = this.transform.position;

            if (Vector3.Distance(CurPos, NextPos) < 1 && !BreakGate)
            {
                print("Prisoner Got Stuck");
                Destroy(this.gameObject);
            }
        }
    }

    public void Tazed()
    {
        agent.isStopped = true;
        this.GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
