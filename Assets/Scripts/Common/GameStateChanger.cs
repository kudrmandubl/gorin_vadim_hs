using HS.Character;
using HS.Config;
using HS.Damager;
using HS.Location;
using HS.UI;
using System.Collections.Generic;
using UnityEngine;

namespace HS.Common
{
    public class GameStateChanger : MonoBehaviour
    {
        private const string GameConfigPath = "GameConfig";
        private const string BasePointPrefabPath = "Prefabs/PointBase";
        private const string PatrolPointPrefabPath = "Prefabs/PointPatrol";

        private ScreensController _screensController;
        ILocationGenerator _locationGenerator;
        private ICharacterStateChanger _characterStateChanger;
        private IDamager _damager;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (_screensController == null)
            {
                _screensController = FindObjectOfType<ScreensController>();
                _screensController.Init();
            }

            MenuScreen menuScreen = _screensController.GetScreen<MenuScreen>();
            menuScreen.OnNewGameButtonClick += StartNewGame;

            GameScreen gameScreen = _screensController.GetScreen<GameScreen>();
            gameScreen.OnMenuButtonClick += StopGame;

            OpenMenu();
        }

        private void StartNewGame()
        {
            IGameConfig gameConfig = (IGameConfig)Resources.Load(GameConfigPath);
            Point basePointPrefab = ((GameObject)Resources.Load(BasePointPrefabPath)).GetComponent<Point>();
            Point patrolPointPrefab = ((GameObject)Resources.Load(PatrolPointPrefabPath)).GetComponent<Point>();

            if (_locationGenerator == null)
            {
                ILocation location = FindObjectOfType<Location.Location>();
                _locationGenerator = new LocationGenerator()
                    .WithPointPrefabs(basePointPrefab, patrolPointPrefab)
                    .WithLocation(location)
                    .WithSettings(gameConfig.PatrolPointCount, gameConfig.MinPointDistance);
            }
            _locationGenerator.Generate(out Point basePoint, out List<Point> points);

            ICharacter character = FindObjectOfType<Character.Character>();
            IHasTransform characterTransform = FindObjectOfType<Character.Character>();
            ICharacterMovement characterMovement = new CharacterMovemenet(characterTransform.Transform, gameConfig.CharacterSpeed);
            ICharacterHealth characterHealth = new CharacterHealth(gameConfig.CharacterHP);
            character.Init(characterMovement, characterHealth);
            characterTransform.Transform.position = basePoint.Position;

            if (_characterStateChanger == null)
            {
                _characterStateChanger = new CharacterStateChanger(character, basePoint, points);
            }
            _characterStateChanger.Init();

            if (_damager == null)
            {
                UnityEngine.Camera mainCamera = UnityEngine.Camera.main;
                _damager = new Damager.Damager(mainCamera, gameConfig.Damage);
            }

            GameScreen gameScreen = _screensController.GetScreen<GameScreen>();
            gameScreen.OnIdleButtonClick += _characterStateChanger.SetIdle;
            gameScreen.OnPatrolButtonClick += _characterStateChanger.SetPatrol;
            gameScreen.OnBaseButtonClick += _characterStateChanger.SetBase;
            gameScreen.Init(characterHealth);

            _screensController.ShowScreen<GameScreen>();
        }

        private void StopGame()
        {
            GameScreen gameScreen = _screensController.GetScreen<GameScreen>();
            gameScreen.OnIdleButtonClick -= _characterStateChanger.SetIdle;
            gameScreen.OnPatrolButtonClick -= _characterStateChanger.SetPatrol;
            gameScreen.OnBaseButtonClick -= _characterStateChanger.SetBase;

            OpenMenu();
        }

        private void OpenMenu()
        {
            _screensController.ShowScreen<MenuScreen>();
        }

        private void Update()
        {
            TryDamage();
        }

        private void TryDamage()
        {
            if (!Input.GetMouseButtonDown(0) || _damager == null)
            {
                return;
            }
            _damager.TryDamage();
        }

        private void OnDestroy()
        {
            if (!_screensController)
            {
                return;
            }

            MenuScreen menuScreen = _screensController.GetScreen<MenuScreen>();
            if (menuScreen)
            {
                menuScreen.OnNewGameButtonClick -= StartNewGame;
            }

            GameScreen gameScreen = _screensController.GetScreen<GameScreen>();
            if (gameScreen)
            {
                gameScreen.OnMenuButtonClick -= StopGame;
                if (_characterStateChanger != null)
                {
                    gameScreen.OnIdleButtonClick -= _characterStateChanger.SetIdle;
                    gameScreen.OnPatrolButtonClick -= _characterStateChanger.SetPatrol;
                    gameScreen.OnBaseButtonClick -= _characterStateChanger.SetBase;
                }
            }
        }
    }
}
