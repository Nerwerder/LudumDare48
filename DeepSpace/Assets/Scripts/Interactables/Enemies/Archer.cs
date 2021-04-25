using UnityEngine;
using UnityEngine.Assertions;

public class Archer : Enemy
{
    public Transform weapon;
    public float minWeaponCoolDown = 1f;
    public float maxWeaponCoolDown = 3f;
    public float weaponRange = 40f;
    public float bulletSpeed = 5f;
    public int bulletDamage = 1;
    public GameObject bulletPrefab;
    public GameObject bulletParentPrefab;

    private Transform bulletParent;
    float waponCoolDownTimer = 0f;
    float curWeaponCoolDown = 0f;

    private new void Start() {
        base.Start();
        //Check if a BulletParent exists
        Assert.IsNotNull(bulletParentPrefab);
        var bp = GameObject.Find("/BulletParent");
        if(bp == null)
            bp = Instantiate(bulletParentPrefab);
        bulletParent = bp.transform;
        Assert.IsNotNull(bulletParent);
        calcNewWeaponCoolDown();
    }

    void shoot() {
        var go = Instantiate(bulletPrefab, weapon.position, weapon.rotation, bulletParent);
        var bu = go.GetComponent<Bullet>();
        Assert.IsNotNull(bu);
        bu.velocity = (target.position - transform.position).normalized * bulletSpeed;
        bu.range = weaponRange;
        bu.source = (Vector2)transform.position;
        bu.damage = bulletDamage;
    }

    void calcNewWeaponCoolDown() {
        waponCoolDownTimer = 0f;
        curWeaponCoolDown = Random.Range(minWeaponCoolDown, maxWeaponCoolDown);
    }

    void Update()
    {
        turnToTaget();
        waponCoolDownTimer += Time.deltaTime;
        var tDis = Vector2.Distance((Vector2)target.position, (Vector2)transform.position);
        if ((tDis < weaponRange) && (waponCoolDownTimer >= curWeaponCoolDown) && (getAngleToTaget() < 5f)) {
            shoot();
            calcNewWeaponCoolDown();
        }
        if (tDis > (weaponRange / 2)) {
            rb.AddForce(transform.right * Time.deltaTime * movementForce);
        }
    }
}
