namespace PhoneBookApplication;
class Program
{
    
    static void Main(string[] args)
    {
        List<Contact> _contacts = new List<Contact>();
        AddFiveContacts(_contacts);
        int chosen;
        bool result;
        do
        {
            GetMenu();
            do
        {
            result = int.TryParse(Console.ReadLine(), out chosen);
            if(chosen == 1 || chosen == 2 || chosen == 3 || chosen == 4 || chosen == 5 || chosen == 6)
                result = true;
            else
                result = false;
                
            if (result == false)
                Console.Write("Please make a valid selection : ");            
        } while (result == false);

            switch (chosen)
            {
                case 1:
                    AddContact(_contacts);
                    break;
                case 2:
                    DeleteContact(_contacts,chosen,result);
                    break;
                case 3:
                    UpdateContact(_contacts,chosen,result);
                    break;
                case 4:
                    ListPhoneBook(_contacts,chosen,result);
                    break;
                case 5:
                    SearchPhoneBook(_contacts, out chosen, out result);
                    break;
                default:
                    break;
            }
        } while (chosen != 6);


    }

    private static void SearchPhoneBook(List<Contact> _contacts, out int chosen, out bool result)
    {
        Console.WriteLine("What would you like to search by?");
        Console.WriteLine("**********************************************");
        Console.WriteLine("");
        Console.WriteLine("To search by first or last name: (1)");
        Console.WriteLine("To search by phone number:       (2)");
        Chosen(out chosen, out result);

        if (chosen == 1)
        {
            Console.Write("Enter the name or surname you want to search: ");
            string searchNameOrSurname = Console.ReadLine();
            List<Contact> search = _contacts.Where(x => x.Name == searchNameOrSurname || x.Surname == searchNameOrSurname).ToList();
            Ordered(search, "Search Result");
        }
        else
        {
            Console.Write("Enter the phone number you want to search: ");
            string searchNumber = Console.ReadLine();
            List<Contact> search = _contacts.Where(x => x.Number == searchNumber).ToList();
            Ordered(search, "Search Result");
        }
    }

    private static void Chosen(out int chosen, out bool result)
    {
        do
        {
            result = int.TryParse(Console.ReadLine(), out chosen);
            if(chosen == 1 || chosen == 2)
                result = true;
            else
                result = false;
                
            if (result == false)
                Console.Write("Please make a valid selection : ");            
        } while (result == false);
    }

    private static void ListPhoneBook(List<Contact> _contacts, int chosen, bool result )
    {
        Console.WriteLine("What would you like to list by?");
        Console.WriteLine("**********************************************");
        Console.WriteLine("");
        Console.WriteLine("A to Z: (1)");
        Console.WriteLine("Z to A: (2)");

        Chosen(out chosen, out result);
        if (chosen == 1)
        {
            List<Contact> order = _contacts.OrderBy(x => x.Name).ToList();
            Ordered(order, "Phone Book");
        }
        else if(chosen == 2)
        {
            List<Contact> order = _contacts.OrderByDescending(x => x.Name).ToList();
            Ordered(order, "Phone Book");
        };
    }

    private static void Ordered(List<Contact> order, string head)
    {
        Console.WriteLine("");
        Console.WriteLine(head);
        Console.WriteLine("**********************************************");
        foreach (var contact in order)
        {
            Console.WriteLine("Name: " + contact.Name);
            Console.WriteLine("Surame: " + contact.Surname);
            Console.WriteLine("Number: " + contact.Number);
            Console.WriteLine("-");
        }
        if(order.Count == 0)
            Console.WriteLine("No contact found.");
        ReturnMenu();
    }

