using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns
{
    public class Person
    {
        //Location where he lives
        public string DoorNumber, StreetAddress, City, PostalCode;
        //Where he works
        public string CompanyName, Designation;
        public double AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(DoorNumber)}:{DoorNumber},{nameof(StreetAddress)}:{StreetAddress},{nameof(City)}:{City}," +
                $"{nameof(PostalCode)}:{PostalCode},{nameof(CompanyName)}:{CompanyName}," +
                $"{nameof(Designation)}:{Designation},{nameof(AnnualIncome)}:{AnnualIncome}";
                //$":{nameof(ProjectName)}:{nameof(ManagerName)}";
        }
    }
    public class PersonBuilder //facade
    {
        //the object we are going to build
        protected Person person = new Person();//this is a reference!

        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        public PersonJobBuilder DoinJob => new PersonJobBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }
    public class PersonJobBuilder:PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }
        public PersonJobBuilder WorksAt(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }
        public PersonJobBuilder WorkAs(string designation)
        {
            person.Designation = designation;
            return this;
        }
        public PersonJobBuilder AnuallIncome(double annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }
    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAddressBuilder At(string doorNumber)
        {
            person.DoorNumber = doorNumber;
            return this;
        }
        public PersonAddressBuilder InStreet(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }
        public PersonAddressBuilder InCity(string city)
        {
            person.City = city;
            return this;
        }
        public PersonAddressBuilder WithPostalCode(string postcode)
        {
            person.PostalCode = postcode;
            return this;
        }


    }
    public class FacadePattern
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives
                .At("139/9")
                .InStreet("Shanmuganagar,7th cross,UKT Malai")
                .InCity("Trichy")
                .WithPostalCode("6200025")
                .DoinJob
                .WorksAt("E&Y")
                .WorkAs("Project Manager")
                .AnuallIncome(5000000);

            WriteLine(person);
            ReadKey();
        }
    }
}
