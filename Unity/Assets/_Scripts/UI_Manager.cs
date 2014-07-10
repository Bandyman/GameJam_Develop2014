using UnityEngine;
using System.Collections;

public class UI_Manager : MonoBehaviour
{
		// Raycasting 
		private bool is_3DUIEnabled = false ;
		private Ray _raycast;
		private RaycastHit _hitInfo;
		private LayerMask _mask = 1 << 10;
		private float _distance = 100f;


		[SerializeField] private GJ_AddBuilding Manager_Building;

		[SerializeField] private GameObject SubMenu_Description ;
		[SerializeField] private GameObject SubMenu_Building ;
		[SerializeField] private GameObject SubMenu_Buying ;
		[SerializeField] private GameObject SubMenu_Main ;

		[SerializeField] private GameObject StartButton ;

		[SerializeField] private UILabel moneyLabel ;
		[SerializeField] private UILabel warningLabel ;

		[SerializeField] private UILabel _lablTitle ;
		[SerializeField] private UILabel _lablDescription ;
		[SerializeField] private UILabel _lablMoneyMade ;
		[SerializeField] private UILabel _lablFunValue ;

		[SerializeField] private GameObject _Ghosts ;

		public void BTNPressed_Building_1 ()
		{
				PressButton_Index (0);
		}

		public void BTNPressed_Building_2 ()
		{
				PressButton_Index (1);
		}
		public void BTNPressed_Building_3 ()
		{
				PressButton_Index (2);
		}
		public void BTNPressed_Building_4 ()
		{
				PressButton_Index (3);
		}
		public void BTNPressed_Building_5 ()
		{
				PressButton_Index (4);
		}

		public void BTNPressed_OpenEditMenu ()
		{
				SubMenu_Building.SetActive (!SubMenu_Building.activeSelf);
				is_3DUIEnabled = !SubMenu_Building.activeSelf ;
		}

		public void BTNPressed_START () {
				StartButton.SetActive( false ) ;
				_Ghosts.SetActive( true ) ;
		}




		public void BTNPressed_Cancel () {
				Manager_Building.Manager_CancelBuilding() ;
				SubMenu_Building.SetActive( true ) ;
				SubMenu_Buying.SetActive( false ) ;
		}

		public void BTNPressed_Confirm () {
				if( Manager_Building.Manager_BuyBuilding () ){
						SubMenu_Building.SetActive( true ) ;
						SubMenu_Buying.SetActive( false ) ;
				} else {
						StartCoroutine(COROUTINE_ShowWarningMessageWithText("Can't build here") );
				}
		}




		public void BTNPressed_CollectMoney () {

		}

		public void BTNPressed_ChangePrice () {

		}

		public void BTNPressed_ClosePopUp () {
				SubMenu_Main.SetActive( true );
				SubMenu_Description.SetActive( false ) ;

				is_3DUIEnabled = true ;
		}



		public void Update_UI () {
				moneyLabel.text = PlayerProfile.Instance.Money + " £" ;
		}

		private IEnumerator COROUTINE_ShowWarningMessageWithText( string text ){
				warningLabel.text = text ;
				warningLabel.gameObject.SetActive( true ) ;
				yield return new WaitForSeconds( 2f );
				warningLabel.gameObject.SetActive( false ) ;
		}

		private void PressButton_Index (int Index)
		{
				// CHECK IF ENOUGH MONEY
				if( Manager_Building.Set_CurrentBuildingTo (Index) ){
						SubMenu_Buying.SetActive( true ) ;
						SubMenu_Building.SetActive( false ) ;
				}
				else {
						StartCoroutine( COROUTINE_ShowWarningMessageWithText( "Not Enough Money") );
						SubMenu_Buying.SetActive( false ) ;
						SubMenu_Building.SetActive( false ) ;
						return ;
				}
		}


		// Use this for initialization
		void Start ()
		{
				SubMenu_Main.SetActive( true ) ;
				SubMenu_Buying.SetActive( false ) ;
				SubMenu_Building.SetActive( false ) ;
				SubMenu_Description.SetActive( false ) ;

				warningLabel.gameObject.SetActive( false ) ;

				moneyLabel.text = PlayerProfile.Instance.Money + " £" ;
		}



		private GJ_Building _BuildingReference ;
		private void Update () {
				if( !is_3DUIEnabled ) return ;

				if( !Input.GetMouseButtonDown(0) ) return ;

				_raycast = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (_raycast, out _hitInfo, 10000f, _mask)) {
						// We just collided with something, FUCK_YEAH
						// Do something with it 
						_BuildingReference = _hitInfo.collider.gameObject.GetComponent<GJ_Building>() ;
						if (_BuildingReference != null ){
								_lablTitle.text = _BuildingReference.title ;
								_lablMoneyMade.text = "Money Made : " + _BuildingReference.moneyMade ;
								_lablFunValue.text = "Extcitment :" +_BuildingReference.excitment ;
								_lablDescription.text = _BuildingReference.description ;

								SubMenu_Description.SetActive( true ) ;
								SubMenu_Building.SetActive( false );
								SubMenu_Main.SetActive( false );
								SubMenu_Buying.SetActive( false );

								is_3DUIEnabled = false ;
						}
				}
		}


}
