using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItsWhatYoureHolding : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private Text textOfThingPlayerIsHolding;

    private void Awake()
    {
        textOfThingPlayerIsHolding = this.GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.HeldItem != null)
        {
            textOfThingPlayerIsHolding.text = "" + player.HeldItem.Type.ToString();
        }
        else
        {
            textOfThingPlayerIsHolding.text = "";
        }
	}
}
