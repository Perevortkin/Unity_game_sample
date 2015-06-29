using UnityEngine;
using System.Collections;

public class BGLoop : MonoBehaviour {
	float minPosLava=0.3f;
	float maxPosLava=6.3f;
	int numOfPanels=12;
	int numOfLava=3;
	float groundboxpos=11.9f;
	bool isPassed=true;
	public GameObject ground;
	void Start(){
		GameObject[] boxes= GameObject.FindGameObjectsWithTag("lava");
		foreach (GameObject box in boxes) {
			Vector3 pos = box.transform.position;
			pos.y=Random.Range(minPosLava,maxPosLava);
			box.transform.position = pos;
		}
		ground=GameObject.FindGameObjectWithTag("groundbox");
		Instantiate (ground, new Vector3 (groundboxpos, 5, 0), Quaternion.identity);
		groundboxpos += 11;
		Instantiate (ground, new Vector3 (groundboxpos, 5, 0), Quaternion.identity);
		groundboxpos += 11;
		Instantiate (ground, new Vector3 (groundboxpos, 5, 0), Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D collider){
		float widthOfObject = ((BoxCollider2D)collider).size.x;
		Vector3 pos = collider.transform.position;
		if(collider.gameObject.tag=="money")
			collider.gameObject.SetActive(true);
		else if (collider.tag == "ground") {
			//Если попадаем на кубик земли, смотрим
			//не прошел ли уже триггер блоки лавы
			if(isPassed){
				groundboxpos += 11f;
				Instantiate (ground, new Vector3 (groundboxpos, 5, 0),
				                     Quaternion.identity);
				isPassed=false;
			}
			Destroy (collider.gameObject);
		}
		else
		if (collider.tag == "lava") {
			pos.y = Random.Range (minPosLava, maxPosLava);
			pos.x += widthOfObject * numOfLava;
			isPassed=true;
		} else
			pos.x += widthOfObject * numOfPanels;
		Debug.Log (pos.x);
		collider.transform.position = pos;
	}

}
