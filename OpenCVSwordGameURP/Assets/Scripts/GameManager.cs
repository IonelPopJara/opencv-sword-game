using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartUdp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartUdp()
    {
        UdpManager.instance.Connect("127.0.0.1", 8078);

        UdpManager.instance.SendUDPMessage("data");
    }
}
