using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    public RoomController currentRoom;

    public KeyCode spojiKeyCode = KeyCode.O;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spojiKeyCode) && currentRoom != null) {
            currentRoom.SpojiObjekte();
        }
        
    }
}
