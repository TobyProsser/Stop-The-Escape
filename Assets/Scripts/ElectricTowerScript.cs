using DigitalRuby.LightningBolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTowerScript : MonoBehaviour
{
    private GameObject LightningObject;
    LightningBoltScript LightningScript;

    public GameObject StartObject;

    [HideInInspector]
    public GameObject CurPrisoner;
    private bool TasingPrisoner = false;
    // Start is called before the first frame update
    void Start()
    {
        LightningObject = this.transform.GetChild(0).gameObject;
        LightningScript = LightningObject.GetComponent<LightningBoltScript>();
        LightningObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prisoner" && !TasingPrisoner)
        {
            print("Tase");
            CurPrisoner = other.gameObject;
            TasingPrisoner = true;
            StartCoroutine(ShootLightning(2, other.gameObject));
        }
    }

    private IEnumerator ShootLightning(float waitTime, GameObject Prisoner)
    {
        LightningObject.SetActive(true);
        LightningScript.StartObject = StartObject;
        LightningScript.EndObject = Prisoner;
        Prisoner.GetComponent<PrisonerController>().Tazed();
        yield return new WaitForSeconds(waitTime);
        TasingPrisoner = false;
        LightningObject.SetActive(false);
    }
}
