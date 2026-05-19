using AllCharacter;
using ItemClass;
using System.Numerics;
using System.Threading;

namespace BattleSystem
{

    class Battle
    {
        

        public void StartBattle(Player player, Monster monster, Inventory inventory)// 일반 몬스터 배틀 시작
        {
            Console.WriteLine($"{monster.Name} 등장\n");

            while (!monster.Dead() && !player.Dead())
            {

                Console.Clear();
                Console.WriteLine("플레이어의 턴");
                int choice = ChoiceMenu(player,monster);
                if (choice == 0) // 플레이어 공격
                {
                    Console.Clear();
                    int choiceAttack = ChoiceAttackMenu(player, monster);
                    if (choiceAttack == 0)
                    {
                        player.PlayerNomalAccack(monster);
                   
                    }
                    else if (choiceAttack == 1)
                    {
                        player.PlayerUltimateSkill(monster);
                        
                    }
                    else if (choiceAttack == 2)
                    {
                        continue;
                    }
                    Console.WriteLine("엔터를 눌러 계속 하기");
                    Console.ReadLine();


                }
                else if (choice == 1) // 아이템
                {
                    int itemIndex = ChoiceItemMenu(player, monster, inventory);

                    if (itemIndex != -1)
                    {
                        ItemSystem itemSystem = new ItemSystem();
                        itemSystem.UseItem(player, inventory, itemIndex);
                        Console.ReadLine();
                    }

                }
                else if (choice == 2) //상태창
                {
                    PrintStatus(player);
                    Console.ReadLine();
                    continue;
                }

                else if (choice == 3) // 도망가기
                {
                    Console.WriteLine("도망 쳤습니다");
                    break;

                }
               

                if (monster.Dead()) // 몬스터 죽음 확인
                {
                    DeadAfter(player, monster); // 리워드
                    break;
                }

                Console.Clear();// 지우기

                Drawstatus(player, monster); // 다시 불러오기
                DrawMonsterpainting(monster); // 다시 불러오기

                Console.WriteLine($"{monster.Name}의 턴");
                

                monster.MonsterNomalAccack(player);



                Console.WriteLine();
                Console.WriteLine("엔터를 눌러 계속 하기");
                Console.ReadLine();

                if (player.Dead()) // 플레이어 죽음 확인
                {
                   
                    break;
                }
            }
        }

        public void StartBossBattle(Player player, BossMonster monster, Inventory inventory)// 보스 배틀 시작
        {
            Console.WriteLine($"{monster.Name} 등장\n");

            Random random = new Random();
            while (!monster.Dead() && !player.Dead())
            {
                Console.Clear();
                Console.WriteLine("플레이어의 턴");
                int choice = ChoiceMenu(player, monster);
                if (choice == 0) // 플레이어 공격
                {
                    Console.Clear();
                    int choiceAttack = ChoiceAttackMenu(player, monster);
                    if (choiceAttack == 0)
                    {
                        player.PlayerNomalAccack(monster);

                    }
                    else if (choiceAttack == 1)
                    {
                        player.PlayerUltimateSkill(monster);

                    }
                    else if (choiceAttack == 2)
                    {
                        continue;
                    }
                    Console.WriteLine("엔터를 눌러 계속 하기");
                    Console.ReadLine();


                }
                else if (choice == 1) // 아이템
                {
                    int itemIndex = ChoiceItemMenu(player, monster, inventory);

                    if (itemIndex != -1)
                    {
                        ItemSystem itemSystem = new ItemSystem();
                        itemSystem.UseItem(player, inventory, itemIndex);
                        Console.ReadLine();
                    }

                }
                else if (choice == 2) //상태창
                {
                    PrintStatus(player);
                    Console.ReadLine();
                    continue;
                }

                else if (choice == 3) // 도망가기
                {
                    Console.WriteLine("보스에게선 도망 갈수 없습니다.");
                    continue;

                }


                if (monster.Dead()) // 보스 죽음 확인
                {
                    Console.WriteLine("보스 처치");
                    Console.WriteLine("게임 클리어!");
                    DeadAfter(player, monster); // 리워드
                    break;
                }

                Console.Clear(); // 지우기

                Drawstatus(player, monster); // 다시 불러오기
                DrawMonsterpainting(monster); // 다시 불러오기

                Console.WriteLine($"{monster.Name}의 턴");


                int bossskill = random.Next(0, 2);

                if (bossskill == 1)
                {
                    monster.MonsterNomalAccack(player);
                }
                else if (bossskill == 0)
                {
                    monster.MonsterUltimateSkill(player);
                }
   

                Console.WriteLine();
                Console.WriteLine("엔터를 눌러 계속 하기");
                Console.ReadLine();

                if (player.Dead()) // 플레이어 죽음 확인
                {
                    break;
                }
            }
        }

