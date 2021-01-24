using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    //rate of each bounce
    public float bounceSpeed = 1f;
    //vertical distance from origin of each bounce
    public float bounceDelta = 0.25f;
    //height for the bounce to be centered on
    private float originY;

    private void Start()
    {
        originY = transform.position.y;
        //subscribe to onCoinPickup event
        GameEvents.current.onCoinPickup += HandleCoinPickup;
    }
    private void OnDisable()
    {
        //unsubscribe from coin pick up event when object is disabled
        GameEvents.current.onCoinPickup -= HandleCoinPickup;
    }

    //function to be run on the Coin pickup event
    private void HandleCoinPickup(int _id)
    {
        //if the passed id is this objects id, set inactive
        if (_id == gameObject.GetInstanceID()) {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        // rotate coin in y axis (x here beause coin model rotated 90 on z)
        transform.Rotate(30 * Time.deltaTime, 0f, 0f);

        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceDelta + originY;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
}