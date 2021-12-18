using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControl : MonoBehaviour
{
    [SerializeField] GameObject FlashLight;
    private bool hasCharge = true;
    // Start is called before the first frame update
    void Start()
    {
        FlashLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hasCharge = (Global.BatteryPower != 0);
        if(hasCharge){
            if(Input.GetKeyDown(KeyCode.F)){
                Global.isFlashLightOn = !Global.isFlashLightOn;
                FlashLight.gameObject.SetActive(Global.isFlashLightOn);
            }
        }
        else{
            Global.isFlashLightOn = false;
            FlashLight.gameObject.SetActive(Global.isFlashLightOn);
        }
    }
}
