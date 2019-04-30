using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getlocalip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string hostName = System.Net.Dns.GetHostName();
        string localIP = System.Net.Dns.GetHostEntry(hostName).AddressList[0].ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
