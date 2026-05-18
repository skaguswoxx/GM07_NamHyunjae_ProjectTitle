using AllCharacter;
using Inter;
using BattleSystem;

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
        public Item GetItem(int id) 
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

            if (item == null) // 없는 아이템?
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
        // 판매 // 구매 넣기
    }

    internal class ItemClass
    {
    }
}
