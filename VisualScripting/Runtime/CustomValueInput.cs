using Unity.VisualScripting;

public class CustomValuePort<T>
{
    public string portName;

    public ValueInput GetValueInput()
    {
        return new ValueInput(portName, typeof(T));
    }
}
