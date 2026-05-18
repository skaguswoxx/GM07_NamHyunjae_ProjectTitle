using AllCharacter;
using BattleSystem;
using ItemClass;

namespace Inter
{

    interface PlayerNomalAccack
    {
        void PlayerNomalAccack(Monster target);
    }
    interface PlayerUltimateSkill
    {
        void PlayerUltimateSkill(Monster target);
    }

    interface MonsterNomalAccack
    {
        void MonsterNomalAccack(Player target);
    }

    interface MonsterUltimateSkill
    {
        void MonsterUltimateSkill(Player target);
    }

    internal class Inter
    {

    }
}
