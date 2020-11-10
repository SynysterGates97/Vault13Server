using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Vault13Server
{
    [Serializable]
    class VaultTecServerProtocol
    {
        //commands:
        //sd <name>,<money>- (send dweller) отправить жителя <name> с жаловнием в <money> крышок в пустоши за крышками.
            //чтобы отправить жителя за крышками, нужно его ими снарядить.
            //Жителя отправляют на время T - чем больше время экспедиции и чем меньше здоровье жителе - тем выше вероятность, что он погибнет.
            //Если житель вернулся - он ждет у убежища.

        //howdy <name> - запросить статус жителя:
            //В убежище
            //Путешествует
            //Ожидает у входа
            //Мёртв

        //poplist запросит имена жителей убежища с их статусами

        //popcount - запросить количество жителей в убежище (учитываются и те, кто путешествует (но они должны быть живыми))

        //muneh - запросить количество крышек в убежище (крышки у путешественников не считаются)

        //help

        const string sendDweller = "sd";
        const string getDwellerStatus = "howdy";
        const string populationCount = "popcount";
        const string populationList = "poplist";
        const string getMoney = "muneh";
        const string help = "help";

        string Command
        { get; set; }
    }
}
