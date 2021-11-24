using System;
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
            if (hc.func_prog()) { hc.showMatrix(lbMatrix); }
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
        string[] massDelimiter = new string[] { ",", ";", "+", "-", "=", "*", "/", "<", ">", "{", "}", "(", ")", "++", ":", "--" };
        string[] massSpecialWords = new string[] { "char", "int", "short", "for" };
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
                                else if(text[i] == '>' && text[i + 1] == '=')
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
                    if (symbol != "+" && symbol != "-" && symbol != ">" && symbol != "<" && symbol != "=" && symbol != "!")
                    {
                        tabl.Add(symbol);
                        tablID.Add('D');
                    }

                    //if(symbol != ":" && symbol != "=")
                    //{
                    //    tabl.Add(symbol);
                    //    tablID.Add('D');
                    //}
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
        public void classificationLexemes() // 2 таблица
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
                DGW2.Rows.Add(tablClassification[i,0] + ", " + tablClassification[i, 1]);
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
        string tek;
        int count = 0;

        void next()
        {
            if (count < tabl.Count) { tek = tabl[count]; count++; }
            else { tek = ""; }
        }

        void showMessage(string message)
        {
            MessageBox.Show(message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void showMessage(string mes1, string mes2)
        {
            MessageBox.Show("Ожидалось " + mes1 + " а не " + mes2, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool func_prog()
        {
            next();
            if (tablClassification[count - 1, 0] == 3) { next(); } else { showMessage("Программа должнга начинаться с имени функции."); return false; }
            if (tek == "(") { next(); } else { showMessage("(", tek); return false; }
            if (tek == ")") { next(); } else { showMessage(")", tek); return false; }
            if (tek == "{") { next(); } else { showMessage("{", tek); return false; }
            if (!func_listOfDescriptions()) { return false; }
            if (!func_listOfOperators()) { return false; }// не закончил
            if (tek != "}") { showMessage("}", tek); return false; }
            MessageBox.Show("Успешно!", "СООБЩЕНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        #region Описание
        bool func_listOfDescriptions()
        {
            if (!func_description()) { return false; }
            if (tek == "int" || tek == "long" || tek == "short")
            {
                if (!func_listOfDescriptions()) { return false; }
            }
            return true;
        }

        bool func_description()
        {
            if (!func_type()) return false;
            if (!func_listOfVariables()) return false;
            return true;
        }

        bool func_type()
        {
            if(tek != "int" && tek != "long" && tek != "short") { showMessage("Необходимо указать тип переменной."); return false; }
            next(); return true;
        }

        bool func_listOfVariables()
        {
            if (tablClassification[count - 1, 0] == 3) { next(); } else { showMessage("идентификатор", tek); return false; }
            if (tek == ";") { next(); return true; }
            else if (tek == ",") { next(); if (!func_listOfVariables()) return false; }
            else { showMessage(";", tek); return false; }
            return true;
        }
        #endregion

        bool func_listOfOperators()
        {
            if (!func_operator()) { return false; }
            if (tek != ";" && tek != "}") { showMessage(";", tek); return false; }
            if (tek == ";") { next(); }
            if (tek == "}") { return true; }
            else { if (!func_listOfOperators()) { return false; } }
            return true;
        }

        bool func_operator()
        {
            if(tablClassification[count-1,0] == 3) { if (!func_assignment()) { return false; } }
            else if (!func_cycle()) { return false; }
            return true;
        }

        bool func_increment()
        {
            next();
            if(tablClassification[count-1,0] != 3) { showMessage("идентификатор", tek); }
            next();
            if(tek!="++" && tek != "--") { showMessage("инкрумент или декремент.", tek); return false; }
            next();
            return true;
        }

        bool func_assignment()
        {
            next(); if (tek != "=") { showMessage("=", tek); return false; }
            next(); if(tablClassification[count-1,0] != 3 && tablClassification[count - 1, 0] != 4) { showMessage("операнд",tek); return false; }
            if (!ariphmetic()) { return false; }
            return true;
        }

        bool ariphmetic()
        {
            Stack<string> st = new Stack<string>(); // стек для операций и скобок
            List<string> outputString = new List<string>(); // выходная строка
            List<char> outpunStringID = new List<char>(); // классификация элементов в вых строке
            bool flagI = false; // Если флаг поднят, то следующий символ должен быть операнд
            bool flagR = false; // Если флаг поднят, то следующий символ должен быть операцией
            int countAriphmeticParts = 1; // cчетчик частей выражения
            while (tek != ";") // разбираем выражение пока не встретим ;
            {
                if (tek == "(") { if (flagR) { showMessage("операция(+ - * /)", tek); return false; } flagI = true; st.Push(tek); next(); countAriphmeticParts++; } // если встречаем '(', то добавляем его в стек и поднимаем флаг идентификатора
                if (!flagR) // если символ должен быть R, то не проверяем его как I
                {
                    if (tablClassification[count-1,0] == 3 || tablClassification[count - 1, 0] == 4) { outputString.Add(tek); outpunStringID.Add('I'); next(); countAriphmeticParts++; flagI = false; } // если операгнд, то добавляем его к вых строке
                    else { if (tek != "(") { showMessage("операнд", tek); return false; } } // иначе ошибка, за исключением символа '('
                }

                if (tek != ";") // если ; то выход 
                {
                    if (tablClassification[count-1,0] != 2) { showMessage("операция(+ - * /)", tek); return false; }
                    else { flagR = false;}
                    if (tek == "(") { if (flagR) { showMessage("операция(+ - * /)", tek); return false; } flagI = true; st.Push(tek); next(); countAriphmeticParts++; }
                    else if (tek == "+" || tek == "-") // если + или - то
                    {
                        if (flagI) { showMessage("операнд", tek); return false; } // если флаг I поднят то ошибка
                        if (st.Count == 0) { st.Push(tek); next(); countAriphmeticParts++; flagI = true; } // стек пуст то добавляем символ
                        else // иначе
                        {
                            while (st.Count != 0 && st.Peek() != "(" && st.Peek() != ")") // выталкиваем символы из стека и записываем их в вых строку
                            { // пока не встретим символ с приоритетом меньше чем у входного символа или пока стек нге опустел
                                outputString.Add(st.Pop()); outpunStringID.Add('R');
                            }
                            st.Push(tek); next(); countAriphmeticParts++; flagI = true; // затем добавляем входнгой символ в стек
                        }
                    }
                    else if (tek == "*" || tek == "/") // если * или / то 
                    {
                        if (flagI) { showMessage("операнд", tek); return false; } // если флаг I поднят то выход
                        if (st.Count == 0) { st.Push(tek); next(); countAriphmeticParts++; } // если стек пуст то добавляем символ к вых строке
                        else if (st.Peek() == "*" || st.Peek() == "/") // иначе если на верхушке стека находится * или / то 
                        {
                            while (st.Count != 0 && (st.Peek() == "*" || st.Peek() == "/")) // Если на верхушке стека символ '*' или '/' то добавляем его к выходной строке
                            {
                                outputString.Add(st.Pop()); outpunStringID.Add('R');
                            }
                            st.Push(tek); next(); countAriphmeticParts++; flagI = true; // затем добавляем вх символ в стек
                        }
                        else { st.Push(tek); next(); countAriphmeticParts++; flagI = true; } // если на верхушке стека находится не * и не / то добавляем к стеку вх символ
                    }
                    else if (tek == ")") // если встречаем символ ')' то выталкиваем из стека символы пока не встретится символ '('
                    {
                        if (flagI) { showMessage("операнд", tek); return false; }
                        while (st.Count != 0 && st.Peek() != "(")
                        {
                            outputString.Add(st.Pop()); outpunStringID.Add('R');
                        }
                        if (st.Count == 0) { showMessage("Скобки не сбалансированы"); return false; }
                        st.Pop(); next(); flagR = true; // символ ')' не добавляется в стек и символ '(' удаляется из стека
                    }
                    else { showMessage("* или / или + или - или ) или ;", tek); return false; }
                }
            }
            if (flagI) { showMessage("операнд", tek); return false; }
            while (st.Count != 0) { outputString.Add(st.Pop()); outpunStringID.Add('R'); } // выталкиваем оставшиеся символы из стека
            bool flagBalance = false;
            for (int i = 0; i < outputString.Count; i++)
            {
                if (outputString[i] == "(" || outputString[i] == ")") { flagBalance = true; } // если в вых строке есть символы '(' или ')' то скобки не сбалансированы
            }
            if (flagBalance) { showMessage("Скобки не сбалансированы"); return false; }
            if (countAriphmeticParts > 2) { translateIntoMatrix(outputString, outpunStringID); }
            
            return true;
        }

        List<string> matrix = new List<string>();
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

        bool func_condition()
        {
            if(tablClassification[count-1,0] != 3) { showMessage("идегнтификатор.", tek); return false; }
            next();
            if (tek != ">" && tek != "<" && tek != ">=" && tek != "<=" && tek != "==" && tek != "!=") { showMessage("операция сравггнения", tek); return false; }
            next();
            if(tablClassification[count-1,0] != 3 && tablClassification[count - 1, 0] != 4) { showMessage("операнд.", tek); return false; }
            next();
            return true;
        }

        bool func_cycle()
        {
            next();
            if (tek == "(") { next(); } else { showMessage("(", tek); return false; }
            if (tablClassification[count - 1, 0] == 3) { if (!func_assignment()) { return false; } } else { showMessage("идентификатор.", tek); return false; }
            next();
            if (!func_condition()) { return false; }
            if (!func_increment()) { return false; }
            if (tek == ")") { next(); } else { showMessage(")", tek); return false; }
            if (tek == "{") { next(); } else { showMessage("{", tek); return false; }
            if (!func_listOfOperators()) { return false; }
            if (tek == "}") { next(); if (tek == "") { showMessage("}", tek); return false; } } else { showMessage("}", tek); return false; }
            return true;
        }

        #endregion
    }
}
