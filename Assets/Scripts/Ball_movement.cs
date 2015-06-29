using UnityEngine;
using System.Collections;

public class Ball_movement : MonoBehaviour {

	//Collider2D collider;
	bool click_or_tap=true;
	bool death=false;
	static int score=0;
	//Включает деньги через 2с
	float refreshTime=2.0f;
	GameObject obj;
	bool moneySwitcher=false;
	// Use this for initialization
	void Start () {

	}
	//Обновление графики и ввод
	void Update(){
		if (death)
			return;
		if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) {
			click_or_tap=true;
		}

	}
	
	// Обновление физики
	void FixedUpdate () {

		GameObject Player= GameObject.FindGameObjectWithTag("Player");
		Vector3 pos = Player.transform.position;

			if (death)//Если попал на лаву, ничего не делать
				return;
		if (pos.y >= 8.5) {
			pos.y=8.5f;
		}
			else if (click_or_tap) {
			pos.y +=4* Time.deltaTime;

				click_or_tap = false;
			}
		pos.x += 0.05f;
			pos.x += 1f * Time.deltaTime;
			pos.y -= 0.04f;
			
			Player.transform.position = pos;
		//Включить деньги через 2с
		if(moneySwitcher==true){
			refreshTime-=Time.deltaTime;
			if(refreshTime<=0){
				obj.SetActive(true);
				refreshTime=2.0f;
				moneySwitcher=false;
			}
		}
}
	void OnGUI(){

		GUI.Box (new Rect (0, 0, 70, 30), "Score:"+score);
	}
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.tag == "money") {
			score++;
			collision.collider.gameObject.SetActive(false);
			//Сохранить обьект, чтоб позже включить снова
			obj=collision.collider.gameObject;
			moneySwitcher=true;
		}
		else
		if (collision.collider.tag == "ground") {
			collision.collider.gameObject.SetActive(false);
		}
		else
		if (collision.collider.tag=="lavacube") {
			score=0;
			Application.LoadLevel (Application.loadedLevel);
			death = true;
		}
		}
	
}