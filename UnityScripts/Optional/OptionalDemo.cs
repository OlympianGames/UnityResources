// Made by INeatFreak
// https://gist.github.com/INeatFreak/e01763f844336792ebe07c1cd1b6d018

using UnityEngine;
  
public class OptionalDemo : MonoBehaviour
{
    [Header(" [ Floats ] ")]
    [SerializeField] Optional<float> optionalFloat = 100.95f;


    [Header(" [ Float Ranges ] ")]
    // this won't work as Range attribute check for field Type and theres no way to support custom classes
    [SerializeField][Range(0,500)] Optional<float> optionalRange = 1.5986f;


    [Header(" [ Integers ] ")]
    [SerializeField] Optional<int> optionalInt = 50;
    [SerializeField] Optional<int> optionalInt2;


    [Header(" [ Transforms ] ")]  
    [SerializeField] Optional<Transform> optionalTransform = null;
    [SerializeField] Optional<Transform> optionalTransform2 = null;


    private void Start() {
        optionalFloat += 1.1f;

        if (optionalInt == optionalInt2)
            Debug.Log("optionalInt == optionalInt2");

        if (optionalTransform == optionalTransform2)
            Debug.Log("optionalTransform == optionalTransform2");


        if (optionalTransform == null) {
            Debug.Log("optionalTransform is null");
        } else {
            optionalTransform = null;
        }


        if (optionalInt == null) {
            Debug.Log("optionalInt can't be null. Something is wrong!");
        }

        if (optionalInt) {
            Debug.Log("optionalInt is enabled.");
        }

        // optionalFloat = optionalTransform;
    }
}