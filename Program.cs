using System.Data;

namespace SHARP3;

internal class Program
{
    private static void RewriteLine(string caret, List<char> buffer)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(caret);
        Console.Write(buffer.ToArray());
    }

    private static void Main()
    {
        DataSet dataSet = new();
        DataTable client = new("Client");
        dataSet.Tables.Add(client);
        DataColumn columnName = new("Name", typeof(string));
        columnName.Caption = "Имя";
        DataColumn columnAdress = new("Adress", typeof(string));
        columnAdress.Caption = "Адрес";
        DataColumn columnPhone = new("Phone", typeof(string));
        columnPhone.Caption = "Телефон";
        dataSet.Tables[0].Columns.Add(columnName);
        dataSet.Tables[0].Columns.Add(columnAdress);
        dataSet.Tables[0].Columns.Add(columnPhone);
        
        DataColumn[] keys = new DataColumn[1];
        keys[0] = columnName;
        client.PrimaryKey = keys;

        DataRow row1 = dataSet.Tables[0].NewRow();
        row1["Name"] = "John";
        row1["Adress"] = "Somestreet";
        row1["Phone"] = "88005553535";
        dataSet.Tables[0].Rows.Add(row1);

        DataRow row2 = dataSet.Tables[0].NewRow();
        row2["Name"] = "Smith";
        row2["Adress"] = "Somestreetttttt";
        row2["Phone"] = "82286663222";
        dataSet.Tables[0].Rows.Add(row2);
        
        DataTable Card = new("Card");
        dataSet.Tables.Add(Card);
        DataColumn columnNumber = new("Number", typeof(string));
        columnName.Caption = "Номер карты";
        DataColumn columnType = new("Type", typeof(string));
        columnAdress.Caption = "Тип карты";
        DataColumn columnExpire = new("Expire", typeof(string));
        columnPhone.Caption = "Cрок Действия";
        DataColumn columnBank = new("Bank", typeof(string));
        columnPhone.Caption = "Название Банка";
        DataColumn columnCVC = new("CVC", typeof(string));
        columnPhone.Caption = "CVC";
        
        dataSet.Tables[1].Columns.Add(columnNumber);
        dataSet.Tables[1].Columns.Add(columnType);
        dataSet.Tables[1].Columns.Add(columnExpire);
        dataSet.Tables[1].Columns.Add(columnBank);
        dataSet.Tables[1].Columns.Add(columnCVC);
        
        keys = new DataColumn[1];
        keys[0] = columnNumber;
        Card.PrimaryKey = keys;

        DataRow Cardrow1 = dataSet.Tables[1].NewRow();
        Cardrow1["Number"] = "4399 9250 0167 2723";
        Cardrow1["Type"] = "Visa";
        Cardrow1["Expire"] = "11/2025";
        Cardrow1["Bank"] = "Сбербанк";
        Cardrow1["CVC"] = "830";
        dataSet.Tables[1].Rows.Add(Cardrow1);

        DataRow Cardrow2 = dataSet.Tables[1].NewRow();
        Cardrow2["Number"] = "5526 4230 2899 9611";
        Cardrow2["Type"] = "MasterCard";
        Cardrow2["Expire"] = "11/2025";
        Cardrow2["Bank"] = "02/2026";
        Cardrow2["CVC"] = "335";
        dataSet.Tables[1].Rows.Add(Cardrow2);

        DataTable Goods = new DataTable("Goods");
        dataSet.Tables.Add(Goods);
        DataColumn columnСode = new DataColumn("Сode", typeof(string));
        columnName.Caption = "Артикул";
        DataColumn columnGName = new DataColumn("Name", typeof(string));
        columnAdress.Caption = "Название";
        DataColumn columnDesc = new DataColumn("Desc", typeof(string));
        columnDesc.Caption = "Описание";
        DataColumn columnBuyer = new DataColumn("Buyer", typeof(string));
        columnDesc.Caption = "Покупатель";

        dataSet.Tables[2].Columns.Add(columnСode);
        dataSet.Tables[2].Columns.Add(columnGName);
        dataSet.Tables[2].Columns.Add(columnDesc);
        dataSet.Tables[2].Columns.Add(columnBuyer);
        
        keys = new DataColumn[1];
        keys[0] = columnСode;
        Goods.PrimaryKey = keys;
        
        DataColumn parentColumn = dataSet.Tables["client"]!.Columns["Name"]!;
        DataColumn childColumn = dataSet.Tables["Goods"]!.Columns["Buyer"]!;
        dataSet.Relations.Add(new DataRelation("Buyer", parentColumn, childColumn));


        DataRow Goodsrow1 = dataSet.Tables[2].NewRow();
        Goodsrow1["Сode"] = "2723";
        Goodsrow1["Name"] = "Dildo 20";
        Goodsrow1["Desc"] = "Huge dildo 20 cm in length";
        Goodsrow1["Buyer"] = "Smith";
        dataSet.Tables[2].Rows.Add(Goodsrow1);

        DataRow Goodsrow2 = dataSet.Tables[2].NewRow();
        Goodsrow2["Сode"] = "1234";
        Goodsrow2["Name"] = "Nasvai";
        Goodsrow2["Desc"] = "Only original hach's product";
        Goodsrow2["Buyer"] = "John";
        dataSet.Tables[2].Rows.Add(Goodsrow2);


        List<int> strLen = new();
        string str = "";

        for (int colN = 0; colN < dataSet.Tables[0].Columns.Count; colN++)
        {
            strLen.Add($"{dataSet.Tables[0].Columns[colN].Caption} ({dataSet.Tables[0].Columns[colN].ColumnName})"
                .Length);
            str += $"{dataSet.Tables[0].Columns[colN].Caption} ({dataSet.Tables[0].Columns[colN].ColumnName})\t|\t";
        }

        Console.WriteLine(str);
        for (int row = 0; row < dataSet.Tables[0].Rows.Count; row++)
        {
            foreach (object? t in dataSet.Tables[0].Rows[row].ItemArray)
            {
                Console.Write($"{t}\t|\t");
            }

            Console.WriteLine();
        }

        string ReadInputWithDefault(string defaultValue, string caret = "> ")
        {
            Console.WriteLine();

            List<char> buffer = defaultValue.ToCharArray().Take(Console.WindowWidth - caret.Length - 1).ToList();
            Console.Write(caret);
            Console.Write(buffer.ToArray());
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(Math.Max(Console.CursorLeft - 1, caret.Length), Console.CursorTop);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(Math.Min(Console.CursorLeft + 1, caret.Length + buffer.Count),
                            Console.CursorTop);
                        break;
                    case ConsoleKey.Home:
                        Console.SetCursorPosition(caret.Length, Console.CursorTop);
                        break;
                    case ConsoleKey.End:
                        Console.SetCursorPosition(caret.Length + buffer.Count, Console.CursorTop);
                        break;
                    case ConsoleKey.Backspace:
                        if (Console.CursorLeft <= caret.Length)
                        {
                            break;
                        }

                        int cursorColumnAfterBackspace = Math.Max(Console.CursorLeft - 1, caret.Length);
                        buffer.RemoveAt(Console.CursorLeft - caret.Length - 1);
                        RewriteLine(caret, buffer);
                        Console.SetCursorPosition(cursorColumnAfterBackspace, Console.CursorTop);
                        break;
                    case ConsoleKey.Delete:
                        if (Console.CursorLeft >= caret.Length + buffer.Count)
                        {
                            break;
                        }

                        int cursorColumnAfterDelete = Console.CursorLeft;
                        buffer.RemoveAt(Console.CursorLeft - caret.Length);
                        RewriteLine(caret, buffer);
                        Console.SetCursorPosition(cursorColumnAfterDelete, Console.CursorTop);
                        break;
                    case ConsoleKey.Tab:
                    case ConsoleKey.Clear:
                    case ConsoleKey.Enter:
                    case ConsoleKey.Pause:
                    case ConsoleKey.Escape:
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.PageUp:
                    case ConsoleKey.PageDown:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.Select:
                    case ConsoleKey.Print:
                    case ConsoleKey.Execute:
                    case ConsoleKey.PrintScreen:
                    case ConsoleKey.Insert:
                    case ConsoleKey.Help:
                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                    case ConsoleKey.A:
                    case ConsoleKey.B:
                    case ConsoleKey.C:
                    case ConsoleKey.D:
                    case ConsoleKey.E:
                    case ConsoleKey.F:
                    case ConsoleKey.G:
                    case ConsoleKey.H:
                    case ConsoleKey.I:
                    case ConsoleKey.J:
                    case ConsoleKey.K:
                    case ConsoleKey.L:
                    case ConsoleKey.M:
                    case ConsoleKey.N:
                    case ConsoleKey.O:
                    case ConsoleKey.P:
                    case ConsoleKey.Q:
                    case ConsoleKey.R:
                    case ConsoleKey.S:
                    case ConsoleKey.T:
                    case ConsoleKey.U:
                    case ConsoleKey.V:
                    case ConsoleKey.W:
                    case ConsoleKey.X:
                    case ConsoleKey.Y:
                    case ConsoleKey.Z:
                    case ConsoleKey.LeftWindows:
                    case ConsoleKey.RightWindows:
                    case ConsoleKey.Applications:
                    case ConsoleKey.Sleep:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.NumPad9:
                    case ConsoleKey.Multiply:
                    case ConsoleKey.Add:
                    case ConsoleKey.Separator:
                    case ConsoleKey.Subtract:
                    case ConsoleKey.Decimal:
                    case ConsoleKey.Divide:
                    case ConsoleKey.F1:
                    case ConsoleKey.F2:
                    case ConsoleKey.F3:
                    case ConsoleKey.F4:
                    case ConsoleKey.F5:
                    case ConsoleKey.F6:
                    case ConsoleKey.F7:
                    case ConsoleKey.F8:
                    case ConsoleKey.F9:
                    case ConsoleKey.F10:
                    case ConsoleKey.F11:
                    case ConsoleKey.F12:
                    case ConsoleKey.F13:
                    case ConsoleKey.F14:
                    case ConsoleKey.F15:
                    case ConsoleKey.F16:
                    case ConsoleKey.F17:
                    case ConsoleKey.F18:
                    case ConsoleKey.F19:
                    case ConsoleKey.F20:
                    case ConsoleKey.F21:
                    case ConsoleKey.F22:
                    case ConsoleKey.F23:
                    case ConsoleKey.F24:
                    case ConsoleKey.BrowserBack:
                    case ConsoleKey.BrowserForward:
                    case ConsoleKey.BrowserRefresh:
                    case ConsoleKey.BrowserStop:
                    case ConsoleKey.BrowserSearch:
                    case ConsoleKey.BrowserFavorites:
                    case ConsoleKey.BrowserHome:
                    case ConsoleKey.VolumeMute:
                    case ConsoleKey.VolumeDown:
                    case ConsoleKey.VolumeUp:
                    case ConsoleKey.MediaNext:
                    case ConsoleKey.MediaPrevious:
                    case ConsoleKey.MediaStop:
                    case ConsoleKey.MediaPlay:
                    case ConsoleKey.LaunchMail:
                    case ConsoleKey.LaunchMediaSelect:
                    case ConsoleKey.LaunchApp1:
                    case ConsoleKey.LaunchApp2:
                    case ConsoleKey.Oem1:
                    case ConsoleKey.OemPlus:
                    case ConsoleKey.OemComma:
                    case ConsoleKey.OemMinus:
                    case ConsoleKey.OemPeriod:
                    case ConsoleKey.Oem2:
                    case ConsoleKey.Oem3:
                    case ConsoleKey.Oem4:
                    case ConsoleKey.Oem5:
                    case ConsoleKey.Oem6:
                    case ConsoleKey.Oem7:
                    case ConsoleKey.Oem8:
                    case ConsoleKey.Oem102:
                    case ConsoleKey.Process:
                    case ConsoleKey.Packet:
                    case ConsoleKey.Attention:
                    case ConsoleKey.CrSel:
                    case ConsoleKey.ExSel:
                    case ConsoleKey.EraseEndOfFile:
                    case ConsoleKey.Play:
                    case ConsoleKey.Zoom:
                    case ConsoleKey.NoName:
                    case ConsoleKey.Pa1:
                    case ConsoleKey.OemClear:
                    default:
                        char character = keyInfo.KeyChar;
                        if (character < 32)
                            break;
                        int cursorAfterNewChar = Console.CursorLeft + 1;
                        if (cursorAfterNewChar > Console.WindowWidth ||
                            caret.Length + buffer.Count >= Console.WindowWidth - 1)
                        {
                            break;
                        }

                        buffer.Insert(Console.CursorLeft - caret.Length, character);
                        RewriteLine(caret, buffer);
                        Console.SetCursorPosition(cursorAfterNewChar, Console.CursorTop);
                        break;
                }

                keyInfo = Console.ReadKey(true);
            }

            Console.Write(Environment.NewLine);

            return new string(buffer.ToArray());
        }
        
        string selectOptions(List<string?> Options)
        {
            if (Options.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(Options));
            Console.WriteLine("Select column: ");
            int columnSelected = 0;
            Console.Clear();
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{(i == columnSelected ? "> " : "  ")}{Options[i]}");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                columnSelected = keyInfo.Key switch
                {
                    ConsoleKey.DownArrow => columnSelected + 1 >= Options.Count ? columnSelected : columnSelected + 1,
                    ConsoleKey.UpArrow => columnSelected - 1 <= 0 ? 0 : columnSelected - 1,
                    _ => columnSelected
                };

                Console.Clear();
                for (int i = 0; i < Options.Count; i++)
                {
                    Console.WriteLine($"{(i == columnSelected ? "> " : "  ")}{Options[i]}");
                }

                keyInfo = Console.ReadKey(true);
            }

            return Options[columnSelected]!;
        }

        int selectOptionsInt(List<string> Options)
        {
            Console.WriteLine("Select column: ");
            int columnSelected = 0;
            Console.Clear();
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{(i == columnSelected ? "> " : "  ")}{Options[i]}");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                columnSelected = keyInfo.Key switch
                {
                    ConsoleKey.DownArrow => columnSelected + 1 >= Options.Count ? columnSelected : columnSelected + 1,
                    ConsoleKey.UpArrow => columnSelected - 1 <= 0 ? 0 : columnSelected - 1,
                    _ => columnSelected
                };

                Console.Clear();
                for (int i = 0; i < Options.Count; i++)
                {
                    Console.WriteLine($"{(i == columnSelected ? "> " : "  ")}{Options[i]}");
                }

                keyInfo = Console.ReadKey(true);
            }

            return columnSelected;
        }

        string table = selectOptions((from object? i in dataSet.Tables select i.ToString()).ToList());
        while (true)
        {
            Console.Clear();
            Console.WriteLine(dataSet.Relations[0]);
            Console.WriteLine(dataSet.Tables[table]);
            Console.WriteLine(str);
            for (int row = 0; row < dataSet.Tables[table]!.Rows.Count; row++)
            {
                foreach (object? t in dataSet.Tables[table]!.Rows[row].ItemArray)
                {
                    Console.Write($"{t}\t|\t");
                }

                Console.WriteLine();
            }

            Console.Write($"Input command > ");
            string input = Console.ReadLine()!.ToLower();
            try
            {
                switch (input)
                {
                    case "add":
                        DataRow newrow = dataSet.Tables[table]!.NewRow();
                        foreach (object? i in dataSet.Tables[table]!.Columns)
                        {
                            Console.Write($"{i}: ");
                            newrow[i.ToString()!] = Console.ReadLine();
                        }

                        dataSet.Tables[table]!.Rows.Add(newrow);
                        break;

                    case "edit":
                        if (dataSet.Tables[table]!.Rows.Count != 0)
                        {
                            List<string> ls = new();
                            for (int row = 0; row < dataSet.Tables[table]!.Rows.Count; row++)
                            {
                                ls.Add(dataSet.Tables[table]!.Rows[row].ItemArray.Select(t => t!.ToString()).Aggregate(str, (current, i) => current + i + " "));
                            }

                            DataRow rowToEdit = dataSet.Tables[table]!.Rows[selectOptionsInt(ls)];

                            for (int i = 0; i < dataSet.Tables[table]!.Columns.Count; i++)
                            {
                                DataColumn column = dataSet.Tables[table]!.Columns[i];
                                Console.Write($"{column}: ");
                                rowToEdit[column.ToString()] =
                                    ReadInputWithDefault(rowToEdit[column.ToString()].ToString()!);
                            }
                        }

                        break;

                    case "delete":
                        if (dataSet.Tables[table]!.Rows.Count != 0)
                        {
                            List<string> ls = new();
                            for (int row = 0; row < dataSet.Tables[table]!.Rows.Count; row++)
                            {
                                ls.Add(dataSet.Tables[table]!.Rows[row].ItemArray.Select(t => t!.ToString())
                                    .Aggregate(str, (current, i) => current + i + " "));
                            }

                            dataSet.Tables[table]!.Rows[selectOptionsInt(ls)].Delete();
                        }

                        break;

                    case "table":
                        table = selectOptions((from object? i in dataSet.Tables select i.ToString()).ToList());
                        break;
                    
                    case "serch":
                        if (dataSet.Tables[table]!.Rows.Count != 0)
                        {
                            List<string?> list = dataSet.Tables[table]!.PrimaryKey.Select(i => i.ToString()).ToList()!;

                            string serchby  = dataSet.Tables[table]!.Columns[selectOptions(list)]!.ToString();
                            Console.Write($"Where {serchby}: ");
                            string? key = Console.ReadLine();
                            if (dataSet.Tables[table]!.Rows.Find(key)! is { } foundRow)
                            {
                                bool isRelation = (from object? i in dataSet.Tables[table]!.Columns select i.ToString()).ToList().Where(x => dataSet.Relations.Contains(x)).ToList().Any();
                                if (isRelation)
                                {
                                    Console.WriteLine("relation found!");
                                    DataRelation relation = dataSet.Relations[(from object? i in dataSet.Tables[table]!.Columns select i.ToString()).ToList().IndexOf(serchby)];
                                    DataTable Ptable = relation.ParentTable;
                                    DataRow foundParentRow = Ptable.Rows.Find(foundRow["Buyer"])!;
                                    Console.WriteLine($"{relation}: ");
                                    for (int i = 0; i < foundParentRow.ItemArray.Length; i++)
                                    {
                                        Console.WriteLine($"{Ptable.Columns[i]}: {foundParentRow[i]}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("relation not found!");
                                    for (int i = 0; i < foundRow.ItemArray.Length; i++)
                                    {
                                        Console.WriteLine($"{dataSet.Tables[table]!.Columns[i]}: {foundRow[i]}");
                                    }
                                }
                            }
                            Console.ReadKey();
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}