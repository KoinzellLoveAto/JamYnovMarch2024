using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoImageManager : MonoBehaviour
{
    public ATurretShootController turretShootController;
    public bool isClak;
    public bool isChiot;
    public bool isSoap;

    public GameObject clak;
    public GameObject chiot;
    public GameObject soap;


    public AudioSource chiotSFX;
    public AudioSource soapSFX;
    public AudioSource clakSFX;


    bool hasPlayed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((turretShootController.currentAmmoPrefab.gameObject.tag == "Clak"))
        {
            //clakSFX.Play();
            isClak = true;
            isSoap = false;
            isChiot = false;
        }

        if ((turretShootController.currentAmmoPrefab.gameObject.tag == "Chiot"))
        {
            //chiotSFX.Play();
            isChiot = true;
            isSoap = false;
            isClak = false;
        }

        if ((turretShootController.currentAmmoPrefab.gameObject.tag == "Soap"))
        {
            //soapSFX.Play();
            isSoap = true;
            isClak = false;
            isChiot = false;
        }


        if (isClak)
        {
            clak.SetActive(true);
        }
        else
        {
            clak.SetActive(false);
        }

        if (isChiot) 
        { 
            chiot.SetActive(true);
        }
        else { chiot.SetActive(false); }


        if (isSoap) 
        {
            soap.SetActive(true);
        }
        else
        {
            soap.SetActive(false);
        }
    }


    public IEnumerator PlaySound()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (isClak)
        {
            clakSFX.Play();
        }

        if (isChiot)
        {
           chiotSFX.Play();
        }



        if (isSoap)
        {
            soapSFX.Play();
        }
    }
}
