using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStates : MonoBehaviour
{
    [SerializeField] PostProcessVolume MyVolume;
    [SerializeField] PostProcessProfile GotHurt1;
    [SerializeField] PostProcessProfile GotHurt2;
    [SerializeField] PostProcessProfile Death;
    [SerializeField] Text Health;
    [SerializeField] GameObject Intro;
    [SerializeField] GameObject Instruction;
    [SerializeField] GameObject DeathPanel;
    [SerializeField] GameObject Scream;
    // Start is called before the first frame update
    void Start()
    {
        Global.originalPos = gameObject.transform.position;
        Global.HP = 100;
        DeathPanel.gameObject.SetActive(false);
        Intro.gameObject.SetActive(true);
        Instruction.gameObject.SetActive(true);
        StartCoroutine(IntroTimed());
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = Global.HP.ToString();
        if(Global.HP <= 100 && Global.HP > 70){
            MyVolume.profile = null;
        }
        
        if(Global.HP <= 70 && Global.HP > 40){
            MyVolume.profile = GotHurt1;  
        }
        
        if(Global.HP <= 40 && Global.HP > 0){
            MyVolume.profile = GotHurt2;
        }
        
        if(Global.HP <= 0){
            MyVolume.profile = Death;
            Global.GameOver = true;
            DeathPanel.gameObject.SetActive(true);
            Scream.gameObject.SetActive(true);
            StartCoroutine(ResetTimed());    
        }
    }

    IEnumerator IntroTimed(){ 
        yield return new WaitForSeconds(4);
        Intro.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Instruction.gameObject.SetActive(false);
    }

    IEnumerator ResetTimed(){ 
        yield return new WaitForSeconds(4);
         Scream.gameObject.SetActive(false);
    }
    
}