        public void DrawBattle(Player player, Monster monster, List<string> menu, int menuIndex) 
        {
            Console.Clear();

            Drawstatus(player, monster);

            DrawMonsterpainting(monster);

            DrawMenu(menu, menuIndex);


        }

        public void Drawstatus(Player player, Monster monster)
        {
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("================== 상태 창 ==================");

            Console.WriteLine($"현재 체력 : {player.Hp}\n");

            Console.WriteLine($"적 {monster.Name}의 체력 : {monster.Hp}");

            Console.WriteLine("=============================================");
        }

        public void DrawMonsterpainting(Monster monster)
        {
           
            Console.SetCursorPosition(0, 6);
            Console.WriteLine($"    {monster.Name}");
            Console.WriteLine();

            if (monster is Slime)
            {
                Console.WriteLine("          ██████████          ");
                Console.WriteLine("      ████░░░░░░░░░░████       ");
                Console.WriteLine("    ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██     ");
                Console.WriteLine("  ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██    ");
                Console.WriteLine("  ██▒▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒██    ");
                Console.WriteLine("██▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒██  ");
                Console.WriteLine("██▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒██   ");
                Console.WriteLine("██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██   ");
                Console.WriteLine("██▒▒▒▒▒▒▒▒██▒▒▒▒▒▒██▒▒▒▒▒▒▒▒██   ");
                Console.WriteLine("██▓▓▒▒▒▒▒▒▒▒██████▒▒▒▒▒▒▒▒▓▓██   ");
                Console.WriteLine("  ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██     ");
                Console.WriteLine("    ██████████████████████       ");
            }

            else if (monster is Bat)
            {
                Console.WriteLine("/|\\ ^._.^ /|\\ ");
            }

            else if (monster is Tiger)
            {
                Console.WriteLine("                          __,,,,_                        ");
                Console.WriteLine("          _ ___.--'''`--''// ,-_ `-.                     ");
                Console.WriteLine("      \\`\"' ' |  \\  \\ \\\\/ / // / ,-  `,_            ");
                Console.WriteLine("     /'`  \\   |  Y  | \\|/ / // / -.,__ `-.             ");
                Console.WriteLine("    /<\"\\    \\ \\  |  | ||/ // | \\/    |`-._`-._      ");
                Console.WriteLine("   /  _.-.  .-\\,___|  _-| / \\ \\/|_/  |    `-._        ");
                Console.WriteLine("   `-'  f/ |       / __/ \\__  / |__/ |                  ");
                Console.WriteLine("        `-'       |  -| -|\\__ \\  |-' |                 ");
                Console.WriteLine("               __/   /__,-'    ),'  |'                   ");
                Console.WriteLine("              ((__.-'((____..-' \\__,'                   ");
            }

            Console.WriteLine("");
        }

        public void DrawMenu(List<string> menu, int menuIndex)
        {
            Console.SetCursorPosition(0, 24);
            Console.WriteLine("================================================================");
            for (int i = 0; i < menu.Count; i++)
            {
                
                if (i == menuIndex)
                {
                    Console.WriteLine($"> {menu[i]}");

                }
                else
                {
                    Console.WriteLine($"  {menu[i]}");
                }
            }
        }

