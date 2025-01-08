using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform target;
    public float speed = 70f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (direction.magnitude <= distance) {
            Hit();
            return;
        }

        transform.Translate(direction.normalized * distance, Space.World);
    }

    void Hit()
    {
        target.gameObject.GetComponent<Enemy>().TakeDamage(20);
        //Destroy(target.gameObject);
        Destroy(gameObject);
    }
}


