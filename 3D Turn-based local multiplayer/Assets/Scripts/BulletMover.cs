using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField, Tooltip("This variable tells the trigger algorithm what to ignore")] float speed = 1.0f;
    public string opponentTag = "Player";

    public Vector3 direction = new Vector3(0, 1, 0);

    public AudioSource impactSoundPrefab;

    public Vector3 velocity;

    void Update()
    {
        //Bullet movement
        //transform.position = transform.position + direction.normalized * Time.deltaTime * speed; 
        //Vector3.up replaces direction.normalized

        velocity = direction * speed; //Change speed and direction whenever I want by constantly updating.
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other) //other == what we collide with
    {
        if (other.CompareTag(opponentTag))
        {
            var impactSoundInstance = Instantiate(impactSoundPrefab);
            impactSoundInstance.transform.position = transform.position;
            Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
        }
    }
}
