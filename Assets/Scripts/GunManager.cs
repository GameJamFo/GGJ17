using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class GunManager : MonoBehaviour {

    public int AmmoMax;
    public int Ammo;
    public GameObject Bullet;
    public Transform Endpoint;
    public GameObject Player;
    public VignetteAndChromaticAberration timeRift;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        GameObject.Find("AmmoText").GetComponent<Text>().text = string.Format("{0} Ammo", Ammo);
        if (Ammo < AmmoMax) {
            Image AmmoMeter = GameObject.Find("AmmoBar").GetComponent<Image>();
            AmmoMeter.enabled = true;
            AmmoMeter.fillAmount += Player.GetComponent<CharacterController>().velocity.normalized.sqrMagnitude / 75;
            if (AmmoMeter.fillAmount >= 1) 
            {
                AmmoMeter.fillAmount = 0;
                AmmoMeter.enabled = false;
                Ammo += 1;
            }
        }
        StabilizeGunEffects();
    }

    void StabilizeGunEffects()
    {
        if(timeRift.chromaticAberration > 0)
        {
            timeRift.chromaticAberration -= Time.deltaTime * 50;
        }
    }

    void Fire()
    {
        if(Ammo > 0)
        {
            timeRift.chromaticAberration = 100;
            Ammo--;
            var bullet = Instantiate(Bullet, Endpoint.position, Endpoint.rotation);
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().AddForce(Endpoint.forward * 30, ForceMode.Impulse);
            Destroy(bullet, 10);
        }
    }
}
