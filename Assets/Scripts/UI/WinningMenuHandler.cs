using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Mechadroids.UI {
    public class WinningMenuHandler {
        private readonly UIPrefabs uiPrefabs;
        private readonly InputHandler inputHandler;
        private readonly LevelEndTrigger levelEnd;
        private MenuReference winningMenu;
        private UIButtonReference startButton;
        private UIButtonReference quitButton;

        public WinningMenuHandler(UIPrefabs uiPrefabs, InputHandler inputHandler, LevelEndTrigger levelEnd) {
            this.uiPrefabs = uiPrefabs;
            this.inputHandler = inputHandler;
            this.levelEnd = levelEnd;
        }

        public void Initialize() {
            winningMenu = Object.Instantiate(uiPrefabs.winningMenuReferencePrefab);
            winningMenu.gameObject.SetActive(false);
            levelEnd.SetWinningMenu(this);

            UIButtonReference uiButtonReference = uiPrefabs.GetUIElementReference<UIButtonReference>(UIElementType.Button);

            startButton = Object.Instantiate(uiButtonReference, winningMenu.contentHolder);
            startButton.transform.localPosition = new Vector3(0, 50, 0);
            startButton.SetText("Back To Main Menu");

            quitButton = Object.Instantiate(uiButtonReference, winningMenu.contentHolder);
            quitButton.transform.localPosition = new Vector3(0, -50, 0);
            quitButton.SetText("Quit Game");
        }

        public void Activate() {
            winningMenu.gameObject.SetActive(true);
            inputHandler.SetCursorState(true, CursorLockMode.None);
            Time.timeScale = 0;
        }

        public void Tick() {
            if(inputHandler.InputActions.UI.Click.WasPerformedThisFrame()) {
                if(startButton.clicked) {
                    int sceneCount = SceneManager.loadedSceneCount;
                    SceneManager.LoadScene("Boot");
                }
                if(quitButton.clicked) {
                    Application.Quit();
                }
            }
        }

        public void Dispose() {
            if(winningMenu != null) {
                inputHandler.SetCursorState(false, CursorLockMode.Locked);
                Time.timeScale = 1;
                Object.Destroy(winningMenu.gameObject);
            }
        }
    }
}
