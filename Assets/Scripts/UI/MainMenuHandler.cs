using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Mechadroids.UI {
    public class MainMenuHandler {
        private readonly UIPrefabs uiPrefabs;
        private readonly InputHandler inputHandler;
        private MenuReference mainMenu;
        private UIButtonReference startButton;
        private UIButtonReference quitButton;

        public MainMenuHandler(UIPrefabs uiPrefabs, InputHandler inputHandler) {
            this.uiPrefabs = uiPrefabs;
            this.inputHandler = inputHandler;
        }

        public void Initialize() {
            mainMenu = Object.Instantiate(uiPrefabs.mainMenuReferencePrefab);

            Time.timeScale = 0;

            UIButtonReference uiButtonReference = uiPrefabs.GetUIElementReference<UIButtonReference>(UIElementType.Button);

            startButton = Object.Instantiate(uiButtonReference, mainMenu.contentHolder);
            startButton.transform.localPosition = new Vector3(0, 50, 0);
            startButton.SetText("Start Game");

            quitButton = Object.Instantiate(uiButtonReference, mainMenu.contentHolder);
            quitButton.transform.localPosition = new Vector3(0, -50, 0);
            quitButton.SetText("Quit Game");
        }

        public void Tick() {
            if(inputHandler.InputActions.UI.Click.WasPerformedThisFrame()) {
                if(startButton.clicked) {
                    Dispose();
                }
                if(quitButton.clicked) {
                    Application.Quit();
                }
            }
        }

        public void Dispose() {
            if(mainMenu != null) {
                inputHandler.SetCursorState(false, CursorLockMode.Locked);
                Time.timeScale = 1;
                Object.Destroy(mainMenu.gameObject);
            }
        }
    }
}
