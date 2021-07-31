using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBoxScript : MonoBehaviour
{
    public GameObject LightingStart1, LightingStart2, LightingStart3, LightingStart4;
    public GameObject LightningEnd1, LightningEnd2, LightningEnd3, LightningEnd4;

    private GameObject LightningObject1, LightningObject2, LightningObject3, LightningObject4;

    private float ElectricDelayTime = 1;

    public bool ElectricOn = false;

    // Start is called before the first frame update
    void Start()
    {
        LightningObject1 = this.transform.GetChild(0).gameObject;
        LightningBoltScript LightningScript1 = LightningObject1.GetComponent<LightningBoltScript>();

        LightningObject2 = this.transform.GetChild(1).gameObject;
        LightningBoltScript LightningScript2 = LightningObject2.GetComponent<LightningBoltScript>();

        LightningObject3 = this.transform.GetChild(2).gameObject;
        LightningBoltScript LightningScript3 = LightningObject3.GetComponent<LightningBoltScript>();

        LightningObject4 = this.transform.GetChild(3).gameObject;
        LightningBoltScript LightningScript4 = LightningObject4.GetComponent<LightningBoltScript>();

        LightningScript1.StartObject = LightingStart1;
        LightningScript1.EndObject = LightningEnd1;

        LightningScript2.StartObject = LightingStart2;
        LightningScript2.EndObject = LightningEnd2;

        LightningScript3.StartObject = LightingStart3;
        LightningScript3.EndObject = LightningEnd3;

        LightningScript4.StartObject = LightingStart4;
        LightningScript4.EndObject = LightningEnd4;

        StartCoroutine(ElectricOnOff(ElectricDelayTime));
    }

    private IEnumerator ElectricOnOff(float waitTime)
    {
        LightningObject1.SetActive(false);
        LightningObject2.SetActive(false);
        LightningObject3.SetActive(false);
        LightningObject4.SetActive(false);

        while (true)
        {
            ElectricOn = true;
            LightningObject1.SetActive(true);
            LightningObject2.SetActive(true);
            LightningObject3.SetActive(true);
            LightningObject4.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            ElectricOn = false;
            LightningObject1.SetActive(false);
            LightningObject2.SetActive(false);
            LightningObject3.SetActive(false);
            LightningObject4.SetActive(false);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
