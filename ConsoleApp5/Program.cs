

using Autofac;

public class Shop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public int Masa { get; set; }

}
public interface IContext
{
    public void Add(float price, string name, int masa);
    public List<Shop> Print();
    public void SaveFile(string filepath);
    
}
public class Context : IContext
{
    private List<Shop> _shop = new List<Shop>();
    public void Add(float price, string name, int masa)
    {
        int id = 1;
       if(_shop.Count > 0)
        {
            id = _shop.Count;
        }
        Shop temp = new Shop();
        temp.Id = id;
        temp.Name = name;
        temp.Price = price;
        temp.Masa = masa;
        _shop.Add(temp);
    }

    public List<Shop> Print()
    {
        return this._shop;
    }

    public void SaveFile(string filepath)
    {
        if (filepath == null || filepath == "") 
        {
            filepath = "temp.txt";
        }
        
        using(StreamWriter sw = new StreamWriter(filepath))
        {
            foreach(var i in _shop)
            {
                sw.WriteLine(i.Id + ". " + i.Name + " - " + i.Price + " " +i.Masa);
            }
        }
    }
}
class Program
{
    private static Autofac.IContainer Container { get; set; }
    static void Main(string[] args)
    {
        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterType<Context>().As<IContext>();
        Container = builder.Build();


        IContext _shop = new Context();


        while(true)
        {
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Print");
            Console.WriteLine("3 - Save to file");
            Console.Write("Enter menu__ ");
            int x = Convert.ToInt32(Console.ReadLine());
            if(x == 0)
            {
                Console.WriteLine("Goodbaye");
                break;
            }
            else if(x == 1)
            {
                Shop temp = new Shop();
                Console.WriteLine("Enter Name instrument");
                temp.Name = Console.ReadLine().ToString();
                Console.WriteLine("Enter Price");
                temp.Price = float.Parse(Console.ReadLine());
                Console.WriteLine("Enter mass");
                temp.Masa = Int32.Parse(Console.ReadLine());
                _shop.Add(temp.Price, temp.Name, temp.Masa);
                Console.WriteLine("Data sucses!");
                Console.ReadKey();
                Console.Clear();


            }
            else if(x == 2)
            {
                var temp = _shop.Print();
                foreach(var item in temp)
                {
                    Console.WriteLine(item.Id + ". Name - "+ item.Name);
                    Console.WriteLine("Price - " + item.Price);
                    Console.WriteLine("Mass - " + item.Masa);
                }
            }
            else if(x == 3)
            {
                Console.WriteLine("Enter Path to file, if not path file click - 'Enter'");
                string str = Console.ReadLine();
                if(str == null || str == "")
                {
                    str = "temp.txt";
                }
                _shop.SaveFile(str);
            }
            else
            {
                Console.WriteLine("ERROR!");
                continue;
            }
        }
       
       
    }
}