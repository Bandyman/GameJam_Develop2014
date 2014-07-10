using UnityEngine;
using System.Collections;
using  DIR = GJ_AI.DIRECTION ;

public class GJ_Client : MonoBehaviour {

		private enum STATE {
				NEW,
				MOVING,
				BUSY,
				IDLE,
				LEAVING 
		}

		
		DIR direction ;

		private int moneyLeft = 0 ;
		private int happiness = 0 ;
		private int thirst = 0 ;
		private int hunger = 0 ;
		private int sickness = 0 ;

		private Transform target = null ;
		private STATE currentState = STATE.NEW;

		private int index_W = 3 ;
		private int index_H = 0 ;

	// Use this for initialization
	void Start () {
				moneyLeft = Random.Range( 50, 500 );
				happiness = Random.Range( 50, 100 );

				thirst = Random.Range( 0, 50 ) ;
				hunger = Random.Range( 0, 50 ) ;
				sickness = 0 ;

				currentState = STATE.NEW;

				StartCoroutine( COROUTINE_StartIntro() );
	}

		private IEnumerator COROUTINE_StartIntro () {
				yield return null ;

				currentState = STATE.IDLE ;
		}
	
	// Update is called once per frame
	void Update () {
				switch( currentState ){
				case STATE.BUSY : 
				case STATE.NEW : return ;

				case STATE.IDLE :
						MoveTo_NewDirecton() ;
						break ;
				case STATE.MOVING :
						break ;
				}
			
	}

		private void MoveTo_NewDirecton () {
				StartCoroutine( Move () );
		}
		private IEnumerator Move () {
				currentState = STATE.BUSY ;

				Vector3 target = transform.position ;
				Vector3 origin = transform.position ;
				switch (GJ_AI.Instance.Get_NewDirection(index_W, index_H)) {
				case DIR.DOWN :
						target.x -= 1f ;
						index_W -= 1 ;
						break ;
				case DIR.UP :
						target.x += 1f ;
						index_W += 1 ;
						break ;
				case DIR.LEFT :
						target.z += 1f ;
						index_W += 1 ;
						break ;
				case DIR.RIGHT :
						target.z -= 1f ;
						index_H -= 1 ;
						break ;
				}
				float alpha = 0f ;
				while ( alpha < 1f ){
						transform.position = Vector3.Lerp( origin, target, alpha );
						yield return null ;
						alpha += Time.deltaTime ;

				}
				currentState = STATE.IDLE ;
		}


		private Vector3 tempPos ;

		private IEnumerator Be_IdleFor( float seconds ) {
				currentState = STATE.BUSY ;

				while (seconds > 0f ){
						seconds -= Time.deltaTime ;
						yield return null ;
				}
				currentState = STATE.IDLE ;
		}

}
