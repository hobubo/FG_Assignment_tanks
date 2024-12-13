using UnityEngine;

public class Bullet: MonoBehaviour
{
    [SerializeField] float force = 15;
    void Start() {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other) {
        Destroy(this);
    }

    // void Update() {
    //     transform.position += transform.forward * Time.deltaTime * speed;
    // }
}
