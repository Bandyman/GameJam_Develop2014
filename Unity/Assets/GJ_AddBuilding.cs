using UnityEngine;
using System.Collections;

public class GJ_AddBuilding : MonoBehaviour
{
		// UI touching 
		public static bool is_enabled = false ;

		// Raycasting 
		private Ray _raycast;
		private RaycastHit _hitInfo;
		private LayerMask _mask = 1 << 9;
		private float _distance = 100f;

		// Object to create 
		private GameObject currentReference = null ;

		// Variable of the current building 
		private GameObject currentGameObject = null;
		private Vector3 currentPosition ;

		private enum PHASE
		{
				INIT,
				DOWN,
				RELEASE,
				IDLE
		}
	
		private PHASE current ;

		// Update is called once per frame
		void Update ()
		{
				if( !is_enabled )
						return ;

				if( Input.GetMouseButton( 0 )){
						current = Input.GetMouseButtonDown(0) ? PHASE.INIT : PHASE.DOWN ;
				} else {
						current = Input.GetMouseButtonUp( 0 ) ? PHASE.RELEASE : PHASE.IDLE ;
				}

				Handle_CurrentState();
		}

		public void Set_CurrentBuildingTo (GameObject _g)
		{
				currentReference = _g ;
		}


		private void Handle_CurrentState () {
				if( current == PHASE.IDLE ) return ;

				_raycast = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (_raycast, out _hitInfo, 10000f, _mask)) {
						// We just collided with something, FUCK_YEAH
						// Do something with it 
						currentPosition.x = (int)_hitInfo.point.x ;
						currentPosition.y = 0.5f ;
						currentPosition.z = (int)_hitInfo.point.z ;
				}

				switch ( current ){
				case PHASE.IDLE : return ;
				case PHASE.DOWN : 
						currentGameObject.transform.position = currentPosition ;
						break ;
				case PHASE.INIT : 
						currentGameObject = Instantiate( currentReference, currentPosition, Quaternion.identity ) as GameObject;
						break ;
				case PHASE.RELEASE :
						Debug.Log("Release Check") ;
						if( !GJ_LoadTerrain.Instance.Check_IfSpaceAvailable_ForAttraction (1, (int)currentPosition.x, (int)currentPosition.z) ) {
								Destroy( currentGameObject ) ;
						}
						break ;
				}
		}
}
