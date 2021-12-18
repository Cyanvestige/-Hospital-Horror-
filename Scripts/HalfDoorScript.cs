using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDoorScript : MonoBehaviour
{
    private Animator Anim;
    public bool IsOpen = false;
    private AudioSource Player;
    [SerializeField] AudioClip DoorOpenSound;
    [SerializeField] AudioClip DoorCloseSound;
    [SerializeField] GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        Player = GetComponent<AudioSource>();
        Door.transform.Rotate(0.0f, 30, 0.0f, Space.World);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorSwitch(){
        if(IsOpen == false){
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
}
