using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public enum ShotStatus //Статус выстрела
    { 
        Miss, // промах
        wounded, // ранил 
        Kill, // Убил
        EndBattle // Конец боя
    } 
    public enum CoordStatus //Статус координат
    {
        None, // Пусто
        Ship, // Корабле 
        Shot, // Выстрел
        Got // Попал 

    }
    public enum ShipType //типы кораблей 
    {
        x4,
        x3,
        x2,
        x1
    }
    public enum Direction // Направление
    { Horizontal, Vertical }
    public class model
    {
        //Массив координат своих кораблей (кораблей игрока)
        public CoordStatus[,] PlayerShips = new CoordStatus[10,10];
        //Массив координат кораблей противника
        public CoordStatus[,] EnemyShips = new CoordStatus[10,10];
        //Количество клеток кораблей противника
        public int UndiscoverCell = 20;
        //Поле статуса последнего выстрела 
        public ShotStatus LastShot;
        //поле статус ранения 
        public bool WoundedStatus;
        //Поле координат последнего выстрела
        public string? LastShotCoord;
        //Конструктор. Инициализация полей модели
        public model()
        {
            LastShot = ShotStatus.Miss;
            WoundedStatus = false;
            for(int i = 0; i < 10; i++)
                for(int j = 0; j < 10; j++)
                {
                    PlayerShips[i, j] = CoordStatus.None;
                    EnemyShips[i,j] = CoordStatus.None;
                }
        }
        //Выстрел игрока. Входящий параметр - координаты выстрела в виде строки из 2х цифр
        public ShotStatus Shot(string ShotCoord)
        {
            ShotStatus result = ShotStatus.Miss;
            int x, y; // координаты выстрела в числовом виде
            x = int.Parse(ShotCoord.Substring(0,1));
            y = int.Parse(ShotCoord.Substring(1));
            if (PlayerShips[x,y]==CoordStatus.None)
            { result = ShotStatus.Miss; }
            else 
            {
                result = ShotStatus.Kill;
                if ((x!=9 && PlayerShips[x+1, y] == CoordStatus.Ship) ||
                    (y != 9 && PlayerShips[x, y+1] == CoordStatus.Ship) ||
                    (x !=0 && PlayerShips[x-1,y] == CoordStatus.Ship) ||
                    (y !=0 && PlayerShips[x,y-1] == CoordStatus.Ship))
                    result = ShotStatus.wounded;
                PlayerShips[x, y] = CoordStatus.Got;
                UndiscoverCell--;
                if(UndiscoverCell==0) { result = ShotStatus.EndBattle; }

            }

            return result;
        }
        //Генерация выстрела
        public string ShotGen()
        {

            string result = "00";
            int x, y; //координаты выстрела в цшфровом виде
            Random rand = new Random();
            if (LastShot == ShotStatus.Kill) WoundedStatus = false;
             if((LastShot == ShotStatus.Kill || LastShot == ShotStatus.Miss) && !WoundedStatus)
            {
                x= rand.Next(0, 9);
                y= rand.Next(0, 9);

            }
             else
            { 
                    x = int.Parse(LastShotCoord.Substring(0, 1));
                    y = int.Parse(LastShotCoord.Substring(1));
                    if (LastShot == ShotStatus.wounded)
                    {   
                        if (x != 9 && EnemyShips[x + 1, y] == CoordStatus.Got) x = x - 1;
                        if (y != 9 && EnemyShips[x, y + 1] == CoordStatus.Got) y = y - 1;
                        if (x != 0 && EnemyShips[x - 1, y] == CoordStatus.Got) x = x + 1;
                        if (y != 0 && EnemyShips[x, y - 1] == CoordStatus.Got) y = y + 1;
                        {

                        }
                    }
            }

             result = x.ToString() + y.ToString();
            

            return result;
        }

    }
}