        public void PrintStatus(Player player) // 전투중 상태 창
        {
            Console.WriteLine("========= 상태 창 =========");

            Console.WriteLine($"     {player.Name}       ");
            Console.WriteLine($"현재 레벨     : {player.Level}");
            Console.WriteLine($"현재 체력     : {player.Hp}");
            Console.WriteLine($"현재 공격력   : {player.Dmg}");
            Console.WriteLine($"현재 방어력   : {player.Def}");
            Console.WriteLine($"현재 경험치   : {player.Exp}/{player.MaxExp}");

            Console.WriteLine("===========================");
        }


        public int ChoiceAttackMenu(Player player, Monster monster)
        {
            List<string> attackMenu = new List<string>();
            attackMenu.Add("일반 공격");
            attackMenu.Add("궁극기");
            attackMenu.Add("취소");

            int attackMenuIndex = 0;

            while (true)
            {
                Console.Clear();

                Drawstatus(player, monster);

                DrawMonsterpainting(monster);

                DrawMenu(attackMenu, attackMenuIndex);


                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 키입력
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    attackMenuIndex--;
                    if (attackMenuIndex < 0)
                    {
                        attackMenuIndex = attackMenu.Count - 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    attackMenuIndex++;
                    if (attackMenuIndex >= attackMenu.Count)
                    {
                        attackMenuIndex = 0;
                    }
                }
                else if (keyInfo.KeyChar == 'X' || keyInfo.KeyChar == 'x')
                {
                    return attackMenuIndex;
                }
            }

        }

        public int ChoiceMenu(Player player, Monster monster )
        {
            List<string> menu = new List<string>(); // 메뉴 출력
            menu.Add("공격");
            menu.Add("아이템");
            menu.Add("상태창");
            menu.Add("도망가기");

            int menuIndex = 0;

            while (true)
            {
                Console.Clear();

                Drawstatus(player, monster);

                DrawMonsterpainting(monster);

                DrawMenu(menu, menuIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 키입력
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    menuIndex--;
                    if (menuIndex < 0)
                    {
                        menuIndex = menu.Count - 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    menuIndex++;
                    if (menuIndex >= menu.Count)
                    {
                        menuIndex = 0;
                    }
                }
                else if(keyInfo.KeyChar == 'X' || keyInfo.KeyChar == 'x')
                {
                    return menuIndex;
                }
            }
        }
        public void DeadAfter(Player player, Monster monster)
        {
            if (monster.Dead())// 보상
            {
                Console.WriteLine($"{monster.Name} 처치");

                player.LevelExp(monster.RewardExp);
                player.GetGold(monster.RewardGold);

            }
        } public int ChoiceItemMenu(Player player, Monster monster, Inventory inventory)
        {
            int ItemMenuIndex = 0;
           

            while (true)
            {
                Console.Clear();

                Drawstatus(player, monster);

                DrawMonsterpainting(monster);

                if (inventory.ItemCount() == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다");
                    Console.ReadLine();

                    return -1;
                }

                List<string> itemMenu = new List<string>();

                for (int i = 0; i < inventory.ItemCount(); i++)
                {

                    Item item = inventory.GetItem(i);
                    itemMenu.Add(item.Name);
                }
                DrawMenu(itemMenu, ItemMenuIndex);

             

                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 키입력 표시 x 
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    ItemMenuIndex--;
                    if (ItemMenuIndex < 0)
                    {
                        ItemMenuIndex = inventory.ItemCount() - 1; // 맨 위에서 위로 하면 맨 아래
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    ItemMenuIndex++;
                    if (ItemMenuIndex >= inventory.ItemCount())
                    {   
                        ItemMenuIndex = 0;
                    }
                }
                else if (keyInfo.KeyChar == 'X' || keyInfo.KeyChar == 'x')
                {
                    return ItemMenuIndex;
                }
            }

        }

    }

    class MonsterSpawner
    {
        public Monster RandomMonster()
        {
            Random monsterRandom = new Random();
            int number = monsterRandom.Next(0, 2);
            
            if ( number == 0)
            {
                Monster slime = new Slime();

                return slime;

            }

            else
            {
                Monster bat = new Bat();

                return bat;
            }
        }
    }

    internal class BattleSystem1
    {
       

    }
}