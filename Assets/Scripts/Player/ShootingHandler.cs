using UnityEngine;
using System.Collections;
using StealthARLibrary;
using Photon.Pun;
public class ShootingHandler : ActivatableObject
{
    [SerializeField] ItemSO itemStats;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootForce;
    //Optional if not spawned by network
    [Header("Optional")]
    [SerializeField] GameObject objectToShootFrom;

    private float currentTime = 0;
    private float armo = 0;
    private float maxShoots;
    private float shootdelay = 0;
    private bool onReloadeCooldown = false;

    public void Start()
    {
        maxShoots = itemStats.ItemCount;
        armo = maxShoots;
    }
    public void Update()
    {
        shootdelay += Time.deltaTime;
        if (!onReloadeCooldown) return;
        currentTime += Time.deltaTime;
        if (currentTime >= itemStats.Cooldown)
        {
            onReloadeCooldown = false;
            currentTime = 0;
        }
    }
    public override void OnActivate()
    {
        
        if (onReloadeCooldown) return;
        if(armo > 0 && shootdelay >=1)
        {
            armo--;
            Debug.Log("Just shot");
            GameObject spawnedBullet = PhotonNetwork.Instantiate(bullet.name, objectToShootFrom.transform.position,Quaternion.identity);
            spawnedBullet.GetComponent<Rigidbody>().AddForce(objectToShootFrom.transform.forward * shootForce, ForceMode.Impulse);
            currentTime = 0;
            shootdelay = 0;
        }
        Debug.Log("Armo:" + armo);
        if (armo <= 0)
        {
            armo = maxShoots;
            onReloadeCooldown = true;
        }

       
    }

    public override Sprite GetButtonSprite()
    {
        return itemStats.Icon;
    }
}
