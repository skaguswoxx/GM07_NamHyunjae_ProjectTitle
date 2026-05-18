using Inter;
using ItemClass;
using BattleSystem;

namespace AllCharacter
{
    abstract class AllCharacter
    {
        private string name;
        private int hp;
        private int dmg;
        private int def;
        private int level;
       
     
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }
        public int Hp
        {
            get { return hp; }
            protected set { hp = value; }
        }
        public int Dmg
        {
            get{ return dmg; }
            protected set { dmg = value; }
        }
        public int Def
        {
            get { return def; }
            protected set { def = value; }
        }
        public int Level
        {
            get { return level; }
            protected set { level = value; }
        }
        
        public int TakeDamg(int damag)
        {
            int lastDmg = damag - Def;

            if (lastDmg < 0)
            {
                lastDmg = 0;
            }


            Hp -= lastDmg;

            if (Hp <= 0)
            {
                Hp = 0;
            }

            return lastDmg;
            
        }


        public bool Dead() // 죽음 판정
        {
            if (hp <= 0)
            {
                hp = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    class Player : AllCharacter 
    {

        private int exp;
        private int maxExp;
        private int gold;
        private Weapon bonusWeapon;
        private Armor bonusArmor;
        public int Exp {get; protected set;}
        public int MaxExp { get; protected set;}
        public int Gold { get; protected set;}


        public Player(string name, int hp, int dmg, int def,
                      int gold, int exp, int maxExp, int level)
        {
            Name = name;
            Hp = hp;
            Dmg = dmg;
            Def = def;
            Gold = gold;
            Exp = 0;
            MaxExp = 50;
            Level = 1;
        }

        public void LevelExp(int RewardExp)
        {
            Exp += RewardExp;
            Console.WriteLine($"{RewardExp} 경험치를 획득 했습니다");

            if(Exp >= MaxExp)
            {
                LevelUp();
            }
        }
         
        public void LevelUp()
        {

            Exp -= MaxExp;
            Level++;

            MaxExp += 5 + Level;
            Hp += 10 + Level;
            Dmg += 3 + Level;
            Def += 3 + Level;
            

            Console.WriteLine($"레벨업 현재 레벨 : {Level}");
            Console.WriteLine($"최대 경험치 증가 : {MaxExp}");
            Console.WriteLine($"최대 Hp 증가 : {Hp}");
            Console.WriteLine($"기본 데미지 증가 : {Dmg}");
            Console.WriteLine($"기본 방어력 증가 : {Def}");
        }

        public void GetGold(int gold)
        {
            Gold += gold;
            Console.WriteLine($"{gold} Gold를 획득 했습니다");

            if (Gold > 999)
            {
                Gold = 999;
            }

        }
        

        public void AttackBonus(Weapon AttackBonus) //공격 보너스 적용
        {
            if(bonusWeapon != null) // 장착 중이라면 공격력 감소
            {
                Dmg -= bonusWeapon.AttackBonus;
            }
            bonusWeapon = AttackBonus; // 교체

            Dmg += AttackBonus.AttackBonus;
            Console.WriteLine($"{AttackBonus.Name} 장착 \n공격력 : {AttackBonus.AttackBonus} ");

        }

        public void DefBonus(Armor armor) //방어 보너스 적용
        {
            if (bonusArmor != null) // 장착 중이라면 방어력 감소
            {
                Def -= bonusArmor.DefBonus;
            }

            bonusArmor = armor; 

            Def += armor.DefBonus;
            Console.WriteLine($"{armor.Name} 장착 \n방어력이 {armor.DefBonus} 만큼 상승");

        }
        public void PotionHaling(Potion potion) //회복 적용
        {
            Hp += potion.Haling;
            Console.WriteLine($"{potion.Name} 사용 \n체력이 {potion.Haling} 만큼 회복 남은 체력 : {Hp}" );

        }
        public void PlayerNomalAccack(Monster target) // 공격력 - 방어력 = 최종 데미지
        {
            int finalDamage = target.TakeDamg(Dmg);

            Console.WriteLine($"{Name}의 검 휘두르기 사용 {finalDamage} 만큼의 데미지를 주었습니다.");
            
        }
        public void PlayerUltimateSkill(Monster target)
        {
            int finalDamage = target.TakeDamg(Dmg * 3);
            Console.WriteLine($"{Name}의 궁극기 사용 {finalDamage} 만큼의 데미지를 주었습니다.");
        }



    }
    abstract class Monster : AllCharacter
    {
        private int rewardExp;
        private int rewardGold;

       
        public int RewardExp
        {
            get { return rewardExp; }
            protected set { rewardExp = value; }
        }
        public int RewardGold
        {
            get { return rewardGold; }
            protected set { rewardGold = value; }
        }

        public abstract void MonsterNomalAccack(Player target);
        public abstract void MonsterUltimateSkill(Player target);

    }

    class Slime : Monster
    {
        public Slime()
        {
            Name = "슬라임";
            Hp = 15;
            Dmg = 1;
            Def = 0;
            Level = 1;
            RewardGold = 10;
            RewardExp = 4;
        }
        public override void MonsterNomalAccack(Player target)
        {
            int finalDamage = target.TakeDamg(Dmg);
            Console.WriteLine($"{Name}의 액체 분사 사용 \n{finalDamage} 만큼의 데미지를 주었습니다.");
            
        }
        public override void MonsterUltimateSkill(Player target)
        {
            int finalDamage = target.TakeDamg(Dmg * 3);
            Console.WriteLine($"{Name}의 궁극기 사용 \n{finalDamage} 만큼의 데미지를 주었습니다.");
        }
    }
    class Bat : Monster
    {
        public Bat()
        {
            Name = "박쥐";
            Hp = 14;
            Dmg = 8;
            Def = 1;
            Level = 5;
            RewardGold = 25;
            RewardExp = 7;
        }
        public override void MonsterNomalAccack(Player target)
        {
            int finalDamage = target.TakeDamg(Dmg);
            Console.WriteLine($"{Name}의 깨물기 사용 \n{finalDamage} 만큼의 데미지를 주었습니다.");

        }
        public override void MonsterUltimateSkill(Player target)
        {
            int finalDamage = target.TakeDamg(Dmg * 3);
            Console.WriteLine($"{Name}의 궁극기 사용 \n{finalDamage} 만큼의 데미지를 주었습니다.");
        }
    }

    abstract class BossMonster : Monster
    {
       



    }
    class Sens : BossMonster
    {
        public Sens()
        {
            Name = "샌즈";
            Hp = 100;
            Dmg = 30;
            Def = 0;
            Level = 30;
            RewardGold = 100;
            RewardExp = 85;
        }
        public override void MonsterNomalAccack(Player target)
        {
            int finalDamage = target.TakeDamg(Dmg);
            Console.WriteLine($"{Name}의 블라스터 사용 \n{finalDamage} 만큼의 데미지를 받았습니다.");

        }
        public override void MonsterUltimateSkill(Player target)
        {
            int finalDamage = target.TakeDamg(Dmg * 3);
            Console.WriteLine($"{Name}의 궁극기 사용 \n{finalDamage} 만큼의 데미지를 받았습니다.");
        }
    }

}
