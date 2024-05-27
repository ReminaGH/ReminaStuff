using System;

namespace MyApp {
    abstract class Shape {
        public abstract double getArea();
    }

    class Circle : Shape {
        private double _radius;

        public Circle(double radius) => _radius = radius;

        public override double getArea() => Math.PI * Math.Pow(_radius, 2); //πr²
    }

    class Square : Shape {
        private double _side;

        public Square(double side) => _side = side;

        public override double getArea() => Math.Pow(_side, 2); //x²
    }

    class Rectangle : Shape {
        private double _width;
        private double _height;

        public Rectangle(double w, double h) {
            _width = w;
            _height = h;
        }

        public override double getArea() => _width * _height;
    }

    class Triangle : Shape {
        private double _base;
        private double _height;
        //Bla bla...

        public Triangle(double b, double h) {
            _base = b;
            _height = h;
        }

        public override double getArea() => (_base * _height) / 2;
    }

    internal class Program {
        static void Main(string[] args) {
            Shape s1 = new Circle(13);
            Shape s2 = new Square(5);
            Shape s3 = new Rectangle(8, 9);
            Shape s4 = new Triangle(7, 4);

            List<Shape> shapes = new List<Shape>();
            shapes.Add(s1);
            shapes.Add(s2);
            shapes.Add(s3);
            shapes.Add(s4);

            foreach (var s in shapes) {
                Console.WriteLine($"Area of shape: {s.getArea()}");
            }
        }
    }
}

