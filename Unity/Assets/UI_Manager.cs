using UnityEngine;
using System.Collections;

public class UI_Manager : MonoBehaviour
{

		[SerializeField] private GameObject[] _BuildingButtons;
		[SerializeField] private GameObject[] _BuildingObjects;
		[SerializeField] private GJ_AddBuilding Manager_Building;

		[SerializeField] private GameObject Building_Selector;





		public void BTNPressed_Building_1 ()
		{
				PressButton_Index (0);
		}

		public void BTNPressed_Building_2 ()
		{
				PressButton_Index (1);
		}

		public void BTNPressed_OpenEditMenu ()
		{
				Building_Selector.SetActive (!Building_Selector.activeSelf);
		}




		private void PressButton_Index (int Index)
		{
				// CHECK IF ENOUGH MONEY

				for (int i = 0; i < _BuildingButtons.Length; i++) {
						_BuildingButtons [i].SetActive (true);
				}
				_BuildingButtons [Index].SetActive (false);
				Manager_Building.Set_CurrentBuildingTo (_BuildingObjects [Index]);
		}


		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
