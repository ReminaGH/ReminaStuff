using System;

namespace MyApp {
    class Person {
        private string name;
        private int age;
        private int salary;

        public Person(string name, int age) {
            this.name = name;
            this.age = age;
            this.salary = 0;
        }

        public void greet() {
            Console.WriteLine($"Tja! Mitt namn är {name} och jag är {age} år.");
        }

        public virtual int getSalary() {
            return salary;
        }
    }

    class Teacher : Person {
        public Teacher(string name, int age) : base(name, age) {

        }

        public override int getSalary() {
            return 5;
        }
    }

    class Student : Person {
        public Student(string name, int age) : base(name, age) {

        }

        public override int getSalary() {
            return -10;
        }
    }

    internal class Program {
        static void Main(string[] args) {
            Person p1 = new Person("Kalle", 20);
            Person p2 = new Person("Amir", 30);
            Person p3 = new Person("Eva", 40);
            Person p4 = new Person("Lars", 50);

            p1.greet();
            p2.greet();
            p3.greet();
            p4.greet();
        }
    }
}