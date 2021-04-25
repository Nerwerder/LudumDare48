using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SpaceStation : MonoBehaviour
{
    Text metalText = null;
    private void Start() {
        var metal = GameObject.Find("Canvas/StationMetal");
        Assert.IsNotNull(metal);
        metalText = metal.GetComponent<Text>();
        Assert.IsNotNull(metalText);
        updateText();
    }

    private void updateText() {
        metalText.text = string.Format("Metal:  {0,3}", metal);
    }

    int mMetal;
    public int metal {
        set { 
            mMetal = value;
            updateText();
        }
        get { return mMetal; }
    }
}
