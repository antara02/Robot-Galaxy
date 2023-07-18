using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;

    public float reloadTime;
    public int bulletsLeft, magazineSize;
    bool readyToShoot = true;
    public TextMeshProUGUI ammoDisplay;
    //public Recoil recoil;

    public GameObject muzzleFlash;
    public float impactForce = 30f;
    public Transform fpsCam;
    
    private float nextTimeToFire = 0f;

    public Transform location;


    //private Recoil Recoil_Script;

    private void Start()
    {
        bulletsLeft = magazineSize;

    //    Recoil_Script = transform.Find("../../MainCamera").GetComponent<Recoil>();
    }

    void Update()
    {
        ammoDisplay.SetText(string.Concat(bulletsLeft));

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize)
            Reload();

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire && bulletsLeft > 0 && readyToShoot)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject temp = Instantiate(muzzleFlash, location.position, Quaternion.identity);

        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, -fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

        //    EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

        //     if(target != null)
        //     {
        //         target.TakeDamage(damage);
        //     }

            EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();
            if(enemy != null){
                enemy.TakeDamage(10f);
            }
        }
        //recoil.recoil();

      //  Recoil_Script.RecoilFire();

        bulletsLeft--;
        Destroy(temp, 0.5f);
    }

    private void Reload()
    {
        Invoke("ReloadFinished", reloadTime);
        readyToShoot = false;
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
}
