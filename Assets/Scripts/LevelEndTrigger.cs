using UnityEngine;
using UnityEngine.SceneManagement;
using Mechadroids.UI;

public class LevelEndTrigger : MonoBehaviour {
    private WinningMenuHandler winningMenu;

    public void SetWinningMenu(WinningMenuHandler winningMenuRef) {
        winningMenu = winningMenuRef;
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) winningMenu.Activate();
    }
}
