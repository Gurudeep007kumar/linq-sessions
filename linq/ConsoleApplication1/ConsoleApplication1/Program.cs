using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<Contact> Contacts { get; set; }
        }
        public class Contact
        {

        }
        //this function would be consumed by one of the where clauses
        // a very simple function that just takes in the person and checks to see if the person's age is valid
        public static bool IsValidPerson(Student s)
        {
          return  s.Age >0 ?  true : false;
        }
        static void Main(string[] args)
        {
            //Language Integrate Query (LINQ)
            //a query on the string array such that you have all the names that contain 'u'
            string[] names = new string[] { "Niranjan", "Guru", "Arun" };
            var result = (from n in names
                          where n.Contains("u")//Predicate
                          select n);
            //lazy loading of the linq query is actually a benefit 
            //unless we loop around to yeild the result we are not loading the memory here
            foreach (var item in result)
            {
                Console.WriteLine(item);

            } Console.ReadLine();
            //this loop can be better used by more concise lmabda expression
            Array.ForEach<string>(result.ToArray<string>(), s => Console.WriteLine(s));
            Console.ReadLine(); //it is the choice of the developer how he wants to write statements

            // we want all the integers that are greater than 2
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var numbersResult = from i in numbers
                                where i <= 2
                                select i;
            Array.ForEach<int>(numbersResult.ToArray<int>(), n => Console.WriteLine(n));
            Console.ReadLine();

            //C# code > LINQ -> EF -> database
            using (FoundationEntities db = new FoundationEntities())
            {
                var employeeResult = from c in db.Candidates
                                     select c.Id;
                Array.ForEach<string>(employeeResult.ToArray<string>(), e => Console.WriteLine(e));
                Console.ReadLine();
            }

            Student[] students = new Student[] { 
                new Student(){Age =22, Name= "A", Contacts = new List<Contact>()},
                new Student(){Age =24, Name= "B",Contacts = new List<Contact>()},
                new Student(){Age =31, Name= "C",Contacts = new List<Contact>()},
                new Student(){Age =15, Name= "D",Contacts = new List<Contact>()},
                new Student(){Age =34, Name= "F",Contacts = new List<Contact>()},
                new Student(){Age =26, Name= "G",Contacts = new List<Contact>()},
                new Student(){Age =22, Name= "H",Contacts = new List<Contact>()},
                new Student(){Age =22, Name= "I",Contacts = new List<Contact>()},
                new Student(){Age =22, Name= "J",Contacts = new List<Contact>()},
            };
            //using the mathematics operator and the annonymous projection
            var studentResult = from s in students
                                where s.Age > 22
                                select new { Alias = s.Name };
            Array.ForEach(studentResult.ToArray(), s => Console.WriteLine(s.Alias));
            Console.ReadLine();
            //we can also use the predicate and call the custom function in place of the simple where statements. 
            Predicate<Student> predic = IsValidPerson;

            var resultUsingPredicate = (from s in students
                                        select s).Where(s=>predic.Invoke(s));

            Array.ForEach<Student>(resultUsingPredicate.ToArray<Student>(), s => Console.WriteLine(s.Name));
            Console.ReadLine();

            //cascaded objects
            var contacts = from s in students
                           select s.Contacts; //this statement would not work
            //shape of contacts is  -- IEnumerable<IEnumerable<Contact>>
            var contactsFlat = students.SelectMany(s => s.Contacts);
            // shape of left side would be -- Ienumerable<Contact>

            Array.ForEach<Contact>(contactsFlat.ToArray<Contact>(), c => Console.WriteLine(c));
            Console.ReadLine();


            string[] studentNames = new string[] { "Niranjan", "Guru", "Arun", "Niranjan", "Arun" };
            string[] tutors = new string[] { "Niranjan", "victor", "Shiva" };

            //Arun Arun Guru Guru Niranjan Niranjan
            //Comment from codechira
            // ....
            //Pagination and skipping results from the top
            var myResult = (from n in names
                            orderby n //this statement is vital when it comes to skipping. If missing the entire thing will not work
                            select n).Distinct<string>().Skip<string>(1).Take<string>(2);
            Array.ForEach<string>(myResult.ToArray<string>(), r => Console.WriteLine(r));
            Console.ReadLine();


            //Working with inner joins
            //Niranjan : Niranjan
            //Niranjan :victor
            //Niranjan: Shiva
            //Guru: Niranjan
            //Guru: Victor
            //Guru: shiva ..

            var innerJoin = from n in names
                            join t in tutors on n equals t
                            select n;

            Array.ForEach<string>(innerJoin.ToArray<string>(), i => Console.WriteLine(i));
            Console.ReadLine();

            var orderedNames = from n in names
                               orderby n
                               select n;
            foreach (var item in orderedNames)
            {
                Console.WriteLine(item);
            } Console.ReadLine();

            int[] anotherNumberSet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var orderedResult = from i in numbers
                                where i > 2
                                orderby i descending
                                select i;

            foreach (var item in orderedResult)
            {
                Console.WriteLine(item);
            } Console.ReadLine();
        }
    }



}
