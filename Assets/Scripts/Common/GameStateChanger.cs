using HS.Character;
using HS.Location;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    private const float CharacterSpeed = 4f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ILocationGenerator locationGenerator = FindObjectOfType<LocationGenerator>();
        locationGenerator.Generate(out Point basePoint, out List<Point> points);

        Character character = FindObjectOfType<Character>();
        character.Init(new CharacterMovemenet(character.transform, CharacterSpeed));

        CharacterStateChanger characterStateChanger = FindObjectOfType<CharacterStateChanger>();
        characterStateChanger.Init(basePoint, points);
    }

}
