using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerUpVFX {

	public class DemoManager : MonoBehaviour {
		public Camera main_camera;
		public GameObject[] RestartThis;


		void Start () {
			StartCoroutine(RestartAnim());
			Destroy(GameObject.Find("Instructions"), 9.5f);
		}
		
		void Update () {
			if ( Input.GetKeyDown("up") ){
				main_camera.fieldOfView -= 1.0f;
				if(main_camera.fieldOfView <= 14)
					main_camera.fieldOfView = 14;
			}

			if ( Input.GetKeyDown("down") ){
				main_camera.fieldOfView += 1.0f;
				if(main_camera.fieldOfView >= 33.0f)
					main_camera.fieldOfView = 33.0f;
			}

			if ( Input.GetKeyDown("left") ){
				float aux_left = main_camera.transform.position.x;
				if(aux_left > 0){
					aux_left -= 50;
					main_camera.transform.position = new Vector3(aux_left, main_camera.transform.position.y, main_camera.transform.position.z);
				}					
			}

			if ( Input.GetKeyDown("right")){
				float aux_right = main_camera.transform.position.x;
				if(aux_right < 250){
					aux_right += 50;
					main_camera.transform.position = new Vector3(aux_right, main_camera.transform.position.y, main_camera.transform.position.z);
				}
			}


		}


		IEnumerator RestartAnim()
	    {
	        while(true){
	        	yield return new WaitForSeconds(3.5f);
	        	for(int i = 0; i < RestartThis.Length; i++){
	        		RestartThis[i].GetComponent<AnimationHelper>().PLAY_VFX();
	        	}
	        }	        
	    }


	}

}