    private static void UpdateContact(List<Contact> _contacts, int chosen, bool result)
    {
        bool control = false;
        bool found = false;
        do
        {
            Console.Write("Please enter the first or last name of the contact you want to update : ");
            string nameOrSurname = Console.ReadLine();
            foreach (var contact in _contacts)
            {
                if (contact.Name == nameOrSurname || contact.Surname == nameOrSurname)
                {
                    found = true;
                    
                    Console.Write("Please enter the new number : ");
                    string newNumber = Console.ReadLine();
                    Console.WriteLine("");

                    Console.Write($"The contact named '{contact.Name}' will be updated, do you confirm? (y/n) : ");
                    string response = Console.ReadLine();

                    if (response == "y")
                    {
                        contact.Number = newNumber;
                        Console.WriteLine($"{contact.Name} {contact.Surname}'s number was updated.");
                        ReturnMenu();
                        control = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Contact named '{nameOrSurname}' was not updated.");
                        Console.WriteLine("");
                        control = false;
                        ReturnMenu();
                        break;
                    }
                }
            }
            if(found != true)
            {
                Console.WriteLine("");
                Console.WriteLine($"Contact named/surnamed '{nameOrSurname}' was not found in the phone book. Please make a selection.");
                Console.WriteLine("*To end the update   : (1)");
                Console.WriteLine("*To search again     : (2)");
                Chosen(out chosen, out result);
                
                if (chosen == 2)
                {
                    control = true;
                }
                else
                {
                    GetMenu();
                    break;
                }                        
            }
            
        } while (control == true);
    }

    private static void DeleteContact(List<Contact> _contacts, int chosen, bool result)
    {
        bool control = false;
        bool found = false;
        do
        {
            Console.Write("Please enter the first or last name of the contact you want to delete : ");
            string nameOrSurname = Console.ReadLine();
            foreach (var contact in _contacts)
            {
                if (contact.Name == nameOrSurname || contact.Surname == nameOrSurname)
                {
                    found = true;
                    Console.WriteLine("");
                    Console.Write($"The contact named '{contact.Name}' is about to be deleted from the phone book, do you confirm? (y/n) : ");
                    string response = Console.ReadLine();
                    if (response == "y")
                    {
                        _contacts.Remove(contact);
                        Console.WriteLine($"Contact named '{nameOrSurname}' was deleted.");
                        ReturnMenu();
                        control = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Contact named '{nameOrSurname}' was not deleted.");
                        Console.WriteLine("");
                        control = false;
                        ReturnMenu();
                        break;
                    }
                }                    
            }
            if(found != true)
            {
                Console.WriteLine("");
                Console.WriteLine($"Contact named/surnamed '{nameOrSurname}' was not found in the phone book. Please make a selection.");
                Console.WriteLine("*To end the deletion : (1)");
                Console.WriteLine("*To search again     : (2)");

                Chosen(out chosen, out result);
                if (chosen == 2)
                {
                    control = true;
                }
                else
                {
                    GetMenu();
                    break;
                }                    
            }
            
        } while (control == true);
    }

    private static void AddContact(List<Contact> _contacts)
    {
        Contact contact = new Contact();
        Console.Write("Please enter the name : ");
        string name = Console.ReadLine();
        Console.Write("Please enter the surname : ");
        string surname = Console.ReadLine();
        Console.Write("Please enter the number : ");
        string number = Console.ReadLine();
        contact.Name = name;
        contact.Surname = surname;
        contact.Number = number;
        _contacts.Add(contact);
        Console.WriteLine("New contact added.");
        ReturnMenu();
    }

    private static void ReturnMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Please press any key to return to the main menu!");
        Console.ReadKey();
    }

    private static void AddFiveContacts(List<Contact> contacts)
    {
        contacts.Add(new Contact(){Name = "Arda",Surname = "Güler", Number = "0535 010 10 10"});
        contacts.Add(new Contact(){Name = "Enner",Surname = "Valencia", Number = "0535 013 13 13"});
        contacts.Add(new Contact(){Name = "Altay",Surname = "Bayindir", Number = "0535 001 01 01"});
        contacts.Add(new Contact(){Name = "Irfan Can",Surname = "Kahveci", Number = "0535 017 17 17"});
        contacts.Add(new Contact(){Name = "Ferdi",Surname = "Kadioğlu", Number = "0535 007 07 07"});
    }

    private static void GetMenu()
    {
        Console.Clear();
        Console.WriteLine("Please select an action.");
        Console.WriteLine("************************");

        Console.WriteLine("(1) Add new number");
        Console.WriteLine("(2) Delete a number");
        Console.WriteLine("(3) Update a number");
        Console.WriteLine("(4) List the phone book");
        Console.WriteLine("(5) Search in the phone book");
        Console.WriteLine("(6) Exit");
    }
}
