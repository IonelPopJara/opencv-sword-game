using TMPro;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;

    [Header("Weird Stats")]
    public float upwardForce;
    public float spread;

    [Header("Graphics and UI")]
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    private void Update()
    {
        // set ammo display, if it exists
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(" / ");
    }

    public void Shoot(GameObject bullet, float bulletForce)
    {

        // calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = attackPoint.forward;

        // calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); // add spread to the last direction

        // instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        // rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        // add froces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * bulletForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(attackPoint.transform.up * upwardForce, ForceMode.Impulse);

        StartCoroutine(FindObjectOfType<CameraShake>().Shake(0.1f, 0.1f));

        // instantiate muzzle flash if you have one
        if (muzzleFlash != null)
        {
            GameObject muz = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            Destroy(muz, 2f);
        }
    }
}
