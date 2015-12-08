
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsoleView : IBaseView, IGameDataChangeListener, IGameStateChangeListener
{
    public static string errorMessage = "";

    public ConsoleView()
    {
        TILE_CHARS.Add("floor", '▒');
        TILE_CHARS.Add("wall", '▓');
        Application.Add((IGameDataChangeListener)this);
    }

    public GameData data;
    public bool score;
    public bool hud;
    public IConsoleRenderer currentRenderer;
    private readonly Dictionary<string, char> TILE_CHARS = new Dictionary<string, char>();
    private readonly Dictionary<string, char> ITEM_CHARS = new Dictionary<string, char>();
    private Vector2 loff = new Vector2(2, 0);
    private Vector2 offset = Vector2.ZERO;
    private int viewH = 31;
    private int viewW = 31;

    public ConsolePixel[,] uiContent = new ConsolePixel[44, 55];

    public void Execute()
    {
        Console.Clear(); //Very important for the view to actually work correctly, uncomment and test first before continuing if somethings looks wrong
        if (Application.GetState().Get() == GameStates.MENU)
        {
            RenderMenu();
        }
        if (Application.GetState().Get() == GameStates.MAPS)
        {
            RenderMapSelect();
        }
        if (Application.GetState().Get() == GameStates.GAME)
        {
            RenderGame();
        }
        if (Application.GetState().Get() == GameStates.FINISH)
        {
            RenderFinishScreen();
        }
        //RENDERS THE CURRENT UICONTENT
        for (int i = 0; i < uiContent.GetLength(0); i++)
        {
            for (int j = 0; j < uiContent.GetLength(1); j++)
            {
                Console.ForegroundColor = uiContent[i, j].foreground;
                Console.BackgroundColor = uiContent[i, j].background;
                Console.Write(uiContent[i, j].symbol);
            }
            Console.WriteLine();
        }
    }

    public void RenderMenu()
    {
        ConsoleColor f = ConsoleColor.Gray;
        ConsoleColor b = ConsoleColor.Black;

        char[] label;
        string content;

        ConsoleMenuController temp = (ConsoleMenuController)MasterControlProgram.GetController();

        for (int i = 0; i < uiContent.GetLength(0); i++)
        {
            for (int j = 0; j < uiContent.GetLength(1); j++)
            {
                uiContent[i, j] = new ConsolePixel();
            }
        }

        content = "CONSOLE DUNGEON CRAWLER : MAIN MENU";
        label = content.ToCharArray();
        for (int i = 0; i < label.Length; i++)
        {
            f = ConsoleColor.Green;
            uiContent[0, i] = new ConsolePixel(label[i], f);
        }

        if (temp.states.Count > 0)
        {
            for (int i = 0; i < temp.states.Count; i++)
            {
                content = temp.states[i].ToString();
                label = content.ToCharArray();

                for (int j = 0; j < label.Length; j++)
                {
                    if (i == temp.index)
                    {
                        f = ConsoleColor.Gray;
                        b = ConsoleColor.Magenta;
                    }
                    else
                    {
                        f = ConsoleColor.Gray;
                        b = ConsoleColor.Black;
                    }

                    uiContent[1 + i, j] = new ConsolePixel(label[j], f, b);
                }
            }
        }
    }

    public void RenderMapSelect()
    {
        ConsoleColor f = ConsoleColor.Gray;
        ConsoleColor b = ConsoleColor.Black;

        char[] label;
        string content;

        ConsoleMapSelectionController temp = (ConsoleMapSelectionController)MasterControlProgram.GetController();

        for (int i = 0; i < uiContent.GetLength(0); i++)
        {
            for (int j = 0; j < uiContent.GetLength(1); j++)
            {
                uiContent[i, j] = new ConsolePixel();
            }
        }

        content = "CONSOLE DUNGEON CRAWLER : MAP SELECTION";
        label = content.ToCharArray();
        for (int i = 0; i < label.Length; i++)
        {
            f = ConsoleColor.Green;
            uiContent[0, i] = new ConsolePixel(label[i], f);
        }

        if (temp.states.Count > 0)
        {
            for (int i = 0; i < temp.states.Count; i++)
            {
                content = temp.states[i].ToString();
                label = content.ToCharArray();

                for (int j = 0; j < label.Length; j++)
                {
                    if (i == temp.index)
                    {
                        f = ConsoleColor.Gray;
                        b = ConsoleColor.Magenta;
                    }
                    else
                    {
                        f = ConsoleColor.Gray;
                        b = ConsoleColor.Black;
                    }

                    uiContent[1 + i, j] = new ConsolePixel(label[j], f, b);
                }
            }
        }
    }

    public void RenderFinishScreen()
    {
        ConsoleColor f = ConsoleColor.Gray;
        ConsoleColor b = ConsoleColor.Black;

        char[] label;
        string content;

        for (int i = 0; i < uiContent.GetLength(0); i++)
        {
            for (int j = 0; j < uiContent.GetLength(1); j++)
            {
                uiContent[i, j] = new ConsolePixel();
            }
        }

        content = "YOU DIED";
        label = content.ToCharArray();
        for (int i = 0; i < label.Length; i++)
        {
            f = ConsoleColor.White;
            uiContent[0, i] = new ConsolePixel(label[i], f);
        }

        content = "GAME OVER : SCORE REACHED " + data.score.GetScore().ToString();
        label = content.ToCharArray();
        for (int i = 0; i < label.Length; i++)
        {
            f = ConsoleColor.White;
            uiContent[0, i] = new ConsolePixel(label[i], f);
        }

        //Process all end game information here

        content = "PRESS ANY KEY TO RETURN TO MAIN MENU";
        label = content.ToCharArray();
        for (int i = 0; i < label.Length; i++)
        {
            f = ConsoleColor.Green;
            uiContent[1, i] = new ConsolePixel(label[i], f);
        }
    }

    public void RenderGame()
    {
        char symbol = ' ';
        offset = new Vector2((int)data.player.position.x - (viewH / 2), (int)data.player.position.y - (viewW / 2));

        //Console.Clear();
        ConsoleColor f = ConsoleColor.Gray;
        ConsoleColor b = ConsoleColor.Black;

        for (int i = 0; i < uiContent.GetLength(0); i++)
        {
            for (int j = 0; j < uiContent.GetLength(1); j++)
            {
                uiContent[i, j] = new ConsolePixel();
            }
        }

        //Player Health stuff
        for (int i = 0; i < data.player.maxHealth; i++)
        {

            if (i < data.player.health)
                f = ConsoleColor.Red;
            else
                f = ConsoleColor.DarkGray;

            uiContent[1, 0 + i] = new ConsolePixel('♥', f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Player Actions stuff
        int debug = data.player.maxActions;  // player.maxActions
        int debug2 = data.player.actions; // player.Actions
        for (int i = 0; i < debug; i++)
        {
            if (i < debug2)
                f = ConsoleColor.White;
            else
                f = ConsoleColor.DarkGray;

            uiContent[1, data.player.maxHealth + 1 + i] = new ConsolePixel('>', f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Score stuff
        if (true)
        {
            string content = data.score.GetScore().ToString();
            char[] label = content.ToCharArray();

            for (int i = 0; i < label.Length; i++)
            {
                f = ConsoleColor.White;
                uiContent[1, viewW - content.Length + i] = new ConsolePixel(label[i], f, b);
            }
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/
        //Ammo Stuff
        if (data.player.Weapon.content.currentammo > -1 && data.player.Weapon.content.ammo > -1)
        {
            string content = data.player.Weapon.content.currentammo.ToString() + "/" + data.player.Weapon.content.ammo;
            char[] label = content.ToCharArray();

            for (int i = 0; i < label.Length; i++)
            {
                if (data.player.Weapon.content.currentammo == 0 && label[i].ToString().Equals("0"))
                {
                    f = ConsoleColor.Red;
                }
                else f = ConsoleColor.Gray;
                uiContent[1, viewW - content.Length + i] = new ConsolePixel(label[i], f, b);
            }
        }
        else
        {
            string content = "melee";
            char[] label = content.ToCharArray();

            for (int i = 0; i < label.Length; i++)
            {
                uiContent[1, viewW - content.Length + i] = new ConsolePixel(label[i], f, b);
            }
        }

        //Geography Render
        for (int i = 0; i < viewH; i++)
        {
            for (int j = 0; j < viewW; j++)
            {
                //Todo: Dont render whole level
                //
                //REALLY SIMPLE METHOD, CAN PROBABLY DO BETTER
                /*
                if (Vector2.Distance(new Vector2(i, j), data.player.position) > 5)
                    continue;
                    */

                int x = (int)offset.x + i;
                int y = (int)offset.y + j;

                if ((x >= 0 && x < data.level.structure.GetLength(0)) && (y >= 0 && y < data.level.structure.GetLength(1)))
                {
                    symbol = TILE_CHARS[data.level.structure[x, y].terrain];
                    //?+i/?+j for the level position offset
                    f = ConsoleColor.Gray;
                    b = ConsoleColor.Black;
                    uiContent[i + (int)loff.x, j] = new ConsolePixel(symbol, f, b);
                }
                /*
                else
                {
                    f = ConsoleColor.Black;
                    b = ConsoleColor.DarkGreen;
                    uiContent[i + (int)loff.x, j] = new ConsolePixel(' ', f, b);
                }
                /**/
            }
        }
        /**/


        //Doors Render
        for (int i = 0; i < data.level.doors.Count; i++)
        {
            if (data.level.doors[i].open == true)
            {
                symbol = '▀';
            }
            if (data.level.doors[i].open == false)
            {
                symbol = '■';
            }
            if (data.level.doors[i].type == "red") f = ConsoleColor.Red;
            if (data.level.doors[i].type == "green") f = ConsoleColor.Green;
            if (data.level.doors[i].type == "blue") f = ConsoleColor.Blue;
            if (data.level.doors[i].type == "yellow") f = ConsoleColor.Yellow;

            int x = -(int)offset.x + (int)data.level.doors[i].position.x + (int)loff.x;
            int y = -(int)offset.y + (int)data.level.doors[i].position.y;

            if ((x >= (int)loff.x && x < viewH - (int)loff.x && (y >= 0 && y < viewW)))
            {
                uiContent[-(int)offset.x + (int)data.level.doors[i].position.x + (int)loff.x, -(int)offset.y + (int)data.level.doors[i].position.y] = new ConsolePixel(symbol, f, b);
            }
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/


        //Pickup Render
        for (int i = 0; i < data.level.pickUps.Count; i++)
        {
            symbol = 'P';
            f = ConsoleColor.Yellow;

            if (data.level.pickUps[i].item.type == "med")
            {
                symbol = '+';
                f = ConsoleColor.Green;
            }
            if (data.level.pickUps[i].item.type == "ammo")
            {
                symbol = '‼';
                f = ConsoleColor.White;
            }
            if (data.level.pickUps[i].item.type == "use")
            {
                symbol = '‼';
                f = ConsoleColor.Yellow;
            }
            if (data.level.pickUps[i].item.type == "weap")
            {
                symbol = '¬';
                f = ConsoleColor.Yellow;
            }
            if (data.level.pickUps[i].item.type == "armor")
            {
                symbol = 'A';
                f = ConsoleColor.Blue;
            }
            if (data.level.pickUps[i].item.type == "grenade")
            {
                symbol = 'ó';
                f = ConsoleColor.Cyan;
            }
            if (data.level.pickUps[i].item.type == "key")
            {
                symbol = '¶';
                f = ConsoleColor.Magenta;
            }

            int x = -(int)offset.x + (int)data.level.pickUps[i].position.x + (int)loff.x; ;
            int y = -(int)offset.y + (int)data.level.pickUps[i].position.y;

            if ((x >= (int)loff.x && x < viewH) && (y >= 0 && y < viewW))
            {
                uiContent[-(int)offset.x + (int)data.level.pickUps[i].position.x + (int)loff.x, -(int)offset.y + (int)data.level.pickUps[i].position.y] = new ConsolePixel(symbol, f, b);
            }
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/


        //Subsystem/Level End Trigger Render
        for (int i = 0; i < data.level.trigger.Count; i++)
        {
            if (data.level.trigger[i].name == "endoflevel")
            {
                symbol = '♦';
                f = ConsoleColor.Green;
            }
            if (data.level.trigger[i].name == "subsystem")
            {
                symbol = '♦';
                f = ConsoleColor.Yellow;
            }

            int x = -(int)offset.x + (int)data.level.trigger[i].position.x + (int)loff.x; ;
            int y = -(int)offset.y + (int)data.level.trigger[i].position.y;

            if ((x >= (int)loff.x && x < viewH) && (y >= 0 && y < viewW))
            {
                uiContent[-(int)offset.x + (int)data.level.trigger[i].position.x + (int)loff.x, -(int)offset.y + (int)data.level.trigger[i].position.y] = new ConsolePixel(symbol, f, b);
            }
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/


        //Enemies Render
        for (int i = 0; i < data.level.enemies.Count; i++)
        {
            Actor enemy = data.level.enemies[i];
            symbol = 'O';
            f = ConsoleColor.Red;
            if (data.level.enemies[i].name == "alien_assaulter")
            {
                symbol = 'M';
                f = ConsoleColor.Red;
            }
            if (data.level.enemies[i].name == "alien_trooper")
            {
                symbol = 'R';
                f = ConsoleColor.Red;
            }
            if (data.level.enemies[i].name == "cyberbear")
            {
                symbol = 'B';
                f = ConsoleColor.Red;
            }

            int x = -(int)offset.x + (int)enemy.position.x + (int)loff.x;
            int y = -(int)offset.y + (int)enemy.position.y;

            if ((x >= (int)loff.x && x < viewH - (int)loff.x && (y >= 0 && y < viewW)))
            {
                uiContent[-(int)offset.x + (int)enemy.position.x + (int)loff.x, -(int)offset.y + (int)enemy.position.y] = new ConsolePixel(symbol, f, b);
            }
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Player Render
        Actor player = data.player;
        symbol = 'O';
        f = ConsoleColor.Black;
        b = ConsoleColor.DarkGray;

        if (data.player.actions <= 0)
        {
            f = ConsoleColor.DarkRed;
        }

        uiContent[viewH / 2 + (int)loff.x, viewW / 2] = new ConsolePixel(symbol, f, b);
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Selector Render - (Not anymore) Bugged
        if (data.combat)
        {
            GameObject selector = data.player.selector;
            symbol = uiContent[-(int)offset.x + (int)selector.position.x + (int)loff.x, -(int)offset.y + (int)selector.position.y].symbol;
            f = ConsoleColor.White;
            b = ConsoleColor.Magenta;
            uiContent[-(int)offset.x + (int)selector.position.x + (int)loff.x, -(int)offset.y + (int)selector.position.y] = new ConsolePixel(symbol, f, b);
            f = ConsoleColor.Gray;
            b = ConsoleColor.Black;
        }
        /**/

        #region inventory stuff
        //Inventory Stuff
        if (!data.inv)
        {
            List<string> charactersheet = new List<string>()
            {
                "//////////////////////",
                "////SYSTEM STATUS/////",
                "//",
                "//W: " + data.player.Weapon.content.name,
                "//A: " + data.player.Armor.content.name,
                "//",
                "//DMG   " + data.player.Weapon.content.damage,
                "//ACC   " + data.player.Weapon.content.accuracy,
                "//RANGE " + data.player.Weapon.content.range,
                "//PEN   " + data.player.Weapon.content.penetration,
                "//",
                "//ARMOR: " + data.player.Armor.content.value + " points of ",
                "//" + data.player.Armor.content.armortype + " armor",
                "//",
                "//////////////////////",
                "//////////////////////",
            };

            if (true)
            {
                char[] label;
                string content = "";
                label = content.ToCharArray();

                for (int i = 0; i < charactersheet.Count; i++)
                {
                    content = charactersheet[i];
                    label = content.ToCharArray();
                    for (int j = 0; j < label.Length; j++)
                    {
                        uiContent[i + 1, viewW + 1 + j] = new ConsolePixel(label[j], f, b);
                    }
                }
            }
        }

        if (data.inventory != null && data.inv)
        {
            char[] label;
            string content = "Inventory:";
            int inventoryOffset = 0;
            label = content.ToCharArray();
            for (int i = 0; i < label.Length; i++)
            {
                uiContent[1, (data.level.structure.GetLength(1) + 1) + i] = new ConsolePixel(label[i]);
            }
            content = "Current items: " + data.inventory.content.Count.ToString();
            label = content.ToCharArray();
            for (int i = 0; i < label.Length; i++)
            {
                uiContent[16, (data.level.structure.GetLength(1) + 1) + i] = new ConsolePixel(label[i]);
            }

            if (data.inventory.content.Count > 0)
            {
                if (data.currentItem > 5 && data.currentItem + inventoryOffset < data.inventory.content.Count)
                {
                    inventoryOffset += data.currentItem - 5;
                }
                if (data.currentItem + inventoryOffset > data.inventory.content.Count-1)
                {
                    inventoryOffset = 4;
                }

                for (int i = 0; i < Math.Min(11, data.inventory.content.Count); i++)
                {
                    content = data.inventory.content[i+inventoryOffset].item.name;

                    if (data.inventory.content[i].count > 1)
                    {
                        content = data.inventory.content[i+inventoryOffset].item.name + " " + data.inventory.content[i+inventoryOffset].count.ToString();
                    }

                    label = content.ToCharArray();

                    for (int j = 0; j < label.Length; j++)
                    {
                        if (i+inventoryOffset == data.currentItem)
                        {
                            f = ConsoleColor.Gray;
                            b = ConsoleColor.Magenta;
                        }
                        if (data.inventory.content[i+inventoryOffset].item == data.player.Weapon.content)
                        {
                            f = ConsoleColor.White;
                        }
                        if (data.inventory.content[i+inventoryOffset].item == data.player.Armor.content)
                        {
                            f = ConsoleColor.White;
                        }

                        if (true)
                        {
                            uiContent[i + 3, (data.level.structure.GetLength(1) + 1) + j] = new ConsolePixel(label[j], f, b);
                        }

                        f = ConsoleColor.Gray;
                        b = ConsoleColor.Black;
                    }
                }
            }
            else
            {
                content = "empty inventory";
                label = content.ToCharArray();

                for (int i = 0; i < label.Length; i++)
                {
                    uiContent[1, (data.level.structure.GetLength(1) + 1) + i] = new ConsolePixel(label[i]);
                }
            }
        }
        /**/
        #endregion

        //InfoLog
        if (true)
        {
            List<string> infoLog = new List<string>();

            if (data.inv && data.currentItem < data.inventory.content.Count)
            {
                //data.currentItem = 0;
                ItemWrapper current = data.inventory.content[data.currentItem];

                infoLog.Add("//////////////////////");
                infoLog.Add("///ITEM INFORMATION///");
                infoLog.Add("");
                infoLog.Add(current.item.name);

                if (current.item.type == "weap")
                {
                    Weapon temp = (Weapon)current.item;
                    infoLog.Add("DAMAGE:     " + temp.damage.ToString());
                    infoLog.Add("DMGTYPE:    " + temp.damagetype.ToString());
                    infoLog.Add("AMMO:       " + temp.ammotype.ToString());
                    infoLog.Add("RANGE       " + temp.range.ToString());
                    infoLog.Add("ACCURACY    " + temp.accuracy.ToString());
                    infoLog.Add("PENETRATION " + temp.penetration.ToString());
                }

                if (current.item.type == "armor")
                {
                    Armor temp = (Armor)current.item;
                    infoLog.Add("ARMOR:     " + temp.value.ToString());
                    infoLog.Add("ARMTYPE:   " + temp.armortype.ToString());
                }
            }

            if (data.combat)
            {
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (data.level.enemies[i].position.x == data.player.selector.position.x && data.level.enemies[i].position.y == data.player.selector.position.y)
                    {
                        if (data.level.enemies[i].info == true)
                        {
                            infoLog.Add("//////////////////////");
                            infoLog.Add("//COMBAT INFORMATION//");
                            infoLog.Add("");
                            infoLog.Add("TARGET: " + data.level.enemies[i].name);
                            infoLog.Add("");
                            infoLog.Add("WEAPON:    " + data.level.enemies[i].Weapon.content.name);
                            infoLog.Add("DAMAGE:    " + data.level.enemies[i].Weapon.content.damage);
                            infoLog.Add("DMGTYPE:   " + data.level.enemies[i].Weapon.content.damagetype);
                            infoLog.Add("RANGE:     " + data.level.enemies[i].Weapon.content.range);
                            infoLog.Add("ACCURACY:  " + data.level.enemies[i].Weapon.content.accuracy);
                            infoLog.Add("ARMOR:     " + data.level.enemies[i].Armor.content.value);
                            infoLog.Add("ARMTYPE:   " + data.level.enemies[i].Armor.content.armortype);
                        }
                        else
                        {
                            infoLog.Add("//////////////////////");
                            infoLog.Add("//COMBAT INFORMATION//");
                            infoLog.Add("");
                            infoLog.Add("UNKNOWN SPECIMEN");
                        }
                    }
                }
            }

            char[] label;
            string content = "";
            label = content.ToCharArray();

            if (infoLog.Count > 0)
            {
                for (int i = 0; i < infoLog.Count; i++)
                {
                    content = infoLog[i];
                    label = content.ToCharArray();
                    for (int j = 0; j < label.Length; j++)
                    {
                        uiContent[i + 18, (data.level.structure.GetLength(1) + 1) + j] = new ConsolePixel(label[j], f, b);
                    }
                }
            }
        }

        //ProtoLog
        if (true)
        {
            char[] label;
            string content = "";
            label = content.ToCharArray();

            if (data.combatlog.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    content = data.combatlog[(data.combatlog.Count - 1) - i];
                    label = content.ToCharArray();
                    for (int j = 0; j < label.Length; j++)
                    {
                        uiContent[viewH + 2 + i, 0 + j] = new ConsolePixel(label[j], f, b);
                    }
                }
            }
        }

        if (Application.auto)
        {
            char[] label;
            string content = "AUTO MODE ACTIVE";
            label = content.ToCharArray();
            f = ConsoleColor.White;
            b = ConsoleColor.Red;

            for (int i = 0; i < viewW; i++)
            {
                for (int j = 0; j < label.Length; j++)
                {
                    uiContent[viewH + 1, 0 + j] = new ConsolePixel(label[j], f, b);
                }
                uiContent[viewH + 1, 0 + i] = new ConsolePixel(f, b);
            }
        }

    }

    public void OnGameDataChange(GameData data)
    {
        this.data = data;
        Execute();
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
}