using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Batteries;

public class Pickups : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] float Distance = 4.0f;
    [SerializeField] GameObject PickupMessage;
    [SerializeField] GameObject OpenDoorMessage;
    [SerializeField] GameObject KeyIcon;
    [SerializeField] GameObject AidMessage;
    [SerializeField] GameObject LightEnd;
    [SerializeField] Text DoorText;
    private AudioSource Player;
    [SerializeField] AudioClip BatteryPickUp;
    // Start is called before the first frame update
    private float RayDistance;
    private bool CanSeePickup = false;
    private bool CanSeeOpenDoor = false;
    private bool CanSeeAid = false;
    
    void Start()
    {
        PickupMessage.gameObject.SetActive(false);
        RayDistance = Distance;
        Player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance))
        {
            if(hit.transform.tag == "Battery"){
                CanSeePickup = true;
                if(Input.GetKeyDown(KeyCode.E)){
                    Destroy(hit.transform.gameObject);
                    Player.Play();
                    Global.batteryNum++;
                }
            }
            else if(hit.transform.tag == "Door"){
                CanSeeOpenDoor = true;
                
                if(hit.transform.gameObject.GetComponent<DoorScript>().IsOpen == false){
                    DoorText.text = "Eを押してドアを開ける";
                }
                else{
                    DoorText.text = "Eを押してドアを閉める";
                }
                if(Input.GetKeyDown(KeyCode.E)){
                    hit.transform.gameObject.SendMessage("DoorSwitch");
                }
                
            }

            else if(hit.transform.tag == "Key"){
                CanSeePickup = true;
                if(Input.GetKeyDown(KeyCode.E)){
                    Destroy(hit.transform.gameObject);
                    Player.Play();
                    Global.hasKey = true;
                    if(Global.SpecialDoorUnlockable){
                        Global.JumpScareDoorUnlockable = true;
                    }
                }
            }
            else if(hit.transform.tag == "Aid"){
                CanSeeAid = true;
                if(Input.GetKeyDown(KeyCode.E)){
                    Player.Play();
                    Destroy(hit.transform.gameObject);
                    hit.transform.tag = "Untagged";
                    if(Global.HP + 30 > 100) Global.HP = 100;
                    else Global.HP += 30;

                }
            }
            else if(hit.transform.tag == "Elf"){
                CanSeePickup = true;
                if(Input.GetKeyDown(KeyCode.E)){
                    Destroy(hit.transform.gameObject);
                    Player.Play();
                    Global.hasKey = true;
                    Global.FinalDoorUnlockable = true;
                    LightEnd.gameObject.SetActive(true);
                }
            }
            else{
                CanSeePickup = false;
                CanSeeOpenDoor = false;
                CanSeeAid = false;
            }
            if(Global.hasKey == true)
                KeyIcon.gameObject.SetActive(true);
            else
                KeyIcon.gameObject.SetActive(false);
        }
        
        if(CanSeePickup == true){
            PickupMessage.gameObject.SetActive(true);
            RayDistance = 1000f; 
        }
        else if(CanSeeOpenDoor == true){
            OpenDoorMessage.gameObject.SetActive(true);
            RayDistance = 1000f;
        }

        else if(CanSeeAid == true){
            AidMessage.gameObject.SetActive(true);
            RayDistance = 1000f;
        }
        else{
            PickupMessage.gameObject.SetActive(false);
            OpenDoorMessage.gameObject.SetActive(false);
            AidMessage.gameObject.SetActive(false);
            RayDistance = Distance;
        }

    }
}
