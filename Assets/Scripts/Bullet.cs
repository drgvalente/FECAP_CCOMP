using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f); // destrói a própria bullet após 5 segundos
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Acertou Enemy");
            Enemy en = col.GetComponent<Enemy>();
            en.TakeDamage();
        }
        Destroy(gameObject);
    }
}
