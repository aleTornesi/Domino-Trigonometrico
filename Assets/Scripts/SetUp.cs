using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetUp : MonoBehaviour
{
    public static int n_players;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey("return"))
        {
            if(2 >= int.Parse(GetComponent<InputField>().text) && int.Parse(GetComponent<InputField>().text) <= 4){
                n_players = int.Parse(GetComponent<InputField>().text);
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }
    }
}