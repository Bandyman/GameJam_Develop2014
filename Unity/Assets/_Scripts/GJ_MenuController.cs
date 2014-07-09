using UnityEngine;
using System.Collections;

public class GJ_MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


		public void BTNPressed_NewGame() {
				Application.LoadLevel( "Game" ) ;
		}
}
