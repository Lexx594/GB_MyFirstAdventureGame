using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure
{
    public class InventoryBehaviourScript : MonoBehaviour
    {

        public GameObject stoneIcon;
        public GameObject slowStoneIcon;
        public GameObject fireStoneIcon;

        [HideInInspector] public int stone = 0;
        [HideInInspector] public int slowStone = 0;
        [HideInInspector] public int fireStone = 0;

        public string stoneText;


        private void Update()
        {
            
            if(stone > 0)
            {
                stoneIcon.SetActive(true);
                stoneText = stone.ToString();
                stoneIcon.transform.GetChild(0).GetComponent<Text>().text = stoneText;
                
            }
            else
            {
                stoneIcon.SetActive(false);
            }
            if (slowStone > 0)
            {
                slowStoneIcon.SetActive(true);
            }
            else
            {
                slowStoneIcon.SetActive(false);
            }
            if (fireStone > 0)
            {
                fireStoneIcon.SetActive(true);
            }
            else
            {
                fireStoneIcon.SetActive(false);
            }



        }



    }
}
