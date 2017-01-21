using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float moveSpeed = 10f;
    public float turnSpeed = 150f;

    public GameObject bulletPrefub;
    public Transform bulletSpawn;

    public SteamVR_TrackedObject TrackedObjectRight;

    //void Start () {
    //    bulletSpawn.transform.SetParent(TrackedObjectRight.gameObject.transform, false);
    //    bulletSpawn.transform.localPosition = Vector3.zero;
    //    bulletSpawn.transform.localRotation = Quaternion.Euler(0, 0, 0);
    //}

    // Update is called once per frame
    void Update ()
	{
        if (!isLocalPlayer) return;
        
        float x = Input.GetAxis("Horizontal")*Time.deltaTime * turnSpeed;
        float z = Input.GetAxis("Vertical")* Time.deltaTime * moveSpeed;

	    transform.Rotate(0, x, 0);
	    transform.Translate(0, 0, z);

        if (ControllerRight.GetHairTriggerDown())
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefub, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward*6f;
        NetworkServer.Spawn(bullet);
        Destroy(bullet,2); 
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    private SteamVR_Controller.Device ControllerRight
    {
        get { return SteamVR_Controller.Input((int)TrackedObjectRight.index); }
    }

}

