using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	public GameObject hand;
	// Use this for initialization
	void Start () {
//		transform.Rotate(-270,0,0);
	}
	
	// Update is called once per frame
	void Update () {
//	transform.position.x = hand.transform.position.x;
//	transform.position.y = hand.transform.position.y;
//
//		if(Input.GetKey("w")){
			transform.position = new Vector3(hand.transform.position.x-.1f, hand.transform.position.y+.1f, hand.transform.position.z+.2f);
//		}	
//		else {

//			transform.position = new Vector3(hand.transform.position.x-.15f, hand.transform.position.y+.1f, hand.transform.position.z+.2f);
//		}
//
//		if(Input.GetKeyDown("w")){
//			transform.Rotate(-270,0,0);
//		}
//		if (Input.GetKeyUp ("w")) {
//			transform.Rotate (270, 0, 0);
//		} 
//

	}
}
