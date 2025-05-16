using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTemplate : MonoBehaviour, IDamageable
{
    public int life = 20;
    private int maxLife;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 2f, 0);

    private Image imageBar;
    private GameObject healthBarInstance;
    private Transform mainCamera;
    private GameObject canva;

    private void Start()
    {
        canva = GameObject.FindGameObjectWithTag("UIWorld");
        healthBarInstance = Instantiate(healthBarPrefab, canva.transform);
        imageBar = healthBarInstance.GetComponentInChildren<Image>();
        mainCamera = Camera.main.transform;
        UpdateHealthBarPosition();
        maxLife = life;
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        imageBar.fillAmount = (float)life/maxLife;


        if (life <= 0)
        {
            Explode();
        }
        
        Debug.Log($"the new life is {life}");
    }
    private void Update()
    {
        if (healthBarInstance != null)
        {

            UpdateHealthBarPosition();
           
        }
    }

    private void Explode()
    {
        Destroy(healthBarInstance);
        Destroy(this.gameObject);
    }
    private void UpdateHealthBarPosition()
    {
        Vector3 worldPosition = transform.position + healthBarOffset;
        healthBarInstance.transform.position = worldPosition;
    }
}
