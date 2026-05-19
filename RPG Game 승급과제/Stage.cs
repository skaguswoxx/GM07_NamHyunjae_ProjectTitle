using AllCharacter;
using BattleSystem;
using ItemClass;

namespace FieldStage
{
    class Stage
    {
        public void DrawStage()
        {
            Console.Clear();

            Console.WriteLine("                         [Boss]                        ");
            Console.WriteLine("                    #    #   #     #                       ");
            Console.WriteLine("                  #     #    #      #                      ");
            Console.WriteLine("               #       #     #       #                     ");
            Console.WriteLine("             #        #      #        #                    ");
            Console.WriteLine("          #          #       #         #                   ");
            Console.WriteLine("         #         #         #           #                 ");
            Console.WriteLine("     [상점]     [사냥터]    [동굴]       [보스방]       ");
            Console.WriteLine("       #          #         #          #                ");
            Console.WriteLine("        #          #        #         #                 ");
            Console.WriteLine("           #        #       #        #                  ");
            Console.WriteLine("             #       #      #       #                   ");
            Console.WriteLine("                 #    #     #      #                   ");
            Console.WriteLine("                      [ START ]                        ");

            Console.WriteLine();
            Console.WriteLine("1. 상점으로 이동");
            Console.WriteLine("2. 사냥터로 이동 : 랜덤 등장");
            Console.WriteLine("3. 동굴로 이동 : 박쥐만 등장");
            Console.WriteLine("4. 보스 사냥 이동");

        }


        public int StageChoice()
        {
            while (true)
            {
                DrawStage();
                Console.WriteLine("이동할 스테이지 선택");

                int useChoice;
                bool inputNum = int.TryParse(Console.ReadLine(), out useChoice);

                if(!inputNum)
                {
                    Console.WriteLine("숫자를 입력해 주세요");
                    continue;
                }


                if(useChoice < 0 || useChoice > 4)
                {
                    Console.WriteLine("1~4 번중에서 선택해 주세요");
                    Console.WriteLine();
                    continue;
                }


                return useChoice;


            }

        }
    }
}
