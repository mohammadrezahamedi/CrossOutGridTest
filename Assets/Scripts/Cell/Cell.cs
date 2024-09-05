using UnityEngine;

namespace RealityUnit
{
    public class Cell : BaseCell
    {
        // Implement the abstract method, potentially adding specific behavior for this type of cell
        public override void SpecialBehavior()
        {
            // Add any specialized behavior for this cell here
            Debug.Log("Special behavior for the Cell class.");
        }
    }
}
