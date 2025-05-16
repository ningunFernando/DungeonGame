using UnityEngine;

public class EnemyTemplate : MonoBehaviour, IDamageable
{
    public int life = 20;

    public void TakeDamage(int damage)
    {
        life -= damage;


        if (life <= 0)
        {
            Explode();
        }
        
        Debug.Log($"the new life is {life}");
    }

    private void Explode()
    {
        Destroy(this.gameObject);
    }
}
