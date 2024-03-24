using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    //
    //
    //добавить фсм и разделить игру на стейты
    //
    //

    private IPlayer _player;

    public void Init(IPlayer player)
    {
        _player = player;

        _player.OnDiedEvent += () => StartCoroutine(WaitLevelFailed());
    }

    private IEnumerator WaitLevelFailed()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
