using System;

public class Fraction
{
    private int top;
    private int bottom;

    // Constructors
    public Fraction()
    {
        top = 1;
        bottom = 1;
    }

    public Fraction(int wholeNumber)
    {
        top = wholeNumber;
        bottom = 1;
    }

    public Fraction(int top, int bottom)
    {
        // Check for division by zero
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }

        this.top = top;
        this.bottom = bottom;
    }

    // Getters and setters for the top and bottom numbers
    public int GetTop()
    {
        return top;
    }

    public void SetTop(int top)
    {
        this.top = top;
    }

    public int GetBottom()
    {
        return bottom;
    }

    public void SetBottom(int bottom)
    {
        // Check for division by zero
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }

        this.bottom = bottom;
    }

    // Method to return the fractional representation
    public string GetFractionString()
    {
        return $"{top}/{bottom}";
    }

    // Method to return the decimal representation
    public double GetDecimalValue()
    {
        return (double)top / bottom;
    }
}
