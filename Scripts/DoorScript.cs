using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator Anim;
    public bool IsOpen = false;
    public bool HasLock = false;
    public bool IsLocked = false;
    public bool SpecialDoor = false;
    public bool JumpScareDoor = false;
    public bool FinalDoor = false;
    public bool HalfOpenDoor = false;
    public bool BeginningEventDoor = false;
    private bool oriIsOpen;
    private bool oriHasLock;
    private bool oriHalfOpenDoor;
    private AudioSource Player;
    [SerializeField] AudioClip DoorOpenSound;
    [SerializeField] AudioClip DoorCloseSound;
    [SerializeField] AudioClip TryingToOpenLockedDoorSound;
    [SerializeField] AudioClip UnlockDoorSound;
    [SerializeField] AudioClip HorrorSFX;
    [SerializeField] AudioClip Breath1;
    [SerializeField] AudioClip Breath2;
    [SerializeField] GameObject LockMessage;
    [SerializeField] GameObject DoorEventSound;
    [SerializeField] GameObject DisappearedDoor;
    [SerializeField] GameObject JumpScareSFX;
    [SerializeField] GameObject JumpScareEnemy;
    GameObject[] AllLight;
    GameObject[] AllLamps;
    // Start is called before the first frame update

    void Start()
    {
        AllLight = GameObject.FindGameObjectsWithTag("Light");
        AllLamps = GameObject.FindGameObjectsWithTag("Lamp");
        Anim = GetComponent<Animator>();
        Player = GetComponent<AudioSource>();
        foreach(GameObject light in AllLight)
            light.gameObject.SetActive(true);
        foreach(GameObject lamp in AllLamps)
            lamp.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        oriIsOpen = IsOpen;
        oriHasLock = HasLock;
        oriHalfOpenDoor = HalfOpenDoor;
    }

    // Update is called once per frame
    void Update()
    {
        if(HalfOpenDoor){
            this.GetComponent<Collider>().enabled = false;
            if(Global.KeyEventTriggered)this.GetComponent<Collider>().enabled = true;
        }
        if(HasLock == true){
            if(Global.hasKey == true){
                if(SpecialDoor && !Global.SpecialDoorUnlockable){
                    return;
                }
                if(JumpScareDoor && !Global.JumpScareDoorUnlockable){
                    return;
                }
                if(FinalDoor && !Global.FinalDoorUnlockable){
                    return;
                }
                IsLocked = false;
            }
            else{
                IsLocked = true;
            }
        }
        if(Global.WinTheGame || Global.GameOver){
            Reset();
        }
    }

    public void DoorSwitch(){
        if(IsLocked == false){
            if(BeginningEventDoor == true){
                foreach(GameObject light in AllLight)
                    light.gameObject.SetActive(false);
                foreach(GameObject lamp in AllLamps)
                    lamp.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                DoorEventSound.gameObject.SetActive(true);
                Global.DoorEventTriggerd = true;
                Global.SpecialDoorUnlockable = true;
                DisappearedDoor.gameObject.SetActive(false);
                // Player.clip = Breath1;
                // Player.Play();
                // Player.clip = Breath2;
                // Player.Play(); 
                // gameObject.SetActive(false);
                return;
            }
            
            if(IsOpen == false){
                if(HasLock){
                    StartCoroutine(UnlockAndOpen());
                    Global.hasKey = false;
                    return;
                }
                Anim.SetTrigger("Open");
                Player.clip = DoorOpenSound;
                Player.Play();
                IsOpen = true;
            }
            else if(IsOpen == true){
                Anim.SetTrigger("Close");
                Player.clip = DoorCloseSound;
                Player.Play();
                IsOpen = false;
            }
        }
        else{
            Player.clip = TryingToOpenLockedDoorSound;
            Player.Play();
            StartCoroutine(Timed());
            
        }

        IEnumerator Timed(){ 
            LockMessage.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            LockMessage.gameObject.SetActive(false);
        }

        IEnumerator UnlockAndOpen(){ 
            Player.clip = UnlockDoorSound;
            Player.Play();
            HasLock = false;
            yield return new WaitForSeconds(1);
            Anim.SetTrigger("Open");
            Player.clip = DoorOpenSound;
            IsOpen = true;
            if(JumpScareDoor == true){
                JumpScareSFX.gameObject.SetActive(true);
                JumpScareEnemy.gameObject.SetActive(true);
            }
            else
                Player.Play();
        }

    
    }
    
    void Reset(){
        DoorEventSound.gameObject.SetActive(false);
        DisappearedDoor.gameObject.SetActive(true);
        HasLock = oriHasLock;
        IsOpen = oriIsOpen;
        HalfOpenDoor = oriHalfOpenDoor;
    }
}

