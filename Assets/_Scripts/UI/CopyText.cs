using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CopyText : MonoBehaviour
{
    [SerializeField] TextMeshPro text_to_copy;
    [SerializeField] TextMeshPro my_text;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        my_text.SetText(text_to_copy.text);
    }
}
