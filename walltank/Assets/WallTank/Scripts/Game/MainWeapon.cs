using UnityEngine;
using System.Collections;

public class MainWeapon : MonoBehaviour {

    public float bombRange = 2.0f;
    public float lifeTime = 2.0f;
    public GameObject explosion;
    public float atkPower = 5.0f;
    //public float speed;
    private Rigidbody myRigidBody;

    private float startTime;
    public int bound;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        //myRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (bound <= 0)
        {
            AudioManager.I.PlayAudio("attack", 0.4f);
            GameObject explosionObject = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            explosionObject.transform.parent = gameObject.transform.parent;
            Destroy(explosionObject.gameObject, 1.5f);
            Destroy(gameObject);

        }
        if ((Time.time - startTime) > lifeTime)
            bound = 0;
	}

    public void Shot(float speed)
    {
        myRigidBody = GetComponent<Rigidbody>();
        Vector3 v = this.transform.forward * speed;
		// Todo:ここでバグが発生しているようなので山田君対処お願いします
        // 対処しました
		this.myRigidBody.AddForce(v, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Wall") || c.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            bound -= 1;
        }
        /*
        if (c.gameObject.CompareTag("MainWeapon"))
        {
            bound = 0;
        }
        */
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            c.gameObject.GetComponent<Tank>().Damage(atkPower);
            bound = 0;
        }
    }
}
