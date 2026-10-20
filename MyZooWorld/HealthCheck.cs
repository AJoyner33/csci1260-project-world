using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld;

// One weigh-in for one animal on one date.
// Key fields: animalId + date. Measurement: weightKg.
public class HealthCheck : IEquatable<HealthCheck>, IComparable<HealthCheck>
{
    private string animalId;
    private DateTime date;
    private double weightKg;

    public string AnimalId { get { return animalId; } }
    public DateTime Date { get { return date; } }
    public double WeightKg { get { return weightKg; } }

    public HealthCheck(string animalId, DateTime date, double weightKg)
    {
        if (string.IsNullOrWhiteSpace(animalId))
            throw new ArgumentException("Health check needs an animal id.");
        if (weightKg < 0)
            throw new ArgumentException("Weight cannot be negative.");

        this.animalId = animalId;
        this.date = date;
        this.weightKg = weightKg;
    }

    // Equality uses the two key fields and nothing else.
    public bool Equals(HealthCheck? other)
    {
        if (other is null)
            return false;
        return AnimalId == other.AnimalId && Date == other.Date;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as HealthCheck);
    }

    // Built from exactly the same two fields as Equals.
    public override int GetHashCode()
    {
        return HashCode.Combine(AnimalId, Date);
    }

    // Natural order: by animal id, then by date.
    public int CompareTo(HealthCheck? other)
    {
        if (other is null)
            return 1;

        int byId = string.Compare(AnimalId, other.AnimalId, StringComparison.Ordinal);
        if (byId != 0)
            return byId;
        return Date.CompareTo(other.Date);
    }

    public string Describe()
    {
        return String.Format("{0} {1:yyyy-MM-dd} {2:N1} kg", AnimalId, Date, WeightKg);
    }

    public override string ToString()
    {
        return Describe();
    }
}
