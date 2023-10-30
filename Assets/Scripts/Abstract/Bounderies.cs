[System.Serializable]
public struct Bounderies
{
    public float Up;
    public float Down;
    public float Left;
    public float Right;

    public Bounderies(float up, float down, float left, float right)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
    }
}
