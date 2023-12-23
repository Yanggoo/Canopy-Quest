using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController:MonoBehaviour
{
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject sickle;
    [SerializeField]
    private GameObject bow;
    [SerializeField]
    private GameObject bomb;
    public void EnableGun()
    {
        gun.SetActive(true);
    }
    public void EnableSickle()
    {
        sickle.SetActive(true);
    }
    public void EnableBow()
    {
        bow.SetActive(true);
    }
    public void EnableBomb()
    {
        bomb.SetActive(true);
    }
}
