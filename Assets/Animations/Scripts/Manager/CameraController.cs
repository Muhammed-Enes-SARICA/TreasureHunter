using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform room;
    public Transform activeRoom;

    public float cameraSpeed = 3f;

    
    public static CameraController instance;

    [Range(-5,5)]
    public float minModX,maxModX,minModY,maxMody;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
  
    void Start()
    {
        player = PlayerController.instance.gameObject.transform;
        activeRoom = player;
    }

    // Update is called once per frame

    //Kamera max ve min xy deðerlerine sabitlenir. Rooms için...
    void Update()
    {
       

        var minPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y+minModY;
        var maxPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.max.y+maxMody;
        var minPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x+minModX;
        var maxPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.max.x+maxModX;

        Vector3 clampedPos = new Vector3(Mathf.Clamp(player.position.x, minPosX, maxPosX),
            Mathf.Clamp(player.position.y,minPosY,maxPosY),
            Mathf.Clamp(player.position.z,-10,-10));

        Vector3 smoothPos = Vector3.Lerp(transform.position,clampedPos,cameraSpeed*Time.deltaTime);

        transform.position = smoothPos;
    }
}
