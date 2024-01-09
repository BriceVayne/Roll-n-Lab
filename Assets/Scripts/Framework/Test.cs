using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var myInt = new ModelValue<List<int>>(new List<int>() { 0 });
        DisplayValue(myInt.Value);

        myInt.AddTriggerProperty(DisplayValue);
        myInt.Value[0] = 4;
        myInt.Value = new List<int>() { 20 };

        myInt.TriggerPropertyChanged();

        myInt.RemoveTriggerProperty(DisplayValue);

        myInt.RemoveAllTriggerProperty();
    }

    private void DisplayValue(List<int> _Value)
    {
        Debug.Log($"Value changed to {_Value[0]}");
    }
}
