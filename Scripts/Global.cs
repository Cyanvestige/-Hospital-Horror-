using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static bool NewGame;
    public static bool isFlashLightOn;
    public static float batteryNum;
    public static float BatteryPower;
    public static bool hasKey;
    public static bool DoorEventTriggerd;
    public static bool KeyEventTriggered;
    public static bool SpecialDoorUnlockable;
    public static bool JumpScareDoorUnlockable;
    public static bool FinalDoorUnlockable;
    public static float HP;
    public static bool GameOver;
    public static bool WinTheGame;
    public static Vector3 originalPos;
    

    private void Start(){
        if(NewGame == true){
            NewGame = false;
            isFlashLightOn = false;
            batteryNum = 1;
            BatteryPower = 0;
            hasKey = false;
            DoorEventTriggerd = false;
            KeyEventTriggered = false;
            SpecialDoorUnlockable = false;
            JumpScareDoorUnlockable = false;
            FinalDoorUnlockable = false;
            HP = 100;
            GameOver = false;
            WinTheGame = false;
        }
    }
}

