using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Batteries : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Battery1;
    [SerializeField] GameObject Battery2;
    [SerializeField] GameObject Battery3;
    [SerializeField] float DrainTime = 90.0f;
    void Start(){
        Battery1.GetComponentsInChildren<Image>()[1].fillAmount = 1.0f;
        Battery2.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
        Battery3.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
        Global.batteryNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        addBattery();
        if(Global.isFlashLightOn == true){
            if(Global.batteryNum == 3){
                Battery3.GetComponentsInChildren<Image>()[1].fillAmount -= 1.0f / DrainTime * Time.deltaTime;
                if(Battery3.GetComponentsInChildren<Image>()[1].fillAmount == 0) Global.batteryNum--;
            }
            else if(Global.batteryNum == 2){
                Battery2.GetComponentsInChildren<Image>()[1].fillAmount -= 1.0f / DrainTime * Time.deltaTime;
                if(Battery2.GetComponentsInChildren<Image>()[1].fillAmount == 0) Global.batteryNum--;
            }
            else{
                Battery1.GetComponentsInChildren<Image>()[1].fillAmount -= 1.0f / DrainTime * Time.deltaTime;
                if(Battery1.GetComponentsInChildren<Image>()[1].fillAmount == 0) Global.batteryNum--;
            }
        }
        Global.BatteryPower = Battery1.GetComponentsInChildren<Image>()[1].fillAmount + Battery2.GetComponentsInChildren<Image>()[1].fillAmount + Battery3.GetComponentsInChildren<Image>()[1].fillAmount;
         
    }

    void addBattery(){
        if(Global.batteryNum == 1 && Battery1.GetComponentsInChildren<Image>()[1].fillAmount == 0){
            Battery1.GetComponentsInChildren<Image>()[1].fillAmount = 1;   
        }
        else if(Global.batteryNum == 2 && Battery2.GetComponentsInChildren<Image>()[1].fillAmount == 0){
            Battery2.GetComponentsInChildren<Image>()[1].fillAmount = 1;
        }
        else if(Global.batteryNum == 3 && Battery3.GetComponentsInChildren<Image>()[1].fillAmount == 0){
            Battery3.GetComponentsInChildren<Image>()[1].fillAmount = 1;
        }
    }
}
