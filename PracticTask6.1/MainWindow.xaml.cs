using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
  
namespace PracticTask6._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    [SoapInclude(typeof(Admin))]
    [SoapInclude(typeof(Managers))]
    [SoapInclude(typeof(Intern))]
    public partial class MainWindow : Window
    {

        
        ObservableCollection<Department> depart = new ObservableCollection<Department>();
        public ObservableCollection<Employees> emp = new ObservableCollection<Employees>();

        
        int check = 1;
        public MainWindow()
        {
            InitializeComponent();
 
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee();
            srcs.ItemsSource = emp;//Заполнение DataGrid коллекцией emp

        }
        void fileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//Закрывает окно
        }
        private void TreeView_Initialized(object sender, EventArgs e)//Основной метод для дерева где он будет запущен сразу после запуска программы
        {
            Random random = new Random();
            TreeViewItem bestHacker = new TreeViewItem();//Объявление переменных для создания веток дерева
            TreeViewItem dep1 = new TreeViewItem();//Объявление переменных для создания веток дерева
            TreeViewItem dep2 = new TreeViewItem();
            TreeViewItem dep3 = new TreeViewItem();
            int d1 = random.Next(1, 5);//Объявление рандома
            int d2 = random.Next(1, 5);
            int d3;
            int check = 4;// для цикла чтобы департаменты нумеровались

            TreeView1.Items.Add(bestHacker);
            bestHacker.Header = "ОАО лучшие кодеры";
            bestHacker.Items.Add(dep1);//Создание департамента под заголовком "ОАО лучшие кодеры"
            bestHacker.Items.Add(dep2);
            bestHacker.Items.Add(dep3);
            bestHacker.FontSize = 14;

            dep1.Header = "Департамент1";//Название заголовков
            for (int i = 0; i < d1; i++)
            {
                dep1.Items.Add("Департамент" + check);//Рандомное наполнение под департаментом
                check++;
            }

            dep2.Header = "Департамент2";
            for (int i = 0; i < d2; i++)
            {
                dep2.Items.Add("Департамент" + check);
                check++;
            }

            dep3.Header = "Департамент3";
            d3 = 9 - (d1 + d2);
            for (int i = 0; i < d3; i++)
            {
                dep3.Items.Add("Департамент" + check);
                check++;
            }
        }
    ObservableCollection<Employees> value = new ObservableCollection<Employees>();
   public void CreateEmployee()// Метод для создания наполнения DataGrid
        { 
            Random random = new Random();
            
            for (int i = 0; i < 9; i++)
            {
                Department dep = new Department();
                for (int j = 0; j < 100; j++)
                {
                    int a = random.Next(1, 4);
                    if (a == 1)
                    {
                        dep.employees.Add(new Intern("Интерн" + random.Next(1, 100).ToString()));//Создаётся новый интерн с диапозоном от 1 до 100 в случайном порядке для коллекции employees
                        emp.Add(new Intern { Age = dep.employees[j].Age, ID = dep.employees[j].ID, Name = dep.employees[j].Name, Position = dep.employees[j].Position, Salary = dep.employees[j].Salary });//Создаётся новый интерн с диапозоном от 1 до 100 в случайном порядке для коллекции emp
                        value.Add(new Intern { Age = dep.employees[j].Age, ID = dep.employees[j].ID, Name = dep.employees[j].Name, Position = dep.employees[j].Position, Salary = dep.employees[j].Salary });//Создаётся новый интерн с диапозоном от 1 до 100 в случайном порядке для коллекции value
                    }
                    else if (a == 2)
                    {
                        dep.employees.Add(new Managers("Менеджер" + random.Next(1, 100).ToString()));
                        emp.Add(new Managers { Age = dep.employees[j].Age, ID = dep.employees[j].ID, Name = dep.employees[j].Name, Position = dep.employees[j].Position, Salary = dep.employees[j].Salary });
                        value.Add(new Managers { Age = dep.employees[j].Age, ID = dep.employees[j].ID, Name = dep.employees[j].Name, Position = dep.employees[j].Position, Salary = dep.employees[j].Salary });
                    }

                    else if (a == 3)
                    {
                        dep.employees.Add(new Admin("Начальник" + random.Next(1, 100).ToString()));
                        emp.Add(new Admin { Age = dep.employees[j].Age, ID = dep.employees[j].ID, Name = dep.employees[j].Name, Position = dep.employees[j].Position, Salary = dep.employees[j].Salary });

                        value.Add( new Admin { Age = dep.employees[j].Age, ID = dep.employees[j].ID, Name = dep.employees[j].Name, Position = dep.employees[j].Position, Salary = dep.employees[j].Salary });
                    }
                    check++;
                }
                depart.Add(dep);//Добавление переменной dep в которой всё наполнение для DataGrid в коллекцию depart
                
            }
                    XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Employees>));// Метод загрузки коллекции
                    FileStream ds = new FileStream("data5.xml", FileMode.Create);
                    formatter.Serialize(ds, value);
            ds.Close();
            //XmlSerializer formatter = new XmlSerializer(typeof());
            //using (FileStream fs = new FileStream("Data.xml", FileMode.Create))
            //{//[XmlInclude(typeof(Employes))]
               // formatter.Serialize(fs, emp);
            //}
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)//Метод выгрузки коллекции
        {
            ObservableCollection<Employees> value = new ObservableCollection<Employees>();
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Employees>));
            FileStream ds = new FileStream("data5.xml", FileMode.Open);
            ObservableCollection<Employees> Loadvalue = formatter.Deserialize(ds) as ObservableCollection<Employees>;
            srcs.ItemsSource = Loadvalue;
            ds.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CreateEmployee();
            srcs.ItemsSource = emp;
        }

        private void MouseDoubleClickdep(object sender, MouseButtonEventArgs e) // Не работающий метод, по задумке должен открывать ветление дерева по двойному клику
        {
            for (int i = 0; i < depart.Count; i++)
            {
                if (depart[i].Dep == TreeView1.SelectedItem.ToString())
                {
                    srcs.ItemsSource = depart[i].employees;
                }
            }
        }

        private void srcsc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MenuItem_Clicksave(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Clickload(object sender, RoutedEventArgs e)
        {
        }
        static void Savexml()
        {
         /*   ObservableCollection<Employees> list = new ObservableCollection<Employees>();
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Employees>));
            FileStream ds = new FileStream("data3.xml", FileMode.Create);
            formatter.Serialize(ds, list);*/
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            srcs.ItemsSource = null;
        }
    }
    [XmlInclude(typeof(Admin))]
    [XmlInclude(typeof(Managers))]
    [XmlInclude(typeof(Intern))]
    public abstract class Employees
    {
        [XmlElement("ID")]
        public float ID { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Age")]
        public int Age { get; set; }
        [XmlElement("Position")]
        public abstract string Position { get; set; }
        [XmlElement("Salary")]
        public abstract int Salary { get; set; }
        

        static Random Randomrnd = new Random();

        public static float count = 0;
        public Employees(string name, int age)
        {
            ID = ++count;
            count = count - 0.5f;
            Name = name;
            Age = age;
        }
        public Employees() : this("", 0) { }
        public Employees(string name) : this(name, (int)Randomrnd.Next(20, 60)) { }
    }
    public class Intern : Employees
    {
        static Random Randomrnd = new Random();
      public override string Position { get; set; }

      public override int Salary { get; set; }
        public Intern(string name) : base(name, (int)Randomrnd.Next(20, 60))
        {
            Salary = 500;
            Position = "Intern";
        }
        public Intern() : base("", 0) { }
    }
    public class Managers : Employees
    {
        static Random Randomrnd = new Random();
        public override string Position { get; set; }

        public override int Salary { get; set; }
        public Managers(string name) : base(name, (int)Randomrnd.Next(20, 60))
        {
            Salary = 2500;
            Position = "Manager";
        }
        public Managers() : base("", 0) { }
    }
    
    public class Admin : Employees
    {
        
        static Random Randomrnd = new Random();
        public override string Position { get; set; }

        public override int Salary { get; set; }
        public Admin(string name) : base(name, (int)Randomrnd.Next(20, 60))
        {
            Salary = 5000;
            Position = "Admin";
        }
        public Admin() : base("", 0) { }
    }
    public class Department
    {
        public string Dep { get; set; }
        public int ID { get; set; }
        public ObservableCollection<Employees> employees { get; set; }

        public static int count1 = 4;
        
        public Department(string dep)
        {
          Dep = dep;
          employees = new ObservableCollection<Employees>();
          ID = count1++;
        }
        public Department() : this($"Departament{count1}") { }
    }
}
