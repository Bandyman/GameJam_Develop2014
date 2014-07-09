using UnityEngine;
using System.Collections;

public class GJ_Client : MonoBehaviour {

		private enum STATE {
				NEW,
				MOVING,
				BUSY,
				IDLE,
				LEAVING 
		}

		private enum DIRECTION {

		}

		private int moneyLeft = 0 ;
		private int happiness = 0 ;
		private int thirst = 0 ;
		private int hunger = 0 ;
		private int sickness = 0 ;

		private Transform target = null ;
		private STATE currentState = STATE.NEW;

		private int index_X = 0 ;
		private int index_Y = 0 ;

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
						FindNew_Objective() ;
						break ;
				case STATE.MOVING :
						break ;
				}
			
	}
				
		private IEnumerator COROUTINE_TranslateCharacter( ) {

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
