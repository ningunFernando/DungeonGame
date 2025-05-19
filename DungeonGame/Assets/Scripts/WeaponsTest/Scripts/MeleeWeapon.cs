using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Melee Settings")]
    public Vector3 hitboxCenter= Vector3.zero;
    public Vector3 hitboxSize = new Vector3(1,1,1);
    public LayerMask hitboxMask;
    public int damage = 1;
    public float activeDuration = 10f;
    public float hitDelay = 0f;

    protected override void OnTrigger()
    {
        StartCoroutine(DoMelee());
    }
    private IEnumerator DoMelee()
    {

        if (hitDelay > 0f) yield return new WaitForSeconds(hitDelay);

        float t = 0f;
        while (t < activeDuration)
        {
            Vector3 worldCenter = transform.TransformPoint(hitboxCenter);
            Collider[] hits = Physics.OverlapBox(worldCenter, hitboxSize* 0.5f, transform.rotation, hitboxMask);
            foreach (var c in hits)
            {
              //daño a enemigo  
            }
            t += Time.deltaTime;
            yield return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Matrix4x4 mat = Matrix4x4.TRS(transform.TransformPoint(hitboxCenter), transform.rotation, hitboxSize);
        Gizmos.matrix = mat;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
