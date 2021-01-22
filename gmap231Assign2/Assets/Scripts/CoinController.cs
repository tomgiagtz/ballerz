using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	private bool goingUp = true;
	public float bounceSpeed = 1f;
	public float bounceDelta = 0.5f;
	private float originY;
	private float speedDampen = 0.01f;

	private void Start() {
		originY = transform.position.y;
	}
	void Update () {
		// rotate coin in y axis (x here beause coin model rotated 90 on z)
		transform.Rotate( 30 * Time.deltaTime, 0f, 0f);

		// if (goingUp) {
		// 	transform.Translate(transform.localPosition.x, transform.localPosition.y + (bounceSpeed * Time.deltaTime * speedDampen), transform.localPosition.z);
		// } else {
		// 	transform.Translate(transform.localPosition.x, transform.localPosition.y - (bounceSpeed * Time.deltaTime * speedDampen), transform.localPosition.z);
		// }
		// if (transform.up.y > originY + bounceDelta || transform.up.y < originY - bounceDelta) {
		// 	goingUp = !goingUp;
		// }

		//calculate what the new Y position will be
		float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceDelta + originY;
		//set the object's Y to the new calculated Y
		transform.position = new Vector3(transform.position.x, newY, transform.position.z) ;

	}
}	