/// <summary>
/// Output struct for the LevelFromImage.Build() so we can access the levelcreation information stored in the picture from the outside without loading the bitmap again
/// </summary>
public struct LevelFromImageOutputInfo
{
    public int height;
    public int width;
    public int walls;
    public int floors;
    public int doors;
    public int playerSpawns;
}
