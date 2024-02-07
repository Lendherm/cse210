class Program
{
    static void Main()
    {
        Square square = new Square("Red", 5);
        Rectangle rectangle = new Rectangle("Blue", 4, 6);
        Circle circle = new Circle("Green", 3);

        List<Shape> shapes = new List<Shape> { square, rectangle, circle };

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.Color}");
            Console.WriteLine($"Area: {shape.GetArea()}");
            Console.WriteLine();
        }
    }
}
