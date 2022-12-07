using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Adventure
{

    public class MainMenuScript : MonoBehaviour
    {

        [SerializeField] private Button _resetGame;
        [SerializeField] private Button _loadLastSave;
        [SerializeField] private Button _quit;
        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private Slider _sliderVolume;
        private bool _isPaused = false;

        private void Awake()
        {
            AudioListener.volume = _sliderVolume.value;
            _sliderVolume.onValueChanged.AddListener(value => AudioListener.volume = value);

            _resetGame.onClick.AddListener(ResetGame);
            _quit.onClick.AddListener(Quit);
        }

        private void Quit()
        {
            Application.Quit();
        }

        private void ResetGame()
        {
            SceneManager.LoadScene(0);
        }




        private void Update()
        {            
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if(!_isPaused) Pause();                
                else Resume();
            }
        }

        public void Resume()
        {
            _panelMenu.SetActive(false);
            _isPaused = false;
            Time.timeScale = 1f;
            AudioListener.volume = _sliderVolume.value;
        }

        public void Pause()
        {
            _isPaused = true;
            _panelMenu.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.volume = 0f;
        }




    }
}
