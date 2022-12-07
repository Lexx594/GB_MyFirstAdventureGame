using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure
{
    public class InventoryBehaviourScript : MonoBehaviour
    {

        public GameObject _stoneIcon;
        public GameObject _slowStoneIcon;
        public GameObject _fireStoneIcon;

        [HideInInspector] public int _stone = 0;
        [HideInInspector] public int _slowStone = 0;
        [HideInInspector] public int _fireStone = 0;

        public string stoneText;


        private void Update()
        {

            //заполняем инвентарь
            if (_stone > 0)
            {
                _stoneIcon.SetActive(true);
                _stoneIcon.transform.GetChild(0).GetComponent<Text>().text = _stone.ToString();
                //Transform stoneText = _stoneIcon.transform.GetChild(0);
                //Text txt = stoneText.GetComponent<Text>();
                //txt.text = _stone.ToString();
            }
            else
            {
                _stoneIcon.SetActive(false);
            }
            if (_slowStone > 0)
            {
                _slowStoneIcon.SetActive(true);
                _slowStoneIcon.transform.GetChild(0).GetComponent<Text>().text = _slowStone.ToString();
            }
            else
            {
                _slowStoneIcon.SetActive(false);
            }
            if (_fireStone > 0)
            {
                _fireStoneIcon.SetActive(true);
                _fireStoneIcon.transform.GetChild(0).GetComponent<Text>().text = _fireStone.ToString();
            }
            else
            {
                _fireStoneIcon.SetActive(false);
            }



        }



    }
}
