using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    private float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;
    private bool dropped;

    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;

    public float heartOffset;
    public GameObject heartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();

        runSpeed = GameManager.Instance.sheepRunSpeed;

        /* Below was my initial method to change the runSpeed value of the sheep.
         * As you can see the values are all hardcoded in.
         * I instead opted to change to an Update based Timer that increases speed every 5 seconds 
         * You can find the new implementation in the GameManager script Update()*/
        //SetSpeed();
    }

    //private void SetSpeed()
    //{
    //    //if(GameManager.Instance.sheepSaved > 5)
    //    //{
    //    //    runSpeed = runSpeed * 1.15f;
    //    //}
    //    //else if (GameManager.Instance.sheepSaved > 10)
    //    //{
    //    //    runSpeed = runSpeed * 1.25f;
    //    //}
    //    //else if (GameManager.Instance.sheepSaved > 15)
    //    //{
    //    //    runSpeed = runSpeed * 1.5f;
    //    //}
    //    //else if (GameManager.Instance.sheepSaved > 20)
    //    //{
    //    //    runSpeed = runSpeed * 2f;
    //    //}
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);

        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;

        AudioManager.Instance.PlaySheepHitClip();

        GameManager.Instance.SheepSaved();

        Destroy(gameObject, gotHayDestroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep") && !dropped)
        {
            Drop(); 
        }
    }

    private void Drop()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        dropped = true;
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;

        GameManager.Instance.SheepDropped();

        AudioManager.Instance.PlaySheepDroppedClip();

        Destroy(gameObject, dropDestroyDelay);
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
