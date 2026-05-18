using AllCharacter;
using BattleSystem;
using Inter;
using ItemClass;
using System.Numerics;


namespace RPG_Game_승급과제
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("플레이어", 100, 15, 5, 30, 0, 30, 1);

            Inventory inventory = new Inventory();

            MonsterSpawner spawner = new MonsterSpawner();
         
            Battle battle = new Battle();

            while (!player.Dead())
            {
                Monster monster = spawner.randomMonster();

                battle.StartBattle(player, monster, inventory);

                Console.WriteLine("다음 전투로 이동합니다...");
                Console.ReadLine();
            }

            Console.WriteLine("게임 종료");

        }
    }
}
