using AllCharacter;
using BattleSystem;

using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace ItemClass
{
    class Item
    {
        string name;
        int price;
        int id;

        public Item(string name, int price, int id)
        {
            Name = name;
            Price = price;
            Id = id;

        }

        public string Name { get; protected set; }
        public int Price { get; protected set; }
        public int Id { get; protected set; }

        public void PrintInfo()
        {
            Console.WriteLine($"아이템 : {Name} / 가격 : {Price} 골드");
        }
    }
    class Weapon : Item
    {
        private int attackBonus;

        public int AttackBonus { get; protected set; } // 공격력 보너스

        public Weapon(string Name, int Price, int Id, int attackBonus) : base(Name, Price, Id)
        {
            AttackBonus = attackBonus;
        }

    }
    class Armor : Item
    {
        private int defBonus;
        public int DefBonus { get; protected set; } // 방어력 보너스
        public Armor(string Name, int Price, int Id, int defBonus) : base(Name, Price, Id)
        {
            DefBonus = defBonus;
        }
    }

    class Potion : Item
    {
        private int healing;
        public int Haling { get; protected set; } //포션 힐 
        public Potion(string Name, int Price, int Id, int healing) : base(Name, Price, Id)
        {
            Haling = healing;
        }


    }
   

    class Inventory
    {
        private List<Item> inventory = new List<Item>();

        public void AddItem(Item itme) // 아이템 추가
        {
            inventory.Add(itme);
            Console.WriteLine($"{itme.Name} 획득");

        }
        public Item GetItem(int id)  // 아이템 번호 가져오기
        {

            if (id < 0 || id >= inventory.Count)
            {
                return null;
            }

            return inventory[id];
        }

        public int ItemCount()
        {
            return inventory.Count;
        }
        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
        }
    }
    class ItemSystem 
    {
        public void UseItem(Player player, Inventory inventory, int id) // 아이템 사용 효과
        {
            Item item = inventory.GetItem(id);

            if (item == null) // 없는 아이템
            {
                Console.WriteLine("아이템이 없습니다");
                return;
            }

            if(item is Potion potion) // item 안에 포션의 객체라면 item 을 potion 변수에 저장 , 회복
            {
                player.PotionHaling(potion);
                inventory.RemoveItem(potion);
            }

            else if (item is Weapon weapon) // 무기 장착
            {
                player.AttackBonus(weapon);
            }

            else if(item is Armor armor) // 방어구 장착
            {
                player.DefBonus(armor);
            }
        }
    }

    class Shop
    {
        private List<Item> shopItem = new List<Item>(); 
        public Shop() // 상점 아이템
        {
            shopItem.Add(new Potion("중급 체력 회복 포션", 300, 2, 60));
            shopItem.Add(new Weapon("합성금속 검", 325, 11, 20));
            shopItem.Add(new Armor("쇠사슬 갑옷", 125, 51, 10));
        }

        public void OpenShop(Player player, Inventory inventory) // 상점 출력 및 구매 판매
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== 상점 ==========");
                Console.WriteLine($"보유 골드 : {player.Gold}");
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");

                int choice;
                bool menu = int.TryParse(Console.ReadLine(), out choice);

                if(choice == 0) // 종료
                {
                    break;
                }

                else if (choice ==  1)  // 구매
                {
                    Console.WriteLine("========== 상점 ==========");
                    
                    for (int i = 0; i < shopItem.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {shopItem[i].Name}  : {shopItem[i].Price}");
                    }

                    Console.WriteLine($"보유 골드 : {player.Gold}");
                    Console.WriteLine("0 을 입력해 나가기");

                    int buyChoice;
                    bool buyMenu = int.TryParse(Console.ReadLine(), out buyChoice);

                    if(buyChoice == 0)
                    {
                        continue;
                    }


                    int buyindex = buyChoice - 1; // 아이템 고르기

                    if (buyindex < 0 || buyindex >= shopItem.Count) // 예외 처리 다시 시작
                    {
                        Console.WriteLine("잘못된 선택 입니다");
                        Console.WriteLine("엔터를 눌러 다음으로 진행");
                        Console.ReadLine();
                        continue;
                    }
                    Item buyItem = shopItem[buyindex]; 

                    if(player.Gold < buyItem.Price) // 골드 부족할 때
                    {
                        Console.WriteLine($"골드가 부족합니다 현재 골드 : {player.Gold}");
                        Console.WriteLine("엔터를 눌러 다음으로 진행");
                        Console.ReadLine();
                        continue;
                    }
                    player.UseGold(buyItem.Price);
                    inventory.AddItem(buyItem);

                    Console.WriteLine($"{buyItem.Name} 구매 완료");
                    Console.WriteLine("엔터를 눌러 다음으로 진행");
                    Console.ReadLine();


                }
                else if (choice == 2) // 판매
                {
                    Console.WriteLine("=============== 보유중인 아이템 목록 ===============");

                    for (int i = 0; i < inventory.ItemCount(); i++) // 가지고 있는 아이템 출력
                    {

                        Item item = inventory.GetItem(i);

                        Console.WriteLine($"{i + 1}. {item.Name}, 판매 가격 : {item.Price /2}");

                    }

                    Console.WriteLine("판매할 번호를 입력 하세요.");

                    Console.WriteLine("0번 종료.");


                    int sellChoice;
                    bool buyMenu = int.TryParse(Console.ReadLine(), out sellChoice);

                    if (sellChoice == 0)
                    {
                        continue;
                    }


                    int sellindex = sellChoice - 1; // 아이템 고르기 1 입력 하면 -1 로 0이 되게 만듦

                    if (sellindex < 0 || sellindex >= inventory.ItemCount()) // 예외 처리 다시 시작
                    {
                        Console.WriteLine("잘못된 선택 입니다");
                        Console.WriteLine("엔터를 눌러 다음으로 진행");
                        Console.ReadLine();
                        continue;
                    }

                    else
                    {
                        Item sellItem = inventory.GetItem(sellindex);

                        int itemPrice = sellItem.Price / 2;

                        player.GetGold(itemPrice);

                        inventory.RemoveItem(sellItem);

                        Console.WriteLine($"{sellItem.Name} 판매 완료");
                        Console.WriteLine($"{itemPrice} Gold 획득");
                        continue;
                    }
                        

                }

            }
        }

    }

    internal class ItemClass
    {
    }
}
