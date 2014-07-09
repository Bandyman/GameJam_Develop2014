using UnityEngine;
using System.Collections;

public class BTN_Building : MonoBehaviour {


		public void OnPress( bool isPressed ) {
				GJ_AddBuilding.is_enabled = false ;

				if( !isPressed ) 
						GJ_AddBuilding.is_enabled = true ;

				Debug.Log(isPressed);

		}
}
