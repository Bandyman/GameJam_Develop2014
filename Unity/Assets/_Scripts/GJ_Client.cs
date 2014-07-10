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

				index_H = Random.Range( 0, GJ_ManageBuilding.terrain_Height ) ;
				index_W = Random.Range( 0, GJ_ManageBuilding.terrain_Width ) ;

				Vector3 pos = new Vector3( index_W, 0f, index_H );
				transform.position = pos ;

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
				case STATE.LEAVING : 


						break ;
					}
				
		}
		private bool leaving = false ;
		private void Leave_NewDirection () {

				if( GJ_AI.Instance.Check_ForAttractions( index_W, index_H) ){
						Be_IdleFor( 1f );
				}

				StartCoroutine( Move_Hunger () );
		}


		private void MoveTo_NewDirecton () {

				if( leaving ) {
						StartCoroutine( Move_Leaving() );
						return;
				}

				if( GJ_AI.Instance.Check_ForAttractions( index_W, index_H) ){
						Be_IdleFor( 1f );
						return ;
				}

				StartCoroutine( Move_Hunger () );
		}
		private IEnumerator Move_Hunger () {
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
						index_H += 1 ;
						break ;
				case DIR.RIGHT :
						target.z -= 1f ;
						index_H -= 1 ;
						break ;
				}
				if( index_H < 0 ){
						index_H = 0 ;
						happiness -= 10 ;
				}
				if( index_W < 0 ){
						index_W = 0 ;
						happiness -= 10 ;
				}
				if( index_H >= GJ_ManageBuilding.terrain_Height ){
						index_H =  GJ_ManageBuilding.terrain_Height -1 ;
				}
				if( index_W >= GJ_ManageBuilding.terrain_Width ){
						index_W =  GJ_ManageBuilding.terrain_Width -1 ;
				}

				happiness -= Random.Range(0, 5);

				float alpha = 0f ;
				while ( alpha < 1f ){
						transform.position = Vector3.Lerp( origin, target, alpha );
						yield return null ;
						alpha += Time.deltaTime ;
				}

				currentState = STATE.IDLE ;
		}

		private IEnumerator Move_Leaving () {
				currentState = STATE.BUSY ;

				Vector3 target = transform.position ;
				Vector3 origin = transform.position ;
				switch (GJ_AI.Instance.Get_NewDirectionLEAVING(index_W, index_H)) {
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
						index_H += 1 ;
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

				Debug.Log( "New Pos is Width : " + index_W + "height : " + index_H );
				currentState = STATE.IDLE ;
		}


		private Vector3 tempPos ;

		private IEnumerator Be_IdleFor( float seconds ) {
				Debug.Log( "BUSY" );

				currentState = STATE.BUSY ;
				Vector3 mem = transform.position ;
				transform.position = new Vector3( 1000f, 0f , 0f ) ;
				while (seconds > 0f ){
						seconds -= Time.deltaTime ;
						yield return null ;
				}
				transform.position = mem ;
				currentState = STATE.IDLE ;
				int val = Random.Range( 5, 15 ) ;
				moneyLeft -= val ;
				PlayerProfile.Instance.Money -= val ;

		}

}
