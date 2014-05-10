using UnityEngine;

namespace Tactics
{

    public class MapEditor : MonoBehaviour
    {
        void Start()
        {
            InputController.waitForInput(InputSelectionType.None, InputSelectionType.Edition, 100, null, null);
        }
    }
}