
public enum EventID
{
    //---UI OVERLAY---
    OpenUiOverlay,
    CloseUiOverlay,
  
    //---CAGE---
    OpenInteractCage,
    CloseInteractCage,

   
}
public enum TypeUI
{
    Main,
    InteractCage,
    ViewAnimal,
    ViewDetailAnimal
}
public enum SoundTypeInCage
{
    Chirp,
    Environment,
    Food,
    Characteristic,
    Conservationlevel,
    StorySpecial
}

public enum TypeAnimationMove
{
    horizontal,
    vertical
}

public enum GameScenes
{
    MainMenu,
    ChooseZoo,
    ChooseMinigame,
    MapZoo1,
    Map2,
    BuildMap,
    AnimalPuzzle,
    JigsawPuzzle,
    Maze
}
public enum SpeciesAnimal
{
    Forset,
    Meadow
}