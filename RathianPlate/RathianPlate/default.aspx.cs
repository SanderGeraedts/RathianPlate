using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RathianPlate
{
    public partial class _default : System.Web.UI.Page
    {
        private Hunt hunt1;
        private Hunt hunt2;
        private Hunt hunt3;
        private Hunt hunt4;
        private Hunt hunt5;

        private Monster monster1;
        private Monster monster2;
        private Monster monster3;
        private Monster monster4;
        private Monster monster5;

        private Hunter hunter1;
        private Hunter hunter2;
        private Hunter hunter3;
        private Hunter hunter4;
        private Hunter hunter5;

        private Quest quest1;
        private Quest quest2;
        private Quest quest3;

        private List<Hunt> hunts; 

        protected void Page_Init(object sender, EventArgs e)
        {
            hunt1 = new Hunt(1, "Test1", DateTime.Now, "36-5201-9363-2512");
            hunt2 = new Hunt(2, "Test2", DateTime.Now, "36-5201-9363-2512");
            hunt3 = new Hunt(3, "Test3", DateTime.Now, "36-5201-9363-2512");
            hunt4 = new Hunt(4, "Test4", DateTime.Now, "36-5201-9363-2512");
            hunt5 = new Hunt(5, "Test5", DateTime.Now, "36-5201-9363-2512");

            monster1 = new Monster(1, "Great Jaggi", "LOW", "", "");
            monster2 = new Monster(1, "Barroth", "LOW", "", "");
            monster3 = new Monster(1, "Rathian", "LOW", "", "");
            monster4 = new Monster(1, "Queropeco", "LOW", "", "");
            monster5 = new Monster(1, "Gore Magala", "LOW", "", "");

            hunter1 = new Hunter(1, "Sander", "s.geraedts", "something", "1");
            hunter2 = new Hunter(2, "Sander", "s.geraedts", "something", "1");
            hunter3 = new Hunter(3, "Sander", "s.geraedts", "something", "1");
            hunter4 = new Hunter(4, "Sander", "s.geraedts", "something", "1");
            hunter5 = new Hunter(5, "Sander", "s.geraedts", "something", "1");

            quest1 = new Quest(1, "", "", "", "", false, "", "");
            quest2 = new Quest(2, "", "", "", "", false, "", "");
            quest3 = new Quest(3, "", "", "", "", false, "", "");

            quest1.Monsters.Add(monster2);
            quest1.Monsters.Add(monster1);
            quest1.Monsters.Add(monster3);
            quest1.Monsters.Add(monster4);
            quest2.Monsters.Add(monster1);
            quest2.Monsters.Add(monster2);
            quest3.Monsters.Add(monster5);

            hunt1.Hunters.Add(hunter1);
            hunt1.Hunters.Add(hunter2);
            hunt1.Hunters.Add(hunter3);
            hunt2.Hunters.Add(hunter1);
            hunt3.Hunters.Add(hunter1);
            hunt4.Hunters.Add(hunter1);
            hunt5.Hunters.Add(hunter1);

            hunt1.Quest = quest1;
            hunt2.Quest = quest2;
            hunt3.Quest = quest3;
            hunt4.Quest = quest1;
            hunt5.Quest = quest2;

            hunts = new List<Hunt>();

            hunts.Add(hunt1);
            hunts.Add(hunt2);
            hunts.Add(hunt3);
            hunts.Add(hunt4);
            hunts.Add(hunt5);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.rptHunts.DataSource = hunts;
            //this.rptHunts.DataBind();
        }
    }
}