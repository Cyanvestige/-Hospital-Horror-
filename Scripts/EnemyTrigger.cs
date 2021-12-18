using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Collider Col;
    private AudioSource OneShotSound;
    public bool FirstSceneTrigger;
    [SerializeField] GameObject Crawler;
    // [SerializeField] GameObject DoorQuad;
    // private bool isDoorOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        OneShotSound = GetComponent<AudioSource>();
        Col = GetComponent<Collider>();
        // isDoorOpen = DoorQuad.GetComponent<DoorScript>().IsOpen;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && FirstSceneTrigger && Global.DoorEventTriggerd){
            Crawler.gameObject.SetActive(true);
        }
    }
}
