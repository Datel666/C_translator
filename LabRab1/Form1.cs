﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LabRab1
{
    public partial class Compiler : Form
    {
        void clear()
        {
            lbMatrix.Items.Clear();
            DGW.Rows.Clear();
            DGW2.Rows.Clear();
            DGW3.Rows.Clear();
            DGW4.Rows.Clear();
            DGW5.Rows.Clear();
            DGW6.Rows.Clear();
        }
        public Compiler()
        {
            InitializeComponent();
        }

        string allText;

        private void button_Open(object sender, EventArgs e) // загрузка файла в RichTextBox
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                allText = sr.ReadToEnd();
                allText = allText.ToLower();
                sr.Close();
                richTB.Text = allText;
            }
        }

        HandlingCode hc;
        private void button_Work(object sender, EventArgs e)
        {
            clear();
            hc = new HandlingCode();
            if (!hc.chekContentWorkspace(richTB.Text)) { return; }
            richTB.Text = richTB.Text.ToLower();
            hc.lexicalAnalys(richTB.Text);
            if (hc.flagMaxLenght) { return; }
            hc.showTablLexem(DGW);
            hc.classificationLexemes();
            hc.showAllTables(DGW2, DGW3, DGW4, DGW5, DGW6);
            hc.syntaxisAnalys(0); //{ hc.showMatrix(lbMatrix); }
            showMatrix(hc.matrix);
        }

        void showMatrix(List<string> matrix)
        {
            foreach (var i in matrix) lbMatrix.Items.Add(i);
        }
    }

    class HandlingCode // обработка кода
    {
        public bool chekContentWorkspace(string allText) //Если рабочая область пуста то лексический и синаксические разборы не начнутся
        {
            if (allText == string.Empty) { MessageBox.Show("Введите код!", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            bool flag = false;
            for (int i = 0; i < allText.Length; i++)
            {
                if (allText[i] != ' ' && allText[i] != '\n') { flag = true; }
            }
            if (flag) return true;
            else MessageBox.Show("Введите код!", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Information); return false;
        }

        public void showMatrix(ListBox lb)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                lb.Items.Add(matrix[i]);
            }
        }
        #region Лексический анализ
        bool doubleD = false;
        string[] massDelimiter = new string[] { ",", ";", "+", "-", "=", "*", "/", "<", ">", "{", "}", "(", ")", "!", "|", "&", "^", "&&", "||", "!=", "==", "<=", "=<", ">=", "=>", "=!" };
        string[] massSpecialWords = new string[] { "long", "int", "short", "char", "double","string", "if", "else", "main"};
        string buff;
        int maxLenght = 8;
        int countSymbols = 0;
        List<string> tabl = new List<string>(); // все остальное
        List<char> tablID = new List<char>(); // заглавные буквы
        List<string> tablVariable = new List<string>(); // переменные
        List<string> tablLiterals = new List<string>(); // литералы
        public bool flagMaxLenght = false;

        public void showTablLexem(DataGridView DGW) // Функция выводит на экран таблицу 
        {
            for (int i = 0; i < tabl.Count; i++)
            {
                switch (tablID[i])
                {
                    case 'D': DGW.Rows.Add(new[] { tabl[i], "Разделитель" }); break;
                    case 'I': DGW.Rows.Add(new[] { tabl[i], "Идентификатор" }); break;
                    case 'L': DGW.Rows.Add(new[] { tabl[i], "Литерал" }); break;
                    case 'E': DGW.Rows.Add(new[] { tabl[i], "ОШИБКА: длина идентификатора превышает максимально допустимое згначение" }); break;
                }
            }
        }

        public void lexicalAnalys(string text) // 1 таблица
        {
            bool flagI = false;
            bool flagD = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (checlSpace(text[i]) == false)
                {
                    if (check_Letter(text[i], flagD) == false)
                    {
                        if (check_Number(text[i]) == false)
                        {
                            if (check_Delimiter(text[i].ToString(), flagI, flagD) == false) { }
                            else
                            { // ЭТО РАЗДЕЛИТЕЛЬ
                                if (text[i] == '+' && text[i + 1] == '+') // и не равен концу строки
                                {
                                    tabl.Add("++");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                else if (text[i] == '-' && text[i + 1] == '-') // и не равен концу строки
                                {
                                    tabl.Add("--");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                else if (text[i] == '>' && text[i + 1] == '=')
                                {
                                    tabl.Add(">=");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                else if (text[i] == '<' && text[i + 1] == '=')
                                {
                                    tabl.Add("<=");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                else if (text[i] == '=' && text[i + 1] == '=')
                                {
                                    tabl.Add("==");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                else if (text[i] == '!' && text[i + 1] == '=')
                                {
                                    tabl.Add("!=");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                else if (text[i] == '|' && text[i + 1] == '|')
                                {
                                    tabl.Add("||");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                else if (text[i] == '&' && text[i + 1] == '&')
                                {
                                    tabl.Add("&&");
                                    tablID.Add('D');
                                    i++;
                                    buff = String.Empty;
                                }
                                flagD = false;
                                flagI = false;
                            }
                        }
                        else
                        { // ЭТО ЧИСЛО
                            if (flagI)
                            {
                                countSymbols++;
                            }
                            else
                            {
                                flagD = true;
                            } // Если до числа была буква, то все последующие символы до разделителя
                        }                                                         //или до пробела будут частью одного идентификатора
                    }
                    else
                    { // ЭТО БУКВА

                        flagI = true; // пока флаг не будет опущен последующие символы будут распозноваться как часть одного идентификатора
                        countSymbols++;
                        if (flagD) // предыдущий символ - число
                        {
                            saveBuff('L');
                            flagD = false;
                            buff += text[i]; // буфер пустой, записываем в него текущий символ
                        }
                    }
                }
                else
                { // ЭТО ПРОБЕЛ

                    if (flagI)
                    {
                        if (countSymbols > maxLenght)
                        {
                            saveBuff('E');
                            flagMaxLenght = true;
                            showMessage("Слишком длинный идентификатор, допустимое значение - 8 символов.");
                            return;
                        }
                        else
                        {
                            saveBuff('I');
                        }
                        flagI = false;
                    }
                    if (flagD)
                    {
                        saveBuff('L');
                        flagD = false;
                    }
                }
            }
            if (flagD)
            {
                saveBuff('L');
            } // Сохраняем литералы в таблицу, после окончания всех символов
            if (flagI)
            {
                if (countSymbols > maxLenght)
                {
                    saveBuff('E');
                    flagMaxLenght = true;
                    showMessage("Слишком длинный идентификатор, допустимое значение - 8 символов.");
                    return;
                }
                else
                {
                    saveBuff('I');
                }
            }
        }

        public void saveBuff(char id) // Функция сохраняет значение из буфера в таблицу
        {
            if (buff != "" && buff != null)
            {
                tablID.Add(id);
                tabl.Add(buff);
                buff = "";
                countSymbols = 0;
            }
        }

        public bool checlSpace(char symbol) // Проверка, является ли символ пробелом
        {
            if (doubleD)
            {
                saveBuff('D');
                doubleD = false;
            }
            if (symbol == ' ')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool check_Letter(char symbol, bool flag) // Проверка, является ли символ буквой, флаг говорит функции, был ли предыдущий символ числом
        {
            if (doubleD)
            {
                saveBuff('L');
                doubleD = false;
            }
            if ((int)symbol > 96 && (int)symbol < 123)
            {
                if (!flag)
                {
                    buff += symbol;
                } // если после числа буква, то букву не записываем в буфер
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool check_Number(char symbol) // Проверка, является ли символ числом
        {
            if (doubleD)
            {
                saveBuff('L');
                doubleD = false;
            }
            if ((int)symbol > 47 && (int)symbol < 58)
            {
                buff += symbol;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool check_Delimiter(string symbol, bool flagL, bool flagN) // Проверка, является ли символ разделителем, флаги нужны для сохранения значеня в буфере
        {
            bool flag = false;
            for (int i = 0; i < massDelimiter.Length; i++)
            {
                if (symbol == massDelimiter[i])
                {
                    if (flagL)
                    {
                        if (countSymbols > maxLenght)
                        {
                            saveBuff('E');
                            flagMaxLenght = true;
                            showMessage("Слишком длинный идентификатор, допустимое значение - 8 символов.");
                        }
                        else
                        {
                            saveBuff('I');
                        }
                    } // сохранение предыдущих лексем

                    if (flagN)
                    {
                        saveBuff('L');
                    }
                    ////////////////////////////////////////////////////////
                    if (symbol != "+" && symbol != "-" && symbol != ">" && symbol != "<" && symbol != "=" && symbol != "!" && symbol != "|" && symbol != "&")
                    {
                        tabl.Add(symbol);
                        tablID.Add('D');
                    }
                    else
                    {
                        buff = symbol; doubleD = true;
                    }
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        #endregion

        #region классификация лексем
        int[,] tablClassification;
        public void classificationLexemes() 
        {
            tablClassification = new int[tabl.Count, 2];
            for (int i = 0; i < tabl.Count; i++)
            {
                switch (tablID[i])
                {
                    case 'D':
                        for (int j = 0; j < massDelimiter.Length; j++)
                        {
                            if (tabl[i] == massDelimiter[j])
                            {
                                tablClassification[i, 0] = 2; tablClassification[i, 1] = j + 1;
                            }
                        }
                        break;
                    case 'I':
                        bool flagSpecWords = false;
                        bool flagVariable = false;
                        for (int j = 0; j < massSpecialWords.Length; j++)
                        {
                            if (tabl[i] == massSpecialWords[j])
                            {
                                flagSpecWords = true;
                                tablClassification[i, 0] = 1; tablClassification[i, 1] = j + 1;
                            }
                        }
                        if (!flagSpecWords)
                        {
                            for (int j = 0; j < tablVariable.Count; j++)
                            {
                                if (tabl[i] == tablVariable[j])
                                {
                                    flagVariable = true;
                                    tablClassification[i, 0] = 3; tablClassification[i, 1] = j + 1;
                                }
                            }
                            if (!flagVariable)
                            {
                                tablVariable.Add(tabl[i]);
                                tablClassification[i, 0] = 3; tablClassification[i, 1] = tablVariable.Count;
                            }
                        }
                        break;
                    case 'L':
                        bool flagLiterals = false;
                        for (int j = 0; j < tablLiterals.Count; j++)
                        {
                            if (tabl[i] == tablLiterals[j])
                            {
                                flagLiterals = true;
                                tablClassification[i, 0] = 4; tablClassification[i, 1] = j + 1;
                            }
                        }
                        if (!flagLiterals)
                        {
                            tablLiterals.Add((tabl[i]));
                            tablClassification[i, 0] = 4; tablClassification[i, 1] = tablLiterals.Count;
                        }
                        break;
                }
            }
        }

        public void showAllTables(DataGridView DGW2, DataGridView DGW3, DataGridView DGW4, DataGridView DGW5, DataGridView DGW6)
        {
            for (int i = 0; i < tabl.Count; i++)
            {
                DGW2.Rows.Add(tablClassification[i, 0] + ", " + tablClassification[i, 1]);
            }
            for (int i = 0; i < tablVariable.Count; i++)
            {
                DGW3.Rows.Add(tablVariable[i]);
            }
            for (int i = 0; i < tablLiterals.Count; i++)
            {
                DGW4.Rows.Add(tablLiterals[i]);
            }
            for (int i = 0; i < massDelimiter.Length; i++)
            {
                DGW5.Rows.Add(massDelimiter[i]);
            }
            for (int i = 0; i < massSpecialWords.Length; i++)
            {
                DGW6.Rows.Add(massSpecialWords[i]);
            }
        } // вывод таблиц 
        #endregion

        #region Синтаксический анализ
        Stack<string> stack = new Stack<string>(); 
        int count = 0;
      
        void Offset()
        {
            if (count < tabl.Count) { stack.Push(tabl[count]); count++; }
        }

        void Turn(int x, string pravilo)
        {
            if (stack.Count == 0) { MessageBox.Show("Стек пуст", "Ошибка"); }
            for (int i = 0; i != x; i++)
            {
                stack.Pop();
            }
            stack.Push(pravilo);
            return;
        }

        public void syntaxisAnalys(int state)
        {
            switch (state) // номер состояния с которого начинается разбор
            {
                case 0:
                    if (stack.Count == 0) { Offset(); }
                    if (stack.Peek() == "int") { syntaxisAnalys(2); } else { showMessage("int", stack.Peek()); return; }
                    if (stack.Peek() == "<prog>") { syntaxisAnalys(1); }
                    if (stack.Peek() == "<S>") { showMessage("Разбор успешно завершен!"); return; }
                    break;
                case 1:
                    Turn(1, "<S>");
                    break;
                case 2:
                    Offset();
                    if (stack.Peek() == "main") { syntaxisAnalys(3); } else { showMessage("main", stack.Peek()); return; }
                    break;
                case 3:
                    Offset();
                    if (stack.Peek() == "(") { syntaxisAnalys(4); } else { showMessage("(", stack.Peek()); return; }
                    break;
                case 4:
                    Offset();
                    if (stack.Peek() == ")") { syntaxisAnalys(5); } else { showMessage(")", stack.Peek()); return; }
                    break;
                case 5:
                    Offset();
                    if (stack.Peek() == "{") { syntaxisAnalys(6); } else { showMessage("{", stack.Peek()); return; }
                    if (stack.Peek() == "<ListOfDescriptions>") { syntaxisAnalys(14); }
                    if (stack.Peek() == "<ListOfOperators>") { syntaxisAnalys(26); }
                    break;
                case 6:
                    if (tabl[count] == "}") { MessageBox.Show("Разбор завершён"); break; }
                    if (tabl[count] == "int" || tabl[count] == "short" || tabl[count] == "long" || tabl[count] == "char" || tabl[count] == "double" || tabl[count] == "string") { syntaxisAnalys(7); } else { showMessage("Тип переменной", tabl[count]); }
                    if (stack.Peek() == "<Type>") { syntaxisAnalys(8); }
                    if (stack.Peek() == "<Description>") { syntaxisAnalys(13); }
                    break;
                case 7:
                    Offset();
                    Turn(1, "<Type>");
                    break;
                case 8:
                    Offset();
                    if (tablClassification[count - 1, 0] == 3) { syntaxisAnalys(9); } else { showMessage("id", stack.Peek()); }
                    if (stack.Peek() == "<ListOfVariables>") { syntaxisAnalys(12); }
                    break;
                case 9:
                    Offset();
                    if (stack.Peek() == ",") { syntaxisAnalys(11); }
                    else if (stack.Peek() == ";") { syntaxisAnalys(10);  } 
                    else { showMessage(", или ;", stack.Peek()); stack.Push("Error"); return; }
                    break;
                case 10:
                    Turn(2, "<ListOfVariables>");
                    break;
                case 11:  
                    Offset();
                    if (tablClassification[count - 1, 0] == 3) { Turn(3, "<ListOfVariables>"); syntaxisAnalys(9); } else { showMessage("id", stack.Peek()); }
                    break;
                case 12:
                    Turn(2, "<Description>");
                    break;
                case 13:
                    if(count > tabl.Count - 1) { showMessage("}", stack.Peek()); stack.Push("Error"); return; }
                    if (tabl[count] == "}") { if (stack.Peek() == "<Description>") ; Turn(1, "<ListOfDescriptions>"); break; }
                    if (tabl[count] == "int" || tabl[count] == "short" || tabl[count] == "long" || tabl[count] == "char" || tabl[count] == "double" || tabl[count] == "string") { syntaxisAnalys(6); if (stack.Peek() == "<Description>" || stack.Peek() == "<ListOfDescriptions>") { Turn(2, "<ListOfDescriptions>"); } }
                    else
                    {
                        if (stack.Peek() == "<Description>" || stack.Peek() == "<ListOfDescriptions>")
                        {
                            Turn(1, "<ListOfDescriptions>");
                            syntaxisAnalys(14);
                        }
                    }
                    break;
                case 14:
                    Offset();
                    if (stack.Peek() == "if") { syntaxisAnalys(15); }
                    else if(tablClassification[count - 1, 0] == 3) { syntaxisAnalys(28); }
                    else { if (stack.Peek() == "}") { stack.Pop(); stack.Push("<ListOfOperators>"); count--; } return; }
                    if(stack.Peek() == "<IF>") { syntaxisAnalys(34); }
                    else if(stack.Peek() == "<Assigment>") { syntaxisAnalys(34); }
                    else { return; }
                    if (stack.Peek() == "<Operator>") { syntaxisAnalys(25); }
                    break;
                case 15:
                    if(tabl[count] == "(") { syntaxisAnalys(16); } else { showMessage("(", tabl[count]); stack.Push("Error"); return; }
                    break;
                case 16:
                    if (logic1())
                    {
                        while (stack.Peek() != "if") { stack.Pop(); }
                        stack.Push("(");
                        stack.Push("<Logic>");
                        count--;
                        syntaxisAnalys(17);
                    }
                    else { return; }
                    break;
                case 17:
                    Offset();
                    if (stack.Peek() == ")") { syntaxisAnalys(18); } else { showMessage(")", stack.Peek()); stack.Push("Error"); return; }
                    if (stack.Peek() == "<BoodyIF>" || stack.Peek() == "<Assigment>") { syntaxisAnalys(20); }
                    if(stack.Peek() == "<ELSE>") { syntaxisAnalys(22); } else { if (stack.Peek() == "<BoodyIF>") { syntaxisAnalys(23); } }
                    break;
                case 18:
                    Offset();
                    if (stack.Peek() == "{")
                    {
                        syntaxisAnalys(14);
                        if (stack.Peek() == "<ListOfOperators>" || stack.Peek() == "<Operator>") { Offset(); } else { return; }
                        if (stack.Peek() == "}") { syntaxisAnalys(19); } else { showMessage("}", stack.Peek()); stack.Push("Error"); return; }
                    }
                    else
                    {
                        if (tablClassification[count - 1, 0] == 3) { syntaxisAnalys(28); }
                        else { showMessage("id или {", stack.Peek()); stack.Push("Error"); return; }
                    }
                    break;
                case 19:
                    Turn(3, "<BoodyIF>");
                    break;
                case 20:
                    if (count < tabl.Count)
                    {
                        if (tabl[count] == "else")
                        {
                            Offset();
                            syntaxisAnalys(18);
                            if (stack.Peek() == "<BoodyIF>") { syntaxisAnalys(21); }
                            break;
                        }
                    }
                    if (stack.Peek() == "<Assigment>" || stack.Peek() == "<BoodyIF>") { syntaxisAnalys(23); }

                    break;
                case 21:
                    Turn(2, "<ELSE>");
                    break;
                case 22:
                    Turn(6, "<IF>");
                    break;
                case 23:
                    Turn(5, "<IF>");
                    break;
                case 24:
                    break;
                case 25:
                    if (count == tabl.Count - 1) { if (tabl[count] != "}") { showMessage("}", stack.Peek()); stack.Push("Error"); return; } if (stack.Peek() == "<Operator>") { Turn(1, "<ListOfOperators>"); } break; }
                    if (count >= tabl.Count) { showMessage("}", stack.Peek()); stack.Push("Error"); return; }
                    if (tabl[count] == "if" || tablClassification[count, 0] == 3) { syntaxisAnalys(14); if (stack.Peek() == "<Operator>" || stack.Peek() == "<ListOfOperators>") { Turn(2, "<ListOfOperators>"); } }
                    break;
                case 26:
                    Offset();
                    if(stack.Peek() == "}") { syntaxisAnalys(27); } else { showMessage("}", stack.Peek()); stack.Push("Error"); return; }
                    break;
                case 27:
                    if(count < tabl.Count) { Offset(); showMessage("Сиволы после окончания функции : " + stack.Peek()); stack.Push("Error"); return; }
                    Turn(8, "<prog>");
                    break;
                case 28:
                    Offset();
                    if (stack.Peek() == "=") { syntaxisAnalys(29); } else { showMessage("=", stack.Peek()); stack.Push("Error"); return; }
                    break;
                case 29:
                    Offset();
                    if (tablClassification[count - 1, 0] == 3 || tablClassification[count - 1, 0] == 4) { syntaxisAnalys(30); } else { showMessage("id или литерал", stack.Peek()); stack.Push("Error"); return; }
                    if (stack.Peek() == "<Right>") { syntaxisAnalys(33); }
                    break;
                case 30:
                    Offset();
                    if (stack.Peek() == ";") { syntaxisAnalys(31); }
                    else if (stack.Peek() == "+" || stack.Peek() == "-" || stack.Peek() == "*" || stack.Peek() == "/") { syntaxisAnalys(32); }
                    else { showMessage("; или (+ - / *)", stack.Peek()); stack.Push("Error"); return; }
                    break;
                case 31:
                    Turn(2, "<Right>");
                    break;
                case 32:
                    Offset();
                    if (tablClassification[count - 1, 0] == 3 || tablClassification[count - 1, 0] == 4) { Turn(3, "<Operand>"); syntaxisAnalys(30); } else { showMessage("id или литерал", stack.Peek()); stack.Push("Error"); return; }
                    break;
                case 33:
                    Turn(3, "<Assigment>");
                    break;
                case 34:
                    Turn(1, "<Operator>");
                    break;
            }
        }
        void showError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void showMessage(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void showMessage(string mes1, string mes2)
        {
            MessageBox.Show("Ожидалось " + mes1 + " а не " + mes2, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        bool logic1()
        {
            Stack<string> st = new Stack<string>(); // стек для операций и скобок
            List<string> outputString = new List<string>(); // выходная строка
            List<char> outputStringID = new List<char>();
            bool flagI = false; // Если флаг поднят, то следующий символ должен быть операнд
            bool flagR = false; // Если флаг поднят, то следующий символ должен быть операцией
            int countAriphmeticParts = 1; // cчетчик частей выражения
            while (true)
            {
                Offset();
                if (stack.Peek() == "(") { if (flagR) { showMessage("логическая операция", stack.Peek()); return false; } flagI = true; st.Push(stack.Peek()); Offset(); countAriphmeticParts++; } // если встречаем '(', то добавляем его в стек и поднимаем флаг идентификатора
                if (!flagR) 
                {
                    if (tablClassification[count - 1, 0] == 3 || tablClassification[count - 1, 0] == 4) { outputString.Add(stack.Peek()); outputStringID.Add('I'); Offset(); countAriphmeticParts++; flagI = false; } // если операнд, то добавляем его к вых строке
                    else { if (stack.Peek() != "(" && stack.Peek() != "") { showMessage("операнд", stack.Peek()); return false; } } // иначе ошибка, за исключением символа '('
                }

                if (tablClassification[count - 1, 0] != 2) { showMessage("логическая операция", stack.Peek()); return false; }
                else { flagR = false; }
                if (stack.Peek() == "(" || stack.Peek() == "^") { if (flagR) { showMessage("логическая операция", stack.Peek()); return false; } flagI = true; st.Push(stack.Peek()); countAriphmeticParts++; }
                else if (stack.Peek() == "||" || stack.Peek() == "|")
                {
                    if (flagI) { showMessage("операнд", stack.Peek()); return false; } // если флаг I поднят то ошибка
                    if (st.Count == 0) { st.Push(stack.Peek()); countAriphmeticParts++; flagI = true; } // стек пуст то добавляем символ
                    else
                    {
                        while (st.Count != 0 && st.Peek() != "(") // выталкиваем символы из стека и записываем их в вых строку
                        { // пока не встретим символ с приоритетом меньше чем у входного символа или пока стек не опустел
                            outputString.Add(st.Pop()); outputStringID.Add('R');
                        }
                        st.Push(stack.Peek()); countAriphmeticParts++; flagI = true; // затем добавляем входной символ в стек
                    }
                }
                else if (stack.Peek() == "&&" || stack.Peek() == "&")
                {
                    if (flagI) { showMessage("операнд", stack.Peek()); return false; }
                    if (st.Count == 0) { st.Push(stack.Peek()); countAriphmeticParts++; }
                    else
                    {
                        while (st.Count != 0 && (st.Peek() != "||" && st.Peek() != "|" && st.Peek() != "("))
                        {
                            outputString.Add(st.Pop()); outputStringID.Add('R');
                        }
                        st.Push(stack.Peek()); countAriphmeticParts++; flagI = true;
                    }
                }
                else if (stack.Peek() == "!")
                {
                    if (st.Count == 0) { st.Push(stack.Peek()); countAriphmeticParts++; }
                    else
                    {
                        while (st.Count != 0 && (st.Peek() != "||" && st.Peek() != "|" && st.Peek() != "&&" && st.Peek() != "&" && st.Peek() != "("))
                        {
                            outputString.Add(st.Pop()); outputStringID.Add('R');
                        }
                        st.Push(stack.Peek()); countAriphmeticParts++; flagI = true;
                    }
                }
                else if (stack.Peek() == "!=" || stack.Peek() == ">=" || stack.Peek() == "<=" || stack.Peek() == "==" || stack.Peek() == "<" || stack.Peek() == ">")
                {    
                    if (st.Count == 0) { st.Push(stack.Peek()); countAriphmeticParts++; }
                    else
                    {
                        while (st.Count != 0 &&(st.Peek() == "^" || st.Peek() == "!=" || st.Peek() == ">=" || st.Peek() == "<=" || st.Peek() == "==" || st.Peek() == "<" || st.Peek() == ">"))
                        {
                            outputString.Add(st.Pop()); outputStringID.Add('R');
                        }
                        st.Push(stack.Peek()); countAriphmeticParts++; flagI = true;
                    }
                }
                else if (stack.Peek() == ")") // если встречаем символ ')' то выталкиваем из стека символы пока не встретится символ '('
                {
                    if (flagI) { showMessage("операнд", stack.Peek()); return false; }
                    while (st.Count != 0 && st.Peek() != "(")
                    {
                        outputString.Add(st.Pop()); outputStringID.Add('R');
                    }
                    if (st.Count == 0) { showMessage("Скобки не сбалансированы"); return false; }
                    if(count + 1 >= tabl.Count) { showMessage("Отсутствует тело цикла"); return false; }
                    st.Pop(); flagR = true; // символ ')' не добавляется в стек и символ '(' удаляется из стека
                    if(tabl[count] == "{" || tabl[count] == "else" || tabl[count + 1] == "=") { break; }
                }
                else { showMessage("операция или )", stack.Peek()); return false; }
            }
            if (flagI) { showMessage("операнд", stack.Peek()); return false; }
            while (st.Count != 0) { outputString.Add(st.Pop()); outputStringID.Add('R'); } // выталкиваем оставшиеся символы из стека
            bool flagBalance = false;
            for (int i = 0; i < outputString.Count; i++)
            {
                if (outputString[i] == "(" || outputString[i] == ")") { flagBalance = true; } // если в вых строке есть символы '(' или ')' то скобки не сбалансированы
            }
            if (flagBalance) { showMessage("Скобки не сбалансированы"); return false; }
            if (countAriphmeticParts > 2) { translateIntoMatrix(outputString, outputStringID); }

            return true;
        }

        public List<string> matrix { get; } = new List<string>();
        void translateIntoMatrix(List<string> opn, List<char> id) // функция преобразовывает ОПН в матрицу
        {
            int countSost = 1; //  счетчик состояний
            Stack<string> st = new Stack<string>(); // стек для операндов
            for (int i = 0; i < opn.Count; i++) // перебераем элементы ОПН
            {
                if (id[i] == 'I') // если встречаем операнд, то добавляем его в стек
                {
                    st.Push(opn[i]);
                }
                if (id[i] == 'R') // если встречаем операцию то выталкиваем из стека 2 операнда и образуем тройку операция операнды
                {
                    string buf = st.Pop();
                    matrix.Add("M" + countSost + ": " + opn[i] + " | " + st.Pop() + " | " + buf); st.Push("M" + countSost); countSost++;
                }
            }
        }
        #endregion
    }
}
