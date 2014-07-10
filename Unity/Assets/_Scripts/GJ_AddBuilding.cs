using UnityEngine;
using System.Collections;

public class GJ_AddBuilding : MonoBehaviour
{

		// To Create and Instantiate the different buildings
		[SerializeField] private GameObject[] listPrefabs ;
		private GJ_Building[] listBuildings ;

		[SerializeField] private UI_Manager UIController ;

		[SerializeField] private GameObject DummyRectangle ;

		[SerializeField] private Material allowedMat ;
		[SerializeField] private Material forbideMat ;

		private Vector3 midScreen ;

		// UI touching 
		public static bool is_enabled = false ;

		// Raycasting 
		private Ray _raycast;
		private RaycastHit _hitInfo;
		private LayerMask _mask = 1 << 9;
		private float _distance = 100f;



		// Object to create 
		private int currentReference = -1 ;

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

		private void Start () {
				current = PHASE.IDLE ;
				midScreen = new Vector3 ( Screen.width/2f, Screen.height/2f, 0f);
				DummyRectangle.SetActive( false );
			
				listBuildings = new GJ_Building[listPrefabs.Length] ;

				for(int i=0; i<listBuildings.Length; i++ ) {
						listBuildings[i] = listPrefabs[i].GetComponent<GJ_Building>();
				}
		}

		// Update is called once per frame
		void Update ()
		{
				if( !is_enabled )
						return ;

				Handle_CurrentState();
		}


		private void Activate_DummyRectangle () {
				_raycast = Camera.main.ScreenPointToRay (midScreen);

				if (Physics.Raycast (_raycast, out _hitInfo, 10000f, _mask)) {
						// We just collided with something, FUCK_YEAH
						// Do something with it 
						currentPosition.x = (int)_hitInfo.point.x ;
						currentPosition.y = 0.5f ;
						currentPosition.z = (int)_hitInfo.point.z ;
				}

				Debug.Log( "Checking at " + currentPosition + " :: "+listBuildings[currentReference].size );
				bool allowedMove =  GJ_ManageBuilding.Instance.Check_IfSpaceAvailable_ForAttraction (1, (int)currentPosition.x, (int)currentPosition.z);
				DummyRectangle.renderer.material = allowedMove ? allowedMat : forbideMat ;
				DummyRectangle.transform.position = currentPosition ;
				DummyRectangle.transform.localScale = listBuildings[currentReference].size * Vector3.one;
				DummyRectangle.SetActive( true ) ;

				current= PHASE.DOWN ;
		}

		public bool Set_CurrentBuildingTo (int _reference)
		{
				if( PlayerProfile.Instance.Money > listBuildings[_reference].price ){
						currentReference = _reference ;
						Activate_DummyRectangle() ;
						return true ;
				}
				else {
						return false ;
				}
		}

		public void Manager_CancelBuilding () {
				current =  PHASE.IDLE ;
				currentReference = -1 ;
				DummyRectangle.SetActive( false ) ;
		}

		public bool Manager_BuyBuilding () {
				bool allowedMove =  GJ_ManageBuilding.Instance.Check_IfSpaceAvailable_ForAttraction (listBuildings[currentReference].size, (int)currentPosition.x, (int)currentPosition.z);
				DummyRectangle.renderer.material = allowedMove ? allowedMat : forbideMat ;
				if( !allowedMove ){
						return false ;
				}

				PlayerProfile.Instance.Money -= listBuildings[currentReference].price ;
				DummyRectangle.SetActive( false ) ;
				Instantiate( listPrefabs[currentReference], currentPosition, Quaternion.identity );
				GJ_ManageBuilding.Instance.Update_MapOfBuildings_ForAttraction(listBuildings[currentReference].size,(int)currentPosition.x, (int)currentPosition.z);
				current =  PHASE.IDLE ;
				currentReference = -1 ;
				UIController.Update_UI() ;
				return true ;
		}

		private Vector3 tempValue ;
		private void Handle_CurrentState () {
				if( current == PHASE.IDLE ) return ;
				if( !Input.GetMouseButton(0) ) return ;
				_raycast = Camera.main.ScreenPointToRay (Input.mousePosition);

						if (Physics.Raycast (_raycast, out _hitInfo, 10000f, _mask)) {
								// We just collided with something, FUCK_YEAH
								// Do something with it 
						currentPosition.x = (int)_hitInfo.point.x + listBuildings[currentReference].size/2f -0.5f ;
								currentPosition.y = 0.5f ;
						currentPosition.z = (int)_hitInfo.point.z +listBuildings[currentReference].size/2f -0.5f ;
						}

				switch ( current ){
				case PHASE.IDLE : return ;
				case PHASE.DOWN : 
						DummyRectangle.transform.position= currentPosition ;
						Debug.Log( "Checking at " + currentPosition + " :: "+listBuildings[currentReference].size );
						bool allowedMove =  GJ_ManageBuilding.Instance.Check_IfSpaceAvailable_ForAttraction (listBuildings[currentReference].size, (int)currentPosition.x, (int)currentPosition.z);
						DummyRectangle.renderer.material = allowedMove ? allowedMat : forbideMat ;
						break ;
				case PHASE.INIT : 
						currentGameObject = Instantiate( listBuildings[currentReference], currentPosition, Quaternion.identity ) as GameObject;
						current = PHASE.DOWN ;
						break ;
				default :
						break ;
				}
		}
}
