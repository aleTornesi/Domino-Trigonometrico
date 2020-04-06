using TMPro;
using UnityEngine;

public class setText : MonoBehaviour
{
    public Tessera tessera;

    // Start is called before the first frame update
    void Start()
    {
        tessera = new Tessera();
        if (tessera.getFunction() != null)
            GetComponent<TMP_Text>().SetText($"{this.tessera.getFunction()}({this.tessera.getArg()})");
        else
            GetComponent<TMP_Text>().SetText(this.tessera.getArg());
    }

    public void rewrite()
    {
        if (tessera.getFunction() != null)
            GetComponent<TMP_Text>().SetText($"{this.tessera.getFunction()}({this.tessera.getArg()})");
        else
            GetComponent<TMP_Text>().SetText(this.tessera.getArg());
    }
}
