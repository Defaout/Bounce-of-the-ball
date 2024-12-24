using UnityEngine;

public class SpawnBlockOnMap : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    [SerializeField] private GameObject[] _spawnBlocks;
    [SerializeField] private int _rows = 15; // Количество строк
    [SerializeField] private int _columns = 11; // Количество столбцов

    [SerializeField] private float _blockSize = 0.4f; // Размер блока
    [SerializeField] private float _spacing = 0.01f; // Промежуток между блоками

    [SerializeField] private GameObject _breakableBlocks;
    [SerializeField] private GameObject _unbreakableBlock;
    [SerializeField] private GameObject _enemyBlock;
    
    private int _numEnemy = 0;
    public void SpawnMapFromJsonArray(Block[] blocks)
    {
       _spawnBlocks = new GameObject[blocks.Length];
        int _numBlock = 0;
        // Получаем размеры экрана в мировых координатах
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        float screenHeight = Camera.main.orthographicSize * 2;

        // Вычисляем общую ширину и высоту сетки
        float totalWidth = (_blockSize * _columns) + (_spacing * (_columns - 1));

        // Вычисляем начальную позицию для спавна

        float startX = -screenWidth / 2 + (screenWidth - totalWidth) / 2; // Центрируем по X
        float startY = screenHeight / 2 - (_blockSize / 2); // Начинаем от верхней части

        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                float xPos = (startX + _blockSize / 2) + column * (_blockSize + _spacing); // Позиция по оси X
                float yPos = (startY - _blockSize) - row * (_blockSize + _spacing); // Позиция по оси Y

                Vector2 position = new Vector2(xPos, yPos);

                if (blocks[_numBlock]._id != 0)
                {
                    if (blocks[_numBlock]._id == 1)
                    {

                        GameObject block = Instantiate(_unbreakableBlock, position, Quaternion.identity);
                        block.transform.SetParent(transform, false);
                        _spawnBlocks[_numBlock] = block;
                    }
                    else if (blocks[_numBlock]._id == 6)
                    {
                        GameObject block = Instantiate(_breakableBlocks, position, Quaternion.identity);
                        SetEnemyBlok(block, blocks[_numBlock], _numBlock);
                        _numEnemy++;
                    }
                    else if (blocks[_numBlock]._id == 7)
                    {

                        GameObject block = Instantiate(_breakableBlocks, position, Quaternion.identity);
                        SetEnemyBlok(block, blocks[_numBlock], _numBlock);
                        _numEnemy++;
                    }
                    else
                    {

                        GameObject block = Instantiate(_breakableBlocks, position, Quaternion.identity);
                        SetParametrsInBlock(block, _numBlock, blocks[_numBlock],null);
                    }
                }
                _numBlock++;
            }
        }
        _gameController.SetNumEnimy(_numEnemy);
    }
    private void  SetEnemyBlok(GameObject block,Block blocks, int _numBlock)
    {
       EnemyTurret enemyTurret= block.AddComponent<EnemyTurret>();
        block.AddComponent<Rigidbody2D>();
        int damage = 0;
        int fall_damage = 0;
        if (blocks is Turret turret)
        {      
            damage=turret._damage;
        }
        if(blocks is Enemy enemuTurret)
        {
            damage = enemuTurret._damage;
            fall_damage= enemuTurret._fallDamage;
        }
        block.GetComponent<EnemyTurret>().SetParametrs(damage, fall_damage);
        SetParametrsInBlock(block, _numBlock, blocks, enemyTurret);
    }
    private void SetParametrsInBlock(GameObject block,int _numBlock, Block blocks, EnemyTurret enemyTurret)
    {
        block.transform.SetParent(transform,false);
        TuchBlock _status = block.GetComponent<TuchBlock>();
        _status.OnSpawn(blocks, _gameController, enemyTurret);
        _spawnBlocks[_numBlock] = block;
    }
}
