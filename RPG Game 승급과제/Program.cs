using AllCharacter;
using BattleSystem;
using FieldStage;
using ItemClass;
using System.Threading;



namespace RPG_Game_승급과제
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            Stage stage = new Stage();                                      // 스테이지 맵
            Player player = new Player("플레이어", 100, 15, 5, 1000, 0, 30, 1); //  유저
            Shop shop = new Shop();                                         // 상점
            Inventory inventory = new Inventory();                          // 인벤토리
            inventory.AddItem(new Potion("하급 체력회복 포션", 30, 1,40));   // 인벤토리 아이템
            inventory.AddItem(new Weapon("강철 검", 150, 10, 15));
            inventory.AddItem(new Armor("가죽 옷", 95, 50, 5));
            MonsterSpawner spawner = new MonsterSpawner();                 // 일반 몬스터 및 그림
            Battle battle = new Battle();                                  // 배틀 시스템
            Console.Clear();
            Console.WriteLine("" +
                " ######     ####     ###      ##    #######  ##       ###    ######## \r\n" +
                "##        ###   ###  ## ##    ##    ##       ##     ##  ##   #    \r\n" +
                "##        ###   ###  ## ###   ##    #######  ##     ##  ##   ######    \r\n" +
                "##        ###   ###  ##   ### ##         ##  ##     ##  ##   #      \r\n" +
                "╚###### ╔   ####     ##     ####    #######  ######   ##     ######## \r\n" +
                "\r\n       \n\n\n\n                         RPG     \n");
               



            Console.WriteLine("                     게임 방법                       ");
            Console.WriteLine("          스테이지 이동 -> 번호 입력후 엔터   \n       ");
            Console.WriteLine("           전투 중 -> 방향키 이동 , x 입력      \n     ");

            Console.WriteLine("                 엔터를 눌러 게임 시작                 ");
            Console.ReadLine();

            while (!player.Dead())
            {

                int stageNum = stage.StageChoice();

                if (stageNum == 1)
                {
                    shop.OpenShop(player, inventory);
                    continue;
                }
                if (stageNum == 2)
                {
                    Monster monster = spawner.RandomMonster();

                    battle.StartBattle(player, monster, inventory);

                    if (player.Dead())
                    {
                        Console.WriteLine("게임 오버");
                        break;
                    }

                }
                if (stageNum == 3)
                {
                    Monster monster2 = new Bat();
                    battle.StartBattle(player, monster2, inventory);

                    if (player.Dead())
                    {
                        Console.WriteLine("게임 오버");
                        break;
                    }
                }

                if (stageNum == 4)
                {
                    Console.WriteLine("보스 등장");
                    Console.WriteLine("엔터를 눌러 다음으로 넘어가기");
                    Console.ReadLine();

                    BossMonster bossMonster = new Tiger();
                    battle.StartBossBattle(player, bossMonster, inventory);
                    if (bossMonster.Dead())
                    {
                        break;
                    }

                    break;
                }

            }

            Console.WriteLine("게임 종료");

        }
    }
}
