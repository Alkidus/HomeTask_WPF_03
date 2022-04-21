using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int difficult = 0;//переменная для отображения уровня сложности
        public int fails = 0;//переменная для отображения количества ошибок
        public int speed = 0;//переменная для отображения скорости
        string ex = "";//для сравнения случайного текста с текстом игрока
        string pl = "";//для сравнения строки набранной игроком
        public int next = 0;//переменная-итератор для перебора элементов списка, а так же количества набранных символов
        DateTime start;//переменная для отсчета с начала нажатия кнопки Старт и вычисления скорости печати
        public MainWindow()
        {
            InitializeComponent();
            stopBtn.IsEnabled = false;//изначально нкопка стоп отключена
            playerText.IsEnabled = false;//изначально строчка набора теста отключена
        }
        List<char> abc = new List<char>//список для формирования случайного текста
        {
            '`', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=',
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\\',
            'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'',
            'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/'
        };
        List<char> ABC = new List<char>//для формирования случайного текста, если установлен флажок Case Sensitive
        {
            '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+',
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', '{', '}', '|',
            'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ':', '\"',
            'Z', 'X', 'C', 'V', 'B', 'N', 'M', '<', '>', '?'
        };
        void lowBTN()//функция присваивает кнопкам имена после отпускания кнопки Shift
        {
            oem3.Content = "`";
            d1.Content = "1";
            d2.Content = "2";
            d3.Content = "3";
            d4.Content = "4";
            d5.Content = "5";
            d6.Content = "6";
            d7.Content = "7";
            d8.Content = "8";
            d9.Content = "9";
            d0.Content = "0";
            oemminus.Content = "-";
            oemplus.Content = "=";
            q.Content = "q";
            w.Content = "w";
            ee.Content = "e";//у кнопки двойное "ее" дабы не пересекаться с обработчиком событий
            r.Content = "r";
            t.Content = "t";
            y.Content = "y";
            u.Content = "u";
            i.Content = "i";
            o.Content = "o";
            p.Content = "p";
            oemopenbrackets.Content = "[";
            oem6.Content = "]";
            oem5.Content = "\\";
            a.Content = "a";
            s.Content = "s";
            d.Content = "d";
            f.Content = "f";
            g.Content = "g";
            h.Content = "h";
            j.Content = "j";
            k.Content = "k";
            l.Content = "l";
            oem1.Content = ";";
            oemquotes.Content = "\'";
            z.Content = "z";
            x.Content = "x";
            c.Content = "c";
            v.Content = "v";
            b.Content = "b";
            n.Content = "n";
            m.Content = "m";
            oemcomma.Content = ",";
            oemperiod.Content = ".";
            oemquestion.Content = "/";
        }
        void highBTN()//функция присваивает кнопкам имена после нажатия кнопки Shift или CapsLock
        {
            oem3.Content = "~";
            d1.Content = "!";
            d2.Content = "@";
            d3.Content = "#";
            d4.Content = "$";
            d5.Content = "%";
            d6.Content = "^";
            d7.Content = "&";
            d8.Content = "*";
            d9.Content = "(";
            d0.Content = ")";
            oemminus.Content = "_";
            oemplus.Content = "+";
            q.Content = "Q";
            w.Content = "W";
            ee.Content = "E";
            r.Content = "R";
            t.Content = "T";
            y.Content = "Y";
            u.Content = "U";
            i.Content = "I";
            o.Content = "O";
            p.Content = "P";
            oemopenbrackets.Content = "{";
            oem6.Content = "}";
            oem5.Content = "|";
            a.Content = "A";
            s.Content = "S";
            d.Content = "D";
            f.Content = "F";
            g.Content = "G";
            h.Content = "H";
            j.Content = "J";
            k.Content = "K";
            l.Content = "L";
            oem1.Content = ":";
            oemquotes.Content = "\"";
            z.Content = "Z";
            x.Content = "X";
            c.Content = "C";
            v.Content = "V";
            b.Content = "B";
            n.Content = "N";
            m.Content = "M";
            oemcomma.Content = "<";
            oemperiod.Content = ">";
            oemquestion.Content = "?";
        }
        List<char> example = new List<char>();//список для формирования случайных символов 
        private void level_changer_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dif_Level_text.Content = (int)e.NewValue;
            difficult = (int)e.NewValue;//измененние ползунка слайдера генерирует уровень сложности
        }
        
        private void startBtn_Click(object sender, RoutedEventArgs e)//нажатие кнопки Старт
        {
            playerText.IsEnabled = true;//активируем поле набора
            randomText.Text = "";//очищаем поле раннее сгенерированных строк
            playerText.Text = "";//очищаем поле если оно было ранее заполнено игроком
            example.Clear();//очищаем список случайных символов
            startBtn.IsEnabled = false;//отключаем кнопку Старт
            stopBtn.IsEnabled = true;//включаем кнопку Стоп
            fails_numb_text.Content = "0";//очищаем ошибки
            speed_text.Content = "0";//очищаем скорость
            pl = "";//очищаем символ набранный игроком
            ex = "";//очищаем сивол из списка случайных символов example
            fails = 0;//ошибки в ноль
            next = 0;//перебор в ноль
            start = DateTime.Now;//присваиваем переменной время нажатия кнопки Старт для расчета скорости набора
            playerText.Focus();//помещаем курсор в поле набора текста игроком
            Random rand = new Random();//подключаем генератор случайных чисел
            if (difficult != 0)
            {
                List<char> random = new List<char>();//список состоящий из случайных сиволов в зависимости от уровня сложности, в него добавляем "пробел"
                random.Add(' ');
                for (int j = 0; j < difficult; j++)
                {
                    random.Add(abc.ElementAt(rand.Next(0, abc.Count)));//добавляем случайные символы нижнего регистра из списка abc
                    if(case_sensitive_check.IsChecked == true)
                        random.Add(ABC.ElementAt(rand.Next(0, abc.Count)));//добавляем случайные символы верхнего регистра из списка ABC
                }
                int i = 0;//итератор
                while (i < 90)//формируем список example (строку образец для набора) длинной в 90 символов
                {
                    example.Add(random.ElementAt(rand.Next(0, random.Count)));
                    i++;
                    if (i > 1 && example[i - 1].ToString() == example[i - 2].ToString() && example[i - 1].ToString() == " ")
                    {
                        example.RemoveAt(i - 1);//запрещаем генерировать два "пробела" подряд 
                        i--;
                    }
                }
                foreach (var el in example)
                {
                    randomText.Text += el.ToString();//заполняем строку єлементами из списка example
                }
            }

        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)//кнопка Стоп
        {
            startBtn.IsEnabled = true;
            stopBtn.IsEnabled = false;
            playerText.IsEnabled = false;
        }

        private void textChanged(object sender, TextChangedEventArgs e)//обработчик изменения текста набранного игроком
        {
            if (!string.IsNullOrEmpty(playerText.Text) && !string.IsNullOrEmpty(randomText.Text))
            {
                pl = playerText.Text.Substring(playerText.Text.Length - 1, 1);//присваиваем переменной последний набранный символ
                ex = example[next].ToString();//сравниваем с элементом из списка example
                if (pl != ex)//если они не равны добавляем ошибку и выводим ее
                {
                    fails++;
                    fails_numb_text.Content = fails;
                }
                next++;//увеличиваем итератор переходим к следующему элементу списка example
            }
            speed_text.Content = Math.Round(next / (DateTime.Now - start).TotalMinutes).ToString(); // выводим скорость набора
            playerText.Background = Brushes.Aquamarine;//цвет поля игрока

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)//нажатие кнопки на клавиатуре
        {
            foreach (Control btn in MainGrid.Children)//перебираем элементы в окне прграммы, ищем кнопки
            {
                if (btn.Name.ToLower() == e.Key.ToString().ToLower())
                {
                    btn.Effect = new DropShadowEffect();//если имена совпадают добавляем к кнопке эффект тени, ее нажатие
                }
                else if(e.Key.ToString().ToLower() == "e")
                {
                    ee.Effect = new DropShadowEffect();//отдельно обрабатываем кнопку "е"
                }
            }
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)//если нажат Шифт - меняем имена кнопок, переводим в верхний регистр
            {
                highBTN();
            }
            else if (e.Key == Key.CapsLock && (string)oem3.Content == "`")//проверка нажатия КапсЛока и состояния имен кнопок, если маленькие - переводим в большие и наоборот
            {
                highBTN();
            }
            else if (e.Key == Key.CapsLock && (string)oem3.Content == "~")
            {
                lowBTN();
            }
                playerText.Focus();//переводим фокусировку на поле игрока для набора текста
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)//отпускаем кнопку на клавиатуре
        {
            foreach (Control btn in MainGrid.Children)
            {
                if (btn.Name.ToLower() == e.Key.ToString().ToLower())
                {
                    btn.Effect = null; // убираем эффект тени у ранее нажатой кнопки
                }
                else if (e.Key.ToString().ToLower() == "e")
                {
                    ee.Effect = null;//снова отдельно кнопка "е"
                }
            }
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) //если отпускается Шифт - возвращаем имена кнопок в нижний регистр
            {
                lowBTN();
            }
            randomText.Focus();//отпуская кнопку фокусируемся на тексте примере
            randomText.Select(0, playerText.Text.Length);//для отслеживания количества пройденого текста на образце применяем выделение
            if (next == 90)//если добрались до конца списка
            {
                MessageBox.Show("Done!", "THE END");//сообщение об окончании списка
                startBtn.IsEnabled = true;
                stopBtn.IsEnabled = false;
                next = 0;
            }
        }

    }
}
