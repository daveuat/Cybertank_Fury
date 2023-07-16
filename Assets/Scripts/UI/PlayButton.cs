using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject hudUI;

    public string sceneName; // The name of the scene to load when the Play button is clicked
    //public GameObject roomGeneratorPrefab; // Reference to the RoomGenerator prefab

    public void OnPlayButtonClicked()
    {
        hudUI.SetActive(true);
        SceneManager.LoadScene(sceneName);

        //GameObject roomGeneratorObj = Instantiate(roomGeneratorPrefab);
        //RoomGenerator roomGenerator = roomGeneratorObj.GetComponent<RoomGenerator>();
        //roomGenerator.GenerateMazeAndRooms();
    }
}
