using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    private Collider Col;
    private AudioSource OneShotSound;
    public bool KeyEvent;
    public bool DoorEvent;
    public bool BeforeWinning;
    public bool WinTheGame;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject ChaseMusic;
    [SerializeField] GameObject Elf;
    [SerializeField] GameObject Ghoul1;
    [SerializeField] GameObject Ghoul2;
    [SerializeField] GameObject Crawler;
    [SerializeField] GameObject SuccessPanel;
    [SerializeField] GameObject EndingMusic;
    [SerializeField] Transform Player;
    // [SerializeField] GameObject DoorQuad;
    [SerializeField] private Animator m_Animator;
    // private bool isDoorOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        OneShotSound = GetComponent<AudioSource>();
        Col = GetComponent<Collider>();
        m_Animator = Door.gameObject.GetComponent<Animator>();
        SuccessPanel.gameObject.SetActive(false);
        // isDoorOpen = DoorQuad.GetComponent<DoorScript>().IsOpen;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
       
        if(other.CompareTag("Player")){
            if(!Global.KeyEventTriggered && KeyEvent){
                OneShotSound.Play();
                m_Animator.SetTrigger("Shut");
                Global.KeyEventTriggered = true;
                Col.enabled = false;
            }
            if(Global.DoorEventTriggerd && DoorEvent){
                OneShotSound.Play();
                Col.enabled = false;
            }
            if(Global.hasKey && Global.JumpScareDoorUnlockable){
                Elf.gameObject.SetActive(true);
                StartCoroutine(Timed());
            }
            if(Global.hasKey && Global.FinalDoorUnlockable){
                Ghoul1.gameObject.SetActive(true);
                Ghoul2.gameObject.SetActive(true);
                Crawler.gameObject.SetActive(true);
            }
            if(BeforeWinning){
                Global.WinTheGame = true;
                EndingMusic.gameObject.SetActive(true);
                ChaseMusic.gameObject.SetActive(false);
            }
            if(WinTheGame){
                SuccessPanel.gameObject.SetActive(true);
                Player.transform.position = Global.originalPos;
            }
        }
    }

    IEnumerator Timed(){ 
        yield return new WaitForSeconds(6);
        Elf.gameObject.SetActive(false);
    }
}
